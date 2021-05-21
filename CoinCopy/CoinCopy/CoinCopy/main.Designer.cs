
namespace CoinCopy
{
    partial class mainForm
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
            this.balanceDgv = new System.Windows.Forms.DataGridView();
            this.CoinName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.number = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.current_price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.avg_price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.profit_per = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.profit_won = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.evaluated_price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.purchase_price = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.cms = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.cms_chart = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsRequest = new System.Windows.Forms.ToolStripMenuItem();
            this.grpBalance = new System.Windows.Forms.GroupBox();
            this.cash_txt = new System.Windows.Forms.Label();
            this.evaluated_txt = new System.Windows.Forms.Label();
            this.purchase_txt = new System.Windows.Forms.Label();
            this.profit_txt = new System.Windows.Forms.Label();
            this.label = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lbl_percent = new System.Windows.Forms.Label();
            this.lbl_profit = new System.Windows.Forms.Label();
            this.lblWon = new System.Windows.Forms.Label();
            this.lblBalance = new System.Windows.Forms.Label();
            this.btnReset = new System.Windows.Forms.Button();
            this.btnSearch = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.balanceDgv)).BeginInit();
            this.cms.SuspendLayout();
            this.grpBalance.SuspendLayout();
            this.SuspendLayout();
            // 
            // balanceDgv
            // 
            this.balanceDgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.balanceDgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CoinName,
            this.number,
            this.current_price,
            this.avg_price,
            this.profit_per,
            this.profit_won,
            this.evaluated_price,
            this.purchase_price});
            this.balanceDgv.ContextMenuStrip = this.cms;
            this.balanceDgv.Location = new System.Drawing.Point(10, 196);
            this.balanceDgv.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.balanceDgv.Name = "balanceDgv";
            this.balanceDgv.RowHeadersWidth = 51;
            this.balanceDgv.RowTemplate.Height = 27;
            this.balanceDgv.Size = new System.Drawing.Size(1103, 377);
            this.balanceDgv.TabIndex = 0;
            // 
            // CoinName
            // 
            this.CoinName.HeaderText = "종목명";
            this.CoinName.MinimumWidth = 6;
            this.CoinName.Name = "CoinName";
            this.CoinName.Width = 125;
            // 
            // number
            // 
            this.number.HeaderText = "보유수량";
            this.number.MinimumWidth = 6;
            this.number.Name = "number";
            this.number.Width = 125;
            // 
            // current_price
            // 
            this.current_price.HeaderText = "현재가";
            this.current_price.MinimumWidth = 6;
            this.current_price.Name = "current_price";
            this.current_price.Width = 125;
            // 
            // avg_price
            // 
            this.avg_price.HeaderText = "평균가";
            this.avg_price.MinimumWidth = 6;
            this.avg_price.Name = "avg_price";
            this.avg_price.Width = 125;
            // 
            // profit_per
            // 
            this.profit_per.HeaderText = "손익률";
            this.profit_per.MinimumWidth = 6;
            this.profit_per.Name = "profit_per";
            this.profit_per.Width = 125;
            // 
            // profit_won
            // 
            this.profit_won.HeaderText = "평가손익";
            this.profit_won.MinimumWidth = 6;
            this.profit_won.Name = "profit_won";
            this.profit_won.Width = 125;
            // 
            // evaluated_price
            // 
            this.evaluated_price.HeaderText = "평가금액";
            this.evaluated_price.MinimumWidth = 6;
            this.evaluated_price.Name = "evaluated_price";
            this.evaluated_price.Width = 125;
            // 
            // purchase_price
            // 
            this.purchase_price.HeaderText = "매입금액";
            this.purchase_price.MinimumWidth = 6;
            this.purchase_price.Name = "purchase_price";
            this.purchase_price.Width = 125;
            // 
            // cms
            // 
            this.cms.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cms_chart,
            this.cmsRequest});
            this.cms.Name = "cms";
            this.cms.Size = new System.Drawing.Size(99, 48);
            // 
            // cms_chart
            // 
            this.cms_chart.Name = "cms_chart";
            this.cms_chart.Size = new System.Drawing.Size(98, 22);
            this.cms_chart.Text = "차트";
            this.cms_chart.Click += new System.EventHandler(this.cms_chart_Click);
            // 
            // cmsRequest
            // 
            this.cmsRequest.Name = "cmsRequest";
            this.cmsRequest.Size = new System.Drawing.Size(98, 22);
            this.cmsRequest.Text = "주문";
            this.cmsRequest.Click += new System.EventHandler(this.cmsRequest_Click);
            // 
            // grpBalance
            // 
            this.grpBalance.Controls.Add(this.cash_txt);
            this.grpBalance.Controls.Add(this.evaluated_txt);
            this.grpBalance.Controls.Add(this.purchase_txt);
            this.grpBalance.Controls.Add(this.profit_txt);
            this.grpBalance.Controls.Add(this.label);
            this.grpBalance.Controls.Add(this.label3);
            this.grpBalance.Controls.Add(this.label2);
            this.grpBalance.Controls.Add(this.label1);
            this.grpBalance.Controls.Add(this.lbl_percent);
            this.grpBalance.Controls.Add(this.lbl_profit);
            this.grpBalance.Controls.Add(this.lblWon);
            this.grpBalance.Controls.Add(this.lblBalance);
            this.grpBalance.Location = new System.Drawing.Point(10, 38);
            this.grpBalance.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpBalance.Name = "grpBalance";
            this.grpBalance.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.grpBalance.Size = new System.Drawing.Size(839, 142);
            this.grpBalance.TabIndex = 1;
            this.grpBalance.TabStop = false;
            this.grpBalance.Text = "잔고";
            // 
            // cash_txt
            // 
            this.cash_txt.AutoSize = true;
            this.cash_txt.Location = new System.Drawing.Point(370, 106);
            this.cash_txt.Name = "cash_txt";
            this.cash_txt.Size = new System.Drawing.Size(11, 12);
            this.cash_txt.TabIndex = 15;
            this.cash_txt.Text = "0";
            // 
            // evaluated_txt
            // 
            this.evaluated_txt.AutoSize = true;
            this.evaluated_txt.Location = new System.Drawing.Point(265, 106);
            this.evaluated_txt.Name = "evaluated_txt";
            this.evaluated_txt.Size = new System.Drawing.Size(11, 12);
            this.evaluated_txt.TabIndex = 14;
            this.evaluated_txt.Text = "0";
            // 
            // purchase_txt
            // 
            this.purchase_txt.AutoSize = true;
            this.purchase_txt.Location = new System.Drawing.Point(160, 106);
            this.purchase_txt.Name = "purchase_txt";
            this.purchase_txt.Size = new System.Drawing.Size(11, 12);
            this.purchase_txt.TabIndex = 13;
            this.purchase_txt.Text = "0";
            // 
            // profit_txt
            // 
            this.profit_txt.AutoSize = true;
            this.profit_txt.Location = new System.Drawing.Point(55, 106);
            this.profit_txt.Name = "profit_txt";
            this.profit_txt.Size = new System.Drawing.Size(11, 12);
            this.profit_txt.TabIndex = 12;
            this.profit_txt.Text = "0";
            // 
            // label
            // 
            this.label.AutoSize = true;
            this.label.Location = new System.Drawing.Point(350, 77);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(53, 12);
            this.label.TabIndex = 11;
            this.label.Text = "보유현금";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(245, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 10;
            this.label3.Text = "유가총액";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(140, 77);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 9;
            this.label2.Text = "매입금액";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 77);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "손익금액";
            // 
            // lbl_percent
            // 
            this.lbl_percent.AutoSize = true;
            this.lbl_percent.Location = new System.Drawing.Point(382, 27);
            this.lbl_percent.Name = "lbl_percent";
            this.lbl_percent.Size = new System.Drawing.Size(21, 12);
            this.lbl_percent.TabIndex = 7;
            this.lbl_percent.Text = "0%";
            // 
            // lbl_profit
            // 
            this.lbl_profit.AutoSize = true;
            this.lbl_profit.Location = new System.Drawing.Point(311, 27);
            this.lbl_profit.Name = "lbl_profit";
            this.lbl_profit.Size = new System.Drawing.Size(41, 12);
            this.lbl_profit.TabIndex = 6;
            this.lbl_profit.Text = "수익률";
            // 
            // lblWon
            // 
            this.lblWon.AutoSize = true;
            this.lblWon.Location = new System.Drawing.Point(158, 27);
            this.lblWon.Name = "lblWon";
            this.lblWon.Size = new System.Drawing.Size(17, 12);
            this.lblWon.TabIndex = 5;
            this.lblWon.Text = "원";
            // 
            // lblBalance
            // 
            this.lblBalance.AutoSize = true;
            this.lblBalance.Location = new System.Drawing.Point(140, 27);
            this.lblBalance.Name = "lblBalance";
            this.lblBalance.Size = new System.Drawing.Size(11, 12);
            this.lblBalance.TabIndex = 4;
            this.lblBalance.Text = "0";
            // 
            // btnReset
            // 
            this.btnReset.Location = new System.Drawing.Point(980, 115);
            this.btnReset.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(66, 19);
            this.btnReset.TabIndex = 3;
            this.btnReset.Text = "초기화";
            this.btnReset.UseVisualStyleBackColor = true;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(962, 38);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(121, 27);
            this.btnSearch.TabIndex = 2;
            this.btnSearch.Text = "종목검색";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1125, 584);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.grpBalance);
            this.Controls.Add(this.balanceDgv);
            this.Controls.Add(this.btnReset);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "mainForm";
            this.Text = "Main";
            this.Load += new System.EventHandler(this.main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.balanceDgv)).EndInit();
            this.cms.ResumeLayout(false);
            this.grpBalance.ResumeLayout(false);
            this.grpBalance.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView balanceDgv;
        private System.Windows.Forms.GroupBox grpBalance;
        private System.Windows.Forms.Label lbl_percent;
        private System.Windows.Forms.Label lbl_profit;
        private System.Windows.Forms.Label lblWon;
        private System.Windows.Forms.Label lblBalance;
        private System.Windows.Forms.Button btnReset;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label;
        private System.Windows.Forms.Label cash_txt;
        private System.Windows.Forms.Label evaluated_txt;
        private System.Windows.Forms.Label purchase_txt;
        private System.Windows.Forms.Label profit_txt;
        private System.Windows.Forms.ContextMenuStrip cms;
        private System.Windows.Forms.ToolStripMenuItem cms_chart;
        private System.Windows.Forms.ToolStripMenuItem cmsRequest;
        private System.Windows.Forms.DataGridViewTextBoxColumn CoinName;
        private System.Windows.Forms.DataGridViewTextBoxColumn number;
        private System.Windows.Forms.DataGridViewTextBoxColumn current_price;
        private System.Windows.Forms.DataGridViewTextBoxColumn avg_price;
        private System.Windows.Forms.DataGridViewTextBoxColumn profit_per;
        private System.Windows.Forms.DataGridViewTextBoxColumn profit_won;
        private System.Windows.Forms.DataGridViewTextBoxColumn evaluated_price;
        private System.Windows.Forms.DataGridViewTextBoxColumn purchase_price;
    }
}

