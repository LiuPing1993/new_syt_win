using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace qgj
{
    public partial class notdiscountControl : UserControl
    {
        Boolean IsCursor = false;
        string sNotDiscountMoney = "输入不可打折金额";
        public notdiscountControl()
        {
            InitializeComponent();
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
        }

        private void notdiscountControl_Load(object sender, EventArgs e)
        {
            BackColor = Defcolor.MainBackColor;
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            IsCursor = IsCursor ? false : true;
            Refresh();
        }

        private void notdiscountControl_Click(object sender, EventArgs e)
        {
            gatherControl _gatherC = (gatherControl)Parent;
            _gatherC.fnFocusIsChange("notdiscount");
            //if (sender != null)
            //{
            //    gc.clearMemberCouponSelected();
            //}
            timer.Start();
        }

        private void notdiscountControl_Enter(object sender, EventArgs e)
        {

        }

        public void notdiscountControl_Leave(object sender, EventArgs e)
        {
            IsCursor = false;
            Refresh();
            timer.Stop();
        }
        public bool getStatus()
        {
            return timer.Enabled;
        }
        public void inputdiscountNum(string _Input)
        {
            if (PublicMethods.moneyCheck(_Input))
            {
                sNotDiscountMoney = _Input;
                this.Refresh();
            }
        }

        private void notdiscountControl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            if (IsCursor)
            {
                e.Graphics.DrawLine(new Pen(Defcolor.MainGrayLineColor, 5), new Point(e.ClipRectangle.Width, 0), new Point(e.ClipRectangle.Width, e.ClipRectangle.Height));
            }
            Font _moneyFont;
            if (sNotDiscountMoney == "0.00" || sNotDiscountMoney == "输入不可打折金额")
            {
                sNotDiscountMoney = "输入不可打折金额";
                _moneyFont = new Font(UserClass.fontName, 14);
            }
            else
            {
                _moneyFont = new Font(UserClass.fontName, 18);
            }
            SizeF _sizeF = e.Graphics.MeasureString(sNotDiscountMoney, _moneyFont);
            string _strLine = String.Format(sNotDiscountMoney);
            PointF _strPoint = new PointF(e.ClipRectangle.Width - 5 - _sizeF.Width, (e.ClipRectangle.Height - _sizeF.Height) / 2);
            e.Graphics.DrawString(_strLine, _moneyFont, new SolidBrush(Defcolor.FontGrayColor), _strPoint);
        }
    }
}