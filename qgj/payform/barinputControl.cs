using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace qgj
{
    public partial class barinputControl : UserControl
    {
        string sBarcode = "";
        bool IsCursor = false;
        public barinputControl()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            InitializeComponent();
        }
        private void barinputControl_Load(object sender, EventArgs e)
        {
            timer.Start();
        }
        public string fnReturnBarcode()
        {
            return sBarcode;
        }
        public void fnInputBarcode(string _insert)
        {
            sBarcode = _insert;
            Refresh();
        }
        private void barinputControl_Click(object sender, EventArgs e)
        {

        }
        private void barinputControl_Paint(object sender, PaintEventArgs e)
        {
            PublicMethods.FrameRoundRectangle(e.ClipRectangle, e.Graphics, 10, Defcolor.MainGrayLineColor);
            PublicMethods.FillRoundRectangle(new Rectangle(e.ClipRectangle.X + 1, e.ClipRectangle.Y + 1, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1), e.Graphics, 10, Defcolor.FontTopSelect);
            if (sBarcode == "")
            {
                sBarcode = "付款码";
            }
            Font myFont = new Font(UserClass.fontName, 10);
            SizeF sizeF = e.Graphics.MeasureString(sBarcode, myFont);
            string strLine = String.Format(sBarcode);
            PointF strPoint = new PointF(10, (e.ClipRectangle.Height - sizeF.Height) / 2);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), strPoint);
            if (IsCursor)
            {
                if (sBarcode == "付款码")
                {
                    e.Graphics.DrawLine(new Pen(Defcolor.MainGrayLineColor, 1), new Point(10, 10), new Point(10, e.ClipRectangle.Height - 10));
                }
                else
                {
                    e.Graphics.DrawLine(new Pen(Defcolor.MainGrayLineColor, 1), new Point(Convert.ToInt32(strPoint.X) + Convert.ToInt32(sizeF.Width) + 2, 10), new Point(Convert.ToInt32(strPoint.X) + Convert.ToInt32(sizeF.Width) + 2, e.ClipRectangle.Height - 10));
                }
            }
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            IsCursor = IsCursor ? false : true;
            this.Refresh();
        }

        private void barinputControl_Enter(object sender, EventArgs e)
        {
            
            keyboardClass.showKeyBoard();
        }

        private void barinputControl_Leave(object sender, EventArgs e)
        {

        }

    }
}
