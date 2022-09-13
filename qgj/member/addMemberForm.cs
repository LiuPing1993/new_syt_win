using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using Newtonsoft.Json;

namespace qgj
{
    public partial class addMemberForm : Form
    {
        string sTitle = "添加会员";
        string sErrorInfo = "";
        DateTimePicker birthdaySelect = new DateTimePicker();
        confirmcancelControl confirmC = new confirmcancelControl("确认");
        confirmcancelControl cancelC = new confirmcancelControl("取消");


        TextBox tbPhone = new TextBox();
        TextBox tbName = new TextBox();

        selectControl maleSelectC = new selectControl();
        selectControl femaleSelectC = new selectControl();

        addmemberSuccess addmemberS;

        int sex = 0;//0-未选择，1-男，2-女
        int iHttpResult = 0;
        string sHttpResult = "";

        memberControl memberC;
        public addMemberForm(memberControl _memberC)
        {
            InitializeComponent();

            memberC = _memberC;

            birthdaySelect.Location = new Point(130, 260);
            birthdaySelect.Font = new System.Drawing.Font(UserClass.fontName, 12);
            Controls.Add(birthdaySelect);

            tbPhone.SetBounds(135, 80, 230, 30);
            tbPhone.Font = new Font(UserClass.fontName, 12);
            tbPhone.ForeColor = Defcolor.FontLiteGrayColor;
            tbPhone.ImeMode = System.Windows.Forms.ImeMode.Off;
            tbPhone.BorderStyle = System.Windows.Forms.BorderStyle.None;
            tbPhone.KeyPress += new KeyPressEventHandler(phone_KeyPress);
            Controls.Add(tbPhone);

            tbName.SetBounds(135, 160, 230, 30);
            tbName.Font = new Font(UserClass.fontName, 12);
            tbName.ForeColor = Defcolor.FontLiteGrayColor;
            tbName.BorderStyle = System.Windows.Forms.BorderStyle.None;
            Controls.Add(tbName);

            confirmC.Location = new Point(300, 320);
            confirmC.MouseUp += new MouseEventHandler(confirmC_click);
            confirmC.TabStop = false;
            Controls.Add(confirmC);

            cancelC.Location = new Point(140, 320);
            cancelC.MouseUp += new MouseEventHandler(cancelC_click);
            cancelC.TabStop = false;
            Controls.Add(cancelC);

            maleSelectC.Location = new Point(130, 218);
            maleSelectC.MouseUp += new MouseEventHandler(male_click);
            maleSelectC.TabStop = false;
            Controls.Add(maleSelectC);

            femaleSelectC.Location = new Point(200, 218);
            femaleSelectC.MouseUp += new MouseEventHandler(female_click);
            femaleSelectC.TabStop = false;
            Controls.Add(femaleSelectC);
        }

        private void addMemberForm_Load(object sender, EventArgs e)
        {
            fnSetDetailMouse();
        }

        private void addMemberForm_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid
            );

            e.Graphics.FillRectangle(new SolidBrush(Color.White), new Rectangle(1, 1, e.ClipRectangle.Width - 2, 46));

