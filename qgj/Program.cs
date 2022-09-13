using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Threading;
using System.Reflection;
using System.Net;
using System.Xml;
using System.Configuration;

namespace qgj
{
    static class Program
    {
        public static System.Threading.Mutex Run;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                //Application.EnableVisualStyles();//部分winxp下会导致无法启动（Window class name is not valid.），初步原因可能是由于mdac2.8版本造成
                Application.SetCompatibleTextRenderingDefault(false);

                //Application.Run(new testsettingForm());
                //Application.Run(new DownloadForm(downType.paipai));
                //Application.Run(new errorinformationForm("这里是提示标题", "这里是错误信息"));

                bool noRun = false;
                Run = new System.Threading.Mutex(true, "QGJControl", out noRun);
                //检测是否已经运行

                Process instance = RunningInstance();            
                if (noRun)
                {
                    while (true)
                    {
                        if (Url.url.IndexOf("test") != -1)
                        {
                            MessageBox.Show("提示：当前程序为测试版本！");
                        }
                        if (Url.url.IndexOf("master") != -1)
                        {
                            MessageBox.Show("提示：当前程序为验收版本！");
                        }

                        UserClass.ppbC = new PaipaiBoxClass();
                        loginForm login = new loginForm();
                        //2021-10-29 测试时候使用
                        //login.ShowDialog();
                        login.DialogResult = DialogResult.OK;
                        if (login.DialogResult == DialogResult.OK)
                        {
                            PublicMethods.newFastLnk();
                            updateClass.autoupdate();

                            main m = new main();
                            SuspendedForm sf = new SuspendedForm(m);
                            m.suspendedF = sf;
                            Application.Run(m);
                        }
                        else
                        {
                            PublicMethods.exitSystem();
                        }
                    }
                }
                else
                {
                    HandleRunningInstance(instance);
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
                PublicMethods.WriteLog(ee);
                errorinformationForm _errorF = new errorinformationForm("系统提示", "（日志已保存）" + ee.ToString());
                _errorF.StartPosition = FormStartPosition.CenterScreen;
                _errorF.TopMost = true;
                _errorF.ShowDialog();
            }
        }
        public static Process RunningInstance()
        {
            Process current = Process.GetCurrentProcess();
            Process[] processes = Process.GetProcessesByName(current.ProcessName);
            foreach (Process process in processes)
            {
                if (process.Id != current.Id)
                {
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == current.MainModule.FileName)
                    {
                        
                        return process;
                    }
                }
            }
            return null;
        }

        public static void HandleRunningInstance(Process instance)
        {
            ShowWindowAsync(instance.MainWindowHandle, WS_SHOWNORMAL);
            SetForegroundWindow(instance.MainWindowHandle);
        }
        [DllImport("User32.dll")]
        public static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);
        [DllImport("User32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        public const int WS_SHOWNORMAL = 1;
    }
}
