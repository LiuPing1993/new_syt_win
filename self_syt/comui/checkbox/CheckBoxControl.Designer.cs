namespace self_syt.comui.checkbox
{
    partial class CheckBoxControl
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel_right = new System.Windows.Forms.Panel();
            this.panel_left = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel_right);
            this.panel1.Controls.Add(this.panel_left);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(125, 26);
            this.panel1.TabIndex = 0;
            // 
            // panel_right
            // 
            this.panel_right.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_right.Location = new System.Drawing.Point(38, 0);
            this.panel_right.Name = "panel_right";
            this.panel_right.Size = new System.Drawing.Size(87, 26);
            this.panel_right.TabIndex = 1;
            this.panel_right.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_right_Paint);
            this.panel_right.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel_right_MouseUp);
            // 
            // panel_left
            // 
            this.panel_left.BackColor = System.Drawing.SystemColors.ActiveBorder;
            this.panel_left.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel_left.Location = new System.Drawing.Point(0, 0);
            this.panel_left.Name = "panel_left";
            this.panel_left.Size = new System.Drawing.Size(38, 26);
            this.panel_left.TabIndex = 0;
            this.panel_left.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_left_Paint);
            this.panel_left.MouseUp += new System.Windows.Forms.MouseEventHandler(this.panel_left_MouseUp);
            // 
            // CheckBoxControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "CheckBoxControl";
            this.Size = new System.Drawing.Size(125, 26);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel_right;
        private System.Windows.Forms.Panel panel_left;
    }
}
