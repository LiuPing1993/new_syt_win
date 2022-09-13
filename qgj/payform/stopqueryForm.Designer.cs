namespace qgj
{
    partial class stopqueryForm
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
            this.contentLabel = new System.Windows.Forms.Label();
            this.titleLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // contentLabel
            // 
            this.contentLabel.Location = new System.Drawing.Point(41, 72);
            this.contentLabel.Name = "contentLabel";
            this.contentLabel.Size = new System.Drawing.Size(284, 65);
            this.contentLabel.TabIndex = 3;
            this.contentLabel.Text = "http://cn.bing.com/search?q=markman+%e7%a0%b4%e8%a7%a3%e7%89%88&qs=AS&pq=markman&" +
    "sk=AS1&sc=8-7&cvid=C4526583F846429689C3D2895BEEBD17&FORM=QBLH&sp=2";
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Location = new System.Drawing.Point(41, 33);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(41, 12);
            this.titleLabel.TabIndex = 2;
            this.titleLabel.Text = "label1";
            // 
            // stopqueryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(370, 230);
            this.Controls.Add(this.contentLabel);
            this.Controls.Add(this.titleLabel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "stopqueryForm";
            this.ShowInTaskbar = false;
            this.Text = "stopqueryForm";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.stopqueryForm_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label contentLabel;
        private System.Windows.Forms.Label titleLabel;
    }
}