            Font myFont = new Font(UserClass.fontName, 10);
            string strLine = String.Format(sTitle);
            PointF strPoint = new PointF(18, 15);
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), strPoint);

            User.drawFormatLeft.Alignment = StringAlignment.Near;
            User.drawFormatLeft.LineAlignment = StringAlignment.Center;
            strLine = string.Format("手机号");
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), new Rectangle(30, 80, 100, 30), User.drawFormatLeft);

            strLine = string.Format("*");
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.MainRadColor), new Rectangle(80, 80, 20, 30), User.drawFormatLeft);

            strLine = string.Format(sErrorInfo);
            e.Graphics.DrawString(strLine, new Font(UserClass.fontName,9), new SolidBrush(Defcolor.MainRadColor), new Rectangle(150, 110, 200, 30), User.drawFormatLeft);

            PublicMethods.FillRoundRectangle(new Rectangle(130, 70, 400, 110), e.Graphics, 7, Color.White);
            PublicMethods.FrameRoundRectangle(new Rectangle(130, 70, 400, 110), e.Graphics, 7, Color.Gray);

            strLine = string.Format("会员姓名");
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), new Rectangle(30, 155, 100, 30), User.drawFormatLeft);
            PublicMethods.FillRoundRectangle(new Rectangle(130, 150, 400, 190), e.Graphics, 7, Color.White);
            PublicMethods.FrameRoundRectangle(new Rectangle(130, 150, 400, 190), e.Graphics, 7, Color.Gray);

            strLine = string.Format("性别");
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), new Rectangle(30, 210, 100, 30), User.drawFormatLeft);

            strLine = string.Format("男");
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), new Rectangle(145, 210, 30, 30), User.drawFormatLeft);

            strLine = string.Format("女");
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), new Rectangle(215, 210, 30, 30), User.drawFormatLeft);

            strLine = string.Format("生日");
            e.Graphics.DrawString(strLine, myFont, new SolidBrush(Defcolor.FontGrayColor), new Rectangle(30, 260, 100, 30), User.drawFormatLeft);
        }

        private void phone_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private void male_click(object sender, MouseEventArgs e)
        {
            sex = 1;
            maleSelectC.fnSelectChange(true);
            femaleSelectC.fnSelectChange(false);
        }
        private void female_click(object sender, MouseEventArgs e)
        {
            sex = 2;
            maleSelectC.fnSelectChange(false);
            femaleSelectC.fnSelectChange(true);
        }
        private void addMemberForm_Shown(object sender, EventArgs e)
        {
            keyboardClass.showKeyBoard();
            tbPhone.Focus();
        }
        private void confirmC_click(object sender, MouseEventArgs e)
        {
            string _temp = tbPhone.Text.Trim();
            if (_temp.Length != 11)
            {
                sErrorInfo = "请填入正确手机号";
                Refresh();
                return;
            }
            else
            {
                if (addmemberHttp())
                {
                    errorinformationForm errorF = new errorinformationForm("成功", "添加会员成功");
                    errorF.TopMost = true;
                    errorF.StartPosition = FormStartPosition.CenterParent;
                    errorF.ShowDialog();
                    memberC.codeinsert.Text = tbPhone.Text.Trim();
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                    Dispose();
                }
                else
                {
                    errorinformationForm errorF = new errorinformationForm("添加会员失败", sHttpResult);
                    errorF.TopMost = true;
                    errorF.StartPosition = FormStartPosition.CenterParent;
                    errorF.ShowDialog();
                    Refresh();
                    return;
                }
            }
            sErrorInfo = "";
            Refresh();
        }
        private void cancelC_click(object sender, MouseEventArgs e)
        {
            DialogResult = System.Windows.Forms.DialogResult.Cancel;
            Dispose();
        }
        private bool addmemberHttp()
        {
            try
            {
                loadconfigClass _lcc = new loadconfigClass("terminal_sn");
                UrlClass _urlC = new UrlClass(Url.addmember);
                _urlC.addParameter("terminal_sn", _lcc.readfromConfig());
                _urlC.addParameter("account", tbPhone.Text.Trim());
                _urlC.addParameter("member_name", tbName.Text.Trim());
                _urlC.addParameter("birthday", birthdaySelect.Value.ToShortDateString());
                _urlC.addParameter("sex", sex.ToString());
                _urlC.addParameter("token", UserClass.Token);
                string _sRequestUrl = _urlC.requestUrl();
                Console.WriteLine("url:" + _sRequestUrl);

                HttpClass httpC = new HttpClass();
                string _sRequestMsg = httpC.HttpGet(_sRequestUrl);
                Console.WriteLine("result:" + _sRequestMsg);
                if (_sRequestMsg.IndexOf("\"errCode\":0") != -1)
                {
                    addmemberS = (addmemberSuccess)JsonConvert.DeserializeObject(_sRequestMsg, typeof(addmemberSuccess));
                    iHttpResult = 0;
                    sHttpResult = "成功";
                    return true;
                }
                else
                {
                    errorClass _errorC = (errorClass)JsonConvert.DeserializeObject(_sRequestMsg, typeof(errorClass));
                    iHttpResult = 1;
                    sHttpResult = _errorC.errMsg;
                    return false;
                }
            }
            catch (Exception e)
            {
                iHttpResult = 1;
                sHttpResult = e.Message.ToString();
                return false;
            }
        }
        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        {
            try
            {
                if (keyData == Keys.Escape)
                {
                    Dispose();
                }
                else if (keyData == Keys.Enter)
                {
                    confirmC_click(null, null);
                }
                else if (UserClass.isUseKeyBorad && fnDetailMouseControl(keyData))
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
        public void fnSetDetailMouse()
        {
            addmemberMouse.Position.Clear();
            addmemberMouse.pos = -1;
            addmemberMouse.Position.Add(new Point(200, 90));//手机号0
            addmemberMouse.Position.Add(new Point(200, 170));//姓名1
            addmemberMouse.Position.Add(new Point(135, 222));//男2
            addmemberMouse.Position.Add(new Point(205, 222));//女3
            addmemberMouse.Position.Add(new Point(140, 270));//年份4
            addmemberMouse.Position.Add(new Point(200, 270));//月5
            addmemberMouse.Position.Add(new Point(230, 270));//日6
            addmemberMouse.Position.Add(new Point(160, 340));//取消7
            addmemberMouse.Position.Add(new Point(340, 340));//确定8
        }
        public bool fnDetailMouseControl(System.Windows.Forms.Keys keyData)
        {
            if (keyData == Keys.Up)
            {
                #region up
                switch (addmemberMouse.pos)
                {
                    case 0: addmemberMouse.pos = 7; break;
                    case 1: addmemberMouse.pos = 0; break;
                    case 2: addmemberMouse.pos = 1; break;
                    case 3: addmemberMouse.pos = 2; break;
                    case 4: addmemberMouse.pos = 2; break;
                    case 5: addmemberMouse.pos = 2; break;
                    case 6: addmemberMouse.pos = 2; break;
                    case 7: addmemberMouse.pos = 4; break;
                    case 8: addmemberMouse.pos = 4; break;
                    default: addmemberMouse.pos = 7; break;
                }
                addmemberMouse._offsetX = Location.X;
                addmemberMouse._offsetY = Location.Y;
                NativeMethods.SetCursorPos(addmemberMouse.Position[addmemberMouse.pos].X + addmemberMouse._offsetX, addmemberMouse.Position[addmemberMouse.pos].Y + addmemberMouse._offsetY);
                return true;
                #endregion
            }
            else if (keyData == Keys.Left)
            {
                #region left
                switch (addmemberMouse.pos)
                {
                    case 2: addmemberMouse.pos = 3; break;
                    case 3: addmemberMouse.pos = 2; break;
                    case 4: addmemberMouse.pos = 6; break;
                    case 5: addmemberMouse.pos = 4; break;
                    case 6: addmemberMouse.pos = 5; break;
                    case 7: addmemberMouse.pos = 8; break;
                    case 8: addmemberMouse.pos = 7; break;
                    default: return true;
                }
                addmemberMouse._offsetX = Location.X;
                addmemberMouse._offsetY = Location.Y;
                NativeMethods.SetCursorPos(addmemberMouse.Position[addmemberMouse.pos].X + addmemberMouse._offsetX, addmemberMouse.Position[addmemberMouse.pos].Y + addmemberMouse._offsetY);
                return true;
                #endregion
            }
            else if (keyData == Keys.Right)
            {
                #region right
                switch (addmemberMouse.pos)
                {
                    case 2: addmemberMouse.pos = 3; break;
                    case 3: addmemberMouse.pos = 2; break;
                    case 4: addmemberMouse.pos = 5; break;
                    case 5: addmemberMouse.pos = 6; break;
                    case 6: addmemberMouse.pos = 4; break;
                    case 7: addmemberMouse.pos = 8; break;
                    case 8: addmemberMouse.pos = 7; break;
                    default: return true;
                }
                addmemberMouse._offsetX = Location.X;
                addmemberMouse._offsetY = Location.Y;
                Console.WriteLine(addmemberMouse.Position[addmemberMouse.pos].X + addmemberMouse._offsetX);
                NativeMethods.SetCursorPos(addmemberMouse.Position[addmemberMouse.pos].X + addmemberMouse._offsetX, addmemberMouse.Position[addmemberMouse.pos].Y + addmemberMouse._offsetY);
                return true;
                #endregion
            }
            else if (keyData == Keys.Down)
            {
                #region Down
                switch (addmemberMouse.pos)
                {
                    case 0: addmemberMouse.pos = 1; break;
                    case 1: addmemberMouse.pos = 2; break;
                    case 2: addmemberMouse.pos = 4; break;
                    case 3: addmemberMouse.pos = 4; break;
                    case 4: addmemberMouse.pos = 7; break;
                    case 5: addmemberMouse.pos = 7; break;
                    case 6: addmemberMouse.pos = 7; break;
                    case 7: addmemberMouse.pos = 0; break;
                    case 8: addmemberMouse.pos = 0; break;
                    default: addmemberMouse.pos = 1; break;
                }
                addmemberMouse._offsetX = Location.X;
                addmemberMouse._offsetY = Location.Y;
                Console.WriteLine(addmemberMouse.Position[addmemberMouse.pos].X + addmemberMouse._offsetX);
                NativeMethods.SetCursorPos(addmemberMouse.Position[addmemberMouse.pos].X + addmemberMouse._offsetX, addmemberMouse.Position[addmemberMouse.pos].Y + addmemberMouse._offsetY);
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
    }
    public class addmemberSuccess
    {
        /// <summary>
        /// 
        /// </summary>
        public string errCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string errMsg { get; set; }
    }
}
