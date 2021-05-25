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
        public sellData sell_data;

        Thread profitCalc;

        string code;
        string priceurl = "https://api.upbit.com/v1/trades/ticks?market=";

        public mainForm()
        {
            InitializeComponent();
            userBalance = new balance();           
            buy_data = new buyData();
            sell_data = new sellData();
            
            decimal totalAsset = userBalance.getTotalAsset();
            decimal cash = userBalance.getCash();
            decimal profitPercent = userBalance.calcProfitPercentage();
            decimal profit = userBalance.getProfit();
            decimal purchase = userBalance.getPurchaseAmount();
            decimal currentTotalPrice = userBalance.getCurrentTotalPrice();

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
            public decimal opening_price { get; set; }
            public decimal high_price { get; set; }
            public decimal low_price { get; set; }
            public decimal trade_price { get; set; }
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
            decimal totalAsset = userBalance.getTotalAsset();
            decimal cash = userBalance.getCash();
            decimal profitPercent = userBalance.calcProfitPercentage();
            decimal profit = userBalance.getProfit();
            decimal purchase = userBalance.getPurchaseAmount();
            decimal currentTotalPrice = userBalance.getCurrentTotalPrice();

            lblBalance.Text = totalAsset.ToString();
            lbl_percent.Text = "0%";
            profit_txt.Text = profit.ToString();
            purchase_txt.Text = purchase.ToString();
            evaluated_txt.Text = currentTotalPrice.ToString();
            cash_txt.Text = cash.ToString();
            
        }

        public void setBalance(balance bl)
        {
            decimal totalAsset = bl.getTotalAsset();
            decimal cash = bl.getCash();
            decimal profitPercent = bl.calcProfitPercentage();
            decimal profit = bl.getProfit();
            decimal purchase = bl.getPurchaseAmount();
            decimal currentTotalPrice = bl.getCurrentTotalPrice();

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
                decimal totalCost = buy_data.buyCost * buy_data.buyQuantity;
                balanceDgv.Rows.Add(buy_data.stockName, buy_data.buyQuantity, buy_data.buyCost , buy_data.buyCost, 0, 0, totalCost, totalCost);
            } else if (checkExistance == 1)
            {
                decimal totalCost = (decimal)tempRow.Cells[7].Value + buy_data.buyCost * buy_data.buyQuantity;
                decimal valuationCost = (decimal)tempRow.Cells[6].Value + buy_data.buyCost * buy_data.buyQuantity;

                tempRow.Cells[1].Value = (decimal)tempRow.Cells[1].Value + buy_data.buyQuantity;               
                tempRow.Cells[3].Value = totalCost / (decimal)tempRow.Cells[1].Value;              
                tempRow.Cells[6].Value = valuationCost;
                tempRow.Cells[7].Value = totalCost;
            }

            decimal cash = userBalance.getCash();
            cash -= buy_data.buyCost * buy_data.buyQuantity;
            userBalance.setCash(cash);

            decimal totalBuyCost = userBalance.getPurchaseAmount();
            totalBuyCost += buy_data.buyCost * buy_data.buyQuantity;
            userBalance.setPurchaseAmount(totalBuyCost);            

        }

        public void updateUserBalance()
        {
            decimal ta = userBalance.getTotalAsset();
            lblBalance.Text = ta.ToString();
            
            decimal percent = userBalance.getProfitPercentage();
            lbl_percent.Text = percent.ToString();

            decimal profit = userBalance.getProfit();
            profit *= -1;
            profit_txt.Text = profit.ToString();            

            decimal purchase = userBalance.getPurchaseAmount();
            purchase_txt.Text = purchase.ToString();

            decimal evaluated = userBalance.getCurrentTotalPrice();
            evaluated_txt.Text = evaluated.ToString();

            decimal cash = userBalance.getCash();
            cash_txt.Text = cash.ToString();

        }
        

        private void profitCalcMethod()
        {
            while (true)
            {
                decimal totalEvalStockPrice = 0;                               

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

                            decimal quantity = (decimal)tempRow.Cells[1].Value;
                            decimal currentPrice = price[0].trade_price;
                            decimal evalPrice = quantity * currentPrice;
                            decimal buyPrice = (decimal)tempRow.Cells[7].Value;
                            decimal evalProfit = evalPrice - buyPrice;
                            decimal profitPercentage = evalProfit / buyPrice * 100;

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

                decimal cash = userBalance.getCash();

                userBalance.setTotalAsset(cash + totalEvalStockPrice);     
               
                userBalance.setCurrentTotalPrice(totalEvalStockPrice);

                decimal totalProfit = userBalance.getCurrentTotalPrice() - userBalance.getPurchaseAmount();
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

        public int sellCalc()
        {
            int i = 0;
            int checkExistance = 0;

            DataGridViewRow tempRow = new DataGridViewRow();

            for (i = 0; i < balanceDgv.Rows.Count; i++) {
                tempRow = balanceDgv.Rows[i];
                if ((string)tempRow.Cells[0].Value == sell_data.stockName) {
                    checkExistance = 1;
                    break;
                }
            }

            if (checkExistance == 0) return 0;

            if (sell_data.sellQuantity > (decimal)tempRow.Cells[1].Value) return 1;
                                 

            int amount = (int)tempRow.Cells[1].Value;
            int totalAsset = (int)tempRow.Cells[7].Value;

            amount = amount - (int)(sell_data.sellQuantity);
            totalAsset = totalAsset - (int)(sell_data.sellCost) * (int)(sell_data.sellQuantity);

            tempRow.Cells[1].Value = (int)amount;
            tempRow.Cells[7].Value = (int)(totalAsset);

            
            
            return 2;
        }

    }
}
