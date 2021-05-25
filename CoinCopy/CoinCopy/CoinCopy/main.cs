using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Threading;
using System.Net;
using Newtonsoft.Json;

namespace CoinCopy
{
    public partial class mainForm : Form
    {
        public balance userBalance;       
        public buyData buy_data;

        Thread profitCalc;

        string code;
        string priceurl = "https://api.upbit.com/v1/trades/ticks?market=";

        public mainForm()
        {
            InitializeComponent();
            userBalance = new balance();           
            buy_data = new buyData();
            
            double totalAsset = userBalance.getTotalAsset();
            double cash = userBalance.getCash();
            double profitPercent = userBalance.calcProfitPercentage();
            double profit = userBalance.getProfit();
            double purchase = userBalance.getPurchaseAmount();
            double currentTotalPrice = userBalance.getCurrentTotalPrice();

            lblBalance.Text = totalAsset.ToString();
            lbl_percent.Text = "0%";
            profit_txt.Text = profit.ToString();
            purchase_txt.Text = purchase.ToString();
            evaluated_txt.Text = currentTotalPrice.ToString();
            cash_txt.Text = cash.ToString();
        }
        class PriceInfo
        {
            public DateTime candle_date_time_kst { get; set; }
            public double opening_price { get; set; }
            public double high_price { get; set; }
            public double low_price { get; set; }
            public double trade_price { get; set; }
        }

        private void main_Load(object sender, EventArgs e)
        {          
            profitCalc = new Thread(new ThreadStart(profitCalcMethod));
            profitCalc.Start();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Search searchform = new Search(userBalance, this);
            searchform.Owner = this;
            searchform.Show();
        }

        private void cmsRequest_Click(object sender, EventArgs e)
        {
            //Request requestform = new Request("","", userBalance, this);
            //requestform.Owner = this;
            //requestform.Show();
        }

        private void cms_chart_Click(object sender, EventArgs e)
        {
            Chart chartform = new Chart(this);
            chartform.Owner = this;
            chartform.Show();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            // 잔고 초기화
            userBalance = new balance();
            double totalAsset = userBalance.getTotalAsset();
            double cash = userBalance.getCash();
            double profitPercent = userBalance.calcProfitPercentage();
            double profit = userBalance.getProfit();
            double purchase = userBalance.getPurchaseAmount();
            double currentTotalPrice = userBalance.getCurrentTotalPrice();

            lblBalance.Text = totalAsset.ToString();
            lbl_percent.Text = "0%";
            profit_txt.Text = profit.ToString();
            purchase_txt.Text = purchase.ToString();
            evaluated_txt.Text = currentTotalPrice.ToString();
            cash_txt.Text = cash.ToString();
            
        }

        public void setBalance(balance bl)
        {
            double totalAsset = bl.getTotalAsset();
            double cash = bl.getCash();
            double profitPercent = bl.calcProfitPercentage();
            double profit = bl.getProfit();
            double purchase = bl.getPurchaseAmount();
            double currentTotalPrice = bl.getCurrentTotalPrice();

            lblBalance.Text = totalAsset.ToString();
            lbl_percent.Text = profitPercent.ToString();
            profit_txt.Text = profit.ToString();
            purchase_txt.Text = purchase.ToString();
            evaluated_txt.Text = currentTotalPrice.ToString();
            cash_txt.Text = cash.ToString();
        }

        public void setRowData(rowData r)
        {
            //balanceDgv.Rows.Add(arr[0], arr[1], arr[2], arr[3], arr[4], arr[5], arr[6], arr[7]);
            
        }

        public void calculation()
        {
            int i;
            int checkExistance = 0;
           
            DataGridViewRow tempRow = new DataGridViewRow();
            for ( i = 0; i < balanceDgv.Rows.Count; i++)
            {
                tempRow = balanceDgv.Rows[i];
                if ((string)tempRow.Cells[0].Value == buy_data.stockName )
                {
                    checkExistance = 1;
                    break;
                }
            }

            if (checkExistance == 0 )
            {
                double totalCost = buy_data.buyCost * buy_data.buyQuantity;
                balanceDgv.Rows.Add(buy_data.stockName, buy_data.buyQuantity, buy_data.buyCost , buy_data.buyCost, 0, 0, totalCost, totalCost);
            } else if (checkExistance == 1)
            {
                double totalCost = (double)tempRow.Cells[7].Value + buy_data.buyCost * buy_data.buyQuantity;
                double valuationCost = (double)tempRow.Cells[6].Value + buy_data.buyCost * buy_data.buyQuantity;

                tempRow.Cells[1].Value = (double)tempRow.Cells[1].Value + buy_data.buyQuantity;               
                tempRow.Cells[3].Value = totalCost / (double)tempRow.Cells[1].Value;              
                tempRow.Cells[6].Value = valuationCost;
                tempRow.Cells[7].Value = totalCost;
            }


            double cash = userBalance.getCash();
            cash -= buy_data.buyCost * buy_data.buyQuantity;
            userBalance.setCash(cash);

            double totalBuyCost = userBalance.getPurchaseAmount();
            totalBuyCost += buy_data.buyCost * buy_data.buyQuantity;
            userBalance.setPurchaseAmount(totalBuyCost);
            

        }

