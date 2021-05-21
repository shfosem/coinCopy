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
        public mainForm()
        {
            InitializeComponent();
            balance b = new balance();
            double totalAsset = b.getTotalAsset();
            double cash = b.getCash();
            double profitPercent = b.calcProfitPercentage();
            double profit = b.getProfit();
            double purchase = b.getPurchaseAmount();
            double currentTotalPrice = b.getCurrentTotalPrice();

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
            Search searchform = new Search();
            searchform.Owner = this;
            searchform.Show();
        }

        private void cmsRequest_Click(object sender, EventArgs e)
        {
            Request requestform = new Request();
            requestform.Owner = this;
            requestform.Show();
        }

        private void cms_chart_Click(object sender, EventArgs e)
        {
            Chart chartform = new Chart();
            chartform.Owner = this;
            chartform.Show();
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            // 잔고 초기화
            balance b = new balance();
            double totalAsset = b.getTotalAsset();
            double cash = b.getCash();
            double profitPercent = b.calcProfitPercentage();
            double profit = b.getProfit();
            double purchase = b.getPurchaseAmount();
            double currentTotalPrice = b.getCurrentTotalPrice();

            lblBalance.Text = totalAsset.ToString();
            lbl_percent.Text = "0%";
            profit_txt.Text = profit.ToString();
            purchase_txt.Text = purchase.ToString();
            evaluated_txt.Text = currentTotalPrice.ToString();
            cash_txt.Text = cash.ToString();

            
        }
    }
}
