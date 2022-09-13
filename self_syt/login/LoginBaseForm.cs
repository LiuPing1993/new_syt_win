using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
namespace self_syt.login
{
    public partial class LoginBaseForm : Form
    {
        public ActiveControl activeC;
        public LoginControl loginC;
        public WaitControl waitC;
        public BaseEnum.LoginTypeEnum loginType = BaseEnum.LoginTypeEnum.active;
        untils.UoperaConfig config = new untils.UoperaConfig("app_id");
        untils.UoperaConfig code_config = new untils.UoperaConfig("merchant_code");

        public string right_bottom_tips = "设置代理上网";
        public string top_title = "亲享收银";

        Rectangle rect_right_bottom_tips;
        Rectangle rect_top_title;

        public PictureBox pic_logo;
        public PictureBox pic_min;
        public PictureBox pic_close;

        public comui.checkbox.CheckBoxControl check_rember;
        public comui.checkbox.CheckBoxControl check_autoLogin;

        untils.UoperaConfig cg = new untils.UoperaConfig(BaseConfigValue.remberPwd);
        public LoginBaseForm()
        {

            InitializeComponent();
            try
            {
                pic_logo = new PictureBox();
                pic_logo.Image = Properties.Resources.login;
                this.panel_top.BackColor = BaseColor.color_Red;
                pic_logo.Size = new System.Drawing.Size(18, 18);
                pic_logo.Location = new Point(14, (panel_top.ClientRectangle.Height - pic_logo.Height) / 2);
                pic_logo.SizeMode = PictureBoxSizeMode.StretchImage;
                this.panel_top.Controls.Add(pic_logo);

                pic_min = new PictureBox();
                pic_min.Size = new System.Drawing.Size(15, 15);
                pic_min.Image = Properties.Resources.min;
                pic_min.SizeMode = PictureBoxSizeMode.StretchImage;
                pic_min.Location = new Point(this.panel_top.ClientRectangle.Width - pic_min.Width * 3 - 10, (this.panel_top.ClientRectangle.Height - pic_min.Height) / 2);
                pic_min.MouseUp += pic_min_MouseUp;
                this.panel_top.Controls.Add(pic_min);

                pic_close = new PictureBox();
                pic_close.Size = new System.Drawing.Size(14, 14);
                pic_close.Image = Properties.Resources.login_close;
                pic_close.SizeMode = PictureBoxSizeMode.StretchImage;
                pic_close.Location = new Point(this.panel_top.ClientRectangle.Width - pic_close.Width * 2, (this.panel_top.ClientRectangle.Height - pic_close.Height) / 2);
                pic_close.MouseUp += pic_close_MouseUp;
                this.panel_top.Controls.Add(pic_close);
                StartPosition = FormStartPosition.CenterScreen;
                rect_right_bottom_tips = new Rectangle((panel_webproxy.ClientRectangle.Width - 165), 0, 150, this.panel_webproxy.ClientRectangle.Height);
                rect_top_title = new Rectangle(this.pic_logo.Location.X + this.pic_logo.Width + 10, 0, 120, panel_top.ClientRectangle.Height);
                BaseModel.loginFormModel = this;
            }
            catch (Exception ex)
            {
                untils.UcomUntils.ConsoleMsg("异常信息：" + ex.Message.ToString() + "；异常对象：" + ex.StackTrace.ToString() + "；调用堆宅：" + ex.StackTrace.ToString() + "；触发方法：" + ex.TargetSite.ToString());
            }
        }

