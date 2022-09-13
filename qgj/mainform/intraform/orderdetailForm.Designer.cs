namespace qgj
{
    partial class orderdetailForm
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
            // orderdetailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(735, 490);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "orderdetailForm";
            this.ShowInTaskbar = false;
            this.Text = "orderdetailForm";
            this.Load += new System.EventHandler(this.orderdetailForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.orderdetailForm_Paint);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.orderdetailForm_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}