using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace qgj
{
    public partial class cashiermoneyinputControl : UserControl
    {
        string sCashierMoney = "0.00";
        bool IsCursor = false;
        public cashiermoneyinputControl()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            InitializeComponent();
        }
        public void inputCashiermoney(string _insert)
        {
            sCashierMoney = _insert;
            this.Refresh();
        }
        private void cashiermoneyinputControl_Load(object sender, EventArgs e)
        {

        }
        private void cashiermoneyinputControl_Click(object sender, EventArgs e)
        {

        }

        private void cashiermoneyinputControl_Paint(object sender, PaintEventArgs e)
        {
            PublicMethods.FrameRoundRectangle(e.ClipRectangle, e.Graphics, 10, Defcolor.MainGrayLineColor);
            PublicMethods.FillRoundRectangle(new Rectangle(e.ClipRectangle.X + 1, e.ClipRectangle.Y + 1, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1), e.Graphics, 10, Defcolor.FontTopSelect);
            if (sCashierMoney == "0.00")
            {
                timer.Stop();
                IsCursor = false;
                sCashierMoney = "收款金额";
            }
            else if (!timer.Enabled)
            {
                timer.Start();
            }
            Font myFont = new Font(UserClass.fontName, 10);
            SizeF sizeF = e.Graphics.MeasureString(sCashierMoney, myFont);
            string strLine = String.Format(sCashierMoney);
            PointF strPoint = new PointF(10, (e.ClipRectangle.Height - sizeF.Height) / 2);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), strPoint);
            if (IsCursor)
            {
                e.Graphics.DrawLine(new Pen(Defcolor.MainGrayLineColor, 1), new Point(Convert.ToInt32(strPoint.X) + Convert.ToInt32(sizeF.Width) + 2, 10), new Point(Convert.ToInt32(strPoint.X) + Convert.ToInt32(sizeF.Width) + 2, e.ClipRectangle.Height - 10));
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            IsCursor = IsCursor ? false : true;
            this.Refresh();
        }

        private void cashiermoneyinputControl_Enter(object sender, EventArgs e)
        {
            keyboardClass.showKeyBoard();
        }
    }
}
