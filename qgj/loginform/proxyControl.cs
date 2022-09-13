using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Net;

namespace qgj
{
    public partial class proxyControl : UserControl
    {
        logininputControl codeinputC = new logininputControl(InType.actcode);
        loginactivatebuttonControl activateC = new loginactivatebuttonControl("保        存");
        Label infoLabel = new Label();

        loadconfigClass lcc = new loadconfigClass("proxy");
        public proxyControl()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            InitializeComponent();

            BackColor = Defcolor.MainBackColor;

            codeinputC.Location = new Point((ClientRectangle.Width - codeinputC.Width) / 2, 135);
            codeinputC.TabStop = false;
            Controls.Add(codeinputC);

            infoLabel.Text = "输入代理服务器地址及端口号（例如：127.0.0.1:8080）";
            infoLabel.BackColor = Defcolor.MainBackColor;
            infoLabel.Font = new System.Drawing.Font(UserClass.fontName, 10);
            infoLabel.ForeColor = Defcolor.FontLiteGrayColor;
            infoLabel.SetBounds(75, 180, 360, 35);
            Controls.Add(infoLabel);

            activateC.Location = new Point((ClientRectangle.Width - activateC.Width) / 2, 250);
            activateC.MouseUp += new MouseEventHandler(proxy_MouseUp);
            Controls.Add(activateC);
        }
        private void proxyControl_Load(object sender, EventArgs e)
        {
            string ip = lcc.readfromConfig();
            if (ip != "") 
            {
                codeinputC.fnSetValue(ip);
            }
            else
            {
                ActiveControl = codeinputC;
                codeinputC.Focus();
            }
            
        }
        private void proxyControl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
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

        private void proxy_MouseUp(object sender, MouseEventArgs e)
        {
            string _sProxy = codeinputC.fnGetValue();
            string _sIp = (_sProxy.Split(':'))[0];
            //if (System.Text.RegularExpressions.Regex.IsMatch(_sIp, "^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$") || _sIp == "")
            try{
                lcc.writetoConfig(_sProxy.ToString().Trim());
                loadconfigClass lcctemp = new loadconfigClass("terminal_sn");
                if (lcctemp.readfromConfig() == "")
                {
                    ((loginForm)Parent).fnShowControls(lgFormType.activate);
                }
                else
                {
                    ((loginForm)Parent).fnShowControls(lgFormType.login);
                }
            }
            catch{ }/*
            else
            {
                errorinformationForm errorF = new errorinformationForm("错误(ip)", "不合法的ip地址");
                errorF.TopMost = true;
                errorF.StartPosition = FormStartPosition.CenterParent;
                errorF.ShowDialog();
                ((loginForm)Parent).Refresh();
            }*/
        }
        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        {
            if (keyData == Keys.Tab)
            {
                if (!codeinputC.tbxValue.Focused)
                {
                    codeinputC.Focus();
                }
                return true;
            }
            else if (keyData == Keys.Enter)
            {
                proxy_MouseUp(null, null);
                return true;
            }
            return base.ProcessCmdKey(ref msg, keyData);
        }

    }
}
