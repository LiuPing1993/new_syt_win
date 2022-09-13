using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace self_syt.login
{
    public partial class ActiveControl : comui.BaseNetControl
    {
        public comui.textbox.TextBoxControl textBoxC;

        public PictureBox pic_login;
        Active ac;
        public ActiveControl()
        {
            InitializeComponent();
            BackColor = BaseColor.color_White;
            Dock = DockStyle.Fill;
        }

        private void ActiveControl_Load(object sender, EventArgs e)
        {

            textBoxC = new comui.textbox.TextBoxControl();
            textBoxC.Size = new System.Drawing.Size(260, 55);
            textBoxC.labelTips = "请输入激活码";
            textBoxC.Location = new Point((ClientRectangle.Width - textBoxC.Width) / 2, (ClientRectangle.Height - textBoxC.Height) / 2 - 25);

            Controls.Add(textBoxC);

            pic_login = new PictureBox();
            pic_login.Size = new Size(48, 48);
            pic_login.Image = Properties.Resources.login_btn;
            pic_login.SizeMode = PictureBoxSizeMode.StretchImage;
            pic_login.MouseUp += pic_login_MouseUp;
            pic_login.Location = new Point((ClientRectangle.Width - pic_login.Width) / 2, this.textBoxC.Location.Y + this.textBoxC.Height + 28);
            Controls.Add(pic_login);

        }

        void pic_login_MouseUp(object sender, MouseEventArgs e)
        {
            if (!string.IsNullOrEmpty(textBoxC.GetTextValue()))
            {
                if (untils.UregexUntils.IsNumeric(textBoxC.GetTextValue()))
                {
                    ActiveMethod();
                }
                else
                {
                    ShowMessage("提示", "激活码不合法请重新输入");
                }
            }
            else
            {
                ShowMessage("提示", "请先输入激活码");
            }
        }

        public void ActiveMethod()
        {
            if (!IsThreadRun)
            {
                ac = new Active();
                ac.code = textBoxC.GetTextValue();
                d = new addDelegate(ActiveResult);
                BaseModel.loginFormModel.ShowForm(BaseEnum.LoginTypeEnum.wait);
                start(ac);
            }
        }
        public void ActiveResult()
        {
            try
            {
                if (iRThread == 0)
                {
                    if (ac.iRHttp == 0)
                    {
                        BaseModel.loginFormModel.ShowForm(BaseEnum.LoginTypeEnum.login);
                    }
                    else
                    {
                        ShowMessage("提示", "注册失败：" + ac.sRHttp);
                        BaseModel.loginFormModel.ShowForm(BaseEnum.LoginTypeEnum.active);
                    }
                }
                else if (iRThread == 1)
                {
                    ShowMessage("提示", "注册失败：" + sRThread);
                    BaseModel.loginFormModel.ShowForm(BaseEnum.LoginTypeEnum.active);
                }
            }
            catch (Exception ex)
            {
                ShowMessage("提示", "接口调取异常：" + ex.Message.ToString());
                untils.UcomUntils.ConsoleMsg("接口调取异常：" + ex.Message.ToString());
            }
        }
    }
}
