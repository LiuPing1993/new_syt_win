using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using System.Windows.Forms;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.VisualBasic;

namespace qgj
{
    class LPTClass
    {
        const int OPEN_EXISTING = 3;
        int line = 5;
        string prnPort = "LPT1";
        Encoding defEncoding = System.Text.Encoding.Default;

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern IntPtr CreateFile(string lpFileName,
        int dwDesiredAccess,
        int dwShareMode,
        int lpSecurityAttributes,
        int dwCreationDisposition,
        int dwFlagsAndAttributes,
        int hTemplateFile);
        public LPTClass(string _prnPort)
        {
            this.prnPort = _prnPort;//打印机端口
            //端口打印机空行的高度，默认为5
            line = fnPrintEmptyLine();

            loadconfigClass lcc = new loadconfigClass("printencoding");

            if (lcc.readfromConfig() == "ASCII")
            {
                defEncoding = System.Text.Encoding.ASCII;
            }
            else if (lcc.readfromConfig() == "BigEndianUnicode")
            {
                defEncoding = System.Text.Encoding.BigEndianUnicode;
            }
            else if (lcc.readfromConfig() == "Unicode")
            {
                defEncoding = System.Text.Encoding.Unicode;
            }
            else if (lcc.readfromConfig() == "UTF32")
            {
                defEncoding = System.Text.Encoding.UTF32;
            }
            else if (lcc.readfromConfig() == "UTF7")
            {
                defEncoding = System.Text.Encoding.UTF7;
            }
            else if (lcc.readfromConfig() == "UTF8")
            {
                defEncoding = System.Text.Encoding.UTF8;
            }
            else
            {
                defEncoding = System.Text.Encoding.Default;
            }
        }
        /// <summary>
        /// 端口测试打印
        /// </summary>
        /// <param name="str">测试字符串</param>
        /// <returns>打印结果</returns>
        public string fnPrintLineForTest(string str)
        {
            IntPtr iHandle = CreateFile(prnPort, 0x40000000, 0, 0, OPEN_EXISTING, 0, 0);
            if (iHandle.ToInt32() == -1)
            {
                Console.WriteLine(iHandle.ToString());
                return "未连接打印机或错误端口，打印异常";
            }
            else
            {
                try
                {
                    Microsoft.Win32.SafeHandles.SafeFileHandle handle = new Microsoft.Win32.SafeHandles.SafeFileHandle(iHandle, true);
                    FileStream fs = new FileStream(handle, FileAccess.ReadWrite);
                    StreamWriter sw = new StreamWriter(fs, defEncoding);
                    sw.WriteLine("            测试页");
                    sw.WriteLine(" ");
                    sw.WriteLine("--------------------------------");
                    sw.WriteLine(" ");
                    sw.WriteLine(str);
                    sw.Close();
                    fs.Close();
                    return "打印成功";
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return "打印失败" + e.Message.ToString();
                }
            }
        }

        /// <summary>
        /// 打印商户或者客户小票
        /// </summary>
        /// <param name="str">打印字符串</param>
        /// <param name="_ismerchant">是否是商户小票</param>
        /// <returns>打印结果</returns>
        public string fnPrintLine(string str, bool _ismerchant, bool _istotal = false)
        {
            IntPtr iHandle = CreateFile(prnPort, 0x40000000, 0, 0, OPEN_EXISTING, 0, 0);
            if (iHandle.ToInt32() == -1)
            {
                Console.WriteLine(iHandle.ToString());
                return "未连接打印机或错误端口，打印异常";
            }
            else
            {
                Console.WriteLine(iHandle.ToString());
                if (_istotal)
                {
                    if (fnPrintTotal(iHandle, str))//打印汇总
                    {
                        return "打印成功!";
                    }
                    else
                    {
                        return "打印失败!";
                    }
                }
                if (_ismerchant)
                {
                    if (fnPrintMerchant(iHandle, str))//打印商户存根
                    {
                        return "打印成功!";
                    }
                    else
                    {
                        return "打印失败!";
                    }
                }
                else
                {
                    if (fnPrintCustomer(iHandle, str))//打印顾客存根
                    {
                        return "打印成功!";
                    }
                    else
                    {
                        return "打印失败!";
                    }
                }
            }
        }

        private bool fnPrintMerchant(IntPtr iHandle, string str)
        {
            try
            {
                Microsoft.Win32.SafeHandles.SafeFileHandle handle = new Microsoft.Win32.SafeHandles.SafeFileHandle(iHandle, true);
                FileStream fs = new FileStream(handle, FileAccess.ReadWrite);
                StreamWriter sw = new StreamWriter(fs, defEncoding);
                sw.WriteLine("            商户存根");
                sw.WriteLine(" ");
                sw.WriteLine("--------------------------------");
                sw.WriteLine(" ");
                sw.WriteLine(str);
                for (int i = 0; i < line; i++)
                {
                    sw.WriteLine(" ");
                }
                sw.Close();
                fs.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        private bool fnPrintCustomer(IntPtr iHandle, string str)
        {
            try
            {
                Microsoft.Win32.SafeHandles.SafeFileHandle handle = new Microsoft.Win32.SafeHandles.SafeFileHandle(iHandle, true);
                FileStream fs = new FileStream(handle, FileAccess.ReadWrite);
                StreamWriter sw = new StreamWriter(fs, defEncoding);
                sw.WriteLine("            客户存根");
                sw.WriteLine(" ");
                sw.WriteLine("--------------------------------");
                sw.WriteLine(" ");
                sw.WriteLine(str);
                for (int i = 0; i < line; i++)
                {
                    sw.WriteLine(" ");
                }
                sw.Close();
                fs.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        private bool fnPrintTotal(IntPtr iHandle, string str)
        {
            try
            {
                Microsoft.Win32.SafeHandles.SafeFileHandle handle = new Microsoft.Win32.SafeHandles.SafeFileHandle(iHandle, true);
                FileStream fs = new FileStream(handle, FileAccess.ReadWrite);
                StreamWriter sw = new StreamWriter(fs, defEncoding);
                sw.WriteLine("            汇总统计");
                sw.WriteLine(" ");
                sw.WriteLine("--------------------------------");
                sw.WriteLine(" ");
                sw.WriteLine(str);
                for (int i = 0; i < line; i++)
                {
                    sw.WriteLine(" ");
                }
                sw.Close();
                fs.Close();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }

        //得到的打印的空白行的大小
        private int fnPrintEmptyLine()
        {
            try
            {
                loadconfigClass lcc = new loadconfigClass("ltpline");
                string temp = lcc.readfromConfig();
                if (temp == "")
                {
                    return 5;
                }
                else
                {
                    return Convert.ToInt32(temp);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return 5;
        }
    }
}
