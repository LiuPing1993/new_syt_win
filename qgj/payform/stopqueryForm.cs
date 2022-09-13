using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace qgj
{
    public partial class stopqueryForm : Form
    {
        confirmcancelControl confirmC = new confirmcancelControl("确定");
        confirmcancelControl cancelC = new confirmcancelControl("取消");
        public stopqueryForm()
        {
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;

            InitializeComponent();

            titleLabel.Font = new Font(UserClass.fontName, 12);
            titleLabel.ForeColor = Defcolor.FontGrayColor;
            titleLabel.Text = "用户未完成支付";

            contentLabel.Font = new Font(UserClass.fontName, 12);
            contentLabel.ForeColor = Defcolor.FontLiteGrayColor;
            contentLabel.Text = "停止等待，可前往订单详情查询支付结果";

            confirmC.Location = new Point(190, 180);
            confirmC.MouseUp += new MouseEventHandler(confirm_MouseUp);
            Controls.Add(confirmC);

            cancelC.Location = new Point(110, 180);
            cancelC.MouseUp += new MouseEventHandler(cancel_MouseUp);
            Controls.Add(cancelC);

            BackColor = Defcolor.MainBackColor;
            StartPosition = FormStartPosition.CenterParent;
        }
        private void confirm_MouseUp(object sender, MouseEventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.OK;
            Dispose();
        }
        private void cancel_MouseUp(object sender, MouseEventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.No;
            Dispose();
        }
        private void stopqueryForm_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid
            );
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            if (keyData == Keys.Enter)
            {
                confirm_MouseUp(null, null);
                return true;
            }
            else if (keyData == Keys.Escape)
            {
                cancel_MouseUp(null, null);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
