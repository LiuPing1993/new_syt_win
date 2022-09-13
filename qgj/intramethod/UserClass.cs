using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace qgj
{
    class UserClass
    {
        private static bool isfirstlogin = true;
        private static bool isshow = true;//界面是否显示
        private static bool ismain = false;//当前是否是主界面
        private static IntPtr mainhandle;//主窗口句柄
        private static string fontname = "微软雅黑";//字体名
        //private static bool mainthread = false;

        private static string token = "b4a3353ec0be903924869145c7fa5d8a";
        private static string store = "";
        private static string merchant = "";
        private static string employee = "";

        private static bool isusekeyborad = false;
        private static string fastpayway = "";

        private static orderInformation orderinfoC = new orderInformation();
        private static storeOrderInformation storeinfoC = new storeOrderInformation();

        public static string PPToken = "";
        public static PaipaiBoxClass ppbC = new PaipaiBoxClass();

        public static string com_money = "0.00";//客显获取的金额
        public static bool IsFirstLogin
        {
            get
            {
                return isfirstlogin;
            }
            set
            {
                isfirstlogin = value;
            }
        }
        public static bool IsShow
        {
            get
            {
                return isshow;
            }
            set
            {
                isshow = value;
            }
        }
        public static bool IsMain
        {
            get
            {
                return ismain;
            }
            set
            {
                ismain = value;
            }
        }
        public static IntPtr mainHandle
        {
            get
            {
                return mainhandle;
            }
            set
            {
                mainhandle = value;
            }
        }
        public static string fontName
        {
            get
            {
                return fontname;
            }
            set
            {
                fontname = value;
            }
        }
        //public static bool mainThread
        //{
        //    get
        //    {
        //        return mainthread;
        //    }
        //    set
        //    {
        //        mainthread = value;
        //    }
        //}
        public static string Token
        {
            get
            {
                return token;
            }
            set
            {
                token = value;
            }
        }
        public static string Store
        {
            get
            {
                return store;
            }
            set
            {
                store = value;
            }
        }
        public static string Employee
        {
            get
            {
                return employee;
            }
            set
            {
                employee = value;
            }
        }
        public static string Merchant
        {
            get
            {
                return merchant;
            }
            set
            {
                merchant = value;
            }
        }
        public static bool isUseKeyBorad
        {
            get
            {
                return isusekeyborad;
            }
            set
            {
                isusekeyborad = value;
            }
        }
        public static string fastPayWay
        {
            get
            {
                return fastpayway;
            }
            set
            {
                fastpayway = value;
            }
        }
        public static orderInformation orderInfoC
        {
            get
            {
                return orderinfoC;
            }
            set
            {
                orderinfoC = value;
            }
        }
        public static storeOrderInformation storeInfoC
        {
            get
            {
                return storeinfoC;
            }
            set
            {
                storeinfoC = value;
            }
        }
    }
    public class User
    {
        public static StringFormat drawFormatTitle = new StringFormat();
        public static StringFormat drawFormatLeft = new StringFormat();
        public static StringFormat drawFormatRight = new StringFormat();
        public static StringFormat drawFormatLeftTop = new StringFormat();
    }

    public class BaseValue
    {
        public static string app_id = "123456789";
        public static string merchant_code = "";

        public static string token = "";
        public static string merchant_name = "";
        public static string store_name = "";
        public static string employee_id = "";
        public static string employee_name = "";
    }
}
