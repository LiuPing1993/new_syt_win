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
    public partial class JointSetControl : UserControl
    {
        loadconfigClass before_time = new loadconfigClass("before_time");

        bool IsAutoGet = false;
        bool IsRealTime = false;
        bool IsScanComfirm = false;
        bool IsOnlyBar = false;
        bool IsAllScan = false;

        beforeControl beforeC = new beforeControl();
        beforeMouseControl beforeMouseC = new beforeMouseControl();

        public JointSetControl()
        {
            InitializeComponent();
            BackColor = Defcolor.MainBackColor;
        }

        private void JointSetControl_Load(object sender, EventArgs e)
        {
            beforeC.Location = new Point(275, 78);
            beforeMouseC.Location = new Point(275, 78);
            Controls.Add(beforeC);
            beforeC.Hide();
            Controls.Add(beforeMouseC);
            beforeMouseC.Hide();
            beforetimeTbx.Hide();
            delayLabel.Hide();

            cust_detail.Hide();
            showRealTime(false);

            beforetimeTbx.Text = before_time.readfromConfig();

            loadconfigClass lcc = new loadconfigClass("recognitionType");
            if (lcc.readfromConfig() == "ocr")
            {
                IsAutoGet = true;
                AutoGetSelectC.fnSelectChange(true);
                cbbAutoWay.SelectedIndex = 0;
                showRealTime(true);
            }
            else if(lcc.readfromConfig() == "text")
            {
                IsAutoGet = true;
                AutoGetSelectC.fnSelectChange(true);
                cbbAutoWay.SelectedIndex = 1;
            }
            else if (lcc.readfromConfig() == "ocrex")
            {
                IsAutoGet = true;
                AutoGetSelectC.fnSelectChange(true);
                cbbAutoWay.SelectedIndex = 2;
                showRealTime(true);
            }
            else if (lcc.readfromConfig() == "clip")
            {
                IsAutoGet = true;
                AutoGetSelectC.fnSelectChange(true);
                cbbAutoWay.SelectedIndex = 3;
            }
            else if (lcc.readfromConfig() == "customer")
            {
                IsAutoGet = true;
                AutoGetSelectC.fnSelectChange(true);
                cbbAutoWay.SelectedIndex = 4;
                cust_detail.Show();
                showRealTime(true);
            }
            else
            {
                cbbAutoWay.SelectedIndex = 0;
            }

            lcc = new loadconfigClass("realtime");
            if (lcc.readfromConfig() == "true")
            {
                IsRealTime = true;
                autoWriteSelectC.fnSelectChange(true);
            }
            else
            {
                IsRealTime = false;
                autoWriteSelectC.fnSelectChange(false);
            }

            lcc = new loadconfigClass("scanComfirm");
            if (lcc.readfromConfig() == "true")
            {
                IsScanComfirm = true;
                scanComfirmSelectC.fnSelectChange(true);
            }
            else
            {
                IsScanComfirm = false;
                scanComfirmSelectC.fnSelectChange(false);
            }

            lcc = new loadconfigClass("onlyBar");
            if (lcc.readfromConfig() == "true")
            {
                IsOnlyBar = true;
                onlyBarSelectC.fnSelectChange(true);
            }
            else
            {
                IsOnlyBar = false;
                onlyBarSelectC.fnSelectChange(false);
            }

            lcc = new loadconfigClass("allscan");
            if (lcc.readfromConfig() == "true")
            {
                IsAllScan = true;
                allscanSelectC.fnSelectChange(true);
            }
            else
            {
                IsAllScan = false;
                allscanSelectC.fnSelectChange(false);
            }

            lcc = new loadconfigClass("orcfastpayway");
            if (lcc.readfromConfig() == "bar")
            {
                cbbFastPay.SelectedIndex = 1;
            }
            else if (lcc.readfromConfig() == "qr")
            {
                cbbFastPay.SelectedIndex = 2;
            }
            else if(lcc.readfromConfig() == "union")
            {
                cbbFastPay.SelectedIndex = 3;
            }
            else if (lcc.readfromConfig() == "cash")
            {
                cbbFastPay.SelectedIndex = 4;
            }
            else if (lcc.readfromConfig() == "balance")
            {
                cbbFastPay.SelectedIndex = 5;
            }
            else if (lcc.readfromConfig() == "notset")
            {
                cbbFastPay.SelectedIndex = 0;
            }
            else
            {
                cbbFastPay.SelectedIndex = 0;
            }

            lcc = new loadconfigClass("before_type");
            if (lcc.readfromConfig() == "key")
            {
                cbxBefore.SelectedIndex = 1;
            }
            else if(lcc.readfromConfig() == "mouse")
            {
                cbxBefore.SelectedIndex = 2;
            }
            else
            {
                cbxBefore.SelectedIndex = 0;
            }
        }

        public void reloadSet()
        {
            loadconfigClass _lcc = new loadconfigClass("realtime");
            if (_lcc.readfromConfig() == "true")
            {
                IsRealTime = true;
                autoWriteSelectC.fnSelectChange(true);
            }
            else
            {
                IsRealTime = false;
                autoWriteSelectC.fnSelectChange(false);
            }
        }

        private void showRealTime(bool is_show)
        {
            if(is_show)
            {
                autoWriteLabel.Show();
                autoWriteSelectC.Show();
            }
            else
            {
                autoWriteLabel.Hide();
                autoWriteSelectC.Hide();
            }
        }

        public void fnSaveHotKey()
        {
            HotKeySetC.HotKeySetControl_Leave(null, null);
        }
        private void autoget_MouseUp(object sender, MouseEventArgs e)
        {
            if (IsAutoGet)
            {
                IsAutoGet = false;
                AutoGetSelectC.fnSelectChange(false);
            }
            else
            {
                IsAutoGet = true;
                AutoGetSelectC.fnSelectChange(true);
            }
            fnAutoGetSave();
        }

        /// <summary>
        /// 保存自动获取金额设置
        /// </summary>
        private void fnAutoGetSave()
        {
            loadconfigClass _lcc = new loadconfigClass("recognitionType");
            if (IsAutoGet)
            {
                cust_detail.Hide();
                showRealTime(false);
                if (cbbAutoWay.SelectedIndex == 0)
                {
                    _lcc.writetoConfig("ocr");
                    showRealTime(true);
                }
                else if (cbbAutoWay.SelectedIndex == 1)
                {
                    _lcc.writetoConfig("text");
                }
                else if (cbbAutoWay.SelectedIndex == 2)
                {
                    _lcc.writetoConfig("ocrex");
                    showRealTime(true);
                }
                else if (cbbAutoWay.SelectedIndex == 3)
                {
                    _lcc.writetoConfig("clip");
                }
                else if (cbbAutoWay.SelectedIndex == 4)
                {
                    _lcc.writetoConfig("customer");
                    showRealTime(true);
                    cust_detail.Show();
                }
            }
            else
            {
                _lcc.writetoConfig("none");
            }
        }

        private void JointSetControl_Paint(object sender, PaintEventArgs e)
        {

        }
        /// <summary>
        /// 设置图像识别区域
        /// </summary>
        public void fnPicAreaSelect()
        {
            try
            {
                pictureClass _pc = new pictureClass();
                ((SetForm)Parent).Hide();
                ((SetForm)Parent).MainForm.Hide();
                pictureClass.Delay(250);
                AreaSelectForm _alf = new AreaSelectForm(_pc.getscreenPicture());
                pictureClass.Delay(250);
                _alf.ShowDialog();
                ((SetForm)Parent).MainForm.Show();
                ((SetForm)Parent).Show();
            }
            catch { }
        }

        public void fnAutoAreaSelect(string no)
        {
            try
            {
                pictureClass _pc = new pictureClass();
                ((SetForm)Parent).Hide();
                ((SetForm)Parent).MainForm.Hide();
                pictureClass.Delay(250);
                AutoAreaForm _alf = new AutoAreaForm(_pc.getscreenPicture(), no);
                pictureClass.Delay(250);
                _alf.ShowDialog();
                ((SetForm)Parent).MainForm.Show();
                ((SetForm)Parent).Show();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString() + e.StackTrace.ToString());
            }
        }

        private void cbbWay_SelectedIndexChanged(object sender, EventArgs e)
        {
            fnAutoGetSave();
        }

        private void cbbFastPay_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadconfigClass lcc = new loadconfigClass("orcfastpayway");
            if (cbbFastPay.SelectedIndex == 1)
            {
                lcc.writetoConfig("bar");
            }
            else if (cbbFastPay.SelectedIndex == 2)
            {
                lcc.writetoConfig("qr");
            }
            else if (cbbFastPay.SelectedIndex == 3)
            {
                lcc.writetoConfig("union");
            }
            else if (cbbFastPay.SelectedIndex == 4)
            {
                lcc.writetoConfig("cash");
            }
            else if (cbbFastPay.SelectedIndex == 5)
            {
                lcc.writetoConfig("balance");
            }
            else if (cbbFastPay.SelectedIndex == 0)
            {
                lcc.writetoConfig("notset");
            }
            else
            {
                lcc.writetoConfig("");
            }
        }

        private void autoWrite_MouseUp(object sender, MouseEventArgs e)
        {
            if (IsRealTime)
            {
                IsRealTime = false;
                autoWriteSelectC.fnSelectChange(false);
            }
            else
            {
                IsRealTime = true;
                autoWriteSelectC.fnSelectChange(true);
            }

            loadconfigClass _lcc = new loadconfigClass("realtime");
            if (IsRealTime)
            {
                _lcc.writetoConfig("true");
                loadconfigClass suspended_lcc = new loadconfigClass("showsuspended");
                suspended_lcc.writetoConfig("true");
            }
            else
            {
                _lcc.writetoConfig("false");
            }
        }

        private void lblLPTtest_Click(object sender, EventArgs e)
        {
            configform.intraControl.comSetForm comSF = new configform.intraControl.comSetForm();
            comSF.TopMost = true;
            comSF.StartPosition = FormStartPosition.CenterScreen;
            comSF.ShowDialog();
            Refresh();
        }

        private void scanComfirmSelectC_MouseUp(object sender, MouseEventArgs e)
        {
            if (IsScanComfirm)
            {
                IsScanComfirm = false;
                scanComfirmSelectC.fnSelectChange(false);
            }
            else
            {
                IsScanComfirm = true;
                scanComfirmSelectC.fnSelectChange(true);
            }

            loadconfigClass _lcc = new loadconfigClass("scanComfirm");
            if (IsScanComfirm)
            {
                _lcc.writetoConfig("true");
            }
            else
            {
                _lcc.writetoConfig("false");
            }
        }

        private void onlyBarSelectC_MouseUp(object sender, MouseEventArgs e)
        {
            if (IsOnlyBar)
            {
                IsOnlyBar = false;
                onlyBarSelectC.fnSelectChange(false);
            }
            else
            {
                IsOnlyBar = true;
                onlyBarSelectC.fnSelectChange(true);
            }

            loadconfigClass _lcc = new loadconfigClass("onlyBar");
            if (IsOnlyBar)
            {
                _lcc.writetoConfig("true");
            }
            else
            {
                _lcc.writetoConfig("false");
            }
        }

        private void allscanSelectC_MouseUp(object sender, MouseEventArgs e)
        {
            if (IsAllScan)
            {
                IsAllScan = false;
                allscanSelectC.fnSelectChange(false);
            }
            else
            {
                IsAllScan = true;
                allscanSelectC.fnSelectChange(true);
            }

            loadconfigClass _lcc = new loadconfigClass("allscan");
            if (IsAllScan)
            {
                _lcc.writetoConfig("true");
            }
            else
            {
                _lcc.writetoConfig("false");
            }
        }

        private void cbxBefore_SelectedIndexChanged(object sender, EventArgs e)
        {
            loadconfigClass lcc = new loadconfigClass("before_type");
            if (cbxBefore.SelectedIndex == 1)
            {
                beforeC.Show();
                beforeMouseC.Hide();
                //beforetimeTbx.Show();
                //delayLabel.Show();
                lcc.writetoConfig("key");
            }
            else if (cbxBefore.SelectedIndex == 2)
            {
                beforeC.Hide();
                beforeMouseC.Show();
                //beforetimeTbx.Show();
                //delayLabel.Show();
                lcc.writetoConfig("mouse");
            }
            else
            {
                beforeC.Hide();
                beforeMouseC.Hide();
                //beforetimeTbx.Hide();
                //delayLabel.Hide();
                lcc.writetoConfig("");
            }
        }

        private void beforetimeTbx_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((e.KeyChar < 48 || e.KeyChar > 57) && e.KeyChar != 8)
            {
                e.Handled = true;
            }
            if (((TextBox)sender).Text.Length >= 4 && e.KeyChar != 8)
            {
                e.Handled = true;
            }
        }

        private void beforetimeTbx_TextChanged(object sender, EventArgs e)
        {
            before_time.writetoConfig( beforetimeTbx.Text.Trim());
        }
    }
}
