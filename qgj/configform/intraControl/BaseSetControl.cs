using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using Microsoft.Win32;

namespace qgj
{
    public partial class BaseSetControl : UserControl
    {

        #region 变量声明
        /// <summary>
        /// 是否开机自动启动
        /// </summary>
        bool IsAutorun = false;
        /// <summary>
        /// 是否使用驱动打印机
        /// </summary>
        bool IsDriveprint = false;
        /// <summary>
        /// 是否直接退出
        /// </summary>
        exit_type IsExit = exit_type.none;
        /// <summary>
        /// 是否使用端口打印机
        /// </summary>
        bool IsLptprint = false;
        /// <summary>
        /// 是否收款成功后最小化
        /// </summary>
        bool IsMini = false;
        /// <summary>
        /// 是否启用键盘操作
        /// </summary>
        bool IsUseKeyBoard = false;
        /// <summary>
        /// 是否显示悬浮窗
        /// </summary>
        bool IsSuspended = false;
        /// <summary>
        /// 是否开启收款成功提示音
        /// </summary>
        bool IsUseVoice = false;
        /// <summary>
        /// 界面是否加载完毕
        /// </summary>
        bool IsLoad = false;
        /// <summary>
        /// 是否使用辅助键盘
        /// </summary>
        bool IsSysKeyboard = false;
        /// <summary>
        /// 是否自动打印
        /// </summary>
        bool IsAutoPrint = false;
        /// <summary>
        /// 是否使用粗体
        /// </summary>
        bool IsBold = false;
        #endregion

        public BaseSetControl()
        {
            InitializeComponent();
            BackColor = Defcolor.MainBackColor;

            cbbVoiceSet.Hide();
            lblLPTtest.Hide();

            loadconfigClass _lc = new loadconfigClass("userprint");
            if (_lc.readfromConfig() == "drive")
            {
                IsDriveprint = true;
                driveprintselectC.fnSelectChange(true);
            }
            else if (_lc.readfromConfig() == "lpt")
            {
                IsLptprint = true;
                lptprintselectC.fnSelectChange(true);
                lblLPTtest.Show();
            }
            else
            {
                IsDriveprint = false;
                IsLptprint = false;
                driveprintselectC.fnSelectChange(false);
                lptprintselectC.fnSelectChange(false);
            }
            _lc = new loadconfigClass("fastpayway");
            if(_lc.readfromConfig() == "bar")
            {
                cbbFastPay.SelectedIndex = 1;
            }
            else if (_lc.readfromConfig() == "qr")
            {
                cbbFastPay.SelectedIndex = 2;
            }
            else if(_lc.readfromConfig() == "notset")
            {
                cbbFastPay.SelectedIndex = 0;
            }
            else
            {
                cbbFastPay.SelectedIndex = 1;
            }
            _lc = new loadconfigClass("printmode");
            if (_lc.readfromConfig() == "mer")
            {
                cbbPrintMode.SelectedIndex = 2;
            }
            else if(_lc.readfromConfig() == "cust")
            {
                cbbPrintMode.SelectedIndex = 1;
            }
            else
            {
                cbbPrintMode.SelectedIndex = 0;
            }
            _lc = new loadconfigClass("emptyline");
            if (_lc.readfromConfig() == "")
            {
                nEmptyLine.Value = 0;
            }
            else
            {
                try
                {
                    nEmptyLine.Value = Convert.ToInt32(_lc.readfromConfig());
                }
                catch
                {
                    nEmptyLine.Value = 0;
                }
            }
            _lc = new loadconfigClass("usevoice");
            if(_lc.readfromConfig() == "true")
            {
                IsUseVoice = true;
                voicetoinformselectC.fnSelectChange(true);
            }
            _lc = new loadconfigClass("syskeyboard");
            if (_lc.readfromConfig() == "true")
            {
                IsSysKeyboard = true;
                keyboardSelectC.fnSelectChange(true);
            }
            _lc = new loadconfigClass("printbold");
            if (_lc.readfromConfig() == "true")
            {
                IsBold = true;
                boldSelectC.fnSelectChange(true);
            }
        }

