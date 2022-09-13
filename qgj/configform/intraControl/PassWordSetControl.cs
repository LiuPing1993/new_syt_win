using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace qgj
{
    public partial class PassWordSetControl : UserControl
    {
        TextBox tbOldPwd = new TextBox();
        TextBox tbNewPwd = new TextBox();
        TextBox tbConPwd = new TextBox();

        confirmcancelControl confirmC = new confirmcancelControl("确认");
        int iHttpResult = 0;
        string sHttpResult = "";
        public PassWordSetControl()
        {
            InitializeComponent();
            BackColor = Defcolor.MainBackColor;

            tbOldPwd.SetBounds(45, 35, 130, 20);
            tbOldPwd.Font = new System.Drawing.Font(UserClass.fontName, 11);
            tbOldPwd.PasswordChar = '*';
            tbOldPwd.TabIndex = 0;
            tbOldPwd.UseSystemPasswordChar = true;
            tbOldPwd.BorderStyle = System.Windows.Forms.BorderStyle.None;
            Controls.Add(tbOldPwd);

            tbNewPwd.SetBounds(45, 75, 130, 20);
            tbNewPwd.Font = new System.Drawing.Font(UserClass.fontName, 11);
            tbNewPwd.PasswordChar = '*';
            tbNewPwd.TabIndex = 1;
            tbNewPwd.UseSystemPasswordChar = true;
            tbNewPwd.BorderStyle = System.Windows.Forms.BorderStyle.None;
            Controls.Add(tbNewPwd);

            tbConPwd.SetBounds(45, 115, 130, 20);
            tbConPwd.Font = new System.Drawing.Font(UserClass.fontName, 11);
            tbConPwd.PasswordChar = '*';
            tbConPwd.TabIndex = 2;
            tbConPwd.UseSystemPasswordChar = true;
            tbConPwd.BorderStyle = System.Windows.Forms.BorderStyle.None;
            Controls.Add(tbConPwd);

            confirmC.Location = new Point(200, 170);
            confirmC.MouseUp += new MouseEventHandler(confirm_MouseUp);
            Controls.Add(confirmC);

        }

        private void PassWordSetControl_Load(object sender, EventArgs e)
        {

        }

        private void confirm_MouseUp(object sender, MouseEventArgs e)
        {
            string _oldpwd = tbOldPwd.Text.ToString();
            string _newpwd = tbNewPwd.Text.ToString();
            string _conpwd = tbConPwd.Text.ToString();
            if (_oldpwd != "" && _newpwd != "" && _conpwd != "")
            {
                if (_newpwd != _conpwd)
                {
                    return;
                }
                fnUpdatePwdHttp();
                if (iHttpResult == 0)
                {
                    errorinformationForm _errorF = new errorinformationForm("修改成功", sHttpResult);
                    _errorF.TopMost = true;
                    _errorF.StartPosition = FormStartPosition.CenterParent;
                    _errorF.ShowDialog();
                }
                else
                {
                    errorinformationForm _errorF = new errorinformationForm("修改失败", sHttpResult);
                    _errorF.TopMost = true;
                    _errorF.StartPosition = FormStartPosition.CenterParent;
                    _errorF.ShowDialog();
                }
                Refresh();
            }
        }
        private bool fnUpdatePwdHttp()
        {
            loadconfigClass _lcc = new loadconfigClass("terminal_sn");
            UrlClass _urlC = new UrlClass(Url.updatepwd);
            _urlC.addParameter("terminal_sn", _lcc.readfromConfig());
            _urlC.addParameter("token", UserClass.Token);
            _urlC.addParameter("old_pwd", PublicMethods.md5(tbOldPwd.Text).ToLower());
            _urlC.addParameter("new_pwd", PublicMethods.md5(tbNewPwd.Text).ToLower());
            _urlC.addParameter("confirm_pwd", PublicMethods.md5(tbConPwd.Text).ToLower());
            string _sRequestUrl = _urlC.requestUrl();
            Console.WriteLine("url:" + _sRequestUrl);
            HttpClass _httpC = new HttpClass();
            string _RequestMsg = _httpC.HttpGet(_sRequestUrl);
            Console.WriteLine("result:" + _RequestMsg);
            if (_RequestMsg.IndexOf("\"errCode\":0") != -1)
            {
                iHttpResult = 0;
                sHttpResult = "已经修改密码";
                return true;
            }
            else
            {
                errorClass _errorC = (errorClass)JsonConvert.DeserializeObject(_RequestMsg, typeof(errorClass));
                iHttpResult = 1;
                sHttpResult = _errorC.errMsg;
                return false;
            }
        }
        private void PassWordSetControl_Paint(object sender, PaintEventArgs e)
        {
            PublicMethods.FillRoundRectangle(new Rectangle(40, 30, 180, 60), e.Graphics, 7, Color.White);
            PublicMethods.FillRoundRectangle(new Rectangle(40, 70, 180, 100), e.Graphics, 7, Color.White);
            PublicMethods.FillRoundRectangle(new Rectangle(40, 110, 180, 140), e.Graphics, 7, Color.White);

            StringFormat _drawFormat = new StringFormat();
            _drawFormat.Alignment = StringAlignment.Near;
            _drawFormat.LineAlignment = StringAlignment.Center;

            Font _myFont = new Font(UserClass.fontName, 11);
            string _strLine = string.Format("旧密码");
            e.Graphics.DrawString(_strLine, _myFont, new SolidBrush(Color.Gray), new Rectangle(200, 30, 100, 30), _drawFormat);
            _myFont = new Font(UserClass.fontName, 11);
            _strLine = string.Format("新密码");
            e.Graphics.DrawString(_strLine, _myFont, new SolidBrush(Color.Gray), new Rectangle(200, 70, 100, 30), _drawFormat);
            _myFont = new Font(UserClass.fontName, 11);
            _strLine = string.Format("确认密码");
            e.Graphics.DrawString(_strLine, _myFont, new SolidBrush(Color.Gray), new Rectangle(200, 110, 100, 30), _drawFormat);
        }
    }
}
