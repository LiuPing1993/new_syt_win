using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Threading;
using System.Drawing.Drawing2D;

namespace qgj
{
    public partial class loginControl : baseRequestControl
    {
        logininputControl accountinputC = new logininputControl(InType.account);
        logininputControl passwordinputC = new logininputControl(InType.password);
        loginactivatebuttonControl loginC = new loginactivatebuttonControl();
        selectControl autologinC = new selectControl(false);
        selectControl rememberC = new selectControl(false);
        Label lblAuto = new Label();
        Label lblRemember = new Label();
        Label lbProxy = new Label();

        loginform.login log;

        public loginControl()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            InitializeComponent();

            #region 控件

            BackColor = Defcolor.MainBackColor;

            Label accountLabel = new Label();
            accountLabel.Text = "账号 :";
            accountLabel.BackColor = Defcolor.MainBackColor;
            accountLabel.ForeColor = Defcolor.FontLiteGrayColor;
            accountLabel.Font = new System.Drawing.Font(UserClass.fontName, 12);
            accountLabel.AutoSize = true;
            accountLabel.Location = new Point((ClientRectangle.Width - accountinputC.Width) / 2 - 35, 105);
            Controls.Add(accountLabel);

            accountinputC.Location = new Point((ClientRectangle.Width - accountinputC.Width) / 2 + 20, 100);
            accountinputC.TabStop = false;
            Controls.Add(accountinputC);

            Label passwordLabel = new Label();
            passwordLabel.Text = "密码 :";
            passwordLabel.BackColor = Defcolor.MainBackColor;
            passwordLabel.ForeColor = Defcolor.FontLiteGrayColor;
            passwordLabel.Font = new System.Drawing.Font(UserClass.fontName, 12);
            passwordLabel.AutoSize = true;
            passwordLabel.Location = new Point((ClientRectangle.Width - passwordinputC.Width) / 2 - 35, 165);
            Controls.Add(passwordLabel);

            passwordinputC.Location = new Point((ClientRectangle.Width - passwordinputC.Width) / 2 + 20, 160);
            passwordinputC.TabStop = false;
            Controls.Add(passwordinputC);

            autologinC.Location = new Point(105, 220);
            Controls.Add(autologinC);
            lblAuto.Text = "自动登录";
            lblAuto.Font = new System.Drawing.Font(UserClass.fontName, 10);
            lblAuto.ForeColor = Defcolor.FontLiteGrayColor;
            lblAuto.Location = new Point(130, 217);
            Controls.Add(lblAuto);

            rememberC.Location = new Point(310, 220);
            Controls.Add(rememberC);
            lblRemember.Text = "记住密码";
            lblRemember.Font = new System.Drawing.Font(UserClass.fontName, 10);
            lblRemember.ForeColor = Defcolor.FontLiteGrayColor;
            lblRemember.Location = new Point(335, 217);
            Controls.Add(lblRemember);

            loginC.Location = new Point((ClientRectangle.Width - passwordinputC.Width) / 2, 260);
            loginC.MouseUp += new MouseEventHandler(login_MouseUp);
            //loginC.TabStop = false;
            Controls.Add(loginC);

            lbProxy.Text = "设置代理上网";
            lbProxy.Font = new System.Drawing.Font(UserClass.fontName, 10);
            lbProxy.ForeColor = Color.FromArgb(100, 100, 100);
            lbProxy.BackColor = Defcolor.MainBackColor;
            lbProxy.Location = new Point(390, 315);
            Controls.Add(lbProxy);
            #endregion

            lbProxy.MouseUp += new MouseEventHandler(fnProxySet_Click);
            lbProxy.MouseEnter += new EventHandler(fnLabel_MouseEnter);
            lbProxy.MouseLeave += new EventHandler(fnLabel_MouseLeave);
            lblAuto.MouseEnter += new EventHandler(fnLabel_MouseEnter);
            lblRemember.MouseEnter += new EventHandler(fnLabel_MouseEnter);
            lblAuto.MouseLeave += new EventHandler(fnLabel_MouseLeave);
            lblRemember.MouseLeave += new EventHandler(fnLabel_MouseLeave);

