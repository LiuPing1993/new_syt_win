using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace qgj
{
    public partial class storeMemberInfoControl : UserControl
    {
        string sMemberType = "";
        string sMemberNum = "";
        string sMemberStore = "";
        string sImageUrl = "";

        public storeMemberInfoControl(string _type, string _num, string _store, string _imgurl)
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            if (_type.Length > 6)
            {
                _type = _type.Substring(0, 5);
                _type += "..";
            }
            if (_num.Length == 12)
            {
                _num = _num.Insert(4, " ");
                _num = _num.Insert(9, " ");
            }
            sMemberType = _type;
            sMemberNum = _num;
            sMemberStore = _store;
            sImageUrl = _imgurl;

            InitializeComponent();

            BackColor = Color.White;

        }

        private void storeMemberInfoControl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            Font myFont = new Font(UserClass.fontName, 10);
            string strLine = String.Format(sMemberType);
            PointF strPoint = new PointF(66, 14);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), strPoint);

            strLine = String.Format(sMemberNum);
            strPoint = new PointF(136, 13);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Color.Gray), strPoint);

            strLine = String.Format("储值余额");
            strPoint = new PointF(66, 40);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), strPoint);

            strLine = String.Format(sMemberStore);
            strPoint = new PointF(136, 39);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontBlueColor), strPoint);
        }

        private void storeMemberInfoControl_Load(object sender, EventArgs e)
        {
            try
            {
                string picstr = sImageUrl.Replace("\\", "");
                avatarPic.SizeMode = PictureBoxSizeMode.Zoom;
                avatarPic.Image = Image.FromStream(System.Net.WebRequest.Create(picstr).GetResponse().GetResponseStream());
            }
            catch { }
        }
    }
}
