using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace qgj
{
    public partial class mainMemberInfoControl : UserControl
    {

        bool IsSelect = false;
        //bool IsIn = false;

        string sMemberType = "";
        string sMenberDiscount = "";
        string sMenberNum = "";

        discountHttpClass discountHttpC = new discountHttpClass();
        public mainMemberInfoControl(string _type,string _discount,string _num)
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);

            if(_type.Length > 6)
            {
                _type = _type.Substring(0, 5);
                _type += "..";
            }
            if(_num.Length == 12)
            {
                _num = _num.Insert(4, " ");
                _num = _num.Insert(9, " ");
            }
            
            sMemberType = _type;
            sMenberDiscount = _discount;
            if (sMenberDiscount == "")
            {
                sMemberType = "无会员卡或会员折扣";
            }
            sMenberNum = _num;

            InitializeComponent();

            BackColor = Defcolor.MainBackColor;
        }

        public void setSelectStatus(bool _isselect)
        {
            IsSelect = _isselect;
        }
        private void mainUserInfoControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (sMenberDiscount == "10" || sMenberDiscount == "")
            {
                return;
            }
            if (UserClass.orderInfoC.getMoney() == "")
            {
                errorinformationForm _errorF = new errorinformationForm("提示", "请输入金额");
                _errorF.TopMost = true;
                _errorF.StartPosition = FormStartPosition.CenterParent;
                _errorF.ShowDialog();
                ((gatherControl)Parent.Parent).Refresh();
                return;
            }
            IsSelect = IsSelect ? false : true;
            if (IsSelect)
            {
                UserClass.orderInfoC.UseMemberDiscount = true;
            }
            else
            {
                UserClass.orderInfoC.UseMemberDiscount = false;
            }
            string _error = "";
            if (!discountHttpC.discountHttp(ref _error))
            {
                errorinformationForm _errorF = new errorinformationForm("提示", _error);
                _errorF.TopMost = true;
                _errorF.StartPosition = FormStartPosition.CenterParent;
                _errorF.ShowDialog();
                IsSelect = IsSelect ? false : true;
                if (IsSelect)
                {
                    UserClass.orderInfoC.UseMemberDiscount = true;
                }
                else
                {
                    UserClass.orderInfoC.UseMemberDiscount = false;
                }
            }
            //((gatherControl)Parent.Parent).moneyshowC.Refresh();
            ((main)Parent.Parent.Parent).Refresh();
            //Refresh();
        }

        private void mainUserInfoControl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            PublicMethods.FillRoundRectangle(e.ClipRectangle, e.Graphics, 10, Color.White);
            Font _myFont = new Font(UserClass.fontName, 10);
            string _strLine = String.Format(sMemberType);
            PointF _strPoint = new PointF(10, 10);
            e.Graphics.DrawString(_strLine, _myFont, new SolidBrush(Defcolor.FontGrayColor), _strPoint);

            if (sMenberDiscount != "10" && sMenberDiscount != "")
            {
                _myFont = new Font(UserClass.fontName, 24);
                _strLine = String.Format(sMenberDiscount);
                _strPoint = new PointF(85, 6);
                SizeF sizeF = e.Graphics.MeasureString(_strLine, _myFont);
                e.Graphics.DrawString(_strLine, _myFont, new SolidBrush(Defcolor.MainRadColor), _strPoint);

                _myFont = new Font(UserClass.fontName, 8);
                _strLine = String.Format("折");
                _strPoint = new PointF(85 + sizeF.Width - 5, 28);
                e.Graphics.DrawString(_strLine, _myFont, new SolidBrush(Defcolor.FontGrayColor), _strPoint);
            }

            _myFont = new Font(UserClass.fontName, 10);
            _strLine = String.Format(sMenberNum);
            _strPoint = new PointF(10, 50);
            e.Graphics.DrawString(_strLine, _myFont, new SolidBrush(Color.Gray), _strPoint);

            //if (IsIn)
            //{
            //    float[] dashValues = { 2, 3 };
            //    Pen blackPen = new Pen(Defcolor.MainRadColor, 2);
            //    blackPen.DashPattern = dashValues;
            //    e.Graphics.DrawRectangle(blackPen, e.ClipRectangle.X + 1, e.ClipRectangle.Y + 1, e.ClipRectangle.Width - 2, e.ClipRectangle.Height - 2);
            //}

            if (IsSelect)
            {
                Pen _myPen = new Pen(new SolidBrush(Defcolor.MainRadColor), 2);
                e.Graphics.DrawEllipse(_myPen, 124, 50, 20, 20);
                e.Graphics.DrawLine(_myPen, new Point(127, 60), new Point(132, 65));
                e.Graphics.DrawLine(_myPen, new Point(140, 55), new Point(132, 65));
            }
        }
        private void mainMemberInfoControl_MouseEnter(object sender, EventArgs e)
        {
            //IsIn = true;
            //Refresh();
        }

        private void mainMemberInfoControl_MouseLeave(object sender, EventArgs e)
        {
            //IsIn = false;
            //Refresh();
        }

    }
}
