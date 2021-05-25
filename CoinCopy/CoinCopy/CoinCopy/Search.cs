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
using Newtonsoft.Json.Linq;
namespace CoinCopy
{
    public partial class Search : Form
    {        
        mainForm mForm;
        public Search(balance uBalance, mainForm mF)
        {
           
            mForm = mF;

            InitializeComponent();
            try
            {

                WebClient client = new WebClient();
                client.Encoding = Encoding.UTF8;

                var reply = client.DownloadString("https://api.upbit.com/v1/market/all");

                DataTable dataTable = (DataTable)JsonConvert.DeserializeObject(reply, (typeof(DataTable)));

                for (int i = dataTable.Rows.Count - 1; i >= 0; i--) //KRW 빼고 모두 삭제
                {
                    DataRow dr = dataTable.Rows[i];
                    if (!dr["market"].ToString().Contains("KRW"))
                        dr.Delete();
                }

                dataTable.AcceptChanges();


                dgvCoin.DataSource = dataTable;


            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void get_price(DataTable dataTable)
        {

            string priceurl = "https://api.upbit.com/v1/candles/minutes/1?market=";
            WebClient tempclient = new WebClient();
            tempclient.Encoding = Encoding.UTF8;
            foreach (DataRow row in dataTable.Rows)
            {     
                //"KRW-BTC" +"&count=1"
                string tempurl = priceurl + row["market"].ToString() + "&count=1";
                var candleinfo = tempclient.DownloadString(tempurl);
                var price = JsonConvert.DeserializeObject<List<PriceEvent>>(candleinfo);
                row["price"] = price[0].opening_price.ToString();
            }
        }

        private void Search_Load(object sender, EventArgs e)
        {

        }
        public class PriceEvent
        {
            public string market { get; set; }
            public DateTime candle_date_time_utc { get; set; }
            public DateTime candle_date_time_kst { get; set; }
            public decimal opening_price { get; set; }
            public decimal high_price { get; set; }
            public decimal low_price { get; set; }
            public decimal trade_price { get; set; }
            public long timestamp { get; set; }
            public decimal candle_acc_trade_price { get; set; }
            public decimal candle_acc_trade_volume { get; set; }
            public int unit { get; set; }
        }

        private void dgvCoin_CellMousedecimalClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int n = dgvCoin.SelectedRows[0].Index;
            string code = dgvCoin.Rows[n].Cells[0].Value.ToString();
            loadCoinInfo(code);
        }

        private void 정보ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            int n = dgvCoin.SelectedRows[0].Index;
            string code = dgvCoin.Rows[n].Cells[0].Value.ToString();
            loadCoinInfo(code);
        }

        public void loadCoinInfo(string code)
        {
            string priceurl = "https://api.upbit.com/v1/candles/minutes/1?market=";
            WebClient tempclient = new WebClient();
            tempclient.Encoding = Encoding.UTF8;

            string tempurl = priceurl + code + "&count=1";
            var candleinfo = tempclient.DownloadString(tempurl);
            var price = JsonConvert.DeserializeObject<List<PriceEvent>>(candleinfo);

            mForm.buy_data.stockName = price[0].market.ToString();
            mForm.buy_data.buyCost = price[0].opening_price;

            Chart chart = new Chart(mForm);
            chart.Owner = this.Owner;
            chart.lblName.Text = price[0].market.ToString();
            chart.lblPrice.Text = price[0].opening_price.ToString();
            chart.code = code;            
            chart.Show();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            // 검색하면 목표 아이템으로 Selected와 Focus 이동 (한글이름)
            string searchValue = txtCoinName.Text;
            dgvCoin.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            int rowIndex = -1;
            try
            {
                foreach (DataGridViewRow row in dgvCoin.Rows)
                {
                    if (row.Cells[1].Value.ToString().Contains(searchValue))
                    {
                        rowIndex = row.Index;
                        dgvCoin.ClearSelection();
                        row.Selected = true;
                        dgvCoin.FirstDisplayedScrollingRowIndex = rowIndex;
                        dgvCoin.Focus();

                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

    
    
}
