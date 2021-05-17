
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
            this.txtCoinName = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.CoinNumber = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CoinName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.CoinPrice = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtCoinName
            // 
            this.txtCoinName.Location = new System.Drawing.Point(12, 33);
            this.txtCoinName.Name = "txtCoinName";
            this.txtCoinName.Size = new System.Drawing.Size(311, 25);
            this.txtCoinName.TabIndex = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(346, 32);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(80, 26);
            this.btnSearch.TabIndex = 1;
            this.btnSearch.Text = "검색";
            this.btnSearch.UseVisualStyleBackColor = true;
            // 
            // dataGridView1
            // 
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.CoinNumber,
            this.CoinName,
            this.CoinPrice});
            this.dataGridView1.Location = new System.Drawing.Point(12, 83);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersWidth = 51;
            this.dataGridView1.RowTemplate.Height = 27;
            this.dataGridView1.Size = new System.Drawing.Size(427, 150);
            this.dataGridView1.TabIndex = 2;
            // 
            // CoinNumber
            // 
            this.CoinNumber.HeaderText = "종목번호";
            this.CoinNumber.MinimumWidth = 6;
            this.CoinNumber.Name = "CoinNumber";
            this.CoinNumber.Width = 125;
            // 
            // CoinName
            // 
            this.CoinName.HeaderText = "종목명";
            this.CoinName.MinimumWidth = 6;
            this.CoinName.Name = "CoinName";
            this.CoinName.Width = 125;
            // 
            // CoinPrice
            // 
            this.CoinPrice.HeaderText = "가격";
            this.CoinPrice.MinimumWidth = 6;
            this.CoinPrice.Name = "CoinPrice";
            this.CoinPrice.Width = 125;
            // 
            // Search
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(451, 245);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtCoinName);
            this.Name = "Search";
            this.Text = "Search";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtCoinName;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn CoinNumber;
        private System.Windows.Forms.DataGridViewTextBoxColumn CoinName;
        private System.Windows.Forms.DataGridViewTextBoxColumn CoinPrice;
    }
}