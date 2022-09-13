using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;

namespace qgj
{
    class typeClass
    {

    }
    class picClass
    {
        private static string picPointX = "0";
        private static string picPointY = "0";
        private static string picPointW = "0";
        private static string picPointH = "0";
        private static string picPointZ = "1";
        private static string picValue = "120";
        private static string picInverse = "false";
        /// <summary>
        /// 图片x位置
        /// </summary>
        public static string PicPointX
        {
            set
            {
                picPointX = value;
            }
            get
            {
                return picPointX;
            }
        }
        /// <summary>
        /// 图片y位置
        /// </summary>
        public static string PicPointY
        {
            set
            {
                picPointY = value;
            }
            get
            {
                return picPointY;
            }
        }
        /// <summary>
        /// 图片宽
        /// </summary>
        public static string PicPointW
        {
            set
            {
                picPointW = value;
            }
            get
            {
                return picPointW;
            }
        }
        /// <summary>
        /// 图片高
        /// </summary>
        public static string PicPointH
        {
            set
            {
                picPointH = value;
            }
            get
            {
                return picPointH;
            }
        }
        /// <summary>
        /// 图片缩放倍率
        /// </summary>
        public static string PicPointZ
        {
            set
            {
                picPointZ = value;
            }
            get
            {
                return picPointZ;
            }
        }
        /// <summary>
        /// 图片二值化阈值
        /// </summary>
        public static string PicValue
        {
            set
            {
                picValue = value;
            }
            get
            {
                return picValue;
            }
        }
        /// <summary>
        /// 图片是否取反色
        /// </summary>
        public static string PicInverse
        {
            set
            {
                picInverse = value;
            }
            get
            {
                return picInverse;
            }
        }
    }
    public enum PayWay
    {
        [Description("条码收款")]
        barcodePay = 0,
        [Description("扫码收款")]
        qrcodePay = 1,
        [Description("银联记账")]
        cardPay = 2,
        [Description("现金记账")]
        cashPay = 3,
        [Description("储值收款")]
        storePay = 4,
        [Description("混合收款")]
        storeAndotherPay =5,
    };
    public enum InType
    {
        [Description("账号")]
        account = 0,
        [Description("密码")]
        password = 1,
        [Description("激活码")]
        actcode = 2,

    };
    public enum lgFormType
    {
        [Description("登陆")]
        login = 0,
        [Description("激活")]
        activate = 1,
        [Description("代理设置")]
        proxy = 2,
        [Description("等待")]
        wait = 3,
    };
    public enum settingType
    {
        [Description("基本设置")]
        basesetting = 0,
        [Description("系统对接")]
        jointsetting = 1,
        [Description("修改密码")]
        passwordsetting = 2,
        [Description("其他设置")]
        otherseting = 3,
    }
    public enum couponType
    {
        [Description("优惠券")]
        cashcoupon = 0,
        [Description("折扣券")]
        discountcoupon = 1,
        [Description("兑换券")]
        giftcoupon = 2,
        [Description("不可使用")]
        useless = 3,
    }
    public enum listType
    {
        [Description("订单明细")]
        orderlist = 0,
        [Description("储值明细")]
        storelist = 1,
        [Description("核销明细")]
        couponlist = 2,
        [Description("流水明细")]
        flowlist = 3,
        [Description("新订单明细")]
        neworderlist = 4,
        [Description("口碑核销明细")]
        koubeilist = 6,

    }
    public enum topitem
    {
        [Description("收银")]
        cashier = 0,
        [Description("明细")]
        detail = 1,
        [Description("储值")]
        store = 2,
        [Description("会员")]
        member = 3,
    }

    public class orderInformation
    {
        string money = "";
        string notdiscount = "";
        string showreceipt = "";
        string showdiscount = "";
        public string member = "";
        public string usestore = "";
        public string coupon = "";
        public PayWay payway = PayWay.barcodePay;
        public bool UseMemberDiscount = false;

        public void reload()
        {
            money = "";
            notdiscount = "";
            showreceipt = "";
            showdiscount = "";
            member = "";
            usestore = "";
            coupon = "";
            payway = PayWay.barcodePay;
            UseMemberDiscount = false;
        }
        public void setMoney(string _insert)
        {
            money = _insert;
        }
        public void setNotDiscount(string _insert)
        {
            notdiscount = _insert;
        }
        public string getMoney()
        {
            return PublicMethods.moneyFormater(money);
        }
        public string getNotDiscount()
        {
            return PublicMethods.moneyFormater(notdiscount);
        }

        public void setShowReceipt(string _insert)
        {
            showreceipt = _insert;
        }
        public void setShowDiscount(string _insert)
        {
            showdiscount = _insert;
        }
        public string getShowReceipt()
        {
            return PublicMethods.moneyFormater(showreceipt);
        }
        public string getShowDiscount()
        {
            return PublicMethods.moneyFormater(showdiscount);
        }
    }
    public class storeOrderInformation
    {
        string storemoney = "";
        public string storetype = "";
        public string code = "";
        public PayWay payway = PayWay.barcodePay;
        public void reload()
        {
            code = "";
            storemoney = "";
            storetype = "";
            payway = PayWay.barcodePay;
        }
        public void setstoremoney(string _insert)
        {
            storemoney = _insert;
        }
        public string getstoremoney()
        {
            return PublicMethods.moneyFormater(storemoney);
        }
    }
}
