namespace self_syt.comui.keybard
{
    partial class KeyBoardBaseControl
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
            this.panel_list = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // panel_list
            // 
            this.panel_list.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_list.Location = new System.Drawing.Point(0, 0);
            this.panel_list.Margin = new System.Windows.Forms.Padding(0);
            this.panel_list.Name = "panel_list";
            this.panel_list.Size = new System.Drawing.Size(358, 280);
            this.panel_list.TabIndex = 0;
            this.panel_list.SizeChanged += new System.EventHandler(this.panel_list_SizeChanged);
            // 
            // KeyBoardBaseControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel_list);
            this.Name = "KeyBoardBaseControl";
            this.Size = new System.Drawing.Size(358, 280);
            this.Load += new System.EventHandler(this.KeyBoardBaseControl_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel panel_list;

    }
}