        void check_autoLogin_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                comui.checkbox.CheckBoxControl ck = (comui.checkbox.CheckBoxControl)sender;
                ck.isSelect = !ck.isSelect;
                cg = new untils.UoperaConfig(BaseConfigValue.autoLogin);
                if (ck.isSelect)
                {
                    cg.WriteConfig("1");
                }
                else
                {
                    cg.WriteConfig("2");
                }
                ck.SetSelected(ck.isSelect);
            }
            catch (Exception ex)
            {
                untils.UcomUntils.ConsoleMsg("设置自动登录异常：" + ex.Message.ToString());
            }
        }

        void check_rember_MouseUp(object sender, MouseEventArgs e)
        {
            comui.checkbox.CheckBoxControl ck = (comui.checkbox.CheckBoxControl)sender;
            ck.isSelect = !ck.isSelect;
            cg = new untils.UoperaConfig(BaseConfigValue.remberPwd);
            if (ck.isSelect)
            {
                cg.WriteConfig("1");
            }
            else
            {
                cg.WriteConfig("2");
            }
            ck.SetSelected(ck.isSelect);
        }

        void pic_close_MouseUp(object sender, MouseEventArgs e)
        {
            //修正不是从主界面退出 修正状态为2
            untils.UoperaConfig cv = new untils.UoperaConfig(BaseConfigValue.IsExitFromMain);
            cv.WriteConfig("2");
            this.Close();
        }

        void pic_min_MouseUp(object sender, MouseEventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        public void ShowForm(BaseEnum.LoginTypeEnum loginType)
        {
            switch (loginType)
            {
                case BaseEnum.LoginTypeEnum.active:
                    panel_webproxy.BackColor = BaseColor.color_White;
                    this.panel_bottom.Controls.Clear();
                    activeC = new ActiveControl();
                    panel_bottom.Controls.Add(activeC);
                    break;
                case BaseEnum.LoginTypeEnum.login:

                    panel_bottom.BackColor = BaseColor.color_White;
                    panel_webproxy.BackColor = BaseColor.bottom_Color;
                    this.Size = new Size(this.Size.Width, 366);
                    this.panel_bottom.Controls.Clear();
                    //防止重新加载时候读取参数
                    if (loginC == null)
                    {
                        loginC = new LoginControl();
                    }
                    panel_bottom.Controls.Add(loginC);
                    check_rember.Visible = true;
                    check_autoLogin.Visible = true;
                    break;
                case BaseEnum.LoginTypeEnum.wait:
                    this.panel_bottom.Controls.Clear();
                    waitC = new WaitControl();
                    panel_bottom.Controls.Add(waitC);
                    break;
                case BaseEnum.LoginTypeEnum.none:
                    this.Hide();
                    break;
                default:
                    break;
            }
        }
        private void Init()
        {
            BaseValue.drawFormatTitle.Alignment = StringAlignment.Center;
            BaseValue.drawFormatTitle.LineAlignment = StringAlignment.Center;

            BaseValue.drawFormatTopTitle.Alignment = StringAlignment.Center;


            BaseValue.drawFormatLeft.Alignment = StringAlignment.Near;
            BaseValue.drawFormatLeft.LineAlignment = StringAlignment.Near;

            BaseValue.drawFormatRight.Alignment = StringAlignment.Far;
            BaseValue.drawFormatRight.LineAlignment = StringAlignment.Near;

            BaseValue.drawFormatLeftMid.Alignment = StringAlignment.Near;
            BaseValue.drawFormatLeftMid.LineAlignment = StringAlignment.Center;

            BaseValue.drawFormatLeftMid.Alignment = StringAlignment.Near;
            BaseValue.drawFormatLeftMid.LineAlignment = StringAlignment.Center;

            BaseValue.drawFormatRightMid.Alignment = StringAlignment.Far;
            BaseValue.drawFormatRightMid.LineAlignment = StringAlignment.Center;

            BaseValue.drawFormatRightFar.Alignment = StringAlignment.Far;
            BaseValue.drawFormatRightFar.LineAlignment = StringAlignment.Far;

            BaseValue.drawFormatLeftFar.Alignment = StringAlignment.Near;
            BaseValue.drawFormatLeftFar.LineAlignment = StringAlignment.Far;

            BaseValue.drawFormatLeftFar.Alignment = StringAlignment.Far;
            BaseValue.drawFormatLeftFar.LineAlignment = StringAlignment.Center;


            BaseValue.app_id = config.ReadConfig();
            BaseValue.merchant_code = code_config.ReadConfig();

            try
            {
                BaseModel.baseLog = new untils.UwriteLogUntils();
                BaseModel.baseLog.start();
                //BaseModel.baseLog.writeLog("收银插件自启动...");
            }
            catch (Exception ex)
            {

                MessageBox.Show("日志类启动异常" + ex.Message.ToString());
            }

            //设置直接退出
            try
            {
                cg = new untils.UoperaConfig(BaseConfigValue.exitOrMin);
                if (cg.ReadConfig() != "") { }
                else
                {
                    cg.WriteConfig("1");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("设置自动退出异常" + ex.Message.ToString());
            }
        }

        private void panel_webproxy_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.DrawString(right_bottom_tips, new Font(BaseValue.baseFont, 9), new SolidBrush(BaseColor.deep_Gray), rect_right_bottom_tips, BaseValue.drawFormatRightMid);
        }

        private void panel_top_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.DrawString(top_title, new Font(BaseValue.baseFont, 11, FontStyle.Bold), new SolidBrush(BaseColor.color_White), rect_top_title, BaseValue.drawFormatLeftMid);
        }

        private void LoginBaseForm_Load(object sender, EventArgs e)
        {
            try
            {
                Init();
                check_rember = new comui.checkbox.CheckBoxControl();
                cg = new untils.UoperaConfig(BaseConfigValue.remberPwd);
                if (cg.ReadConfig() == "1")
                {
                    check_rember.SetSelected(true);
                }
                check_rember.tips = "记住账号";
                check_rember.Visible = false;
                check_rember.Size = new System.Drawing.Size(check_rember.Size.Width - 10, check_rember.Size.Height);
                check_rember.Location = new Point(14, (this.panel_webproxy.ClientRectangle.Height - check_rember.Height) / 2);
                check_rember.MouseUp += check_rember_MouseUp;
                this.panel_webproxy.Controls.Add(check_rember);

                cg = new untils.UoperaConfig(BaseConfigValue.autoLogin);
                check_autoLogin = new comui.checkbox.CheckBoxControl();
                check_autoLogin.tips = "自动登录";
                if (cg.ReadConfig() == "1")
                {
                    check_autoLogin.SetSelected(true);
                }
                check_autoLogin.Visible = false;
                check_autoLogin.Size = check_rember.Size;
                check_autoLogin.MouseUp += check_autoLogin_MouseUp;
                check_autoLogin.Location = new Point(check_rember.Location.X + check_rember.Width + 5, (this.panel_webproxy.ClientRectangle.Height - check_rember.Height) / 2);
                this.panel_webproxy.Controls.Add(check_autoLogin);
                //appid_不为空自动登录
                if (config.ReadConfig() != "")
                {
                    ShowForm(BaseEnum.LoginTypeEnum.login);
                }
                else
                {
                    ShowForm(BaseEnum.LoginTypeEnum.active);
                }
            }
            catch (Exception ex)
            {
                untils.UcomUntils.ConsoleMsg("界面异常信息：" + ex.Message.ToString() + ";界面异常对象：" + ex.Source.ToString() + ";界面调用堆在：" + ex.StackTrace.ToString() + ";界面触发方法：" + ex.TargetSite.ToString());
            }
        }

        private void panel_webproxy_MouseUp(object sender, MouseEventArgs e)
        {
            if (rect_right_bottom_tips.Contains(e.Location))
            {
                SetWebProxyForm form = new SetWebProxyForm();
                form.TopMost = true;
                comui.MaskForm mask = new comui.MaskForm(form);
                mask.ShowDialog();
            }
        }

        private void panel_bottom_Paint(object sender, PaintEventArgs e)
        {

        }
        public void SetWindowRegion()
        {
            System.Drawing.Drawing2D.GraphicsPath FormPath;
            FormPath = new System.Drawing.Drawing2D.GraphicsPath();
            Rectangle rect = new Rectangle(0, 0, ClientRectangle.Width, ClientRectangle.Height);
            FormPath = GetRoundedRectPath(rect, 8);
            this.Region = new Region(FormPath);
        }
        private GraphicsPath GetRoundedRectPath(Rectangle rect, int radius)
        {
            int diameter = 10;
            Rectangle arcRect = new Rectangle(rect.Location, new Size(diameter, diameter));
            GraphicsPath path = new GraphicsPath();

            path.AddArc(arcRect, 180, 90);//左上角

            arcRect.X = rect.Right - diameter;//右上角
            path.AddArc(arcRect, 270, 90);

            arcRect.Y = rect.Bottom - diameter;// 右下角
            path.AddArc(arcRect, 0, 90);

            arcRect.X = rect.Left;// 左下角
            path.AddArc(arcRect, 90, 90);
            path.CloseFigure();
            return path;
        }

        //设置窗体圆角
        private void LoginBaseForm_SizeChanged(object sender, EventArgs e)
        {
            try
            {
                SetWindowRegion();
            }
            catch (Exception ex)
            {
                untils.UcomUntils.ConsoleMsg("设置窗体圆角异常："+ex.Message.ToString());
            }       
        }
    }
}