        #region 从本机获取打印机相关信息
        private static PrintDocument fPrintDocument = new PrintDocument();
        //获取本机默认打印机名称
        public static String DefaultPrinter()
        {
            return fPrintDocument.PrinterSettings.PrinterName;
        }
        public static List<String> GetLocalPrinters()
        {
            List<String> fPrinters = new List<String>();
            fPrinters.Add(DefaultPrinter()); //默认打印机始终出现在列表的第一项
            foreach (String fPrinterName in PrinterSettings.InstalledPrinters)
            {
                if (!fPrinters.Contains(fPrinterName))
                {
                    fPrinters.Add(fPrinterName);
                }
            }
            return fPrinters;
        }
        #endregion
        
        private void BaseSetControl_Load(object sender, EventArgs e)
        {
            loadconfigClass _lcc = new loadconfigClass("driveprintid");
            try
            {
                List<String> _list = GetLocalPrinters();
                foreach (String _s in _list)
                {
                    cbbDrivePrint.Items.Add(_s);
                }
                if (_lcc.readfromConfig() == "")
                {
                    cbbDrivePrint.SelectedIndex = 0;
                }
                else
                {
                    cbbDrivePrint.SelectedIndex = Convert.ToInt32(_lcc.readfromConfig());
                }
            }
            catch
            {
                //cbbDrivePrint.SelectedIndex = 0;
            }
            try
            {
                _lcc = new loadconfigClass("lptprintname");
                if (_lcc.readfromConfig() == "")
                {
                    cbbLPT.SelectedIndex = 0;
                }
                else
                {
                    cbbLPT.SelectedItem = _lcc.readfromConfig();
                }
            }
            catch
            {
                //cbbLPT.SelectedIndex = 0;
            }

            _lcc = new loadconfigClass("autorun");
            if (_lcc.readfromConfig() == "true")
            {
                IsAutorun = true;
                autorunselectC.fnSelectChange(true);
            }


            _lcc = new loadconfigClass("exit");
            if (_lcc.readfromConfig() == "true")
            {
                IsExit = exit_type.exit;
                exitselectC.fnSelectChange(true);
                minselectC.fnSelectChange(false);
            }
            else if(_lcc.readfromConfig() == "false")
            {
                IsExit = exit_type.min;
                exitselectC.fnSelectChange(false);
                minselectC.fnSelectChange(true);
            }
            else
            {
                IsExit = exit_type.none;
                exitselectC.fnSelectChange(false);
                minselectC.fnSelectChange(false);
            }
            _lcc = new loadconfigClass("successmini");
            if(_lcc.readfromConfig() == "true")
            {
                IsMini = true;
                successminiselectC.fnSelectChange(true);
            }
            _lcc = new loadconfigClass("usekeyboard");
            if (_lcc.readfromConfig() == "true")
            {
                IsUseKeyBoard = true;
                usekeyboardselectC.fnSelectChange(true);
            }
            _lcc = new loadconfigClass("showsuspended");
            if (_lcc.readfromConfig() == "true")
            {
                IsSuspended = true;
                suspendedselectC.fnSelectChange(true);
            }
            _lcc = new loadconfigClass("autoprint");
            if (_lcc.readfromConfig() == "true")
            {
                IsAutoPrint = true;
                autoPrintSelectC.fnSelectChange(true);
            }
            else if(_lcc.readfromConfig() == "")
            {
                IsAutoPrint = true;
                autoPrintSelectC.fnSelectChange(true);
                fnAutoPrintSave();
            }
            else
            {
                IsAutoPrint = false;
                autoPrintSelectC.fnSelectChange(false);
            }
            IsLoad = true;
            try
            {
                _lcc = new loadconfigClass("printencoding");
                if (_lcc.readfromConfig() == "")
                {
                    cbbEncoding.SelectedIndex = 0;
                }
                else
                {
                    cbbEncoding.SelectedItem = _lcc.readfromConfig();
                }
            }
            catch
            {
                cbbEncoding.SelectedIndex = 0;
            }
            try
            {
                _lcc = new loadconfigClass("printpagewide");
                if (_lcc.readfromConfig() == "")
                {
                    cbbPageWide.SelectedIndex = 0;
                }
                else
                {
                    cbbPageWide.SelectedItem = _lcc.readfromConfig();
                }
            }
            catch
            {
                cbbPageWide.SelectedIndex = 0;
            }
            try
            {
                _lcc = new loadconfigClass("printcompatible");
                if (_lcc.readfromConfig() == "1")
                {
                    cbbPrintCompatible.SelectedIndex = 1;
                }
                else if (_lcc.readfromConfig() == "2")
                {
                    cbbPrintCompatible.SelectedIndex = 2;
                }
                else
                {
                    cbbPrintCompatible.SelectedIndex = 0;
                }
            }
            catch
            {
                cbbPrintCompatible.SelectedIndex = 0;
            }
        }

