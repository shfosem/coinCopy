
namespace CoinCopy
{
    partial class Request
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
            this.cmbRequest = new System.Windows.Forms.ComboBox();
            this.btnOK = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.grpPrice = new System.Windows.Forms.GroupBox();
            this.rdoMarket = new System.Windows.Forms.RadioButton();
            this.rdoSelect = new System.Windows.Forms.RadioButton();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.grpPrice.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmbRequest
            // 
            this.cmbRequest.FormattingEnabled = true;
            this.cmbRequest.Location = new System.Drawing.Point(12, 71);
            this.cmbRequest.Name = "cmbRequest";
            this.cmbRequest.Size = new System.Drawing.Size(98, 23);
            this.cmbRequest.TabIndex = 0;
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(373, 162);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "확인";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(178, 71);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(72, 25);
            this.textBox1.TabIndex = 2;
            // 
            // grpPrice
            // 
            this.grpPrice.Controls.Add(this.textBox2);
            this.grpPrice.Controls.Add(this.rdoSelect);
            this.grpPrice.Controls.Add(this.rdoMarket);
            this.grpPrice.Location = new System.Drawing.Point(273, 25);
            this.grpPrice.Name = "grpPrice";
            this.grpPrice.Size = new System.Drawing.Size(175, 131);
            this.grpPrice.TabIndex = 3;
            this.grpPrice.TabStop = false;
            this.grpPrice.Text = "가격";
            // 
            // rdoMarket
            // 
            this.rdoMarket.AutoSize = true;
            this.rdoMarket.Location = new System.Drawing.Point(16, 34);
            this.rdoMarket.Name = "rdoMarket";
            this.rdoMarket.Size = new System.Drawing.Size(73, 19);
            this.rdoMarket.TabIndex = 0;
            this.rdoMarket.TabStop = true;
            this.rdoMarket.Text = "시장가";
            this.rdoMarket.UseVisualStyleBackColor = true;
            // 
            // rdoSelect
            // 
            this.rdoSelect.AutoSize = true;
            this.rdoSelect.Location = new System.Drawing.Point(16, 59);
            this.rdoSelect.Name = "rdoSelect";
            this.rdoSelect.Size = new System.Drawing.Size(73, 19);
            this.rdoSelect.TabIndex = 1;
            this.rdoSelect.TabStop = true;
            this.rdoSelect.Text = "지정가";
            this.rdoSelect.UseVisualStyleBackColor = true;
            // 
            // textBox2
            // 
            this.textBox2.Enabled = false;
            this.textBox2.Location = new System.Drawing.Point(16, 84);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(139, 25);
            this.textBox2.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(135, 74);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 15);
            this.label1.TabIndex = 4;
            this.label1.Text = "수량";
            // 
            // Request
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 198);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.grpPrice);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.cmbRequest);
            this.Name = "Request";
            this.Text = "Request";
            this.grpPrice.ResumeLayout(false);
            this.grpPrice.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbRequest;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.GroupBox grpPrice;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.RadioButton rdoSelect;
        private System.Windows.Forms.RadioButton rdoMarket;
        private System.Windows.Forms.Label label1;
    }
}