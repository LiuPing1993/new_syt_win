using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace qgj
{
    public partial class toptitleControl : UserControl
    {
        public Label lblGather = new Label();
        public Label lblDetail = new Label();
        public Label lblmember = new Label();
        public Label lblStore = new Label();
        public Label lblCoupon = new Label();

        PictureBox pbxLogo = new PictureBox();
        PictureBox pbxAnewLogin = new PictureBox();
        PictureBox pbxSet = new PictureBox();
        PictureBox pbxMinimize = new PictureBox();
        PictureBox pbxClose = new PictureBox();

        public topitem nowselectItem = topitem.cashier;

        public toptitleControl()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);

            InitializeComponent();

            #region 控件初始化
            BackColor = Defcolor.MainRadColor;
            lblGather.BackColor = Defcolor.MainRadColor;
            lblGather.Text = "收款";
            lblGather.Font = new Font(UserClass.fontName, 14);
            lblGather.ForeColor = Defcolor.FontTopSelect;
            lblGather.SetBounds(160, (ClientRectangle.Height - 25) / 2, 60, 30);
            lblGather.MouseUp += new MouseEventHandler(topGatherLabel_click);
            lblGather.MouseEnter += new EventHandler(topLabel_MouseEnter);
            lblGather.MouseLeave += new EventHandler(topLabel_MouseLeave);
            this.Controls.Add(lblGather);

            lblDetail.BackColor = Defcolor.MainRadColor;
            lblDetail.Text = "明细";
            lblDetail.Font = new Font(UserClass.fontName, 14);
            lblDetail.ForeColor = Defcolor.FontTopNotSselect;
            lblDetail.SetBounds(230, (ClientRectangle.Height - 25) / 2, 60, 30);
            lblDetail.MouseUp += new MouseEventHandler(topDetailLabel_click);
            lblDetail.MouseEnter += new EventHandler(topLabel_MouseEnter);
            lblDetail.MouseLeave += new EventHandler(topLabel_MouseLeave);
            this.Controls.Add(lblDetail);

            lblStore.BackColor = Defcolor.MainRadColor;
            lblStore.Text = "储值";
            lblStore.Font = new Font(UserClass.fontName, 14);
            lblStore.ForeColor = Defcolor.FontTopNotSselect;
            lblStore.SetBounds(300, (ClientRectangle.Height - 25) / 2, 60, 30);
            lblStore.MouseUp += new MouseEventHandler(topStoreLabel_click);
            lblStore.MouseEnter += new EventHandler(topLabel_MouseEnter);
            lblStore.MouseLeave += new EventHandler(topLabel_MouseLeave);
            this.Controls.Add(lblStore);

            lblmember.BackColor = Defcolor.MainRadColor;
            lblmember.Text = "会员";
            lblmember.Font = new Font(UserClass.fontName, 14);
            lblmember.ForeColor = Defcolor.FontTopNotSselect;
            lblmember.SetBounds(370, (ClientRectangle.Height - 25) / 2, 60, 30);
            lblmember.MouseUp += new MouseEventHandler(topMemberLabel_click);
            lblmember.MouseEnter += new EventHandler(topLabel_MouseEnter);
            lblmember.MouseLeave += new EventHandler(topLabel_MouseLeave);
            this.Controls.Add(lblmember);

            lblCoupon.BackColor = Defcolor.MainRadColor;
            lblCoupon.Text = "验券";
            lblCoupon.Font = new Font(UserClass.fontName, 14);
            lblCoupon.ForeColor = Defcolor.FontTopNotSselect;
            lblCoupon.SetBounds(440, (ClientRectangle.Height - 25) / 2, 60, 30);
            lblCoupon.MouseUp += new MouseEventHandler(topCouponLabel_click);
            lblCoupon.MouseEnter += new EventHandler(topLabel_MouseEnter);
            lblCoupon.MouseLeave += new EventHandler(topLabel_MouseLeave);
            this.Controls.Add(lblCoupon);

            pbxLogo.BackColor = Defcolor.MainRadColor;
            pbxLogo.BackgroundImage = Properties.Resources.qxtitle;
            pbxLogo.BackgroundImageLayout = ImageLayout.Zoom;
            pbxLogo.SetBounds(10, 10, 100, 25);
            this.Controls.Add(pbxLogo);


            pbxAnewLogin.Name = "PICBanewlogin";
            pbxAnewLogin.BackColor = Defcolor.MainRadColor;
            pbxAnewLogin.BackgroundImage = Properties.Resources.relogin;
            pbxAnewLogin.BackgroundImageLayout = ImageLayout.Zoom;
            pbxAnewLogin.MouseUp += new MouseEventHandler(anewlogin_MouseUp);
            pbxAnewLogin.MouseEnter += new EventHandler(anewlogin_MouseEnter);
            pbxAnewLogin.MouseLeave += new EventHandler(anewlogin_MouserLeave);
            pbxAnewLogin.MouseDown += new MouseEventHandler(anewlogin_MouseDown);
            pbxAnewLogin.SetBounds(650, 0, 30, 45);
            this.Controls.Add(pbxAnewLogin);

            pbxSet.Name = "PICBsetting";
            pbxSet.BackColor = Defcolor.MainRadColor;
            pbxSet.BackgroundImage = Properties.Resources.setting;
            pbxSet.BackgroundImageLayout = ImageLayout.Zoom;
            pbxSet.MouseUp += new MouseEventHandler(setting_MouseUp);
            pbxSet.MouseEnter += new EventHandler(setting_MouseEnter);
            pbxSet.MouseLeave += new EventHandler(setting_MouserLeave);
            pbxSet.MouseDown += new MouseEventHandler(setting_MouseDown);
            pbxSet.SetBounds(680, 0, 30, 45);
            this.Controls.Add(pbxSet);

            pbxMinimize.Name = "PICBminimize";
            pbxMinimize.BackColor = Defcolor.MainRadColor;
            pbxMinimize.BackgroundImage = Properties.Resources.minimize;
            pbxMinimize.BackgroundImageLayout = ImageLayout.Zoom;
            pbxMinimize.MouseUp += new MouseEventHandler(minimize_MouseUp);
            pbxMinimize.MouseEnter += new EventHandler(minimize_MouseEnter);
            pbxMinimize.MouseLeave += new EventHandler(minimize_MouserLeave);
            pbxMinimize.MouseDown += new MouseEventHandler(minimize_MouseDown);
            pbxMinimize.SetBounds(710, 0, 30, 45);
            this.Controls.Add(pbxMinimize);

            pbxClose.Name = "PICBclose";
            pbxClose.BackColor = Defcolor.MainRadColor;
            pbxClose.BackgroundImage = Properties.Resources.close;
            pbxClose.BackgroundImageLayout = ImageLayout.None;
            pbxClose.MouseUp += new MouseEventHandler(close_MouseUp);
            pbxClose.MouseEnter += new EventHandler(close_MouseEnter);
            pbxClose.MouseLeave += new EventHandler(close_MouserLeave);
            pbxClose.MouseDown += new MouseEventHandler(close_MouseDown);
            pbxClose.SetBounds(740, 0, 50, 45);
            this.Controls.Add(pbxClose);
            #endregion
        }

        private void ToptitleControl_Load(object sender, EventArgs e)
        {


        }

        private void ToptitleControl_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle,
                Defcolor.MainRadColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainRadColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainRadColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainRadColor, 1, ButtonBorderStyle.Solid
            );
        }

        public void topGatherLabel_click(object sender, MouseEventArgs e)
        {
            ((main)Parent).fnShowGatherC();
        }
        public void topDetailLabel_click(object sender, MouseEventArgs e)
        {
            ((main)Parent).fnShowDetailC();
        }
        public void topStoreLabel_click(object sender, MouseEventArgs e)
        {
            ((main)Parent).fnShowStoreC();
        }
        public void topMemberLabel_click(object sender, MouseEventArgs e)
        {
            ((main)Parent).fnShowMemberC();
        }
        public void topCouponLabel_click(object sender, MouseEventArgs e)
        {
            ((main)Parent).fnShowCouponC();
        }
        private void topLabel_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                Label lb = (Label)sender;
                if(lb.ForeColor == Defcolor.FontTopNotSselect)
                {
                    lb.ForeColor = Defcolor.FontTopEnter;
                }
            }
            catch { }
        }
        private void topLabel_MouseLeave(object sender, EventArgs e)
        {
            try
            {
                Label lb = (Label)sender;
                if (lb.ForeColor == Defcolor.FontTopEnter)
                {
                    lb.ForeColor = Defcolor.FontTopNotSselect;
                }
            }
            catch { }
        }
        #region 功能键鼠标相关事件
        //重新登录鼠标事件
        private void anewlogin_MouseEnter(object sender, EventArgs e)
        {
            pbxAnewLogin.BackgroundImage = Properties.Resources.reloginMouseIn;
        }
        private void anewlogin_MouserLeave(object sender, EventArgs e)
        {
            pbxAnewLogin.BackgroundImage = Properties.Resources.relogin;
        }
        private void anewlogin_MouseDown(object sender, MouseEventArgs e)
        {
            //anewloginPictureBox.BackgroundImage = Properties.Resources.reloginMouseDown;
        }
        private void anewlogin_MouseUp(object sender, MouseEventArgs e)
        {
            branchForm branchF = new branchForm("重新登录", "是否退出当前登录，回到登录界面？");
            branchF.TopMost = true;
            branchF.StartPosition = FormStartPosition.CenterParent;
            branchF.ShowDialog();
            if (branchF.DialogResult == DialogResult.OK)
            {
                UserClass.IsFirstLogin = false;
                ((main)Parent).suspendedF.Close();
                ((main)Parent).suspendedF.Dispose();
                ((main)Parent).Dispose();
            }
            else
            {
                ((main)Parent).Refresh();
            }
        }

        //设定鼠标事件
        private void setting_MouseEnter(object sender, EventArgs e)
        {
            pbxSet.BackgroundImage = Properties.Resources.settingMouseIn;
        }
        private void setting_MouserLeave(object sender, EventArgs e)
        {
            pbxSet.BackgroundImage = Properties.Resources.setting;
        }
        private void setting_MouseDown(object sender, MouseEventArgs e)
        {
            //settingPictureBox.BackgroundImage = Properties.Resources.settingMouseDown;
        }
        private void setting_MouseUp(object sender, MouseEventArgs e)
        {
            pbxSet.BackgroundImage = Properties.Resources.setting;
            Application.DoEvents();

            UserClass.IsMain = false;
            SetForm settingF = new SetForm((main)Parent);
            settingF.TopMost = true;
            settingF.StartPosition = FormStartPosition.CenterParent;
            settingF.ShowDialog();
            ((main)Parent).Refresh();

            loadconfigClass lcc;
            lcc = new loadconfigClass("usekeyboard");
            if (lcc.readfromConfig() == "true")
            {
                UserClass.isUseKeyBorad = true;
            }
            else
            {
                UserClass.isUseKeyBorad = false;
            }
            lcc = new loadconfigClass("fastpayway");
            UserClass.fastPayWay = lcc.readfromConfig();
        }

        //最小化鼠标事件
        private void minimize_MouseEnter(object sender, EventArgs e)
        {
            pbxMinimize.BackgroundImage = Properties.Resources.minimizeMouseIn;
        }
        private void minimize_MouserLeave(object sender, EventArgs e)
        {
            pbxMinimize.BackgroundImage = Properties.Resources.minimize;
        }
        private void minimize_MouseDown(object sender, MouseEventArgs e)
        {
            //minimizePictureBox.BackgroundImage = Properties.Resources.minimizeMousedown;
        }
        private void minimize_MouseUp(object sender, MouseEventArgs e)
        {
            pbxMinimize.BackgroundImage = Properties.Resources.minimize;
            ((main)Parent).gatherC.reloadPayWay(true);
            Application.DoEvents();
            ((main)Parent).WindowState = FormWindowState.Minimized;
        }

        //关闭鼠标事件
        private void close_MouseEnter(object sender, EventArgs e)
        {
            pbxClose.BackgroundImage = Properties.Resources.closeMouseIn;
            pbxClose.BackColor = Color.FromArgb(237, 28, 36);
        }
        private void close_MouserLeave(object sender, EventArgs e)
        {
            pbxClose.BackgroundImage = Properties.Resources.close;
            pbxClose.BackColor = Color.FromArgb(212, 60, 51);
        }
        private void close_MouseDown(object sender, MouseEventArgs e)
        {
            //closePictureBox.BackgroundImage = Properties.Resources.closeMouseDown;
            //closePictureBox.BackColor = Color.FromArgb(136, 0, 21);
        }
        public void close_MouseUp(object sender, MouseEventArgs e)
        {
            loadconfigClass lcc = new loadconfigClass("exit");
            string _exit = lcc.readfromConfig();
            if (_exit == "")
            {
                branchForm branchF = new branchForm("退出", "退出当前系统？");
                branchF.TopMost = true;
                branchF.StartPosition = FormStartPosition.CenterParent;
                branchF.ShowDialog();
                if (branchF.DialogResult == DialogResult.OK)
                {
                    ((main)Parent).notifyIcon.Dispose();
                    PublicMethods.exitSystem();
                }
                else if(branchF.DialogResult == DialogResult.No)
                {
                    ((main)Parent).gatherC.reloadPayWay(true);
                    ((main)Parent).Hide();
                }
                else
                {
                    ((main)Parent).Refresh();
                }
            }
            else if (_exit == "true")
            {
                branchForm branchF = new branchForm("关闭", "是否退出系统？");
                branchF.TopMost = true;
                branchF.StartPosition = FormStartPosition.CenterParent;
                branchF.ShowDialog();
                if (branchF.DialogResult == DialogResult.OK)
                {
                    ((main)Parent).notifyIcon.Dispose();
                    PublicMethods.exitSystem();
                }
                else
                {
                    ((main)Parent).Refresh();
                }
            }
            else
            {
                ((main)Parent).gatherC.reloadPayWay(true);
                ((main)Parent).Hide();
            }

        }
        #endregion
    }
}