            lblAuto.MouseUp += new MouseEventHandler(selectlabel_Click);
            lblRemember.MouseUp += new MouseEventHandler(selectlabel_Click);
            autologinC.MouseUp += new MouseEventHandler(selectcontrol_Click);
            autologinC.Name = "autologinC";
            rememberC.MouseUp += new MouseEventHandler(selectcontrol_Click);
            rememberC.Name = "rememberC";

            loadconfigClass _lcc = new loadconfigClass("autologin");
            if (_lcc.readfromConfig() == "true")
            {
                autologinC.fnSelectChange(true);
                rememberC.fnSelectChange(true);
                loadconfigClass _key = new loadconfigClass("key1");
                accountinputC.fnSetValue(SecurityClass.outCode(_key.readfromConfig()));
                _key = new loadconfigClass("key2");
                passwordinputC.fnSetValue(SecurityClass.outCode(_key.readfromConfig()));
            }
            else
            {
                _lcc = new loadconfigClass("remember");
                if (_lcc.readfromConfig() == "true")
                {
                    rememberC.fnSelectChange(true);
                    loadconfigClass key = new loadconfigClass("key1");
                    accountinputC.fnSetValue(SecurityClass.outCode(key.readfromConfig()));
                    key = new loadconfigClass("key2");
                    passwordinputC.fnSetValue(SecurityClass.outCode(key.readfromConfig()));
                }
            }
        }
        private void loginControl_Load(object sender, EventArgs e)
        {
            if (rememberC.returnStatus())
            {
                
            }
            if (autologinC.returnStatus() && UserClass.IsFirstLogin) 
            {
                login_MouseUp(null, null);
            }
        }
        private void login_MouseUp(object sender, MouseEventArgs e)
        {
            if (accountinputC.fnGetValue() == "" || passwordinputC.fnGetValue() == "")
            {
                return;
            }
            start();
        }

        public void start()
        {
            if (!IsThreadRun)
            {
                Console.WriteLine("密码:" + passwordinputC.fnGetValue());
                log = new loginform.login();
                log.account = accountinputC.fnGetValue();
                log.pwd = passwordinputC.fnGetValue();
                d = new addDelegate(result);
                start(log);
                ((loginForm)Parent).waitC.sInfo = "登录中";
                ((loginForm)Parent).fnShowControls(lgFormType.wait);
            }
        }
        public void result()
        {
            string temp_error = "";
            errorinformationForm _infoF;
            try
            {
                if (iRThread == 0)
                {
                    if (log.iRHttp == 0)
                    {
                        if (UserClass.ppbC.IsUsePaipai)
                        {
                            pictureClass.Delay(2000);
                        }

                        //登陸成功
                        BaseValue.token = log.resultS.data.token;
                        BaseValue.store_name = log.resultS.data.store_name;
                        BaseValue.merchant_name = log.resultS.data.merchant_name;
                        BaseValue.employee_id = log.resultS.data.employee_code;
                        BaseValue.employee_name = log.resultS.data.employee_name;
                        

                        loadconfigClass _autologinlcc = new loadconfigClass("autologin");
                        loadconfigClass _rememberlcc = new loadconfigClass("remember");

                        if (autologinC.returnStatus())
                        {
                            _autologinlcc.writetoConfig("true");
                            _rememberlcc.writetoConfig("true");
                            fnLoginInfoSave();
                        }
                        else if (rememberC.returnStatus())
                        {
                            _autologinlcc.writetoConfig("false");
                            _rememberlcc.writetoConfig("true");
                            fnLoginInfoSave();
                        }
                        else
                        {
                            _autologinlcc.writetoConfig("false");
                            _rememberlcc.writetoConfig("false");
                        }

                        ((loginForm)Parent).DialogResult = DialogResult.OK;
                        ((loginForm)Parent).Dispose();
                        return;
                    }
                    else
                    {
                        temp_error = log.sRHttp;
                    }
                }
                else if (iRThread == 1)
                {
                    temp_error = sRThread;
                }
            }
            catch (Exception e)
            {
                PublicMethods.WriteLog(e);
            }

            ((loginForm)Parent).fnShowControls(lgFormType.login);
            _infoF = new errorinformationForm("登录出现问题", temp_error);
            _infoF.TopMost = true;
            _infoF.StartPosition = FormStartPosition.CenterScreen;
            _infoF.ShowDialog();
            ((loginForm)Parent).Refresh();
            ((loginForm)Parent).Activate();
        }

