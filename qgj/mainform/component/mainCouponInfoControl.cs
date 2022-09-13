using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace qgj
{
    public partial class mainCouponInfoControl : UserControl
    {

        bool IsSelect = false;
        //bool IsIn = false;

        couponType type = couponType.cashcoupon;
        string sLeftTop = "";
        string sLeftBottom = "";
        string sCouponTitle = "";
        string sCouponInfo = "";
        string sCouponDate = "";

        discountHttpClass discountHttpC = new discountHttpClass();

        Color colorCoupon = new Color();

        public mainCouponInfoControl(string _lefttop, string _leftbottom, string _title, string _info, string _date, string _color, couponType _type)
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);

            sLeftTop = _lefttop;
            sLeftBottom = _leftbottom;
            sCouponTitle = _title;
            sCouponInfo = _info;
            sCouponDate = _date;
            colorCoupon = ColorTranslator.FromHtml(_color);
            type = _type;

            InitializeComponent();

            BackColor = Color.White;
        }

        public void fnSetSelectStatus(bool _isselect)
        {
            IsSelect = _isselect;
        }
        private void mainCouponInfoControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (UserClass.orderInfoC.getMoney() == "" && type != couponType.giftcoupon)
            {
                errorinformationForm errorF = new errorinformationForm("提示", "请输入金额");
                errorF.TopMost = true;
                errorF.StartPosition = FormStartPosition.CenterParent;
                errorF.ShowDialog();
                ((gatherControl)Parent.Parent).Refresh();
                return;
            }
            if (type == couponType.useless)
            {
                return;
            }
            IsSelect = IsSelect ? false : true;
            if(IsSelect)
            {
                UserClass.orderInfoC.coupon += this.Name + ",";
            }
            else
            {
                UserClass.orderInfoC.coupon = UserClass.orderInfoC.coupon.Replace(this.Name + ",", "");
            }
            string _error = "";
            if (!discountHttpC.discountHttp(ref _error))
            {
                errorinformationForm errorF = new errorinformationForm("提示", _error);
                errorF.TopMost = true;
                errorF.StartPosition = FormStartPosition.CenterParent;
                errorF.ShowDialog();

                IsSelect = IsSelect ? false : true;
                if (IsSelect)
                {
                    UserClass.orderInfoC.coupon += this.Name + ",";
                }
                else
                {
                    UserClass.orderInfoC.coupon = UserClass.orderInfoC.coupon.Replace(this.Name + ",", "");
                }
            }
            //((gatherControl)Parent.Parent).moneyshowC.Refresh();
            ((main)Parent.Parent.Parent).Refresh();
            //Refresh();
        }

        private void mainCouponInfoControl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.FillRectangle(new SolidBrush(colorCoupon), new Rectangle(0, 0, 100, 75));
            e.Graphics.FillPie(new SolidBrush(Defcolor.MainBackColor), new Rectangle(94, -6, 12, 12), 0, 360);
            e.Graphics.FillPie(new SolidBrush(Defcolor.MainBackColor), new Rectangle(94, 69, 12, 12), 0, 360);

            #region 券左上部分绘制
            int _fontsize = 24;
            if (sLeftTop.Length > 4)
            {
                _fontsize = 18;
            }
            Font _lefttopFont1 = new Font(UserClass.fontName, _fontsize);
            Font _lefttopFont2 = new Font(UserClass.fontName, 10);
            string _lefttopLine1 = String.Format(sLeftTop);
            string _lefttopLine2 = String.Format("");

            float _y = 8;
            if(type == couponType.cashcoupon)
            {
                _lefttopLine2 = String.Format("元");
            }
            else if(type == couponType.discountcoupon)
            {
                _lefttopLine2 = String.Format("折");
            }
            else if(type == couponType.giftcoupon)
            {
                _lefttopFont1 = new Font(UserClass.fontName, 16);
                _y = 10;
            }
            SizeF _lefttopsizeF1 = e.Graphics.MeasureString(_lefttopLine1, _lefttopFont1);
            SizeF _lefttopsizeF2 = e.Graphics.MeasureString(_lefttopLine2, _lefttopFont2);

            float _x1 = (100 - _lefttopsizeF1.Width - _lefttopsizeF2.Width) / 2;
            float _x2 = _x1 + _lefttopsizeF1.Width;
            PointF _lefttopPoint1 = new PointF(_x1, _y);
            PointF _lefttopPoint2 = new PointF(_x2 - 5, 28);
            e.Graphics.DrawString(_lefttopLine1, _lefttopFont1, new SolidBrush(Color.White), _lefttopPoint1);
            e.Graphics.DrawString(_lefttopLine2, _lefttopFont2, new SolidBrush(Color.White), _lefttopPoint2);
            #endregion

            Font _myFont = new Font(UserClass.fontName, 9);
            string _strLine = String.Format(sLeftBottom);
            SizeF _sizeF = e.Graphics.MeasureString(_strLine, _myFont);
            PointF _pointF = new PointF((100 - _sizeF.Width) / 2, 50);
            e.Graphics.DrawString(_strLine, _myFont, new SolidBrush(Color.White), _pointF);

            _myFont = new Font(UserClass.fontName, 10);
            _strLine = String.Format(sCouponInfo);
            _pointF = new PointF(115, 32);
            e.Graphics.DrawString(_strLine, _myFont, new SolidBrush(Defcolor.MainGrayLineColor), _pointF);

            _strLine = String.Format(sCouponDate);
            _pointF = new PointF(115, 50);
            e.Graphics.DrawString(_strLine, _myFont, new SolidBrush(Defcolor.MainGrayLineColor), _pointF);

            _myFont = new Font(UserClass.fontName, 12);
            _strLine = String.Format(sCouponTitle);
            _pointF = new PointF(115, 8);
            e.Graphics.DrawString(_strLine, _myFont, new SolidBrush(Color.Gray), _pointF);

            //if (IsIn)
            //{
            //    float[] dashValues = { 2, 3 };
            //    Pen blackPen = new Pen(Defcolor.MainRadColor, 2);
            //    blackPen.DashPattern = dashValues;
            //    e.Graphics.DrawRectangle(blackPen, e.ClipRectangle.X + 1, e.ClipRectangle.Y + 1, e.ClipRectangle.Width - 2, e.ClipRectangle.Height - 2);
            //}
            if(IsSelect)
            {
                Pen _myPen = new Pen(new SolidBrush(Defcolor.MainRadColor), 2);
                e.Graphics.DrawEllipse(_myPen,290, 45, 20, 20);
                e.Graphics.DrawLine(_myPen, new Point(293, 55), new Point(298, 60));
                e.Graphics.DrawLine(_myPen, new Point(306, 50), new Point(298, 60));
            }
        }

        private void mainCouponInfoControl_MouseEnter(object sender, EventArgs e)
        {
            //Location = new Point(Location.X - 2, Location.Y - 2);
            //IsIn = true;
            //Refresh();
        }

        private void mainCouponInfoControl_MouseLeave(object sender, EventArgs e)
        {
            //Location = new Point(Location.X + 2, Location.Y + 2);
            //IsIn = false;
            //Refresh();
        }
    }
}