        public void reloadSet()
        {
            loadconfigClass _lcc = new loadconfigClass("showsuspended");
            if (_lcc.readfromConfig() == "true")
            {
                IsSuspended = true;
                suspendedselectC.fnSelectChange(true);
            }
            else
            {
                IsSuspended = false;
                suspendedselectC.fnSelectChange(false);
            }
        }

        private void autorun_MouseUp(object sender, MouseEventArgs e)
        {
            if (IsAutorun)
            {
                IsAutorun = false;
                autorunselectC.fnSelectChange(false);
            }
            else
            {
                IsAutorun = true;
                autorunselectC.fnSelectChange(true);
            }
            fnAutoRunSave();
        }
        private void driveprint_MouseUp(object sender,MouseEventArgs e)
        {
            if (IsDriveprint)
            {
                IsDriveprint = false;
                driveprintselectC.fnSelectChange(false);
            }
            else
            {
                IsDriveprint = true;
                driveprintselectC.fnSelectChange(true);
                IsLptprint = false;
                lptprintselectC.fnSelectChange(false);
                lblLPTtest.Hide();
            }
            fnPrintSave();
        }
        private void lblLPTPrint_MouseUp(object sender, MouseEventArgs e)
        {
            if (IsLptprint)
            {
                IsLptprint = false;
                lptprintselectC.fnSelectChange(false);
                lblLPTtest.Hide();
            }
            else
            {
                IsLptprint = true;
                lptprintselectC.fnSelectChange(true);
                IsDriveprint = false;
                driveprintselectC.fnSelectChange(false);
                lblLPTtest.Show();
            }
            fnPrintSave();
        }
        private void exit_MouseUp(object sender, MouseEventArgs e)
        {
            if (IsExit == exit_type.exit)
            {
                IsExit = exit_type.none;
                exitselectC.fnSelectChange(false);
                minselectC.fnSelectChange(false);
            }
            else
            {
                IsExit = exit_type.exit;
                exitselectC.fnSelectChange(true);
                minselectC.fnSelectChange(false);
            }
            fnExitSave();
        }
        private void exitMin_MouseUp(object sender, MouseEventArgs e)
        {
            if (IsExit == exit_type.min)
            {
                IsExit = exit_type.none;
                exitselectC.fnSelectChange(false);
                minselectC.fnSelectChange(false);
            }
            else
            {
                IsExit = exit_type.min;
                exitselectC.fnSelectChange(false);
                minselectC.fnSelectChange(true);
            }
            fnExitSave();
        }
        private void mini_MouseUp(object sender, MouseEventArgs e)
        {
            if (IsMini)
            {
                IsMini = false;
                successminiselectC.fnSelectChange(false);
            }
            else
            {
                IsMini = true;
                successminiselectC.fnSelectChange(true);
            }
            fnSuccessMiniSave();
        }
        private void usekeyboard_MouseUp(object sender, MouseEventArgs e)
        {
            if (IsUseKeyBoard)
            {
                IsUseKeyBoard = false;
                usekeyboardselectC.fnSelectChange(false);
            }
            else
            {
                IsUseKeyBoard = true;
                usekeyboardselectC.fnSelectChange(true);
            }
            fnUsekeyBoardSave();
        }
        private void suspended_MouseUp(object sender, MouseEventArgs e)
        {
            if (IsSuspended)
            {
                IsSuspended = false;
                suspendedselectC.fnSelectChange(false);
            }
            else
            {
                IsSuspended = true;
                suspendedselectC.fnSelectChange(true);
            }
            fnSuspendedSave();
        }
        private void voicetoinformselectC_MouseUp(object sender, MouseEventArgs e)
        {
            if(IsUseVoice)
            {
                IsUseVoice = false;
                voicetoinformselectC.fnSelectChange(false);
            }
            else
            {
                IsUseVoice = true;
                voicetoinformselectC.fnSelectChange(true);
            }
            fnVoiceSave();
        }
        private void keyboardSelectC_MouseUp(object sender, MouseEventArgs e)
        {
            if (IsSysKeyboard)
            {
                IsSysKeyboard = false;
                keyboardSelectC.fnSelectChange(false);
            }
            else
            {
                IsSysKeyboard = true;
                keyboardSelectC.fnSelectChange(true);
            }
            fnSysKeyBoardSave();
        }
        private void autoprint_MouseUp(object sender, MouseEventArgs e)
        {
            if (IsAutoPrint)
            {
                IsAutoPrint = false;
                autoPrintSelectC.fnSelectChange(false);
            }
            else
            {
                IsAutoPrint = true;
                autoPrintSelectC.fnSelectChange(true);
            }
            fnAutoPrintSave();
        }

