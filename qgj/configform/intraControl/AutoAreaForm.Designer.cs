namespace qgj
{
    partial class AutoAreaForm
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
            this.SuspendLayout();
            // 
            // AutoAreaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.ControlBox = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AutoAreaForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "区域选择";
            this.TopMost = true;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.AutoAreaForm_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AutoAreaForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.AutoAreaForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.AutoAreaForm_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}