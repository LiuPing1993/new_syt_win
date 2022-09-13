using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace qgj
{
    public partial class storemoneyinputControl : UserControl
    {
        Boolean IsCursor = false;
        string sMoney = "0.00";
        public bool IsAble = false;
        Color colorFont = Defcolor.FontGrayColor;
        public storemoneyinputControl()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            InitializeComponent();
        }
        private void storemoneyinputControl_Paint(object sender, PaintEventArgs e)
        {
            if (IsAble)
            {
                colorFont = Defcolor.FontGrayColor;
            }
            else
            {
                colorFont = Color.Gray;
            }
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            if (IsCursor)
            {
                e.Graphics.DrawLine(new Pen(Color.Gray, 5), new Point(e.ClipRectangle.Width, 0), new Point(e.ClipRectangle.Width, e.ClipRectangle.Height));
            }

            Font moneyFont = new Font(UserClass.fontName, 32);
            SizeF sizeF = e.Graphics.MeasureString(sMoney, moneyFont);
            string strLine = String.Format(sMoney);
            PointF strPoint = new PointF(e.ClipRectangle.Width - 5 - sizeF.Width, (e.ClipRectangle.Height - sizeF.Height) / 2);
            e.Graphics.DrawString(strLine, moneyFont, new SolidBrush(colorFont), strPoint);
        }

        public void storemoneyinputControl_Click(object sender, EventArgs e)
        {
            if (IsAble)
            {
                storeControl sc = (storeControl)Parent;
                sc.fnFocusIsChange("moneyinput");
                timer.Start();
            }
        }

        private void storemoneyinputControl_Load(object sender, EventArgs e)
        {
            BackColor = Defcolor.MainBackColor;
        }

        private void storemoneyinputControl_Enter(object sender, EventArgs e)
        {

        }

        public void storemoneyinputControl_Leave(object sender, EventArgs e)
        {
            IsCursor = false;
            Refresh();
            timer.Stop();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            IsCursor = IsCursor ? false : true;
            Refresh();
        }
        public bool fnGetStatus()
        {
            return timer.Enabled;
        }
        public void fnInputNum(string inputnum)
        {
            if (PublicMethods.moneyCheck(inputnum)&&IsAble)
            {
                sMoney = inputnum;
                Refresh();
            }
        }
    }
}
