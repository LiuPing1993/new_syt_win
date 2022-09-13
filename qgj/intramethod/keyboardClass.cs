using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Diagnostics;
using System.Management;

namespace qgj
{
    class keyboardClass
    {
        // 申明要使用的dll和api
        [DllImport("User32.dll", EntryPoint = "FindWindow")]
        public extern static IntPtr FindWindow(string lpClassName, string lpWindowName); 
       [System.Runtime.InteropServices.DllImportAttribute("user32.dll", EntryPoint = "MoveWindow")]
       public static extern bool MoveWindow(System.IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);


        [DllImport("user32.dll")]
         static extern bool SetForegroundWindow(IntPtr hWnd);


        private System.Diagnostics.Process softKey; 

        public void showOsk()
        {
            //打开软键盘
            try
            {
                if (!System.IO.File.Exists(Environment.SystemDirectory + "\\osk.exe"))
                {
                    MessageBox.Show("系统辅助软键盘不存在！");
                    return;
                }

                softKey = System.Diagnostics.Process.Start(@"C:\WINDOWS\System32\osk.exe");
                // 上面的语句在打开软键盘后，系统还没用立刻把软键盘的窗口创建出来了。所以下面的代码用循环来查询窗口是否创建，只有创建了窗口
                // FindWindow才能找到窗口句柄，才可以移动窗口的位置和设置窗口的大小。这里是关键。
                IntPtr intptr = IntPtr.Zero;
                while (IntPtr.Zero == intptr)
                {
                    System.Threading.Thread.Sleep(100);
                    intptr = FindWindow(null, "屏幕键盘");
                }

                
                // 获取屏幕尺寸
                int iActulaWidth = Screen.PrimaryScreen.Bounds.Width;
                int iActulaHeight = Screen.PrimaryScreen.Bounds.Height;


                // 设置软键盘的显示位置，底部居中
                int posX = (iActulaWidth - 1000) / 2;
                int posY = (iActulaHeight - 300);


                //设定键盘显示位置
                MoveWindow(intptr, posX, posY, 1000, 300, true);


                //设置软键盘到前端显示
                SetForegroundWindow(intptr);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public static void showKeyBoard()
        {
            try
            {
                loadconfigClass lcc = new loadconfigClass("syskeyboard");
                if (lcc.readfromConfig() != "true")
                {
                    return;
                }
                if (!System.IO.File.Exists(Environment.SystemDirectory + "\\osk.exe"))
                {
                    return;
                }
                if (Distinguish64or32System() != "32")
                {
                    Console.WriteLine("暂不支持64位系统");
                    
                    return;
                }
                System.Diagnostics.Process[] myPs;
                myPs = System.Diagnostics.Process.GetProcesses();
                foreach (System.Diagnostics.Process p in myPs)
                {
                    if (p.ProcessName == "osk.exe")
                    {
                        return;
                    }
                }
                System.Diagnostics.Process.Start(@"C:\WINDOWS\System32\osk.exe");
            }
            catch { }
        }
        private static string Distinguish64or32System()
        {
            try
            {
                string addressWidth = String.Empty;
                ConnectionOptions mConnOption = new ConnectionOptions();
                ManagementScope mMs = new ManagementScope("//localhost", mConnOption);
                ObjectQuery mQuery = new ObjectQuery("select AddressWidth from Win32_Processor");
                ManagementObjectSearcher mSearcher = new ManagementObjectSearcher(mMs, mQuery);
                ManagementObjectCollection mObjectCollection = mSearcher.Get();
                foreach (ManagementObject mObject in mObjectCollection)
                {
                    addressWidth = mObject["AddressWidth"].ToString();
                }
                return addressWidth;
            }
            catch 
            {
                return String.Empty;
            }
        }
    }
}
