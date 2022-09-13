using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace qgj
{
    public partial class memberDetailForm : Form
    {
        string sTitle = "会员详情";
        string sUsername = "";
        string sLevel = "";
        string sMembercode = "";
        string sPhoneNum = "";
        string sBalance = "";
        string sBonus = "";

        string sBirthday = "";
        string sID = "";
        string sEmail = "";
        string sAddress = "";
        string sEducational = "";
        string sOccupation = "";
        string sIndustry = "";
        string sIncome = "";
        string sInterest = "";

        membersearchSuccess memberinfoS;

        PictureBox avatarPic = new PictureBox();

        confirmcancelControl addbalanceC = new confirmcancelControl("储值码兑换");

        bool isError = false;
        public memberDetailForm(membersearchSuccess _data)
        {
            InitializeComponent();
            BackColor = Defcolor.MainBackColor;

            avatarPic.SetBounds(18, 70, 100, 100);
            avatarPic.BackColor = Color.White;
            avatarPic.SizeMode = PictureBoxSizeMode.Zoom;
            Controls.Add(avatarPic);

            addbalanceC.Location = new Point(640, 70);
            addbalanceC.MouseUp += new MouseEventHandler(addbalance_MouseUp);
            Controls.Add(addbalanceC);

            memberinfoS = _data;
        }

        private void memberDetailForm_Load(object sender, EventArgs e)
        {
            fnSetMemberDetailMouse();
            try
            {
                sUsername = memberinfoS.data.user_name;
                sLevel = memberinfoS.data.member_level;
                sMembercode = memberinfoS.data.member_code;
                sPhoneNum = memberinfoS.data.account;
                sBalance = memberinfoS.data.balance;
                sBonus = memberinfoS.data.bonus;

                sBirthday = memberinfoS.data.birthday;
                sID = memberinfoS.data.ID_number;
                sEmail = memberinfoS.data.email;
                sAddress = memberinfoS.data.address;
                sEducational = memberinfoS.data.educational;
                sOccupation = memberinfoS.data.occupation;
                sIndustry = memberinfoS.data.industry;
                sIncome = memberinfoS.data.income;
                sInterest = memberinfoS.data.interest;
            }
            catch
            {
                isError = true;
            }
            try
            {
                string picstr = memberinfoS.data.avatar.Replace("\\", "");
                avatarPic.SizeMode = PictureBoxSizeMode.Zoom;
                avatarPic.Image = Image.FromStream(System.Net.WebRequest.Create(picstr).GetResponse().GetResponseStream());
            }
            catch { }
        }

        private void memberDetailForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid
            );
            e.Graphics.FillRectangle(new SolidBrush(Color.White), new Rectangle(1, 1, e.ClipRectangle.Width - 2, 46));

            Font myFont = new Font(UserClass.fontName, 12);
            string strLine = String.Format(sTitle);
            PointF strPoint = new PointF(18, 15);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), strPoint);

            e.Graphics.DrawLine(new Pen(Defcolor.FontGrayColor, 2), new Point(700, 20), new Point(710, 30));
            e.Graphics.DrawLine(new Pen(Defcolor.FontGrayColor, 2), new Point(700, 30), new Point(710, 20));

            myFont = new Font(UserClass.fontName, 9);

            User.drawFormatLeft.Alignment = StringAlignment.Near;
            User.drawFormatLeft.LineAlignment = StringAlignment.Center;

            strLine = string.Format("姓名");
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), new Rectangle(136, 60, 60, 30), User.drawFormatLeft);
            strLine = string.Format(sUsername);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(200, 60, 100, 30), User.drawFormatLeft);

            strLine = string.Format("会员等级");
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), new Rectangle(136, 105, 60, 30), User.drawFormatLeft);
            strLine = string.Format(sLevel);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(200, 105, 100, 30), User.drawFormatLeft);

            strLine = string.Format("会员卡号");
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), new Rectangle(136, 150, 60, 30), User.drawFormatLeft);
            strLine = string.Format(sMembercode);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(200, 150, 100, 30), User.drawFormatLeft);

            strLine = string.Format("手机号");
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), new Rectangle(336, 60, 60, 30), User.drawFormatLeft);
            strLine = string.Format(sPhoneNum);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(400, 60, 100, 30), User.drawFormatLeft);

            strLine = string.Format("余额");
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), new Rectangle(336, 105, 60, 30), User.drawFormatLeft);
            strLine = string.Format(sBalance);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(400, 105, 100, 30), User.drawFormatLeft);

            strLine = string.Format("积分");
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), new Rectangle(336, 150, 60, 30), User.drawFormatLeft);
            strLine = string.Format(sBonus);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(400, 150, 100, 30), User.drawFormatLeft);

            e.Graphics.DrawLine(new Pen(Defcolor.MainGrayLineColor, 1), new Point(20, 200), new Point(715, 200));

            strLine = string.Format("生日");
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), new Rectangle(18, 210, 60, 30), User.drawFormatLeft);
            strLine = string.Format(sBirthday);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(100, 210, 500, 30), User.drawFormatLeft);

            strLine = string.Format("身份证");
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), new Rectangle(18, 240, 60, 30), User.drawFormatLeft);
            strLine = string.Format(sID);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(100, 240, 500, 30), User.drawFormatLeft);

            strLine = string.Format("邮箱");
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), new Rectangle(18, 270, 60, 30), User.drawFormatLeft);
            strLine = string.Format(sEmail);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(100, 270, 500, 30), User.drawFormatLeft);

            strLine = string.Format("详细地址");
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), new Rectangle(18, 300, 60, 30), User.drawFormatLeft);
            strLine = string.Format(sAddress);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(100, 300, 500, 30), User.drawFormatLeft);

            strLine = string.Format("教育背景");
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), new Rectangle(18, 330, 60, 30), User.drawFormatLeft);
            strLine = string.Format(sEducational);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(100, 330, 500, 30), User.drawFormatLeft);

            strLine = string.Format("职业");
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), new Rectangle(18, 360, 60, 30), User.drawFormatLeft);
            strLine = string.Format(sOccupation);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(100, 360, 500, 30), User.drawFormatLeft);

            strLine = string.Format("行业");
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), new Rectangle(18, 390, 60, 30), User.drawFormatLeft);
            strLine = string.Format(sIndustry);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(100, 390, 500, 30), User.drawFormatLeft);

            strLine = string.Format("收入");
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), new Rectangle(18, 420, 60, 30), User.drawFormatLeft);
            strLine = string.Format(sIncome);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(100, 420, 500, 30), User.drawFormatLeft);

            strLine = string.Format("兴趣");
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), new Rectangle(18, 450, 60, 30), User.drawFormatLeft);
            strLine = string.Format(sInterest);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontLiteGrayColor), new Rectangle(100, 450, 500, 30), User.drawFormatLeft);
        }

        private void memberDetailForm_MouseUp(object sender, MouseEventArgs e)
        {
            Rectangle _rectClose = new Rectangle(700, 20, 10, 10);
            if (_rectClose.Contains(e.Location))
            {
                Dispose();
            }
        }
        private void addbalance_MouseUp(object sender, MouseEventArgs e)
        {
            if (isError)
            {
                return;
            }
            addBalanceForm addbalanceF = new addBalanceForm(memberinfoS.data.member_id, this);
            addbalanceF.TopMost = true;
            addbalanceF.StartPosition = FormStartPosition.CenterParent;
            addbalanceF.ShowDialog();
            Refresh();
        }
        public void addbalanceSuccess(string _newbalance)
        {
            sBalance = _newbalance;
        }
        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        {
            try
            {
                if (keyData == Keys.Escape)
                {
                    Dispose();
                }
                else if (UserClass.isUseKeyBorad && fnMemberDetailMouseControl(keyData))
                {
                    return true;
                }
                return base.ProcessCmdKey(ref msg, keyData);
            }
            catch
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        #region 会员详情页鼠标操作
        public void fnSetMemberDetailMouse()
        {

            memberDetailMouse.Position.Clear();
            memberDetailMouse.pos = -1;
            memberDetailMouse.Position.Add(new Point(650, 80));//添加储值码0
        }
        public bool fnMemberDetailMouseControl(System.Windows.Forms.Keys keyData)
        {
            if (keyData == Keys.Up)
            {
                #region up
                switch (memberDetailMouse.pos)
                {
                    case 0: memberDetailMouse.pos = 0; break;
                    default: memberDetailMouse.pos = 0; break;
                }
                memberDetailMouse._offsetX = Location.X;
                memberDetailMouse._offsetY = Location.Y;
                NativeMethods.SetCursorPos(memberDetailMouse.Position[memberDetailMouse.pos].X + memberDetailMouse._offsetX, memberDetailMouse.Position[memberDetailMouse.pos].Y + memberDetailMouse._offsetY);
                return true;
                #endregion
            }
            else if (keyData == Keys.Left)
            {
                #region left
                switch (memberDetailMouse.pos)
                {
                    case 0: memberDetailMouse.pos = 0; break;
                    default: memberDetailMouse.pos = 0; break;
                }
                memberDetailMouse._offsetX = Location.X;
                memberDetailMouse._offsetY = Location.Y;
                NativeMethods.SetCursorPos(memberDetailMouse.Position[memberDetailMouse.pos].X + memberDetailMouse._offsetX, memberDetailMouse.Position[memberDetailMouse.pos].Y + memberDetailMouse._offsetY);
                return true;
                #endregion
            }
            else if (keyData == Keys.Right)
            {
                #region right
                switch (memberDetailMouse.pos)
                {
                    case 0: memberDetailMouse.pos = 0; break;
                    default: memberDetailMouse.pos = 0; break;
                }
                memberDetailMouse._offsetX = Location.X;
                memberDetailMouse._offsetY = Location.Y;
                NativeMethods.SetCursorPos(memberDetailMouse.Position[memberDetailMouse.pos].X + memberDetailMouse._offsetX, memberDetailMouse.Position[memberDetailMouse.pos].Y + memberDetailMouse._offsetY);
                return true;
                #endregion
            }
            else if (keyData == Keys.Down)
            {
                #region Down
                switch (memberDetailMouse.pos)
                {
                    case 0: memberDetailMouse.pos = 0; break;
                    default: memberDetailMouse.pos = 0; break;
                }
                memberDetailMouse._offsetX = Location.X;
                memberDetailMouse._offsetY = Location.Y;
                NativeMethods.SetCursorPos(memberDetailMouse.Position[memberDetailMouse.pos].X + memberDetailMouse._offsetX, memberDetailMouse.Position[memberDetailMouse.pos].Y + memberDetailMouse._offsetY);
                return true;
                #endregion
            }
            else if (keyData == Keys.Space)
            {
                NativeMethods.mouse_event(NativeMethods.MOUSEEVENTF_LEFTDOWN | NativeMethods.MOUSEEVENTF_LEFTUP, Cursor.Position.X, Cursor.Position.Y, 0, 0);
                return true;
            }
            else
            {
                return false;
            }
        }
        #endregion

    }
}
