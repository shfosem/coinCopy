using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoinCopy
{
    public partial class mainForm : Form
    {
        public balance userBalance;
       
        public buyData buy_data;
       
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

        private void main_Load(object sender, EventArgs e)
        {
           
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Search searchform = new Search(userBalance, this);
            searchform.Owner = this;
            searchform.Show();
        }

        private void cmsRequest_Click(object sender, EventArgs e)
        {
            Request requestform = new Request("","", userBalance, this);
            requestform.Owner = this;
            requestform.Show();
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
                tempRow.Cells[2].Value = buy_data.buyCost;
                tempRow.Cells[3].Value = totalCost / (double)tempRow.Cells[1].Value;
                tempRow.Cells[4].Value = 0;
                tempRow.Cells[5].Value = 0;
                tempRow.Cells[6].Value = valuationCost;
                tempRow.Cells[7].Value = totalCost;
            }

            double cash = userBalance.getCash() - buy_data.buyCost;
            userBalance.setCash(cash);

            double purchaseAmount = userBalance.getPurchaseAmount();
            purchaseAmount += buy_data.buyCost * buy_data.buyQuantity;
            userBalance.setPurchaseAmount(purchaseAmount);

            double currentTotalPrice = userBalance.getCurrentTotalPrice();
            currentTotalPrice += buy_data.buyCost * buy_data.buyQuantity;
            userBalance.setCurrentTotalPrice(currentTotalPrice);

            updateUserBalance();
        }

        public void updateUserBalance()
        {
            double ta = userBalance.getTotalAsset();
            lblBalance.Text = ta.ToString();
            // 수익률 : 수익금액 / 매입금액(투자원금) * 100
            double percent = (userBalance.getProfit() / userBalance.getPurchaseAmount() * 100);
            lbl_percent.Text = percent.ToString();

            double profit = userBalance.getProfit();
            profit_txt.Text = profit.ToString();

            double purchase = userBalance.getPurchaseAmount();
            purchase_txt.Text = purchase.ToString();

            double evaluated = userBalance.getCurrentTotalPrice();
            evaluated_txt.Text = evaluated.ToString();

            double cash = userBalance.getCash();
            cash_txt.Text = cash.ToString();

        }
        
    }
}
