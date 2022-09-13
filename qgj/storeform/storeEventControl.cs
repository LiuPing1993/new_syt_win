using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace qgj
{
    public partial class storeEventControl : UserControl
    {
        bool IsSelect = false;
        string sId = "";
        string sMoney = "";
        string sName = "";
        string sInfo = "";
        string sDate = "";

        public storeEventControl(string _id, string _money, string _name, string _info, string _date)
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);

            sId = _id;
            sMoney = _money;
            sName = _name;
            sInfo = _info;
            if (sId != "0")
            {
                sDate = _date;
            }

            InitializeComponent();

            BackColor = Color.White;
        }
        public void fnSetSelectStatus(bool _isselect)
        {
            IsSelect = _isselect;
            Refresh();
        }
        private void storeEventControl_MouseUp(object sender, MouseEventArgs e)
        {
            //没有选择会员
            if (UserClass.storeInfoC.code == "")
            {
                errorinformationForm _errorF = new errorinformationForm("提示", "请选择会员");
                _errorF.TopMost = true;
                _errorF.StartPosition = FormStartPosition.CenterParent;
                _errorF.ShowDialog();
                ((storeControl)Parent.Parent).Refresh();
                return;
            }
            //在选择了一种活动后选择其他活动，需要清空除会员以外的信息
            if (UserClass.storeInfoC.storetype != "" && UserClass.storeInfoC.storetype != sId)
            {
                ((storeControl)Parent.Parent).fnClearStoreSelect();
                UserClass.storeInfoC.storetype = "";
                UserClass.storeInfoC.setstoremoney("");
                ((storeControl)Parent.Parent).sAllMoney = "";
                ((storeControl)Parent.Parent).storemoneyinputC.fnInputNum("0.00");
                ((storeControl)Parent.Parent).storemoneyinputC.IsAble = false;
            }
            IsSelect = IsSelect ? false : true;
            if (IsSelect)
            {
                if (sId == "0")
                {
                    ((storeControl)Parent.Parent).storemoneyinputC.IsAble = true;
                    ((storeControl)Parent.Parent).fnSetFocus();
                    UserClass.storeInfoC.storetype = sId;
                    UserClass.storeInfoC.setstoremoney("");
                }
                else
                {
                    UserClass.storeInfoC.storetype = sId;
                    UserClass.storeInfoC.setstoremoney(sMoney);
                }
            }
            else
            {
                UserClass.storeInfoC.storetype = "";
                UserClass.storeInfoC.setstoremoney("");
                ((storeControl)Parent.Parent).sAllMoney = "";
                ((storeControl)Parent.Parent).storemoneyinputC.fnInputNum("0.00");
                ((storeControl)Parent.Parent).storemoneyinputC.IsAble = false;
            }
            ((storeControl)Parent.Parent).storeshowC.Refresh();
            ((storeControl)Parent.Parent).storemoneyinputC.Refresh();
            Refresh();
        }

        private void storeEventControl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Font myFont = new Font(UserClass.fontName, 9);
            string strLine = String.Format(sName);
            PointF pointF = new PointF(10, 10);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), pointF);

            strLine = String.Format(sInfo);
            pointF = new PointF(10, 30);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Color.Gray), pointF);

            strLine = String.Format(sDate);
            pointF = new PointF(10, 50);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Color.Gray), pointF);

            if (IsSelect)
            {
                Pen myPen = new Pen(new SolidBrush(Defcolor.MainRadColor), 2);
                e.Graphics.DrawEllipse(myPen, 290, 45, 20, 20);
                e.Graphics.DrawLine(myPen, new Point(293, 55), new Point(298, 60));
                e.Graphics.DrawLine(myPen, new Point(306, 50), new Point(298, 60));
            }
        }
    }
}
