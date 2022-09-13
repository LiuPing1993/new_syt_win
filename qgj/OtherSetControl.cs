using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace qgj
{
    public partial class OtherSetControl : UserControl
    {
        bool IsPaipai = false;
        bool HasPaipai = false;
        HotKeySetControl HotKeySetC = new HotKeySetControl(2);

        Label lblTitle = new Label();

        public OtherSetControl()
        {
            InitializeComponent();
            BackColor = Defcolor.MainBackColor;
            this.HotKeySetC.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(242)))), ((int)(((byte)(242)))));
            this.HotKeySetC.Location = new System.Drawing.Point(35, 68);
            this.HotKeySetC.Name = "HotKeySetC";
            this.HotKeySetC.Size = new System.Drawing.Size(420, 70);
            this.HotKeySetC.TabIndex = 0;
            this.Controls.Add(HotKeySetC);

            lblTitle.Text = "日汇总打印快捷键";
            lblTitle.SetBounds(37, 30,420,30);
            lblTitle.Font = new Font(UserClass.fontName, 9);
            this.Controls.Add(lblTitle);

            HasPaipai = fnIsHasPaipaiExe();
        }

        private void OtherSetControl_Load(object sender, EventArgs e)
        {
            loadconfigClass lcc = new loadconfigClass("paipai");
            if (lcc.readfromConfig() == "true" && HasPaipai)
            {
                IsPaipai = true;
                PaipaiselectC.fnSelectChange(true);
            }
            else
            {
                IsPaipai = false;
                PaipaiselectC.fnSelectChange(false);
            }
        }

        private void OtherSetControl_MouseUp(object sender, MouseEventArgs e)
        {
            lblTitle.Focus();
        }

        private void PaipaiselectC_MouseUp(object sender, MouseEventArgs e)
        {
            if (HasPaipai == false)
            {
                IsPaipai = false;
                PaipaiselectC.fnSelectChange(false);
                fnPaipaiSave();

                errorinformationForm errorF = new errorinformationForm("提示", "缺少派派小盒必需的组件，将进行安装");
                errorF.TopMost = true;
                errorF.StartPosition = FormStartPosition.CenterParent;
                errorF.ShowDialog();
                Refresh();

                DownloadForm DownLoadF = new DownloadForm(downType.paipai);
                DownLoadF.TopMost = true;
                DownLoadF.StartPosition = FormStartPosition.CenterParent;
                DownLoadF.ShowDialog();
                Refresh();

                if (DownLoadF.DialogResult == DialogResult.OK)
                {
                    HasPaipai = fnIsHasPaipaiExe();
                }
                else
                {
                    try
                    {
                        DirectoryInfo di = new DirectoryInfo(download.packagePath);
                        di.Delete(true);
                    }
                    catch { }
                    return;
                }
            }
            if (IsPaipai)
            {
                IsPaipai = false;
                PaipaiselectC.fnSelectChange(false);
            }
            else
            {
                IsPaipai = true;
                PaipaiselectC.fnSelectChange(true);
            }
            fnPaipaiSave();
        }
        /// <summary>
        /// 判断派派小盒本地程序是否存在
        /// </summary>
        /// <returns>bool 是否存在</returns>
        private bool fnIsHasPaipaiExe()
        {
            if (!File.Exists(download.paipaiExeFilePath))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        /// <summary>
        /// 保存是否使用派派小盒
        /// </summary>
        private void fnPaipaiSave()
        {
            loadconfigClass _lcc = new loadconfigClass("paipai");
            if (IsPaipai)
            {
                _lcc.writetoConfig("true");
            }
            else
            {
                _lcc.writetoConfig("false");
            }
        }

        private void lblPaipaiInfo_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                DirectoryInfo di = new DirectoryInfo(download.paiaiPath);
                di.Delete(true);
                IsPaipai = false;
                PaipaiselectC.fnSelectChange(false);
                fnPaipaiSave();
            }
            catch { }
        }
    }
}
