namespace self_syt.comui
{
    partial class ErrorForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel_top = new System.Windows.Forms.Panel();
            this.panel_mid = new System.Windows.Forms.Panel();
            this.panel_bottom = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel_bottom);
            this.panel1.Controls.Add(this.panel_mid);
            this.panel1.Controls.Add(this.panel_top);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(324, 224);
            this.panel1.TabIndex = 0;
            // 
            // panel_top
            // 
            this.panel_top.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_top.Location = new System.Drawing.Point(0, 0);
            this.panel_top.Name = "panel_top";
            this.panel_top.Size = new System.Drawing.Size(324, 56);
            this.panel_top.TabIndex = 0;
            this.panel_top.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_top_Paint);
            // 
            // panel_mid
            // 
            this.panel_mid.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_mid.Location = new System.Drawing.Point(0, 56);
            this.panel_mid.Name = "panel_mid";
            this.panel_mid.Size = new System.Drawing.Size(324, 113);
            this.panel_mid.TabIndex = 1;
            this.panel_mid.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_mid_Paint);
            // 
            // panel_bottom
            // 
            this.panel_bottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_bottom.Location = new System.Drawing.Point(0, 169);
            this.panel_bottom.Name = "panel_bottom";
            this.panel_bottom.Size = new System.Drawing.Size(324, 55);
            this.panel_bottom.TabIndex = 2;
            // 
            // ErrorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 224);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ErrorForm";
            this.Text = "ErrorForm";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel_bottom;
        private System.Windows.Forms.Panel panel_mid;
        private System.Windows.Forms.Panel panel_top;
    }
}