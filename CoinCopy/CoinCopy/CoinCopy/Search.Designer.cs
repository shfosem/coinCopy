
namespace CoinCopy
{
    partial class Search
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
            this.txtCoinName = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dgvCoin = new System.Windows.Forms.DataGridView();
            this.cms = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.정보ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.매수ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCoin)).BeginInit();
            this.cms.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtCoinName
            // 
            this.txtCoinName.Location = new System.Drawing.Point(12, 33);
            this.txtCoinName.Name = "txtCoinName";
            this.txtCoinName.Size = new System.Drawing.Size(382, 25);
            this.txtCoinName.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(400, 33);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(80, 26);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "검색";
            this.btnSearch.UseVisualStyleBackColor = true;
            // 
            // dgvCoin
            // 
            this.dgvCoin.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvCoin.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCoin.ContextMenuStrip = this.cms;
            this.dgvCoin.Location = new System.Drawing.Point(12, 83);
            this.dgvCoin.MultiSelect = false;
            this.dgvCoin.Name = "dgvCoin";
            this.dgvCoin.RowHeadersVisible = false;
            this.dgvCoin.RowHeadersWidth = 51;
            this.dgvCoin.RowTemplate.Height = 27;
            this.dgvCoin.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvCoin.Size = new System.Drawing.Size(468, 239);
            this.dgvCoin.TabIndex = 2;
            this.dgvCoin.CellMouseDoubleClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgvCoin_CellMouseDoubleClick);
            // 
            // cms
            // 
            this.cms.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.cms.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.정보ToolStripMenuItem,
            this.매수ToolStripMenuItem});
            this.cms.Name = "cms";
            this.cms.Size = new System.Drawing.Size(109, 52);
            // 
            // 정보ToolStripMenuItem
            // 
            this.정보ToolStripMenuItem.Name = "정보ToolStripMenuItem";
            this.정보ToolStripMenuItem.Size = new System.Drawing.Size(210, 24);
            this.정보ToolStripMenuItem.Text = "정보";
            this.정보ToolStripMenuItem.Click += new System.EventHandler(this.정보ToolStripMenuItem_Click);
            // 
            // 매수ToolStripMenuItem
            // 
            this.매수ToolStripMenuItem.Name = "매수ToolStripMenuItem";
            this.매수ToolStripMenuItem.Size = new System.Drawing.Size(108, 24);
            this.매수ToolStripMenuItem.Text = "매수";
            // 
            // Search
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(493, 334);
            this.Controls.Add(this.dgvCoin);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtCoinName);
            this.Name = "Search";
            this.Text = "Search";
            this.Load += new System.EventHandler(this.Search_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCoin)).EndInit();
            this.cms.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCoinName;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dgvCoin;
        private System.Windows.Forms.ContextMenuStrip cms;
        private System.Windows.Forms.ToolStripMenuItem 정보ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 매수ToolStripMenuItem;
    }
}