        private void boldSelectC_MouseUp(object sender, MouseEventArgs e)
        {
            if (IsBold)
            {
                IsBold = false;
                boldSelectC.fnSelectChange(false);
            }
            else
            {
                IsBold = true;
                boldSelectC.fnSelectChange(true);
            }
            fnBoldSave();
        }

        private void fnBoldSave()
        {
            try
            {
                loadconfigClass _lcc = new loadconfigClass("printbold");
                if (IsBold)
                {
                    _lcc.writetoConfig("true");
                }
                else
                {
                    _lcc.writetoConfig("false");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        private void fnAutoPrintSave()
        {
            try
            {
                loadconfigClass _lcc = new loadconfigClass("autoprint");
                if (IsAutoPrint)
                {
                    _lcc.writetoConfig("true");
                }
                else
                {
                    _lcc.writetoConfig("false");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        private void fnAutoRunSave()
        {
            try
            {
                loadconfigClass _lcc = new loadconfigClass("autorun");
                if (IsAutorun)
                {
                    string _exePath = Application.ExecutablePath;
                    WTRegedit("qgj.exe", _exePath);
                    _lcc.writetoConfig("true");
                    Console.WriteLine("create");
                }
                else
                {
                    _lcc.writetoConfig("false");
                    if (this.IsRegeditExit("qgj.exe"))
                    {
                        this.DLRegedit("qgj.exe");
                        Console.WriteLine("delete");
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
        private void fnExitSave()
        {
            try
            {
                loadconfigClass _lcc = new loadconfigClass("exit");
                if (IsExit == exit_type.exit)
                {
                    _lcc.writetoConfig("true");
                }
                else if (IsExit == exit_type.min)
                {
                    _lcc.writetoConfig("false");
                }
                else
                {
                    _lcc.writetoConfig("");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        private void fnSuccessMiniSave()
        {
            try
            {
                loadconfigClass _lcc = new loadconfigClass("successmini");
                if (IsMini)
                {
                    _lcc.writetoConfig("true");
                }
                else
                {
                    _lcc.writetoConfig("false");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        private void fnPrintSave()
        {
            loadconfigClass _lcc = new loadconfigClass("userprint");
            if (IsDriveprint)
            {
                _lcc.writetoConfig("drive");
                _lcc = new loadconfigClass("driveprintname");
                _lcc.writetoConfig(cbbDrivePrint.SelectedItem.ToString());
                _lcc = new loadconfigClass("driveprintid");
                _lcc.writetoConfig(cbbDrivePrint.SelectedIndex.ToString());
            }
            else if(IsLptprint)
            {
                _lcc.writetoConfig("lpt");
                _lcc = new loadconfigClass("lptprintname");
                _lcc.writetoConfig(cbbLPT.SelectedItem.ToString());
            }
            else
            {
                _lcc.writetoConfig("");
            }
        }
        private void fnLPTEmptyLineSave()
        {
            loadconfigClass _lcc = new loadconfigClass("emptyline");
            try
            {
                _lcc.writetoConfig(nEmptyLine.Value.ToString());
            }
            catch
            {
                _lcc.writetoConfig("0");
            }
        }
        private void fnUsekeyBoardSave()
        {
            try
            {
                loadconfigClass _lcc = new loadconfigClass("usekeyboard");
                if (IsUseKeyBoard)
                {
                    _lcc.writetoConfig("true");
                }
                else
                {
                    _lcc.writetoConfig("false");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
        private void fnFastPaySave()
        {
            loadconfigClass _lcc = new loadconfigClass("fastpayway");
            if (cbbFastPay.SelectedIndex == 1)
            {
                _lcc.writetoConfig("bar");
            }
            else if (cbbFastPay.SelectedIndex == 2)
            {
                _lcc.writetoConfig("qr");
            }
            else if (cbbFastPay.SelectedIndex == 0)
            {
                _lcc.writetoConfig("notset");
            }
            else
            {
                _lcc.writetoConfig("");
            }
        }
        private void fnPrintModeSave()
        {
            loadconfigClass _lcc = new loadconfigClass("printmode");
            if (cbbPrintMode.SelectedIndex == 1)
            {
                _lcc.writetoConfig("cust");
            }
            else if (cbbPrintMode.SelectedIndex == 2)
            {
                _lcc.writetoConfig("mer");
            }
            else
            {
                _lcc.writetoConfig("all");
            }
        }
        private void fnPrintEncodingSave()
        {
            loadconfigClass _lcc = new loadconfigClass("printencoding");
            try
            {
                _lcc.writetoConfig(cbbEncoding.SelectedItem.ToString().Trim());
            }
            catch
            {
                _lcc.writetoConfig("ANSI");
            }

        }
        private void fnPrintWideSave()
        {
            loadconfigClass _lcc = new loadconfigClass("printpagewide");
            try
            {
                _lcc.writetoConfig(cbbPageWide.SelectedItem.ToString().Trim());
            }
            catch
            {
                _lcc.writetoConfig("50");
            }
        }

        private void fnPrintCompatibleSave()
        {
            loadconfigClass _lcc = new loadconfigClass("printcompatible");
            try
            {
                if (cbbPrintCompatible.SelectedIndex == 1)
                {
                    _lcc.writetoConfig("1");
                }
                else if (cbbPrintCompatible.SelectedIndex == 2)
                {
                    _lcc.writetoConfig("2");
                }
                else
                {
                    _lcc.writetoConfig("0");
                }
            }
            catch
            {
                _lcc.writetoConfig("0");
            }
        }

        private void fnSuspendedSave()
        {
            loadconfigClass _lcc = new loadconfigClass("showsuspended");
            if (IsSuspended)
            {
                _lcc.writetoConfig("true");
            }
            else
            {
                _lcc.writetoConfig("false");
                _lcc = new loadconfigClass("realtime");
                _lcc.writetoConfig("false");
            }
        }
        private void fnVoiceSave()
        {
            loadconfigClass _lcc = new loadconfigClass("usevoice");
            if(IsUseVoice)
            {
                _lcc.writetoConfig("true");
                PublicMethods.gfnVoiceToInform();
            }
            else
            {
                _lcc.writetoConfig("false");
            }
        }
        private void fnVoiceSetSave()
        {

        }

        private void fnSysKeyBoardSave()
        {
            loadconfigClass _lcc = new loadconfigClass("syskeyboard");
            if (IsSysKeyboard)
            {
                _lcc.writetoConfig("true");
            }
            else
            {
                _lcc.writetoConfig("false");
            }
        }

        #region 注册表操作(开机自动启动)
        private bool IsRegeditExit(string _name)
        {
            bool _IsExit = false;
            string[] _subkeyNames;
            RegistryKey _hklm = Registry.LocalMachine;
            RegistryKey _software = _hklm.OpenSubKey("SOFTWARE", true);
            RegistryKey _aimdir = _software.OpenSubKey("Microsoft\\Windows\\CurrentVersion\\Run", true);
            _subkeyNames = _aimdir.GetValueNames();
            foreach (string keyName in _subkeyNames)
            {
                if (keyName == _name)
                {
                    _IsExit = true;
                    Console.WriteLine("yes");
                    return _IsExit;
                }
            }
            Console.WriteLine("no");
            return _IsExit;
        }
        private void WTRegedit(string _name, string _tovalue)
        {
            RegistryKey _hklm = Registry.LocalMachine;
            RegistryKey _software = _hklm.OpenSubKey("SOFTWARE", true);
            RegistryKey _aimdir = _software.CreateSubKey("Microsoft\\Windows\\CurrentVersion\\Run");
            _aimdir.SetValue(_name, _tovalue, RegistryValueKind.String);
        }

        private void DLRegedit(string _name)
        {
            RegistryKey _hklm = Registry.LocalMachine;
            RegistryKey _software = _hklm.OpenSubKey("SOFTWARE", true);
            RegistryKey _aimdir = _software.CreateSubKey("Microsoft\\Windows\\CurrentVersion\\Run");
            _aimdir.DeleteValue(_name, true);
        }
        #endregion
        private void cbbPrintCompatible_SelectedIndexChanged(object sender, EventArgs e)
        {
            fnPrintCompatibleSave();
        }
        private void cbbDrivePrint_SelectedIndexChanged(object sender, EventArgs e)
        {
            fnPrintSave();
        }
        private void cbbLPT_SelectedIndexChanged(object sender, EventArgs e)
        {
            fnPrintSave();
        }
        private void cbbFastPay_SelectedIndexChanged(object sender, EventArgs e)
        {
            fnFastPaySave();
        }
        private void cbbPrintMode_SelectedValueChanged(object sender, EventArgs e)
        {
            fnPrintModeSave();
        }
        private void cbbEncoding_SelectedValueChanged(object sender, EventArgs e)
        {
            fnPrintEncodingSave();
        }
        private void cbbPageWide_SelectedValueChanged(object sender, EventArgs e)
        {
            fnPrintWideSave();
        }
        private void nEmptyLine_ValueChanged(object sender, EventArgs e)
        {
            fnLPTEmptyLineSave();
        }
        private void cbbVoiceSet_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (IsLoad)
            {
                PublicMethods.gfnVoiceToInform();
            }
            fnVoiceSetSave();
        }
        private void lblLPTtest_MouseUp(object sender, MouseEventArgs e)
        {
            string _sLPTName = cbbLPT.SelectedItem.ToString();
            if (_sLPTName == "")
            {
                _sLPTName = "LPT1";
            }
            try
            {
                LPTClass _lpC = new LPTClass(_sLPTName);
                string _sResult = _lpC.fnPrintLineForTest("这是一页测试" + System.Environment.NewLine + "12345qwerty");
                MessageBox.Show(_sResult);
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.ToString());
            }
            
        }

    }

    public enum exit_type
    {
        none = 0,
        exit = 1,
        min = 2
    }
}
