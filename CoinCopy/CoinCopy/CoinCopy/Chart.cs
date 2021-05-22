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

using System.Threading;

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
        public Thread updatethr;
        bool thronoff = false;

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
            //chart1.AxisViewChanged += chart1_AxisViewChanged;
            chart1.ChartAreas["ChartArea1"].CursorX.IsUserEnabled = true;
            chart1.ChartAreas["ChartArea1"].CursorX.IsUserSelectionEnabled = true;
            chart1.MouseWheel += chart1_MouseWheel;
            //chart1.ChartAreas["ChartArea1"].CursorX.Interval = 1;

        }

        private void Chart_Load(object sender, EventArgs e)
        {
            name = this.lblName.Text.ToString();
            MessageBox.Show(name.ToString());

            requestChart("minutes", 1);

            ax = chart1.ChartAreas[0].AxisX;
            ay = chart1.ChartAreas[0].AxisY;

            
        }

        private void requestChart(string kind, int num)
        {
            string priceurl = null;

            if (kind == "minutes")
            {
                if (num == 1)
                    priceurl = "https://api.upbit.com/v1/candles/minutes/1?market=";
                else if(num == 3)
                    priceurl = "https://api.upbit.com/v1/candles/minutes/3?market=";
                else if(num ==5)
                    priceurl = "https://api.upbit.com/v1/candles/minutes/5?market=";
                else if(num == 10)
                    priceurl = "https://api.upbit.com/v1/candles/minutes/10?market=";
                else if(num == 30)
                    priceurl = "https://api.upbit.com/v1/candles/minutes/30?market=";
                else if(num == 60)
                    priceurl = "https://api.upbit.com/v1/candles/minutes/60?market=";
            }
            else if(kind == "days")
                priceurl = "https://api.upbit.com/v1/candles/days?market=";
            else if(kind == "weeks")
                priceurl = "https://api.upbit.com/v1/candles/weeks?market=";

           
            WebClient tempclient = new WebClient();
            tempclient.Encoding = Encoding.UTF8;

            string tempurl = priceurl + code + "&count=100";
            var candleinfo = tempclient.DownloadString(tempurl);
            var price = JsonConvert.DeserializeObject<List<PriceInfo>>(candleinfo);

            int candleCount = price.Count;

            priceinfolist.Clear();
            chart1.Series["Series1"].Points.Clear();

            double min = -1;
            double max = -1;

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

                if (min < 0)
                    min = price[i].low_price;
                else if (min > price[i].low_price)
                    min = price[i].low_price;

                if (max < price[i].high_price)
                    max = price[i].high_price;

                

            }

            if (price[price.Count - 1].low_price > min)
                this.chart1.ChartAreas[0].AxisY.Minimum = min * 0.995;
            else if (2 * min - max > 0)
                this.chart1.ChartAreas[0].AxisY.Minimum = 1.5 * min - 0.5 * max;
            else
                this.chart1.ChartAreas[0].AxisY.Minimum = 0;

            this.chart1.ChartAreas[0].AxisY.Maximum = 1.2 * max - 0.2 * min;

            if (thronoff)
            {
                this.updatethr.Abort();
                thronoff = false;
            }
            
            this.updatethr = new Thread(new ParameterizedThreadStart(updateCandle));
            //updatethr.Start();

            if(kind == "minutes")
            {
                if (num == 1)
                    updatethr.Start("1m");
                else if (num == 3)
                    updatethr.Start("3m");
                else if (num == 5)
                    updatethr.Start("5m");
                else if (num == 10)
                    updatethr.Start("10m");
                else if (num == 30)
                    updatethr.Start("30m");
                else if (num == 60)
                    updatethr.Start("60m");
            }

            thronoff = true;
            
        }

        /* Change the MAX and min val of the Y-axis after drag the chart (*******PROBLEM******)
        private void chart1_AxisViewChanged(object sender, ViewEventArgs e)
        {
            /*
            if (sender.Equals(chart1))
            {
                int start = (int)e.Axis.ScaleView.ViewMinimum;
                int end = (int)e.Axis.ScaleView.ViewMaximum;

                double max = (double)e.ChartArea.AxisY.ScaleView.ViewMaximum;
                double min = (double)e.ChartArea.AxisY.ScaleView.ViewMinimum;

                this.chart1.ChartAreas[0].AxisY.Minimum = priceinfolist[0].low_price;
                //min = priceinfolist[start].low_price;

                min = 0;
                max = 0;

                if (((int)e.Axis.ScaleView.ViewMaximum - (int)e.Axis.ScaleView.ViewMinimum) == (priceinfolist.Count + 1))
                    min = 0;

                for (int i = start - 1; i < end; i++)
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

                //this.chart1.ChartAreas[0].AxisY.Maximum = max;
                //this.chart1.ChartAreas[0].AxisY.Minimum = min;



                if (2 * min - max > 0)
                    this.chart1.ChartAreas[0].AxisY.Minimum = 2 * min - max;
                else
                    this.chart1.ChartAreas[0].AxisY.Minimum = 0;

                this.chart1.ChartAreas[0].AxisY.Maximum = 1.2 * max - 0.2 * min;
            }
            
        }
        */

        private void chart1_Click(object sender, EventArgs e)
        {
            Text = String.Format("Pos = {0}, Size = {1}, Min = {2}, Max = {3}, X = {4}, Y = {5}",

                  chart1.ChartAreas[0].AxisX.ScaleView.Position,

                  chart1.ChartAreas[0].AxisX.ScaleView.Size,

                  chart1.ChartAreas[0].AxisX.ScaleView.ViewMinimum,

                  chart1.ChartAreas[0].AxisX.ScaleView.ViewMaximum,

                  chart1.ChartAreas[0].CursorX.Position,
                  
                  chart1.ChartAreas[0].CursorY.Position);
        }


        private void chart1_MouseWheel(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Delta < 0)
                {
                    chart1.ChartAreas[0].AxisX.ScaleView.ZoomReset();
                    chart1.ChartAreas[0].AxisY.ScaleView.ZoomReset();
                }

                if (e.Delta > 0)
                {
                    double xMin = chart1.ChartAreas[0].AxisX.ScaleView.ViewMinimum;
                    double xMax = chart1.ChartAreas[0].AxisX.ScaleView.ViewMaximum;
                    double yMin = chart1.ChartAreas[0].AxisY.ScaleView.ViewMinimum;
                    double yMax = chart1.ChartAreas[0].AxisY.ScaleView.ViewMaximum;

                    double posXStart = chart1.ChartAreas[0].AxisX.PixelPositionToValue(e.Location.X) - (xMax - xMin) / 2;
                    double posXFinish = chart1.ChartAreas[0].AxisX.PixelPositionToValue(e.Location.X) + (xMax - xMin) / 2;
                    double posYStart = chart1.ChartAreas[0].AxisY.PixelPositionToValue(e.Location.Y) - (yMax - yMin) / 2;
                    double posYFinish = chart1.ChartAreas[0].AxisY.PixelPositionToValue(e.Location.Y) + (yMax - yMin) / 2;

                    chart1.ChartAreas[0].AxisX.ScaleView.Zoom(posXStart, posXFinish);
                    chart1.ChartAreas[0].AxisY.ScaleView.Zoom(posYStart, posYFinish);
                }
            }
            catch { }
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

        private void updateCandle(object kind)
        {
            string priceurl = null;

            if(Convert.ToString(kind) == "1m")
                priceurl = "https://api.upbit.com/v1/candles/minutes/1?market=";
            else if(Convert.ToString(kind) == "3m")
                priceurl = "https://api.upbit.com/v1/candles/minutes/3?market=";
            else if (Convert.ToString(kind) == "5m")
                priceurl = "https://api.upbit.com/v1/candles/minutes/5?market=";
            else if (Convert.ToString(kind) == "10m")
                priceurl = "https://api.upbit.com/v1/candles/minutes/10?market=";
            else if (Convert.ToString(kind) == "30m")
                priceurl = "https://api.upbit.com/v1/candles/minutes/30?market=";
            else if (Convert.ToString(kind) == "60m")
                priceurl = "https://api.upbit.com/v1/candles/minutes/60?market=";
            else if (Convert.ToString(kind) == "day")
                priceurl = "https://api.upbit.com/v1/candles/days?market=";
            else if (Convert.ToString(kind) == "weeks")
                priceurl = "https://api.upbit.com/v1/candles/weeks?market=";
            
            while (true)
            {
                //MessageBox.Show(" ");
                //string priceurl = "https://api.upbit.com/v1/candles/minutes/1?market=";
                WebClient tempclient = new WebClient();
                tempclient.Encoding = Encoding.UTF8;

                string tempurl = priceurl + code + "&count=1";
                var candleinfo = tempclient.DownloadString(tempurl);
                var price = JsonConvert.DeserializeObject<List<PriceInfo>>(candleinfo);

                int candleCount = priceinfolist.Count - 1;

                bool newcandle = false;




                if (Convert.ToString(kind) == "1m")
                    newcandle = (price[0].candle_date_time_kst.Minute.ToString() == priceinfolist[candleCount].candle_date_time_kst.Minute.ToString()) ? true : false;
                else if (Convert.ToString(kind) == "3m")
                    newcandle = (price[0].candle_date_time_kst.Minute.ToString() == priceinfolist[candleCount].candle_date_time_kst.Minute.ToString()) ? true : false;
                else if (Convert.ToString(kind) == "5m")
                    newcandle = (price[0].candle_date_time_kst.Minute.ToString() == priceinfolist[candleCount].candle_date_time_kst.Minute.ToString()) ? true : false;
                else if (Convert.ToString(kind) == "10m")
                    newcandle = (price[0].candle_date_time_kst.Minute.ToString() == priceinfolist[candleCount].candle_date_time_kst.Minute.ToString()) ? true : false;
                else if (Convert.ToString(kind) == "30m")
                    newcandle = (price[0].candle_date_time_kst.Minute.ToString() == priceinfolist[candleCount].candle_date_time_kst.Minute.ToString()) ? true : false;
                else if (Convert.ToString(kind) == "60m")
                    newcandle = (price[0].candle_date_time_kst.Minute.ToString() == priceinfolist[candleCount].candle_date_time_kst.Minute.ToString()) ? true : false;
                else if (Convert.ToString(kind) == "day")
                    newcandle = (price[0].candle_date_time_kst.Date.ToString() == priceinfolist[candleCount].candle_date_time_kst.Date.ToString()) ? true : false;
                else if (Convert.ToString(kind) == "weeks")
                    newcandle = (price[0].candle_date_time_kst.Date.ToString() == priceinfolist[candleCount].candle_date_time_kst.Date.ToString()) ? true : false;





                if (price[0].candle_date_time_kst.Minute.ToString() == priceinfolist[candleCount].candle_date_time_kst.Minute.ToString())
                {
                    this.Invoke(new MethodInvoker(delegate ()
                    {
                        priceinfolist[candleCount].candle_date_time_kst = price[0].candle_date_time_kst;
                        priceinfolist[candleCount].high_price = price[0].high_price;
                        priceinfolist[candleCount].low_price = price[0].low_price;
                        priceinfolist[candleCount].opening_price = price[0].opening_price;
                        priceinfolist[candleCount].trade_price = price[0].trade_price;

                        chart1.Series["Series1"].Points.RemoveAt(candleCount);

                        chart1.Series["Series1"].Points.AddXY(price[0].candle_date_time_kst, price[0].high_price);

                        chart1.Series["Series1"].Points[candleCount].YValues[1] = price[0].low_price;
                        chart1.Series["Series1"].Points[candleCount].YValues[2] = price[0].opening_price;
                        chart1.Series["Series1"].Points[candleCount].YValues[3] = price[0].trade_price;
                    }));
                }
                else
                {
                    this.Invoke(new MethodInvoker(delegate ()
                    {
                        priceinfolist.Add(new PriceInfo()
                        {
                            candle_date_time_kst = price[0].candle_date_time_kst,
                            opening_price = price[0].opening_price,
                            high_price = price[0].high_price,
                            low_price = price[0].low_price,
                            trade_price = price[0].trade_price
                        });


                        //adding data n high_price
                        chart1.Series["Series1"].Points.AddXY(price[0].candle_date_time_kst, price[0].high_price);

                        //adding low / open / close
                        chart1.Series["Series1"].Points[candleCount].YValues[1] = price[0].low_price;
                        chart1.Series["Series1"].Points[candleCount].YValues[2] = price[0].opening_price;
                        chart1.Series["Series1"].Points[candleCount].YValues[3] = price[0].trade_price;

                    }));


                    candleCount += 1;


                }

                Delay(1000);

            }
        }

        private void Chart_FormClosing(object sender, FormClosingEventArgs e)
        {
            if(thronoff)
                this.updatethr.Abort();
        }


        public void mnuView_Click(object sender, EventArgs e)
        {
            ToolStripMenuItem item = (ToolStripMenuItem)sender;

            candle1m.Checked = false;
            candle3m.Checked = false;
            candle5m.Checked = false;
            candle10m.Checked = false;
            candle30m.Checked = false;
            candle60m.Checked = false;
            candle1day.Checked = false;
            candle1w.Checked = false;

            switch(item.Text)
            {
                case "1m":
                    candle1m.Checked = true;
                    requestChart("minutes", 1);
                    ///
                    break;
                case "3m":
                    candle3m.Checked = true;
                    requestChart("minutes", 3);
                    ///
                    break;
                case "5m":
                    candle5m.Checked = true;
                    requestChart("minutes", 5);
                    ///
                    break;
                case "10m":
                    candle10m.Checked = true;
                    requestChart("minutes", 10);
                    ///
                    break;
                case "30m":
                    candle30m.Checked = true;
                    requestChart("minutes", 30);
                    ///
                    break;
                case "60m":
                    candle60m.Checked = true;
                    requestChart("minutes", 60);
                    ///
                    break;
                case "day":
                    candle1day.Checked = true;
                    requestChart("days", 1);
                    ///
                    break;
                case "week":
                    candle1w.Checked = true;
                    requestChart("weeks", 1);
                    ///
                    break;
            }

        }

    }
}
 