        public void updateUserBalance()
        {
            double ta = userBalance.getTotalAsset();
            lblBalance.Text = ta.ToString();
            
            double percent = userBalance.getProfitPercentage();
            lbl_percent.Text = percent.ToString();

            double profit = userBalance.getProfit();
            profit *= -1;
            profit_txt.Text = profit.ToString();            

            double purchase = userBalance.getPurchaseAmount();
            purchase_txt.Text = purchase.ToString();

            double evaluated = userBalance.getCurrentTotalPrice();
            evaluated_txt.Text = evaluated.ToString();

            double cash = userBalance.getCash();
            cash_txt.Text = cash.ToString();

        }
        

        private void profitCalcMethod()
        {
            while (true)
            {
                double totalEvalStockPrice = 0;                               

                for (int i = 0; i < balanceDgv.Rows.Count; i++)
                {
                    try
                    {
                        DataGridViewRow tempRow = new DataGridViewRow();
                        tempRow = balanceDgv.Rows[i];
                        code = (string)tempRow.Cells[0].Value;

                        WebClient tempclient = new WebClient();
                        tempclient.Encoding = Encoding.UTF8;

                        string tempurl = priceurl + code + "&count=1";
                        var candleinfo = tempclient.DownloadString(tempurl);
                        var price = JsonConvert.DeserializeObject<List<PriceInfo>>(candleinfo);

                        this.Invoke(new MethodInvoker(delegate ()
                        {
                            // 데이터 그리드 뷰 먼저 업데이트
                            tempRow = balanceDgv.Rows[i];
                            code = (string)tempRow.Cells[0].Value;

                            double quantity = (double)tempRow.Cells[1].Value;
                            double currentPrice = price[0].trade_price;
                            double evalPrice = quantity * currentPrice;
                            double buyPrice = (double)tempRow.Cells[7].Value;
                            double evalProfit = evalPrice - buyPrice;
                            double profitPercentage = evalProfit / buyPrice * 100;

                            totalEvalStockPrice += evalPrice;                                                    

                            // 현재가 업데이트
                            tempRow.Cells[2].Value = currentPrice;

                            // 평가금액 업데이트 : 평가금액 = 보유수량 * 현재가             
                            tempRow.Cells[6].Value = evalPrice;

                            // 평가손익 업데이트 : 평가손익 = 평가금액 - 매입금액
                            tempRow.Cells[5].Value = evalProfit;

                            //손익률 : 평가손익 / 매입금액 * 100
                            tempRow.Cells[4].Value = profitPercentage;

                            // + , - 에 따라 색 변환
                            if (profitPercentage > 0)
                            {
                                tempRow.Cells[4].Style.ForeColor = Color.Red;
                                tempRow.Cells[5].Style.ForeColor = Color.Red;
                            }
                            else if (profitPercentage < 0)
                            {
                                tempRow.Cells[4].Style.ForeColor = Color.Blue;
                                tempRow.Cells[5].Style.ForeColor = Color.Blue;
                            }    
                        }));                        
                    } catch (Exception e)
                    {
                      
                    }

                // for 문 끝
                }

                // ***  잔고 업데이트   ***

                double cash = userBalance.getCash();

                userBalance.setTotalAsset(cash + totalEvalStockPrice);     
               
                userBalance.setCurrentTotalPrice(totalEvalStockPrice);

                double totalProfit = userBalance.getCurrentTotalPrice() - userBalance.getPurchaseAmount();
                userBalance.setProfit(totalProfit);

                userBalance.profitPercentageCalculation();

                this.Invoke(new MethodInvoker(delegate ()
                {
                    updateUserBalance();
                }));

                Delay(500);
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

        private void mainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
           profitCalc.Abort();
        }
    }
}