        /// <summary>
        /// 本地保存账号密码
        /// </summary>
        private void fnLoginInfoSave()
        {
            loadconfigClass _loginkey = new loadconfigClass("key1");
            _loginkey.writetoConfig(SecurityClass.inCode(accountinputC.fnGetValue()));
            _loginkey = new loadconfigClass("key2");
            _loginkey.writetoConfig(SecurityClass.inCode(passwordinputC.fnGetValue()));
        }
        public void selectlabel_Click(object sender, MouseEventArgs e)
        {
            Label _lb = (Label)sender;
            if (_lb.Text == "自动登录")
            {
                if (!autologinC.returnStatus())
                {
                    autologinC.fnSelectChange(true);
                    rememberC.fnSelectChange(true);
                }
                else
                {
                    autologinC.fnSelectChange(false);
                    rememberC.fnSelectChange(false);
                }
            }
            else if (_lb.Text == "记住密码")
            {
                if (!rememberC.returnStatus())
                {
                    rememberC.fnSelectChange(true);
                }
                else
                {
                    autologinC.fnSelectChange(false);
                    rememberC.fnSelectChange(false);
                }
            }
        }
        public void selectcontrol_Click(object sender, MouseEventArgs e)
        {
            selectControl _selectC = (selectControl)sender;
            if (_selectC.Name == "autologinC")
            {
                if (!autologinC.returnStatus())
                {
                    autologinC.fnSelectChange(true);
                    rememberC.fnSelectChange(true);
                }
                else
                {
                    autologinC.fnSelectChange(false);
                    rememberC.fnSelectChange(false);
                }
            }
            else if (_selectC.Name == "rememberC")
            {
                if (!rememberC.returnStatus())
                {
                    rememberC.fnSelectChange(true);
                }
                else
                {
                    autologinC.fnSelectChange(false);
                    rememberC.fnSelectChange(false);
                }
            }
        }
        public void fnProxySet_Click(object sender, MouseEventArgs e)
        {
            ((loginForm)Parent).fnShowControls(lgFormType.proxy);
        }
        public void fnLabel_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                Label lb = (Label)sender;
                lb.ForeColor = Color.FromArgb(170, 170, 170);
            }
            catch { }
        }
        public void fnLabel_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                Label lb = (Label)sender;
                lb.ForeColor = Color.FromArgb(100, 100, 100);
            }
            catch { }
        }
        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        {
            if (keyData == Keys.Tab)
            {
                if (accountinputC.tbxValue.Focused)
                {
                    passwordinputC.Focus();
                }
                else if (passwordinputC.tbxValue.Focused)
                {
                    accountinputC.Focus();
                }
                else
                {
                    accountinputC.Focus();
                }
                return true;
            }
            else if(keyData == Keys.Enter)
            {
                login_MouseUp(null, null);
            }
            else if (keyData == Keys.Escape)
            {
                PublicMethods.exitSystem();
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void loginControl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle,
                Defcolor.MainRadColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainRadColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainRadColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainRadColor, 1, ButtonBorderStyle.Solid
            );

            e.Graphics.FillRectangle(new SolidBrush(Defcolor.MainRadColor), new Rectangle(0, 0, e.ClipRectangle.Width, 45));
            e.Graphics.DrawLine(new Pen(Defcolor.MainBackColor, 2), new Point(466, 10), new Point(486, 30));
            e.Graphics.DrawLine(new Pen(Defcolor.MainBackColor, 2), new Point(466, 30), new Point(486, 10));
            e.Graphics.DrawImage(Properties.Resources.qxtitle, 10, 8, 105, 27);
        }
    }
    public class loginData
    {
        public string token { get; set; }
        public string merchant_name { get; set; }
        public string store_name { get; set; }
        public string employee_name { get; set; }
        public string paipai_token { get; set; }
    }
    public class loginSuccess
    {
        public int errCode { get; set; }
        public string errMsg { get; set; }
        public loginData data { get; set; }
    }
}
