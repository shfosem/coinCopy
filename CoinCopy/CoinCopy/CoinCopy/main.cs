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
            
            long totalAsset = userBalance.getTotalAsset();
            long cash = userBalance.getCash();
            double profitPercent = userBalance.getProfitPercentage();
            long profit = userBalance.getProfit();
            long purchase = userBalance.getPurchaseAmount();
            long currentTotalPrice = userBalance.getCurrentTotalPrice();

            lblBalance.Text = totalAsset.ToString();
            lbl_percent.Text = "0%";
            profit_txt.Text = profit.ToString();
            lblPurchaseNumber.Text = purchase.ToString();
            evaluated_txt.Text = currentTotalPrice.ToString();
            cash_txt.Text = cash.ToString();
        }
        class PriceInfo
        {
            public DateTime candle_date_time_kst { get; set; }
            public long opening_price { get; set; }
            public long high_price { get; set; }
            public long low_price { get; set; }
            public long trade_price { get; set; }
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
            long totalAsset = userBalance.getTotalAsset();
            long cash = userBalance.getCash();            
            long profit = userBalance.getProfit();
            long purchase = userBalance.getPurchaseAmount();
            long currentTotalPrice = userBalance.getCurrentTotalPrice();

            lblBalance.Text = totalAsset.ToString();
            lbl_percent.Text = "0%";
            profit_txt.Text = profit.ToString();
            lblPurchaseNumber.Text = purchase.ToString();
            evaluated_txt.Text = currentTotalPrice.ToString();
            cash_txt.Text = cash.ToString();
            
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
                long totalCost = buy_data.buyCost * buy_data.buyQuantity;
                balanceDgv.Rows.Add(buy_data.stockName, buy_data.buyQuantity, buy_data.buyCost , buy_data.buyCost, 0, 0, totalCost, totalCost);
            } else if (checkExistance == 1)
            {
                long totalCost = (long)tempRow.Cells[7].Value + buy_data.buyCost * buy_data.buyQuantity;
                long valuationCost = (long)tempRow.Cells[6].Value + buy_data.buyCost * buy_data.buyQuantity;

                tempRow.Cells[1].Value = (long)tempRow.Cells[1].Value + buy_data.buyQuantity;               
                tempRow.Cells[3].Value = totalCost / (long)tempRow.Cells[1].Value;              
                tempRow.Cells[6].Value = valuationCost;
                tempRow.Cells[7].Value = totalCost;
            }

            long cash = userBalance.getCash();
            cash -= buy_data.buyCost * buy_data.buyQuantity;
            userBalance.setCash(cash);

            long totalBuyCost = userBalance.getPurchaseAmount();
            totalBuyCost += buy_data.buyCost * buy_data.buyQuantity;
            userBalance.setPurchaseAmount(totalBuyCost);            

        }

        public void updateUserBalance()
        {
            long ta = userBalance.getTotalAsset();
            lblBalance.Text = ta.ToString();
            
            double percent = userBalance.getProfitPercentage();
            lbl_percent.Text = percent.ToString();

            long profit = userBalance.getProfit(); 
            profit_txt.Text = profit.ToString();

            long purchase = userBalance.getPurchaseAmount();
            lblPurchaseNumber.Text = purchase.ToString();

            long evaluated = userBalance.getCurrentTotalPrice();
            evaluated_txt.Text = evaluated.ToString();

            long cash = userBalance.getCash();
            cash_txt.Text = cash.ToString();

        }
        

        private void profitCalcMethod()
        {
            while (true)
            {
                long totalEvalStockPrice = 0;

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

                            long quantity = (long)tempRow.Cells[1].Value;
                            double cP = price[0].trade_price;
                            long currentPrice = Convert.ToInt64(cP);
                            long evalPrice = quantity * currentPrice;
                            long buyPrice = (long)tempRow.Cells[7].Value;
                            long evalProfit = evalPrice - buyPrice;

                            double eP = Convert.ToDouble(evalProfit);
                            double bP = Convert.ToDouble(buyPrice);

                            double profitPercentage = Math.Round( eP / bP * 100 , 5);

                            totalEvalStockPrice += evalPrice;                                                    

                            // 현재가 업데이트
                            tempRow.Cells[2].Value = Convert.ToInt64(currentPrice);

                            // 평가금액 업데이트 : 평가금액 = 보유수량 * 현재가             
                            tempRow.Cells[6].Value = evalPrice;

                            // 평가손익 업데이트 : 평가손익 = 평가금액 - 매입금액
                            tempRow.Cells[5].Value = evalProfit;

                            //손익률 : 평가손익 / 매입금액 * 100
                            tempRow.Cells[4].Value = profitPercentage;

                            // + , - 에 따라 색 변환
                            if (evalProfit > 0)
                            {
                                tempRow.Cells[4].Style.ForeColor = Color.Red;
                                tempRow.Cells[5].Style.ForeColor = Color.Red;
                            }
                            else if (evalProfit < 0)
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
                balanceUpdate(totalEvalStockPrice);

                this.Invoke(new MethodInvoker(delegate ()
                {
                    updateUserBalance();
                }));

                Delay(500);
            }

        }

        private void balanceUpdate(long totalEvalStockPrice)
        {
            long cash = userBalance.getCash();

            userBalance.setTotalAsset(cash + totalEvalStockPrice);

            userBalance.setCurrentTotalPrice(totalEvalStockPrice);

            long totalProfit = userBalance.getTotalAsset() - userBalance.INIT_CASH;
            userBalance.setProfit(totalProfit);

            userBalance.profitPercentageCalculation();
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

        /*
         * 시장가 매도 시 변경되는 데이터
         * - 계좌 데이터
         * 현금 보유량
         * 유가총액
         * 매입금액
         *          * 
         */
        public int sellCalc()
        {
            int i = 0;
            int checkExistance = 0;

            DataGridViewRow tempRow = new DataGridViewRow();

            for (i = 0; i < balanceDgv.Rows.Count; i++)
            {
                tempRow = balanceDgv.Rows[i];
                if ((string)tempRow.Cells[0].Value == sell_data.stockName)
                {
                    checkExistance = 1;
                    break;
                }
            }

            if (checkExistance == 0)
                return 0;

            if (sell_data.sellQuantity > (long)tempRow.Cells[1].Value)
                return 1;
                        
            // 매도한 양 만큼 보유수량 감소
            long amount = (long)tempRow.Cells[1].Value;

            amount = amount - (long)sell_data.sellQuantity;

            tempRow.Cells[1].Value = amount;

            //매수평균가 * 매도 수량 값만큼 매입금액 감소
            long purchaseCost = (long)tempRow.Cells[7].Value;
            long averageCost = (long)tempRow.Cells[3].Value;

            purchaseCost -= (long)sell_data.sellQuantity * averageCost;

            tempRow.Cells[7].Value = purchaseCost;
            

            //매도한만큼 사용자 계좌 현금 증가
            long userCash = userBalance.getCash();
            userCash += (long)sell_data.sellCost;
            userBalance.setCash(userCash);

            //매도한만큼 사용자 계좌 유가총액 감소
            long userStockPrice = userBalance.getCurrentTotalPrice();
            userStockPrice -= sell_data.sellCost;
            userBalance.setCurrentTotalPrice(userStockPrice);

            //매도한만큼 사용자 계좌 매입금액 감소 
            long userTotalPurchaseCost = userBalance.getPurchaseAmount();
            
            userTotalPurchaseCost -= sell_data.sellQuantity * averageCost;
            userBalance.setPurchaseAmount(userTotalPurchaseCost);

            if (0 == (long)tempRow.Cells[1].Value)
            {
                balanceDgv.Rows.Remove(tempRow);
            }

            return 2;
        }

    }
}
