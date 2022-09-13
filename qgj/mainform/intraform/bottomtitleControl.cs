using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace qgj
{
    public partial class bottomtitleControl : UserControl
    {
        Label lblMerchant = new Label();
        Label lblStore = new Label();
        Label lblEmployee = new Label();
        public bottomtitleControl()
        {
            InitializeComponent();
        }
        private void BottomtitleControl_Load(object sender, EventArgs e)
        {
            try
            {
                versionLabel.Text = System.Reflection.Assembly.GetAssembly(this.GetType()).GetName().Version.ToString();
                if (Url.url.IndexOf("test") != -1)
                {
                    versionLabel.Text += " T";
                }
                else if (Url.url.IndexOf("master") != -1)
                {
                    versionLabel.Text += " M";
                }
                else if (Url.url.IndexOf("qinguanjia") == -1)
                {
                    versionLabel.Text += " IP";
                }
                else
                {
                    versionLabel.Text += " R";
                }
                versionLabel.MouseUp += new MouseEventHandler(clearLog_MouseUp);
            }
            catch { }

            lblMerchant.Text = "商家 : " + UserClass.Merchant;
            lblStore.Text = "门店 : " + UserClass.Store;
            lblEmployee.Text = "收银员 : " + UserClass.Employee;

            lblEmployee.Font = new System.Drawing.Font(UserClass.fontName, 9);
            lblEmployee.AutoSize = true;
            lblEmployee.ForeColor = Defcolor.FontLiteGrayColor;
            lblEmployee.Location = new Point(5, 5);
            Controls.Add(lblEmployee);

            lblMerchant.Font = new System.Drawing.Font(UserClass.fontName, 9);
            lblMerchant.AutoSize = true;
            lblMerchant.ForeColor = Defcolor.FontLiteGrayColor;
            lblMerchant.Location = new Point(lblEmployee.Right + 10, 5);
            Controls.Add(lblMerchant);

            lblStore.Font = new System.Drawing.Font(UserClass.fontName, 9);
            lblStore.AutoSize = true;
            lblStore.ForeColor = Defcolor.FontLiteGrayColor;
            lblStore.Location = new Point(lblMerchant.Right + 10, 5);
            Controls.Add(lblStore);

            BackColor = Defcolor.BottomBackColor;
        }
        private void BottomtitleControl_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, ClientRectangle,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid
            );
        }
        public void clearLog_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if (System.IO.Directory.Exists(logPath.paipaiLogFilePath))
                {
                    System.IO.DirectoryInfo di = new System.IO.DirectoryInfo(logPath.paipaiLogFilePath);
                    di.Delete(true);
                    errorinformationForm errorF = new errorinformationForm("提示", "已经清除派派小盒日志文件");
                    errorF.TopMost = true;
                    errorF.StartPosition = FormStartPosition.CenterScreen;
                    errorF.ShowDialog();
                    ((main)Parent).Refresh();
                }
            }
            catch { }
        }
    }
}
