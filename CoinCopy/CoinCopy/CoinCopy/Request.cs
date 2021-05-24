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
    public partial class Request : Form
    {
        string marketPrice;
        public Request(string mPrice)
        {
            marketPrice = mPrice;
            InitializeComponent();
            cmbRequest.Items.Add("매수");
            cmbRequest.Items.Add("매도");
        }

        private void stockNumberTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            // 숫자와 백스페이스만 입력 받도록 하는 코드
            if ( !(char.IsDigit(e.KeyChar) || e.KeyChar == Convert.ToChar(Keys.Back)))
            {
                e.Handled = true;
            }
        }
        /*
         * selection == 0 : 매수 , selection == 1 : 매도         
         *
         */
        private void btnOK_Click(object sender, EventArgs e)
        {
            int selection = cmbRequest.SelectedIndex;

           if (selection == 0)
            {
                double howMany = Int32.Parse(stockNumberTextBox.Text);
                double marketPriceInteger = Int32.Parse(marketPrice);
                
                if ( rdoMarketPrice.Checked == true )
                {
                    
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
    }
}
