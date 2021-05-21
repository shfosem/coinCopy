using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Windows.Forms.DataVisualization.Charting;

using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CoinCopy
{

    public partial class Chart : Form
    {
        class PriceInfo
        {
            //public string market { get; set; }
            //public DateTime candle_date_time_utc { get; set; }
            public DateTime candle_date_time_kst { get; set; }
            public double opening_price { get; set; }
            public double high_price { get; set; }
            public double low_price { get; set; }
            public double trade_price { get; set; }
            //public long timestamp { get; set; }
            //public double candle_acc_trade_price { get; set; }
            //public double candle_acc_trade_volume { get; set; }
            //public int unit { get; set; }
        }


        private string name;
        public string code;

        Series chartSeries;
        List<PriceInfo> priceinfolist = new List<PriceInfo>();

        Axis ax;
        Axis ay;


        public Chart()
        {
            InitializeComponent();


            chartSeries = chart1.Series["Series1"];
            chart1.Series["Series1"]["PriceUpColor"] = "Red";
            chart1.Series["Series1"]["PriceDownColor"] = "Blue";
            chart1.AxisViewChanged += chart1_AxisViewChanged;
        }

        private void Chart_Load(object sender, EventArgs e)
        {
            name = this.lblName.Text.ToString();
            MessageBox.Show(name.ToString());

            requestChart_Daily();

            ax = chart1.ChartAreas[0].AxisX;
            ay = chart1.ChartAreas[0].AxisY;

            //chart1.ChartAreas[0].CursorX.IsUserEnabled = Enabled;
            //chart1.ChartAreas[0].CursorY.IsUserEnabled = Enabled;
            //chart1.ChartAreas[0].CursorX.AutoScroll = true;
        }

        private void requestChart_Daily()
        {
            string priceurl = "https://api.upbit.com/v1/candles/days/?market=";
            WebClient tempclient = new WebClient();
            tempclient.Encoding = Encoding.UTF8;

            string tempurl = priceurl + code + "&count=100";
            var candleinfo = tempclient.DownloadString(tempurl);
            var price = JsonConvert.DeserializeObject<List<PriceInfo>>(candleinfo);

            int candleCount = price.Count;

            for (int i = 0; i < candleCount; i++)
            {
                /* add datas to use in other method */
                priceinfolist.Add(new PriceInfo()
                {
                    candle_date_time_kst = price[i].candle_date_time_kst,
                    opening_price = price[i].opening_price,
                    high_price = price[i].high_price,
                    low_price = price[i].low_price,
                    trade_price = price[i].trade_price
                });
                

                //adding data n high_price
                chart1.Series["Series1"].Points.AddXY(price[i].candle_date_time_kst, price[i].high_price);

                //adding low / open / close
                chart1.Series["Series1"].Points[i].YValues[1] = price[i].low_price;
                chart1.Series["Series1"].Points[i].YValues[2] = price[i].opening_price;
                chart1.Series["Series1"].Points[i].YValues[3] = price[i].trade_price;

            }

        }
        
        /* Change the MAX and min val of the Y-axis after drag the chart (*******PROBLEM******)*/
        private void chart1_AxisViewChanged(object sender, ViewEventArgs e)
        {
            if(sender.Equals(chart1))
            {
                int start = (int)e.Axis.ScaleView.ViewMinimum;
                int end = (int)e.Axis.ScaleView.ViewMaximum;

                double max = (double)e.ChartArea.AxisY.ScaleView.ViewMaximum;
                double min = (double)e.ChartArea.AxisY.ScaleView.ViewMinimum;
                
                this.chart1.ChartAreas[0].AxisY.Minimum = priceinfolist[0].low_price;
                //min = priceinfolist[start].low_price;
                
                
                
                if (((int)e.Axis.ScaleView.ViewMaximum - (int)e.Axis.ScaleView.ViewMinimum) == (priceinfolist.Count + 1))
                    min = 0;
                
                for(int i = start-1; i < end; i++)
                {
                    if (i >= priceinfolist.Count)
                        break;
                    if (i < 0)
                        i = 0;

                    if (priceinfolist[i].high_price > max)
                        max = priceinfolist[i].high_price;
                    if (priceinfolist[i].low_price < min)
                        min = priceinfolist[i].low_price;
                }

                this.chart1.ChartAreas[0].AxisY.Maximum = max;
                this.chart1.ChartAreas[0].AxisY.Minimum = min;
            }
        }

        private void chart1_Click(object sender, EventArgs e)
        {
            Text = String.Format("Pos = {0}, Size = {1}, Min = {2}, Max = {3}",

                  chart1.ChartAreas[0].AxisX.ScaleView.Position,

                  chart1.ChartAreas[0].AxisX.ScaleView.Size,

                  chart1.ChartAreas[0].AxisX.ScaleView.ViewMinimum,

                  chart1.ChartAreas[0].AxisX.ScaleView.ViewMaximum);
        }
    }
}
