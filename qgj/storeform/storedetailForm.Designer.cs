namespace qgj
{
    partial class storedetailForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(storedetailForm));
            this.SuspendLayout();
            // 
            // storedetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(735, 490);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "storedetailForm";
            this.ShowInTaskbar = false;
            this.Text = "储值明细";
            this.Load += new System.EventHandler(this.storedetailForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.storedetailForm_Paint);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.storedetailForm_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}