using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace qgj.mainform.intraform
{
    public partial class barPayControl : UserControl
    {
        public string code = "";
        string sPayWayTitle = "确认收款";
        Color colorString = Defcolor.FontGrayColor;

        gatherControl gatherC;

        public barPayControl(gatherControl g)
        {
            gatherC = g;
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);

            InitializeComponent();

            this.BackColor = Color.FromArgb(248, 230, 230);//Defcolor.BackColor;
        }

        private void barPayControl_Load(object sender, EventArgs e) { }

        private void barPayControl_MouseEnter(object sender, EventArgs e) { }

        private void barPayControl_MouseLeave(object sender, EventArgs e) { }

        private void barPayControl_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle,
                Defcolor.MainRadColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainRadColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainRadColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainRadColor, 1, ButtonBorderStyle.Solid
            );
            if (code != "")
            {
                string tip = string.Format("获取到，{0}:{1}", PublicMethods.codeChannel(code), code);
                Font numPanelFont = new Font(UserClass.fontName, 9);
                SizeF sizeF = e.Graphics.MeasureString(tip, numPanelFont);
                string strLine = String.Format(tip);
                PointF strPoint = new PointF((e.ClipRectangle.Width - sizeF.Width) / 2, (e.ClipRectangle.Height - sizeF.Height) / 2 - 14);
                e.Graphics.DrawString(strLine, numPanelFont, new SolidBrush(Defcolor.FontLiteGrayColor), strPoint);

                numPanelFont = new Font(UserClass.fontName, 14);
                sizeF = e.Graphics.MeasureString(sPayWayTitle, numPanelFont);
                strLine = String.Format(sPayWayTitle);
                strPoint = new PointF((e.ClipRectangle.Width - sizeF.Width) / 2, (e.ClipRectangle.Height - sizeF.Height) / 2 + 12);
                e.Graphics.DrawString(strLine, numPanelFont, new SolidBrush(Defcolor.MainRadColor), strPoint);
            }
            else
            {
                Font numPanelFont = new Font(UserClass.fontName, 14);
                SizeF sizeF = e.Graphics.MeasureString(sPayWayTitle, numPanelFont);
                string strLine = String.Format(sPayWayTitle);
                PointF strPoint = new PointF((e.ClipRectangle.Width - sizeF.Width) / 2, (e.ClipRectangle.Height - sizeF.Height) / 2);
                e.Graphics.DrawString(strLine, numPanelFont, new SolidBrush(Defcolor.MainRadColor), strPoint);
            }
        }
        private void barPayControl_MouseDown(object sender, MouseEventArgs e) 
        {
            BackColor = Defcolor.BackColor;
        }
        public void barPayControl_MouseUp(object sender, MouseEventArgs e)
        {
            BackColor = Color.FromArgb(248, 230, 230);
            gatherC.paywayC[0].fnFastPay(code);
        }
    }
}
