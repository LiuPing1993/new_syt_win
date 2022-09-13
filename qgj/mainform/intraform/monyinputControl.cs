using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace qgj
{
    public partial class monyinputControl : UserControl
    {
        Boolean IsCursor = false;
        string sMoney = "0.00";

        public monyinputControl()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            InitializeComponent();
        }

        private void monyinputControl_Load(object sender, EventArgs e)
        {
            BackColor = Defcolor.MainBackColor;
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            IsCursor = IsCursor ? false : true;
            Refresh();
        }
        public void monyinputControl_Click(object sender, EventArgs e)
        {
            gatherControl _gatherC = (gatherControl)Parent;
            _gatherC.fnFocusIsChange("moneyinput");
            //if (sender != null) 
            //{
            //    gc.clearMemberCouponSelected();
            //}
            timer.Start();
            //monyinputControl_Enter(this, e);
        }

        private void monyinputControl_Enter(object sender, EventArgs e)
        {
            //this.timer.Start();
        }

        public void monyinputControl_Leave(object sender, EventArgs e)
        {
            IsCursor = false;
            Refresh();
            timer.Stop();
        }
        public bool fnGetStatus()
        {
            return timer.Enabled;
        }

        public void fnInputNum(string _sInput)
        {
            if (PublicMethods.moneyCheck(_sInput))
            {
                sMoney = _sInput;
                Refresh();
            }
        }
        private void monyinputControl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            if (IsCursor)
            {
                e.Graphics.DrawLine(new Pen(Color.Gray, 5), new Point(e.ClipRectangle.Width, 0), new Point(e.ClipRectangle.Width, e.ClipRectangle.Height));
            }
            Font _moneyFont = new Font(UserClass.fontName, 32);
            SizeF _sizeF = e.Graphics.MeasureString(sMoney, _moneyFont);
            string _strLine = String.Format(sMoney);
            PointF _strPoint = new PointF(e.ClipRectangle.Width - 5 - _sizeF.Width, (e.ClipRectangle.Height - _sizeF.Height) / 2);
            e.Graphics.DrawString(_strLine, _moneyFont, new SolidBrush(Defcolor.FontGrayColor), _strPoint);
        }
    }
}
