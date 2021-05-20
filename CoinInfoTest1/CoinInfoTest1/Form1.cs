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

                // var reply = "{\"item\":" + client.DownloadString("https://api.upbit.com/v1/market/all") + "}";
                var reply = client.DownloadString("https://api.upbit.com/v1/market/all");
               
                DataTable dataTable = (DataTable)JsonConvert.DeserializeObject(reply, (typeof(DataTable)));
                dataGridView1.DataSource = dataTable;

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public class Coin
        {
            public string market { get; set; }
            public string korean_name { get; set; }
            public string english_name { get; set; }

        }
        public class DataList
        {
            public List<Coin> StoriesMasters { get; set; }
        }

        //public void getItemData()

        //{
        //    WebClient client = new WebClient();
        //    string reply = client.DownloadString("https://api.upbit.com/v1/market/all");
            
        //    //JObject jObj = JObject.Parse(reply);

        //    DataTable dt = JsonConvert.DeserializeObject<DataTable>(reply);

        //    dataGridView1.DataSource = dt;
        //}
        
    }
    public class CoinItem
    {
        public string market { get; set; }
        public string korean_name { get; set; }
        public string english_name { get; set; }
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