using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net;
using Newtonsoft.Json;
using System.Threading;

namespace CoinCopy
{
    public partial class Request : Form
    {
        class PriceInfo
        {
            public DateTime candle_date_time_kst { get; set; }
            public double opening_price { get; set; }
            public double high_price { get; set; }
            public double low_price { get; set; }
            public long trade_price { get; set; }
        }


        private string marketPrice;
        private string coinName;
        public balance userBalance;
        mainForm mForm;
        string code;
        string priceurl = "https://api.upbit.com/v1/trades/ticks?market=";
        Thread checkingmarketprice;

        Thread order_control;
        List<Thread> limitOrderList = new List<Thread>();

        public Request(string code, string coinName, string mPrice, balance uBalance, mainForm mF)
        {
            InitializeComponent();
            cmbRequest.Items.Add("매수");
            cmbRequest.Items.Add("매도");
            userBalance = uBalance;
            marketPrice = mPrice;
            this.coinName = coinName;
            this.code = code;
            mForm = mF;

            order_control = new Thread(new ThreadStart(Order_Control));
            order_control.Start();

        }

        private void stockNumberTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 숫자와 백스페이스만 입력 받도록 하는 코드
            if ( char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)  ) {
                e.Handled = false;
            } else
                e.Handled = true;            
        }
        /*
         * selection == 0 : 매수 , selection == 1 : 매도         
         *
         */
        private void btnOK_Click(object sender, EventArgs e)
        {
            int selection = cmbRequest.SelectedIndex;

           if (selection == 0 && stockNumberTextBox.Text != null)
           {
                long howMany = long.Parse(stockNumberTextBox.Text);

                long marketPriceLong = Convert.ToInt64(priceTextBox.Text);
                long totalCost = howMany * marketPriceLong;

                /*지정매수목록에 걸려있는 금액 계산*/
                long money_must_not_be_used = 0;

                foreach (Thread thr in limitOrderList)
                {
                    string[] orderinfo = thr.Name.Split(' ');
                    if (orderinfo[0] == "매수")
                        money_must_not_be_used += Convert.ToInt64(orderinfo[1]) * Convert.ToInt64(orderinfo[2]);
                }
                /*                                       */

                if (rdoMarketPrice.Checked)
                {
                    
                    
                    if (userBalance.getCash() - money_must_not_be_used < totalCost)
                    {
                        MessageBox.Show("금액 부족\n" + "소유 현금 : " + userBalance.getCash() +"\n" + "가격 총합 : " + totalCost ,"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    mForm.buy_data.buyQuantity = howMany;
                    mForm.buy_data.buyCost = marketPriceLong;
                    mForm.calculation();

                    MessageBox.Show("매수 체결되었습니다");                    
                    //this.Close();
                }
                else if (rdoCustomPrice.Checked)
                {
                    if (userBalance.getCash() - money_must_not_be_used < totalCost)
                    {
                        MessageBox.Show("금액 부족\n" + "소유 현금 : " + userBalance.getCash() + "\n" + "가격 총합 : " + totalCost, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    List<object> parameters = new List<object>();

                    parameters.Add(marketPriceLong);
                    parameters.Add(howMany);

                    Thread buylimit = new Thread(new ParameterizedThreadStart(limitOrder_BuyingPoint));
                    buylimit.Start(parameters);

                    limitOrderList.Add(buylimit);
                    limitOrderList[limitOrderList.Count - 1].Name = "매수" + " " + howMany + " " + marketPriceLong;
                }
           }

           if ( selection == 1 && stockNumberTextBox.Text != null)
           {
                long howMany = long.Parse(stockNumberTextBox.Text);
                long marketPriceLong = Convert.ToInt64(priceTextBox.Text);
                long totalCost = howMany * marketPriceLong;

                // stockName 단순이름에서 market name으로 변경하였습니다 coinName -> code
                mForm.sell_data.stockName = this.code;
                mForm.sell_data.sellCost = totalCost;
                mForm.sell_data.sellQuantity = howMany;

                /*지정매수목록에 걸려있는 코인 계산*/
                long coin_must_not_be_used = 0;

                foreach (Thread thr in limitOrderList)
                {
                    string[] orderinfo = thr.Name.Split(' ');
                    if (orderinfo[0] == "매도")
                        coin_must_not_be_used += long.Parse(orderinfo[1]);
                }
                /*                                       */

                if (rdoMarketPrice.Checked)
                {
                    int result = mForm.sellCalc();

                    if (result == 0)
                    {
                        MessageBox.Show("해당 코인이 없습니다!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (result == 1)
                    {
                        MessageBox.Show("코인 수가 부족합니다", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (result == 2)
                    {
                        MessageBox.Show("매도 체결되었습니다.");
                    }
                }

                else if (rdoCustomPrice.Checked)
                {
                    mForm.sell_data.sellQuantity += coin_must_not_be_used;

                    int result = mForm.beforesellCalc();
                    if (result == 0)
                    {
                        MessageBox.Show("해당 코인이 없습니다!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (result == 1)
                    {
                        MessageBox.Show("코인 수가 부족합니다", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else if (result == 2)
                    {
                        List<object> parameters = new List<object>();
                        parameters.Add(marketPriceLong);
                        parameters.Add(totalCost);
                        parameters.Add(howMany);
                        parameters.Add(this.code);

                        Thread selllimit = new Thread(new ParameterizedThreadStart(limitOrder_SellingPoint));
                        selllimit.Start(parameters);

                        limitOrderList.Add(selllimit);
                        limitOrderList[limitOrderList.Count - 1].Name = "매도" + " " + howMany + " " + marketPriceLong;
                    }
                }
            }

        }

        private void rdoCustomPrice_MouseClick(object sender, MouseEventArgs e)
        {
            priceTextBox.Enabled = true;
        }

        private void rdoMarketPrice_CheckedChanged(object sender, EventArgs e)
        {
            priceTextBox.Enabled = false;
            priceTextBox.Text = marketPrice;
        }

        public void limitOrder_BuyingPoint(object obj)
        {
            List<object> parameters = obj as List<object>;

            while (true)
            {
                WebClient tempclient = new WebClient();
                tempclient.Encoding = Encoding.UTF8;

                string tempurl = priceurl + code + "&count=1";
                var candleinfo = tempclient.DownloadString(tempurl);
                var price = JsonConvert.DeserializeObject<List<PriceInfo>>(candleinfo);

                if (Convert.ToDouble(parameters[0]) >= Convert.ToDouble(price[0].trade_price.ToString()))
                {
                    this.Invoke(new MethodInvoker(delegate ()
                    {
                        limitOrder_Buy(parameters);
                    }));
                    break;
                }

                Delay(1000);
            }
        }

        private void limitOrder_Buy(object obj)
        {
            List<object> parameters = obj as List<object>;

            if (userBalance.getCash() < Convert.ToInt64(parameters[0]) * Convert.ToInt64(parameters[1]))
            {
                MessageBox.Show("금액 부족", "매수 채결 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            mForm.buy_data.buyQuantity = Convert.ToInt64(parameters[1]);
            mForm.buy_data.buyCost = Convert.ToInt64(parameters[0]);
            mForm.calculation();

            MessageBox.Show("매수 체결되었습니다.");
        }

        public void limitOrder_SellingPoint(object obj)
        {
            List<object> parameters = obj as List<object>;

            while (true)
            {
                WebClient tempclient = new WebClient();
                tempclient.Encoding = Encoding.UTF8;

                string tempurl = priceurl + code + "&count=1";
                var candleinfo = tempclient.DownloadString(tempurl);
                var price = JsonConvert.DeserializeObject<List<PriceInfo>>(candleinfo);

                if (Convert.ToDouble(parameters[0]) <= Convert.ToDouble(price[0].trade_price.ToString()))
                {
                    this.Invoke(new MethodInvoker(delegate ()
                    {
                        limitOrder_Sell(parameters);
                    }));
                    break;
                }

                Delay(1000);
            }
        }

        private void limitOrder_Sell(object obj)
        {
            List<object> parameters = obj as List<object>;

            mForm.sell_data.stockName = parameters[3].ToString();
            mForm.sell_data.sellCost = Convert.ToInt64(parameters[1]);
            mForm.sell_data.sellQuantity = Convert.ToInt64(parameters[2]);

            int result = mForm.sellCalc();

            if (result == 0)
            {
                MessageBox.Show("해당 코인이 없습니다!", "매도 채결 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (result == 1)
            {
                MessageBox.Show("코인 수가 부족합니다", "매도 채결 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else if (result == 2)
            {
                MessageBox.Show("매도 체결되었습니다.");
            }
        }

        private static DateTime Delay(int MS)
        {
            DateTime ThisMoment = DateTime.Now;
            TimeSpan duration = new TimeSpan(0, 0, 0, 0, MS);
            DateTime AfterWards = ThisMoment.Add(duration);

            while (AfterWards >= ThisMoment)
            {
                System.Windows.Forms.Application.DoEvents();
                ThisMoment = DateTime.Now;
            }

            return DateTime.Now;
        }

        private void Request_Load(object sender, EventArgs e)
        {
            checkingmarketprice = new Thread(new ThreadStart(marketchecker));
            checkingmarketprice.Start();
        }

        private void marketchecker()
        {
            while (true)
            {
                try
                {
                    WebClient tempclient = new WebClient();
                    tempclient.Encoding = Encoding.UTF8;

                    string tempurl = priceurl + code + "&count=1";
                    var candleinfo = tempclient.DownloadString(tempurl);
                    var price = JsonConvert.DeserializeObject<List<PriceInfo>>(candleinfo);

                    this.Invoke(new MethodInvoker(delegate ()
                    {
                        if (rdoMarketPrice.Checked)
                            priceTextBox.Text = price[0].trade_price.ToString();
                    }));
                } catch (Exception e)
                {

                }

                Delay(500);

            }
        }

        private void Request_FormClosing(object sender, FormClosingEventArgs e)
        {
            checkingmarketprice.Abort();
            order_control.Abort();
            for (int i = 0; i < limitOrderList.Count; i++)
                limitOrderList[i].Abort();
        }

        private void Order_Control()
        {
            int tmp = 0;

            int x_location_label = 10;
            int x_location_btn = 300;
            int y_location_label = 16;
            int y_location_btn = 10;

            while (true)
            {
                if (limitOrderList.Count != 0)
                    for (int i = 0; i < limitOrderList.Count; i++)
                        if (!limitOrderList[i].IsAlive)
                            limitOrderList.RemoveAt(i);

                if (tmp < limitOrderList.Count)
                {
                    Button mybutton = new Button();
                    mybutton.Text = "주문 취소";
                    mybutton.Name = tmp.ToString();
                    mybutton.Location = new System.Drawing.Point(x_location_btn, y_location_btn);
                    mybutton.Click += btnClick_Cancel_Order;

                    
                    string[] orderinfo = limitOrderList[tmp].Name.Split(' ');
                    
                    foreach (string str in orderinfo)
                    {
                        Label mylabel = new Label();
                        mylabel.Text = str;
                        mylabel.AutoSize = true;
                        mylabel.Location = new System.Drawing.Point(x_location_label, y_location_label);
                        x_location_label += 100;
                        this.Invoke(new MethodInvoker(delegate ()
                        {
                            order_panel.Controls.Add(mylabel);
                        }));
                    }
                    x_location_label = 10;

                    this.Invoke(new MethodInvoker(delegate ()
                    {
                        order_panel.Controls.Add(mybutton);
                    }));

                    y_location_btn += 30;
                    y_location_label += 30;
                    tmp++;
                }
                else if (tmp > limitOrderList.Count)
                {
                    this.Invoke(new MethodInvoker(delegate ()
                    {
                        order_panel.Controls.Clear();
                    }));

                    tmp = 0;
                    y_location_btn = 10;
                    y_location_label = 16;

                }
            }
        }

        private void btnClick_Cancel_Order(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            int num = Convert.ToInt32(btn.Name);

            limitOrderList[num].Abort();
            limitOrderList.RemoveAt(num);
        }
    }
}

