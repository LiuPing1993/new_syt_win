namespace qgj
{
    partial class AutoStepControl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.lbl_click = new System.Windows.Forms.Label();
            this.lbl_del = new System.Windows.Forms.Label();
            this.tbx_time = new System.Windows.Forms.TextBox();
            this.lbl_title = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbl_click
            // 
            this.lbl_click.AutoSize = true;
            this.lbl_click.Location = new System.Drawing.Point(150, 9);
            this.lbl_click.Name = "lbl_click";
            this.lbl_click.Size = new System.Drawing.Size(53, 12);
            this.lbl_click.TabIndex = 19;
            this.lbl_click.Text = "ms后点击";
            // 
            // lbl_del
            // 
            this.lbl_del.AutoSize = true;
            this.lbl_del.Location = new System.Drawing.Point(301, 8);
            this.lbl_del.Name = "lbl_del";
            this.lbl_del.Size = new System.Drawing.Size(29, 12);
            this.lbl_del.TabIndex = 18;
            this.lbl_del.Text = "删除";
            // 
            // tbx_time
            // 
            this.tbx_time.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbx_time.Location = new System.Drawing.Point(95, 8);
            this.tbx_time.Name = "tbx_time";
            this.tbx_time.Size = new System.Drawing.Size(49, 14);
            this.tbx_time.TabIndex = 17;
            this.tbx_time.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.tbx_time.TextChanged += new System.EventHandler(this.tbx_time_TextChanged);
            this.tbx_time.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.tbx_time_KeyPress);
            // 
            // lbl_title
            // 
            this.lbl_title.AutoSize = true;
            this.lbl_title.Location = new System.Drawing.Point(6, 9);
            this.lbl_title.Name = "lbl_title";
            this.lbl_title.Size = new System.Drawing.Size(83, 12);
            this.lbl_title.TabIndex = 16;
            this.lbl_title.Text = "第一步 : 延迟";
            // 
            // AutoStepControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbl_click);
            this.Controls.Add(this.lbl_del);
            this.Controls.Add(this.tbx_time);
            this.Controls.Add(this.lbl_title);
            this.Name = "AutoStepControl";
            this.Size = new System.Drawing.Size(345, 30);
            this.Load += new System.EventHandler(this.AutoStepControl_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.AutoStepControl_Paint);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.AutoStepControl_MouseUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_click;
        public System.Windows.Forms.Label lbl_del;
        public System.Windows.Forms.TextBox tbx_time;
        private System.Windows.Forms.Label lbl_title;
    }
}
