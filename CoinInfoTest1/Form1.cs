using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;

namespace CoinInfoTest1
{


    public partial class Form1 : Form
    {


        public Form1()
        {
            InitializeComponent();
            //getItemData();
            try
            {

                WebClient client = new WebClient();
                client.Encoding = Encoding.UTF8;

                var reply = client.DownloadString("https://api.upbit.com/v1/market/all");

                DataTable dataTable = (DataTable)JsonConvert.DeserializeObject(reply, (typeof(DataTable)));
                //string priceurl = "https://api.upbit.com/v1/candles/minutes/1?market=";
                ////"KRW-BTC" +"&count=1"
                //dataTable.Columns.Add("price");
                //foreach (DataRow row in dataTable.Rows)
                //{
                //    string tempurl = priceurl + row["market"].ToString() + "&count=1";
                //    WebClient tempclient = new WebClient();
                //    tempclient.Encoding = Encoding.UTF8;
                //    var candleinfo = tempclient.DownloadString(tempurl);
                //    var price = JsonConvert.DeserializeObject<PriceEvent>(candleinfo);
                //    row["price"] = price.opening_price.ToString();
                   

                //}
                dataGridView1.DataSource = dataTable;

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public class PriceEvent
        {
            public string market { get; set; }
            public DateTime candle_date_time_utc { get; set; }
            public DateTime candle_date_time_kst { get; set; }
            public double opening_price { get; set; }
            public double high_price { get; set; }
            public double low_price { get; set; }
            public double trade_price { get; set; }
            public long timestamp { get; set; }
            public double candle_acc_trade_price { get; set; }
            public double candle_acc_trade_volume { get; set; }
            public int unit { get; set; }
        }
    }

}


//List<CoinItem> coinItem = JsonConvert.DeserializeObject<List<CoinItem>>(jObj["market"].ToString());

////foreach(CoinItem s in coinItem)
////    dataGridView1.Columns.Add(s.Code.)

//CoinData coinSet = new CoinData();

//foreach (CoinItem tci in coinItem)
//{
//    coinSet.Tables["CoinDT"].Rows.Add(new object[] { tci.Code, "", "" });
//}

//dataGridView1.DataSource = coinSet.Tables["CoinDT"];