using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace qgj
{
    public partial class ProxySetControl : UserControl
    {
        TextBox proxyTextBox = new TextBox();
        Label lbInfo = new Label();
        Label lbProxySave = new Label();

        loadconfigClass lcc = new loadconfigClass("proxy");
        public ProxySetControl()
        {
            InitializeComponent();
            BackColor = Defcolor.MainBackColor;
            proxyTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            proxyTextBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            proxyTextBox.SetBounds(10, 13, 100, 25);
            Controls.Add(proxyTextBox);
            lbInfo.Text = "输入代理服务器地址及端口号（例如：127.0.0.1:8080）";
            lbInfo.BackColor = Defcolor.MainBackColor;
            lbInfo.Font = new System.Drawing.Font(UserClass.fontName, 9);
            lbInfo.ForeColor = Color.Gray;
            lbInfo.SetBounds(5, 35, 350, 35);
            Controls.Add(lbInfo);

            lbProxySave.Text = "保存";
            lbProxySave.Font = new System.Drawing.Font(UserClass.fontName, 8, FontStyle.Underline);
            lbProxySave.ForeColor = Defcolor.FontBlueColor;
            lbProxySave.BackColor = Defcolor.MainBackColor;
            lbProxySave.Location = new Point(150, 12);
            lbProxySave.MouseUp += new MouseEventHandler(proxy_MouseUp);
            Controls.Add(lbProxySave);
        }

        private void ProxySetControl_Load(object sender, EventArgs e)
        {
            string ip = lcc.readfromConfig();
            if (ip != "")
            {
                proxyTextBox.Text = ip;
            }
        }

        private void ProxySetControl_Paint(object sender, PaintEventArgs e)
        {
            PublicMethods.FillRoundRectangle(new Rectangle(5, 9, 150, 30), e.Graphics, 7, Color.White);
            PublicMethods.FrameRoundRectangle(new Rectangle(5, 9, 150, 30), e.Graphics, 7, Defcolor.MainGrayLineColor);
        }
        private void proxy_MouseUp(object sender, MouseEventArgs e)
        {
            string _sProxy = proxyTextBox.Text.Trim();
            string _sIp = (_sProxy.Split(':'))[0];
            if (System.Text.RegularExpressions.Regex.IsMatch(_sIp, "^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$") || _sIp == "")
            {
                lcc.writetoConfig(_sProxy.ToString().Trim());
            }
            else
            {
                errorinformationForm errorF = new errorinformationForm("错误(ip)", "不合法的ip地址");
                errorF.TopMost = true;
                errorF.StartPosition = FormStartPosition.CenterParent;
                errorF.ShowDialog();
                Parent.Refresh();
            }
        }
    }
}
