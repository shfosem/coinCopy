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
            public double trade_price { get; set; }
        }


        private string marketPrice;
        private string coinName;
        public balance userBalance;
        mainForm mForm;
        string code;
        string priceurl = "https://api.upbit.com/v1/trades/ticks?market=";
        Thread checkingmarketprice;

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
        }

        private void stockNumberTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 숫자와 백스페이스, . 만 입력 받도록 하는 코드
            if ( char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back) || ( e.KeyChar == '.' ) ) {
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
                double howMany = Double.Parse(stockNumberTextBox.Text);
               
                if (rdoMarketPrice.Checked)
                {
                    //double marketPriceDouble = Double.Parse(marketPrice); =>
                    double marketPriceDouble = Convert.ToDouble(priceTextBox.Text);

                    double totalCost = howMany * marketPriceDouble;
                    
                    if (userBalance.getCash() < totalCost)
                    {
                        MessageBox.Show("금액 부족\n" + "소유 현금 : " + userBalance.getCash() +"\n" + "가격 총합 : " + totalCost ,"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    mForm.buy_data.buyQuantity = howMany;
                    //market price need to keep changing
                    mForm.buy_data.buyCost = marketPriceDouble;
                    mForm.calculation();

                    MessageBox.Show("매수 채결");                    
                    //this.Close();
                }
                else if(rdoCustomPrice.Checked)
                {
                    double marketPriceDouble = Convert.ToDouble(priceTextBox.Text);
                    double totalCost = howMany * marketPriceDouble;

                    List<object> parameters = new List<object>();

                    if(userBalance.getCash() < totalCost)
                    {
                        MessageBox.Show("금액 부족\n" + "소유 현금 : " + userBalance.getCash() + "\n" + "가격 총합 : " + totalCost, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    parameters.Add(marketPriceDouble);
                    parameters.Add(howMany);

                    Thread buylimit = new Thread(new ParameterizedThreadStart(limitOrder_BuyingPoint));
                    buylimit.Start(parameters);


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


                Delay(500);

            }
        }

        private void limitOrder_Buy(object obj)
        {
            List<object> parameters = obj as List<object>;
            mForm.buy_data.buyQuantity = Convert.ToDouble(parameters[1]);
            //market price need to keep changing
            mForm.buy_data.buyCost = Convert.ToDouble(parameters[0]);
            mForm.calculation();

            MessageBox.Show("매수 채결");

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

                Delay(500);

            }
        }

        private void Request_FormClosing(object sender, FormClosingEventArgs e)
        {
            checkingmarketprice.Abort();
        }
    }
}

