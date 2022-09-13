using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace qgj
{

    class PaipaiBoxClass
    {
        static public PAIPAI_EVENT g_PPEvent = PAIPAI_EVENT.BOX_CONNECTED;
        PPRet rc = 0;
        string code = "";
        public bool IsUsePaipai = false;
        static pp_event_callback_t p = null;

        public PaipaiBoxClass() 
        {
            loadconfigClass lcc = new loadconfigClass("paipai");
            if (lcc.readfromConfig() == "true")
            {
                IsUsePaipai = true;
            }
            else
            {
                IsUsePaipai = false;
            }
        }
        private void getCode()
        {
            try
            {
                if (PublicMethods.IsInt(code.Trim()))
                {
                    foreach (char c in code.Trim())
                    {
                        simulatePress("D" + c.ToString());
                    }
                    simulatePress("Enter");
                }
            }
            catch
            {
                Console.WriteLine("do nothing");
            }
        }
        private void simulatePress(string _value)
        {
            Keys getType = (Keys)Enum.Parse(typeof(Keys), _value);
            NativeMethods.keybd_event((byte)getType, 0, 0, 0);
            NativeMethods.keybd_event((byte)getType, 0, 2, 0);
        }
        /// <summary>
        /// 设置扫码场景
        /// </summary>
        /// <param name="_i">场景编号</param>
        /// <returns>是否设置成功</returns>
        public bool setSence(int _i)
        {
            if (!IsUsePaipai)
            {
                return false;
            }
            try
            {
                rc = PPApi.SetSceneState(_i);
                if (rc != PPRet.PP_NO_ERROR)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// 启动派派小盒客户端程序
        /// </summary>
        /// <returns></returns>
        public bool startPaipaiBox()
        {
            if (!IsUsePaipai)
            {
                return false;
            }
            try
            {
                rc = PPApi.InitialPaipai(IntPtr.Zero, new StringBuilder(download.paipaiExeFilePath), IntPtr.Zero);
                Console.WriteLine(rc.ToString());
                return true;
            }
            catch(Exception e)
            {
                PublicMethods.WriteLog(e);
                IsUsePaipai = false;
                return false;
            }
        }
        /// <summary>
        /// 初始化派派小盒
        /// </summary>
        /// <param name="_errorMsg">错误信息</param>
        /// <returns>是否成功</returns>
        public bool initPaipai(ref string _errorMsg)
        {
            try
            {
                if (!IsUsePaipai)
                {
                    return true;
                }
                StringBuilder strToken = new StringBuilder(UserClass.PPToken);
                rc = PPApi.configSPToken(strToken);
                if (rc != PPRet.PP_NO_ERROR)
                {
                    Console.WriteLine("configSPToken {0} error.", strToken.ToString());
                    _errorMsg = "激活设备失败" + Environment.NewLine
                                + "1.请检查设备是否连接;" + Environment.NewLine
                                + "2.驱动程序是否启动;";
                    return false;
                }
                //pp_event_callback_t p = new pp_event_callback_t(eventcb);
                p = new pp_event_callback_t(eventcb);
                GC.KeepAlive(p);
                rc = PPApi.addEventCallback(p);
                if (rc != PPRet.PP_NO_ERROR)
                {
                    Console.WriteLine("addEventCallback error.");
                    _errorMsg = "设置设备响应失败";
                    return false;
                }
                rc = PPApi.SetScanInterval(2000);
                if (rc != PPRet.PP_NO_ERROR)
                {
                    Console.WriteLine("SetScanInterval error.");
                    _errorMsg = "设置扫描间隔失败";
                    return false;
                }
                return true;
            }
            catch (Exception e)
            {
                PublicMethods.WriteLog(e);
                _errorMsg = "其他异常" + e.Message.ToString();
                return false;
            }
        }
        /// <summary>
        /// 退出派派小盒（调用api进行析构）
        /// </summary>
        /// <returns></returns>
        public bool disposePaipai()
        {
            if (PPApi.TerminalPaipai() == PPRet.PP_NO_ERROR)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// 扫码响应事件
        /// </summary>
        /// <param name="evt"></param>
        /// <param name="decode"></param>
        /// <returns>响应结果</returns>
        private int eventcb(PAIPAI_EVENT evt, IntPtr decode)
        {
            Console.WriteLine("APITEST: status {0}", evt);
            if (evt == PAIPAI_EVENT.DECODE_SUCCESS)
            {
                PAIPAI_DECODE_RESULT result = (PAIPAI_DECODE_RESULT)Marshal.PtrToStructure(decode, typeof(PAIPAI_DECODE_RESULT));
                Console.WriteLine("APITEST: {0}", result.data);
                code = result.data;
                getCode();
            }
            else
            {
                g_PPEvent = evt;
            }
            return 0;
        }
    }
}
