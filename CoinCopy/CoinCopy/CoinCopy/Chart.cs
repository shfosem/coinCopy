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
        mainForm mForm;
        class PriceInfo
        {
            public DateTime candle_date_time_kst { get; set; }
            public double opening_price { get; set; }
            public double high_price { get; set; }
            public double low_price { get; set; }
            public double trade_price { get; set; }
        }

        class TradeInfo
        {
            public string trade_time_utc { get; set; }
            public double trade_price { get; set; }
            public double trade_volume { get; set; }
            public string ask_bid { get; set; }
        }

        private string name;
        public string code;
        
        public Thread updatethr;
        bool thronoff = false;

        public Thread tradethr;
        bool tradeonoff = false;

        Series chartSeries;
        List<PriceInfo> priceinfolist = new List<PriceInfo>();

        Axis ax;
        Axis ay;
      

        public Chart(mainForm mF)
        {
            InitializeComponent();
            mForm = mF;
            
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

            this.tradethr = new Thread(new ThreadStart(onGoingTrade));
            tradethr.Start();
            tradeonoff = true;
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
                    chart1.Series["Series1"].Points.AddXY(price[i].candle_date_time_kst.ToString("dd HH:mm:ss"), price[i].high_price);

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
                WebClient tempclient = new WebClient();
                tempclient.Encoding = Encoding.UTF8;

                string tempurl = priceurl + code + "&count=1";
                var candleinfo = tempclient.DownloadString(tempurl);
                var price = JsonConvert.DeserializeObject<List<PriceInfo>>(candleinfo);

                int candleCount = priceinfolist.Count - 1;

                bool samecandle = false;

                if (Convert.ToString(kind) == "1m" || Convert.ToString(kind) == "3m" || Convert.ToString(kind) == "5m" 
                    || Convert.ToString(kind) == "10m" || Convert.ToString(kind) == "30m" || Convert.ToString(kind) == "60m")
                    samecandle = (price[0].candle_date_time_kst.ToString("dd HH:mm:ss") == chart1.Series["Series1"].Points[0].AxisLabel) ? true : false;
                else if (Convert.ToString(kind) == "day")
                    samecandle = (price[0].candle_date_time_kst.Date.ToString("dd") == priceinfolist[priceinfolist.Count - 1].candle_date_time_kst.Date.ToString("dd")) ? true : false;
                else if (Convert.ToString(kind) == "weeks")
                    samecandle = (price[0].candle_date_time_kst.Date.ToString("dd") == priceinfolist[priceinfolist.Count - 1].candle_date_time_kst.Date.ToString("dd")) ? true : false;


                if (samecandle)
                {
                    this.Invoke(new MethodInvoker(delegate ()
                    {
                        priceinfolist[0].candle_date_time_kst = price[0].candle_date_time_kst;
                        priceinfolist[0].high_price = price[0].high_price;
                        priceinfolist[0].low_price = price[0].low_price;
                        priceinfolist[0].opening_price = price[0].opening_price;
                        priceinfolist[0].trade_price = price[0].trade_price;

                        chart1.Series["Series1"].Points.RemoveAt(0);

                        chart1.Series["Series1"].Points.InsertXY(0, price[0].candle_date_time_kst.ToString("dd HH:mm:ss"), price[0].high_price);

                        chart1.Series["Series1"].Points[0].YValues[1] = price[0].low_price;
                        chart1.Series["Series1"].Points[0].YValues[2] = price[0].opening_price;
                        chart1.Series["Series1"].Points[0].YValues[3] = price[0].trade_price;

                        //chart1.ResetAutoValues();
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

                        chart1.Series["Series1"].Points.InsertXY(0, price[0].candle_date_time_kst.ToString("dd HH:mm:ss"), price[0].high_price);

                        chart1.Series["Series1"].Points[0].YValues[1] = price[0].low_price;
                        chart1.Series["Series1"].Points[0].YValues[2] = price[0].opening_price;
                        chart1.Series["Series1"].Points[0].YValues[3] = price[0].trade_price;
                        /*
                        //하나씩 뒤로 미는 작업
                        for(int i = chart1.Series["Series1"].Points.Count - 1; i >0; i--)
                        {
                            chart1.Series["Series1"].Points[i].AxisLabel = chart1.Series["Series1"].Points[i - 1].AxisLabel;
                            chart1.Series["Series1"].Points[i].YValues[1] = chart1.Series["Series1"].Points[i - 1].YValues[1];
                            chart1.Series["Series1"].Points[i].YValues[2] = chart1.Series["Series1"].Points[i - 1].YValues[2];
                            chart1.Series["Series1"].Points[i].YValues[3] = chart1.Series["Series1"].Points[i - 1].YValues[3];
                        }

                        //adding low / open / close
                        chart1.Series["Series1"].Points[0].AxisLabel = price[0].candle_date_time_kst.ToString("dd HH:mm:ss");
                        chart1.Series["Series1"].Points[0].YValues[1] = price[0].low_price;
                        chart1.Series["Series1"].Points[0].YValues[2] = price[0].opening_price;
                        chart1.Series["Series1"].Points[0].YValues[3] = price[0].trade_price;

                        chart1.ResetAutoValues();
                        */

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
            if (tradeonoff)
                this.tradethr.Abort();
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
                    requestChart("days", 0);
                    ///
                    break;
                case "week":
                    candle1w.Checked = true;
                    requestChart("weeks", 0);
                    ///
                    break;
            }

        }

        public void onGoingTrade()
        {
            string priceurl = "https://api.upbit.com/v1/trades/ticks?market=";
            WebClient tempclient = new WebClient();
            tempclient.Encoding = Encoding.UTF8;

            string tempurl = priceurl + code + "&count=5";

            while (tradeonoff)
            {
                var candleinfo = tempclient.DownloadString(tempurl);
                var trade = JsonConvert.DeserializeObject<List<TradeInfo>>(candleinfo);
                DateTime utc2kst;

                StringBuilder[] strbld = new StringBuilder[5];
                for (int i = 0; i < strbld.Length; i++)
                    strbld[i] = new StringBuilder("");

                for (int i = 0; i < 5; i++)
                {
                    strbld[i].Clear();

                    utc2kst = Convert.ToDateTime(trade[i].trade_time_utc.ToString());
                    utc2kst = utc2kst.AddHours(9);

                    strbld[i].Append(utc2kst.ToString("HH:mm:ss"));
                    strbld[i].Append("  ");
                    strbld[i].Append(trade[i].trade_price.ToString());
                    strbld[i].Append("  ");
                    strbld[i].Append(trade[i].trade_volume.ToString());
                }
                if (IsHandleCreated)
                {
                    //try
                    //{
                    this.Invoke(new MethodInvoker(delegate ()
                    {
                        trdlabel1.Text = strbld[0].ToString();
                        if (trade[0].ask_bid.ToString() == "ASK")
                            trdlabel1.ForeColor = Color.Red;
                        else
                            trdlabel1.ForeColor = Color.Blue;

                        trdlabel2.Text = strbld[1].ToString();
                        if (trade[1].ask_bid.ToString() == "ASK")
                            trdlabel2.ForeColor = Color.Red;
                        else
                            trdlabel2.ForeColor = Color.Blue;

                        trdlabel3.Text = strbld[2].ToString();
                        if (trade[2].ask_bid.ToString() == "ASK")
                            trdlabel3.ForeColor = Color.Red;
                        else
                            trdlabel3.ForeColor = Color.Blue;

                        trdlabel4.Text = strbld[3].ToString();
                        if (trade[3].ask_bid.ToString() == "ASK")
                            trdlabel4.ForeColor = Color.Red;
                        else
                            trdlabel4.ForeColor = Color.Blue;

                        trdlabel5.Text = strbld[4].ToString();
                        if (trade[4].ask_bid.ToString() == "ASK")
                            trdlabel5.ForeColor = Color.Red;
                        else
                            trdlabel5.ForeColor = Color.Blue;
                    }));
                    //}
                    //catch (Exception e)
                    //{ }
                }

                Delay(1000);
            }
        }

        private void btnOpenRequest_Click(object sender, EventArgs e)
        {
            Request r = new Request(code, lblName.Text, lblPrice.Text, mForm.userBalance, mForm);
            r.Owner = this;
            r.Show();
        }
    }
}
 