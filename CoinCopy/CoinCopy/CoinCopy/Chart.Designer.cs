
namespace CoinCopy
{
    partial class Chart
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.label1 = new System.Windows.Forms.Label();
            this.lblName = new System.Windows.Forms.Label();
            this.lbl2 = new System.Windows.Forms.Label();
            this.lblPrice = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.minToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.candle1m = new System.Windows.Forms.ToolStripMenuItem();
            this.candle3m = new System.Windows.Forms.ToolStripMenuItem();
            this.candle5m = new System.Windows.Forms.ToolStripMenuItem();
            this.candle10m = new System.Windows.Forms.ToolStripMenuItem();
            this.candle30m = new System.Windows.Forms.ToolStripMenuItem();
            this.candle60m = new System.Windows.Forms.ToolStripMenuItem();
            this.candle1day = new System.Windows.Forms.ToolStripMenuItem();
            this.candle1w = new System.Windows.Forms.ToolStripMenuItem();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.trdlabel1 = new System.Windows.Forms.Label();
            this.trdlabel3 = new System.Windows.Forms.Label();
            this.trdlabel2 = new System.Windows.Forms.Label();
            this.trdlabel4 = new System.Windows.Forms.Label();
            this.trdlabel5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 46);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "종목명 :";
            // 
            // lblName
            // 
            this.lblName.AutoSize = true;
            this.lblName.Location = new System.Drawing.Point(118, 46);
            this.lblName.Name = "lblName";
            this.lblName.Size = new System.Drawing.Size(45, 15);
            this.lblName.TabIndex = 2;
            this.lblName.Text = "label2";
            // 
            // lbl2
            // 
            this.lbl2.AutoSize = true;
            this.lbl2.Location = new System.Drawing.Point(26, 84);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(82, 15);
            this.lbl2.TabIndex = 3;
            this.lbl2.Text = "가격정보 : ";
            // 
            // lblPrice
            // 
            this.lblPrice.AutoSize = true;
            this.lblPrice.Location = new System.Drawing.Point(118, 84);
            this.lblPrice.Name = "lblPrice";
            this.lblPrice.Size = new System.Drawing.Size(45, 15);
            this.lblPrice.TabIndex = 4;
            this.lblPrice.Text = "label4";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.minToolStripMenuItem,
            this.candle1day,
            this.candle1w});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(114, 76);
            // 
            // minToolStripMenuItem
            // 
            this.minToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.candle1m,
            this.candle3m,
            this.candle5m,
            this.candle10m,
            this.candle30m,
            this.candle60m});
            this.minToolStripMenuItem.Name = "minToolStripMenuItem";
            this.minToolStripMenuItem.Size = new System.Drawing.Size(113, 24);
            this.minToolStripMenuItem.Text = "min";
            // 
            // candle1m
            // 
            this.candle1m.Checked = true;
            this.candle1m.CheckState = System.Windows.Forms.CheckState.Checked;
            this.candle1m.Name = "candle1m";
            this.candle1m.Size = new System.Drawing.Size(224, 26);
            this.candle1m.Text = "1m";
            this.candle1m.Click += new System.EventHandler(this.mnuView_Click);
            // 
            // candle3m
            // 
            this.candle3m.Name = "candle3m";
            this.candle3m.Size = new System.Drawing.Size(224, 26);
            this.candle3m.Text = "3m";
            this.candle3m.Click += new System.EventHandler(this.mnuView_Click);
            // 
            // candle5m
            // 
            this.candle5m.Name = "candle5m";
            this.candle5m.Size = new System.Drawing.Size(224, 26);
            this.candle5m.Text = "5m";
            this.candle5m.Click += new System.EventHandler(this.mnuView_Click);
            // 
            // candle10m
            // 
            this.candle10m.Name = "candle10m";
            this.candle10m.Size = new System.Drawing.Size(224, 26);
            this.candle10m.Text = "10m";
            this.candle10m.Click += new System.EventHandler(this.mnuView_Click);
            // 
            // candle30m
            // 
            this.candle30m.Name = "candle30m";
            this.candle30m.Size = new System.Drawing.Size(224, 26);
            this.candle30m.Text = "30m";
            this.candle30m.Click += new System.EventHandler(this.mnuView_Click);
            // 
            // candle60m
            // 
            this.candle60m.Name = "candle60m";
            this.candle60m.Size = new System.Drawing.Size(224, 26);
            this.candle60m.Text = "60m";
            this.candle60m.Click += new System.EventHandler(this.mnuView_Click);
            // 
            // candle1day
            // 
            this.candle1day.Name = "candle1day";
            this.candle1day.Size = new System.Drawing.Size(113, 24);
            this.candle1day.Text = "day";
            this.candle1day.Click += new System.EventHandler(this.mnuView_Click);
            // 
            // candle1w
            // 
            this.candle1w.Name = "candle1w";
            this.candle1w.Size = new System.Drawing.Size(113, 24);
            this.candle1w.Text = "week";
            this.candle1w.Click += new System.EventHandler(this.mnuView_Click);
            // 
            // chart1
            // 
            this.chart1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.AxisX.IsReversed = true;
            chartArea1.CursorX.Interval = 0D;
            chartArea1.CursorX.IsUserEnabled = true;
            chartArea1.CursorX.IsUserSelectionEnabled = true;
            chartArea1.CursorY.IsUserEnabled = true;
            chartArea1.Name = "ChartArea1";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.ContextMenuStrip = this.contextMenuStrip1;
            legend1.Docking = System.Windows.Forms.DataVisualization.Charting.Docking.Top;
            legend1.Enabled = false;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(12, 132);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick;
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            series1.YValuesPerPoint = 4;
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(1057, 484);
            this.chart1.TabIndex = 6;
            this.chart1.Text = "chart1";
            this.chart1.Click += new System.EventHandler(this.chart1_Click);
            // 
            // trdlabel1
            // 
            this.trdlabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trdlabel1.AutoSize = true;
            this.trdlabel1.Location = new System.Drawing.Point(800, 18);
            this.trdlabel1.Name = "trdlabel1";
            this.trdlabel1.Size = new System.Drawing.Size(45, 15);
            this.trdlabel1.TabIndex = 7;
            this.trdlabel1.Text = "label2";
            // 
            // trdlabel3
            // 
            this.trdlabel3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trdlabel3.AutoSize = true;
            this.trdlabel3.Location = new System.Drawing.Point(800, 58);
            this.trdlabel3.Name = "trdlabel3";
            this.trdlabel3.Size = new System.Drawing.Size(45, 15);
            this.trdlabel3.TabIndex = 8;
            this.trdlabel3.Text = "label3";
            // 
            // trdlabel2
            // 
            this.trdlabel2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trdlabel2.AutoSize = true;
            this.trdlabel2.Location = new System.Drawing.Point(800, 38);
            this.trdlabel2.Name = "trdlabel2";
            this.trdlabel2.Size = new System.Drawing.Size(45, 15);
            this.trdlabel2.TabIndex = 9;
            this.trdlabel2.Text = "label4";
            // 
            // trdlabel4
            // 
            this.trdlabel4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trdlabel4.AutoSize = true;
            this.trdlabel4.Location = new System.Drawing.Point(800, 78);
            this.trdlabel4.Name = "trdlabel4";
            this.trdlabel4.Size = new System.Drawing.Size(45, 15);
            this.trdlabel4.TabIndex = 10;
            this.trdlabel4.Text = "label5";
            // 
            // trdlabel5
            // 
            this.trdlabel5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.trdlabel5.AutoSize = true;
            this.trdlabel5.Location = new System.Drawing.Point(800, 98);
            this.trdlabel5.Name = "trdlabel5";
            this.trdlabel5.Size = new System.Drawing.Size(45, 15);
            this.trdlabel5.TabIndex = 11;
            this.trdlabel5.Text = "label6";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Location = new System.Drawing.Point(789, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(280, 112);
            this.label2.TabIndex = 13;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(716, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(67, 15);
            this.label3.TabIndex = 14;
            this.label3.Text = "최근거래";
            // 
            // Chart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1081, 628);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.trdlabel5);
            this.Controls.Add(this.trdlabel4);
            this.Controls.Add(this.trdlabel2);
            this.Controls.Add(this.trdlabel3);
            this.Controls.Add(this.trdlabel1);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.lblPrice);
            this.Controls.Add(this.lbl2);
            this.Controls.Add(this.lblName);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Name = "Chart";
            this.Text = "Chart";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Chart_FormClosing);
            this.Load += new System.EventHandler(this.Chart_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lbl2;
        internal System.Windows.Forms.Label lblName;
        internal System.Windows.Forms.Label lblPrice;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem minToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem candle1m;
        private System.Windows.Forms.ToolStripMenuItem candle3m;
        private System.Windows.Forms.ToolStripMenuItem candle5m;
        private System.Windows.Forms.ToolStripMenuItem candle10m;
        private System.Windows.Forms.ToolStripMenuItem candle30m;
        private System.Windows.Forms.ToolStripMenuItem candle60m;
        private System.Windows.Forms.ToolStripMenuItem candle1day;
        private System.Windows.Forms.ToolStripMenuItem candle1w;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.Label trdlabel1;
        private System.Windows.Forms.Label trdlabel3;
        private System.Windows.Forms.Label trdlabel2;
        private System.Windows.Forms.Label trdlabel4;
        private System.Windows.Forms.Label trdlabel5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}