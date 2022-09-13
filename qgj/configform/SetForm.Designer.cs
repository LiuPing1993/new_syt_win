namespace qgj
{
    partial class SetForm
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
            this.lblBaseSet = new System.Windows.Forms.Label();
            this.lblJointSet = new System.Windows.Forms.Label();
            this.lblPassWordSet = new System.Windows.Forms.Label();
            this.lblOtherSet = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblBaseSet
            // 
            this.lblBaseSet.Location = new System.Drawing.Point(1, 50);
            this.lblBaseSet.Name = "lblBaseSet";
            this.lblBaseSet.Size = new System.Drawing.Size(129, 50);
            this.lblBaseSet.TabIndex = 0;
            this.lblBaseSet.Text = "基本设置";
            this.lblBaseSet.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblBaseSet.Paint += new System.Windows.Forms.PaintEventHandler(this.lblSetTitle_Paint);
            this.lblBaseSet.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lblSetTitle_MouseUp);
            // 
            // lblJointSet
            // 
            this.lblJointSet.Location = new System.Drawing.Point(1, 100);
            this.lblJointSet.Name = "lblJointSet";
            this.lblJointSet.Size = new System.Drawing.Size(129, 50);
            this.lblJointSet.TabIndex = 1;
            this.lblJointSet.Text = "系统对接";
            this.lblJointSet.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblJointSet.Paint += new System.Windows.Forms.PaintEventHandler(this.lblSetTitle_Paint);
            this.lblJointSet.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lblSetTitle_MouseUp);
            // 
            // lblPassWordSet
            // 
            this.lblPassWordSet.Location = new System.Drawing.Point(1, 150);
            this.lblPassWordSet.Name = "lblPassWordSet";
            this.lblPassWordSet.Size = new System.Drawing.Size(129, 50);
            this.lblPassWordSet.TabIndex = 2;
            this.lblPassWordSet.Text = "修改密码";
            this.lblPassWordSet.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblPassWordSet.Paint += new System.Windows.Forms.PaintEventHandler(this.lblSetTitle_Paint);
            this.lblPassWordSet.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lblSetTitle_MouseUp);
            // 
            // lblOtherSet
            // 
            this.lblOtherSet.Location = new System.Drawing.Point(1, 200);
            this.lblOtherSet.Name = "lblOtherSet";
            this.lblOtherSet.Size = new System.Drawing.Size(129, 50);
            this.lblOtherSet.TabIndex = 3;
            this.lblOtherSet.Text = "其他设置";
            this.lblOtherSet.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblOtherSet.Paint += new System.Windows.Forms.PaintEventHandler(this.lblSetTitle_Paint);
            this.lblOtherSet.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lblSetTitle_MouseUp);
            // 
            // SetForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(740, 490);
            this.Controls.Add(this.lblOtherSet);
            this.Controls.Add(this.lblPassWordSet);
            this.Controls.Add(this.lblJointSet);
            this.Controls.Add(this.lblBaseSet);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "SetForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "设置";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SetForm_FormClosing);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.SetForm_Paint);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.SetForm_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblBaseSet;
        private System.Windows.Forms.Label lblJointSet;
        private System.Windows.Forms.Label lblPassWordSet;
        private System.Windows.Forms.Label lblOtherSet;
    }
}