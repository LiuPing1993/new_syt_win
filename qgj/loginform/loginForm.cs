using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Imaging;

namespace qgj
{
    public partial class loginForm : Form
    {
        //SkinForm skin;
        loginControl loginC = new loginControl();
        activateControl activateC = new activateControl();
        public loginwaitControl waitC = new loginwaitControl();
        public proxyControl proxyC = new proxyControl();
        Rectangle rectClose = new Rectangle(466, 10, 20, 20);
        public loginForm()
        {
            loadconfigClass config = new loadconfigClass("font");
            string font = config.readfromConfig();
            if (font != "" && font == "songti")
            {
                UserClass.fontName = "宋体";
            }

            keyboardClass.showKeyBoard();
            //NativeMethods.SetClassLong(this.Handle, NativeMethods.GCL_STYLE, NativeMethods.GetClassLong(this.Handle, NativeMethods.GCL_STYLE) | NativeMethods.CS_DropSHADOW);
            InitializeComponent();
            try
            {
                Rectangle _clientRectangle = ClientRectangle;
                Point _clientPoint = PointToScreen(new Point(0, 0));
                _clientRectangle.Offset(_clientPoint.X - Left, _clientPoint.Y - Top);
                Region = new Region(_clientRectangle);
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.ToString());
                FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            }
            loginC.MouseDown += new MouseEventHandler(loginForm_MouseDown);
            loginC.Location = new Point(0, 0);
            Controls.Add(loginC);

            activateC.MouseDown += new MouseEventHandler(loginForm_MouseDown);
            activateC.Location = new Point(0, 0);
            Controls.Add(activateC);

            waitC.MouseUp += new MouseEventHandler(loginForm_MouseDown);
            waitC.Location = new Point(0, 0);
            Controls.Add(waitC);

            proxyC.MouseUp += new MouseEventHandler(loginForm_MouseDown);
            proxyC.Location = new Point(0, 0);
            Controls.Add(proxyC);

            loadconfigClass _lcc = new loadconfigClass("app_id");
            string app_id = _lcc.readfromConfig();
            if (app_id == "")
            {
                fnShowControls(lgFormType.activate);
            }
            else
            {
                _lcc = new loadconfigClass("merchant_code");
                BaseValue.merchant_code = _lcc.readfromConfig();
                BaseValue.app_id = app_id;
                fnShowControls(lgFormType.login);
            }
            TopMost = true;
            StartPosition = FormStartPosition.CenterScreen;
        }

        public void fnShowControls(lgFormType _type)
        {
            if (_type == lgFormType.activate)
            {
                loginC.Hide();
                activateC.Show();
                waitC.Hide();
                proxyC.Hide();
            }
            else if(_type == lgFormType.login)
            {
                loginC.Show();
                activateC.Hide();
                waitC.Hide();
                proxyC.Hide();
            }
            else if (_type == lgFormType.proxy)
            {
                loginC.Hide();
                activateC.Hide();
                waitC.Hide();
                proxyC.Show();
            }
            else
            {
                loginC.Hide();
                activateC.Hide();
                waitC.Show();
            }
        }


        private void loginForm_Paint(object sender, PaintEventArgs e)
        {
            
        }
        protected override void OnMove(EventArgs e)
        {
            Refresh();
        }
        private void loginForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (rectClose.Contains(e.Location))
            {
                PublicMethods.exitSystem();
            }
            NativeMethods.ReleaseCapture();
            NativeMethods.SendMessage((int)this.Handle, NativeMethods.WM_NCLBUTTONDOWN, NativeMethods.HTCAPTION, 0);
        }
        private void loginForm_Load(object sender, EventArgs e)
        {
            UserClass.ppbC.startPaipaiBox();//启动派派小盒程序
            Activate();
        }
        //protected override void OnVisibleChanged(EventArgs e)
        //{
        //    if (Visible)
        //    {
        //        //启用窗口淡入淡出
        //        if (!DesignMode)
        //        {
        //            //淡入特效
        //            Win32.AnimateWindow(this.Handle, 150, Win32.AW_BLEND | Win32.AW_ACTIVATE);
        //        }
        //        //判断不是在设计器中
        //        if (!DesignMode && skin == null)
        //        {
        //            skin = new SkinForm(this);
        //            skin.Show(this);
        //        }
        //        base.OnVisibleChanged(e);
        //    }
        //    else
        //    {
        //        base.OnVisibleChanged(e);
        //        Win32.AnimateWindow(this.Handle, 150, Win32.AW_BLEND | Win32.AW_HIDE);
        //    }
        //}
    }
}
