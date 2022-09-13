using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace self_syt.login
{
    public partial class LoginControl : comui.BaseNetControl
    {
        public comui.textbox.TextBoxControl textUserName;
        public comui.textbox.TextBoxControl textPassWord;
        public comui.button.ButtonControl btn_confirm;

        public PictureBox pic_login;

        Login login;

        untils.UoperaConfig config = new untils.UoperaConfig(BaseConfigValue.remberPwd);
        untils.UoperaConfig cf = new untils.UoperaConfig(BaseConfigValue.IsExitFromMain);

        public LoginControl()
        {
            InitializeComponent();
            Dock = DockStyle.Fill;
            textUserName = new comui.textbox.TextBoxControl();
            config = new untils.UoperaConfig(BaseConfigValue.remberPwd);
            if (config.ReadConfig() == "1")
            {
                config = new untils.UoperaConfig(BaseConfigValue.userName);
                if (config.ReadConfig() != "")
                {
                    textUserName.textName = config.ReadConfig();
                }
            }
            textUserName.Size = new System.Drawing.Size(260, 60);
            textUserName.title = "账号";
            textUserName.labelTips = "请输入账号";
            textUserName.Location = new Point((ClientRectangle.Width - textUserName.Width) / 2, 60);
            Controls.Add(textUserName);

            textPassWord = new comui.textbox.TextBoxControl();
            config = new untils.UoperaConfig(BaseConfigValue.remberPwd);
            if (config.ReadConfig() == "1")
            {
                config = new untils.UoperaConfig(BaseConfigValue.passwWord);
                if (config.ReadConfig() != "")
                {
                    textPassWord.textName = untils.Usecurity.outCode(config.ReadConfig());
                }
            }
            textPassWord.isPassWord = true;
            textPassWord.Size = new System.Drawing.Size(260, 60);
            textPassWord.title = "密码";
            textPassWord.Location = new Point((ClientRectangle.Width - textPassWord.Width) / 2, this.textUserName.Bottom + 15);
            textPassWord.labelTips = "请输入密码";
            Controls.Add(textPassWord);

            pic_login = new PictureBox();
            pic_login.Size = new Size(48, 48);
            pic_login.Image = Properties.Resources.login_btn;
            pic_login.SizeMode = PictureBoxSizeMode.StretchImage;
            pic_login.MouseUp += pic_login_MouseUp;
            pic_login.Location = new Point((ClientRectangle.Width - pic_login.Width) / 2, this.textPassWord.Location.Y + this.textPassWord.Height + 30);
            Controls.Add(pic_login);


        }

        void pic_login_MouseUp(object sender, MouseEventArgs e)
        {
            string acount = this.textUserName.GetTextValue();
            string pwd = this.textPassWord.GetTextValue();
            cf.WriteConfig("2");
            untils.UcomUntils.ConsoleMsg("登录的账号："+acount);
            untils.UcomUntils.ConsoleMsg("登录的密码："+pwd);

            if (!string.IsNullOrEmpty(acount))
            {
                if (!string.IsNullOrEmpty(pwd))
                {
                    LoginMethod(acount, pwd);
                    //重新的写入账号和密码
                    config = new untils.UoperaConfig(BaseConfigValue.userName);
                    config.WriteConfig(acount);
                    config = new untils.UoperaConfig(BaseConfigValue.passwWord);
                    config.WriteConfig(untils.Usecurity.inCode(pwd));
                }
                else
                {
                    ShowMessage("提示", "用户密码不能为空");
                }
            }
            else
            {
                ShowMessage("提示", "用户名不能为空");
            }
        }

        public void LoginMethod(string account, string pwd)
        {
            try
            {
                if (!IsThreadRun)
                {
                    //cf.WriteConfig("2");
                    login = new Login();
                    login.account = account;
                    login.pwd = pwd;
                    d = new addDelegate(LoginResult);
                    //等待界面展示
                    BaseModel.loginFormModel.ShowForm(BaseEnum.LoginTypeEnum.wait);
                    start(login);
                }
            }
            catch (Exception ex)
            {

                ShowMessage("提示", "登录异常：" + ex.Message.ToString());
            }

        }

        public void LoginResult()
        {
            try
            {
                if (iRThread == 0)
                {
                    if (login.iRHttp == 0)
                    {
                        //创建快捷方式
                        untils.UcomUntils.newFastLnk();
                        BaseModel.loginFormModel.ShowForm(BaseEnum.LoginTypeEnum.none);
                        pay.PayBaseForm form = new pay.PayBaseForm();
                        form.ShowDialog();
                    }
                    else
                    {
                       
                        ShowMessage("提示", "登录失败：" + login.sRHttp);
                        BaseModel.loginFormModel.ShowForm(BaseEnum.LoginTypeEnum.login);
                    }
                }
                else
                {
                    
                    ShowMessage("提示", "登录失败：" + sRThread);
                    BaseModel.loginFormModel.ShowForm(BaseEnum.LoginTypeEnum.login);
                }
            }
            catch (Exception ex)
            {
                ShowMessage("提示", "登录异常：" + ex.Message.ToString());
                untils.UcomUntils.ConsoleMsg(ex.Message.ToString());
            }
        }

        private void LoginControl_Load(object sender, EventArgs e)
        {
            try
            {
                config = new untils.UoperaConfig(BaseConfigValue.autoLogin);
                if (config.ReadConfig() == "1"&&cf.ReadConfig()!="1")
                {
                    pic_login_MouseUp(null,null);      
                }
            }
            catch (Exception ex)
            {
                ShowMessage("异常", "系统登录初始化异常：" + ex.Message.ToString());
            }
        }
    }
}
