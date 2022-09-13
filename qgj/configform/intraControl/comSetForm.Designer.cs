namespace qgj.configform.intraControl
{
    partial class comSetForm
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
            this.name_lbl = new System.Windows.Forms.Label();
            this.name_cbx = new System.Windows.Forms.ComboBox();
            this.stop_lbl = new System.Windows.Forms.Label();
            this.parity_lbl = new System.Windows.Forms.Label();
            this.rate_lbl = new System.Windows.Forms.Label();
            this.stop_cbx = new System.Windows.Forms.ComboBox();
            this.parity_cbx = new System.Windows.Forms.ComboBox();
            this.rate_cbx = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // name_lbl
            // 
            this.name_lbl.AutoSize = true;
            this.name_lbl.Location = new System.Drawing.Point(84, 38);
            this.name_lbl.Name = "name_lbl";
            this.name_lbl.Size = new System.Drawing.Size(41, 12);
            this.name_lbl.TabIndex = 19;
            this.name_lbl.Text = "串口名";
            // 
            // name_cbx
            // 
            this.name_cbx.FormattingEnabled = true;
            this.name_cbx.Items.AddRange(new object[] {
            "COM1",
            "COM2",
            "COM3",
            "COM4"});
            this.name_cbx.Location = new System.Drawing.Point(131, 35);
            this.name_cbx.Name = "name_cbx";
            this.name_cbx.Size = new System.Drawing.Size(121, 20);
            this.name_cbx.TabIndex = 18;
            this.name_cbx.SelectedIndexChanged += new System.EventHandler(this.name_cbx_SelectedIndexChanged);
            // 
            // stop_lbl
            // 
            this.stop_lbl.AutoSize = true;
            this.stop_lbl.Location = new System.Drawing.Point(84, 140);
            this.stop_lbl.Name = "stop_lbl";
            this.stop_lbl.Size = new System.Drawing.Size(41, 12);
            this.stop_lbl.TabIndex = 16;
            this.stop_lbl.Text = "停止位";
            // 
            // parity_lbl
            // 
            this.parity_lbl.AutoSize = true;
            this.parity_lbl.Location = new System.Drawing.Point(84, 107);
            this.parity_lbl.Name = "parity_lbl";
            this.parity_lbl.Size = new System.Drawing.Size(41, 12);
            this.parity_lbl.TabIndex = 15;
            this.parity_lbl.Text = "校验位";
            // 
            // rate_lbl
            // 
            this.rate_lbl.AutoSize = true;
            this.rate_lbl.Location = new System.Drawing.Point(84, 70);
            this.rate_lbl.Name = "rate_lbl";
            this.rate_lbl.Size = new System.Drawing.Size(41, 12);
            this.rate_lbl.TabIndex = 14;
            this.rate_lbl.Text = "波特率";
            // 
            // stop_cbx
            // 
            this.stop_cbx.FormattingEnabled = true;
            this.stop_cbx.Items.AddRange(new object[] {
            "1",
            "1.5",
            "2",
            "无"});
            this.stop_cbx.Location = new System.Drawing.Point(131, 137);
            this.stop_cbx.Name = "stop_cbx";
            this.stop_cbx.Size = new System.Drawing.Size(121, 20);
            this.stop_cbx.TabIndex = 12;
            this.stop_cbx.SelectedIndexChanged += new System.EventHandler(this.stop_cbx_SelectedIndexChanged);
            // 
            // parity_cbx
            // 
            this.parity_cbx.FormattingEnabled = true;
            this.parity_cbx.Items.AddRange(new object[] {
            "奇校验",
            "偶校验",
            "无"});
            this.parity_cbx.Location = new System.Drawing.Point(131, 104);
            this.parity_cbx.Name = "parity_cbx";
            this.parity_cbx.Size = new System.Drawing.Size(121, 20);
            this.parity_cbx.TabIndex = 11;
            this.parity_cbx.SelectedIndexChanged += new System.EventHandler(this.parity_cbx_SelectedIndexChanged);
            // 
            // rate_cbx
            // 
            this.rate_cbx.FormattingEnabled = true;
            this.rate_cbx.Items.AddRange(new object[] {
            "2400",
            "4800",
            "9600",
            "19200",
            "38400",
            "43000",
            "56000",
            "115200"});
            this.rate_cbx.Location = new System.Drawing.Point(131, 67);
            this.rate_cbx.Name = "rate_cbx";
            this.rate_cbx.Size = new System.Drawing.Size(121, 20);
            this.rate_cbx.TabIndex = 10;
            this.rate_cbx.SelectedIndexChanged += new System.EventHandler(this.rate_cbx_SelectedIndexChanged);
            // 
            // comSetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(350, 250);
            this.Controls.Add(this.name_lbl);
            this.Controls.Add(this.name_cbx);
            this.Controls.Add(this.stop_lbl);
            this.Controls.Add(this.parity_lbl);
            this.Controls.Add(this.rate_lbl);
            this.Controls.Add(this.stop_cbx);
            this.Controls.Add(this.parity_cbx);
            this.Controls.Add(this.rate_cbx);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "comSetForm";
            this.Text = "comSetForm";
            this.Load += new System.EventHandler(this.comSetForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.comSetForm_Paint);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label name_lbl;
        private System.Windows.Forms.ComboBox name_cbx;
        private System.Windows.Forms.Label stop_lbl;
        private System.Windows.Forms.Label parity_lbl;
        private System.Windows.Forms.Label rate_lbl;
        private System.Windows.Forms.ComboBox stop_cbx;
        private System.Windows.Forms.ComboBox parity_cbx;
        private System.Windows.Forms.ComboBox rate_cbx;
    }
}