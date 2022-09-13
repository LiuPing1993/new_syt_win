using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


namespace self_syt
{

    /*
     * 
     * 项目所用的所有的枚举
     */

    public class BaseEnum
    {

        /// <summary>
        /// 打印的订单类型的枚举
        /// </summary>
        public enum PrintTypeEnum
        {
            [Description("商户联")]
            print_merchant = 1,
            [Description("顾客联")]
            print_customer = 2,
            [Description("商户联+顾客联")]
            print_merAndCus = 3
        }
        /// <summary>
        /// 支付场景枚举
        /// </summary>
        public enum PaySenceEnum
        {
            [Description("收款")]
            pay_sence_trade = 1,
            [Description("线下储值")]
            pay_sence_recharge = 2,
            [Description("营销活动")]
            pay_sence_marking = 3,
            [Description("线上商城")]
            pay_sence_mall = 4,
            [Description("线上储值")]
            pay_sence_recharge_online = 5,
            [Description("餐饮小程序")]
            pay_sence_wechatapp = 6,
            [Description("门店POS")]
            pay_sence_pos = 7,
            [Description("自助POS")]
            pay_sence_self_pos = 8,
            [Description("外卖平台")]
            pay_sence_w_platform = 9,
            [Description("付费购券")]
            pay_sence_payCupon = 10,
            [Description("订购服务")]
            pay_sence_payServices = 11,
            [Description("支付宝小程序")]
            pay_sence_alipayapp = 12


        }
        public enum RecongtionTypeEnum
        {
            [Description("图像识别")]
            ocr,
            [Description("虚拟串口")]
            com
        }
        /// <summary>
        /// 设置的类型
        /// </summary>
        public enum SetTypeEnum
        {
            [Description("基本设置")]
            baseSet = 1,
            [Description("收银设置")]
            paySet = 2,
            [Description("打印设置")]
            printSet = 3,
            [Description("其他设置")]
            otherSet = 5
        }
        /// <summary>
        /// 支付场景下的支付类型 12-17都为自定义支付
        /// </summary>
        public enum PayChannelTypeEnum
        {
            [Description("条码支付")]
            bar_pay = 1,
            [Description("扫码支付")]
            scanBar_pay = 2,
            [Description("JSAPI支付")]
            jsapi_pay = 3,
            [Description("小程序支付")]
            xcx_pay = 4,
            [Description("银联记账")]
            union_pay = 5,
            [Description("银联收款")]
            union_receivedPay = 6,
            [Description("刷脸支付")]
            face_pay = 7,
            [Description("现金支付")]
            crash_pay = 8,
            [Description("饿了么记账")]
            ele_pay = 9,
            [Description("美团记账")]
            mt_pay = 10,
            [Description("储值支付")]
            recharge_pay = 11,
            [Description("自定义")]
            other = 12,
        }
        /// <summary>
        /// 支付的类型
        /// </summary>
        public enum PayChannelEnum
        {
            [Description("未知")]
            other = 0,
            [Description("支付宝")]
            zfb = 1,
            [Description("微信")]
            wx = 2,
            [Description("记账")]
            jz = 3,
            [Description("储值")]
            recharge = 4,
            [Description("自定义")]
            zdy = 5,
            [Description("云闪付")]
            cloudPay = 6
        }
        /// <summary>
        /// 订单状态
        /// </summary>
        public enum OrderStatusEnum
        {
            [Description("待付款")]
            waitPay = 1,
            [Description("已付款")]
            paySuccess = 2,
            [Description("部分退款")]
            partRefund = 3,
            [Description("全部退款")]
            allRefund = 4
        }
        /// <summary>
        /// 订单流水枚举类型
        /// </summary>
        public enum OrderListEnum
        {
            [Description("财务流水")]
            moneyFlow = 1,
            [Description("交易订单")]
            transOrder = 2,
        }
        /// <summary>
        /// 支付结果
        /// </summary>
        public enum PayResultType
        {
            [Description("成功")]
            success = 1,
            [Description("失败")]
            failed = 2
        }
        /// <summary>
        /// 支付的几种方式
        /// </summary>
        public enum PayChannelType
        {
            [Description("条码支付")]
            codeBar = 1,
            //[Description("扫码支付")]
            //scanBar = 2,
            //[Description("储值支付")]
            //recharge = 3,
            [Description("现金支付")]
            crash = 4
        }
        /// <summary>
        /// 菜单的类型
        /// </summary>
        public enum MenuBarType
        {
            [Description("收款")]
            pay = 1,
            [Description("订单")]
            order = 2,
            //[Description("验券")]
            //checkCoupe = 3,
            //[Description("储值")]
            //recharge = 4,
            //[Description("会员")]
            //member = 5,
            [Description("设置")]
            setting = 6,
            [Description("退出")]
            exit = 8,
            [Description("无")]
            none = 7
        }
        /// <summary>
        /// 登录的类型的枚举
        /// </summary>
        public enum LoginTypeEnum
        {
            [Description("注册")]
            active = 0,
            [Description("登录")]
            login = 1,
            [Description("等待")]
            wait = 2,
            [Description("关闭")]
            none = 3
        }
        /// <summary>
        /// 控件圆角位置的枚举
        /// </summary>
        public enum RadiusLocation
        {
            [Description("无")]
            none = 0,
            [Description("左上")]
            left_top = 1,
            [Description("右上")]
            right_top = 2,
            [Description("左下")]
            left_bottom = 3,
            [Description("右下")]
            right_bottom = 4
        }
        /// <summary>
        /// 打印时候照片的位置
        /// </summary>
        public enum PrintImageLocation
        {
            [Description("左")]
            left = 1,
            [Description("中")]
            center = 2,
            [Description("右")]
            right = 3
        }
        /// <summary>
        /// 打印字体的位置
        /// </summary>
        public enum PrintStrLocation
        {
            [Description("一分")]
            one,
            [Description("二分一")]
            two_one,
            [Description("二分二")]
            two_two,
            [Description("三分一")]
            three_one,
            [Description("三分二")]
            three_two,
            [Description("三分三")]
            three_three,
            [Description("四分一")]
            four_one,
            [Description("四分二")]
            four_two,
            [Description("四分三")]
            four_three,
            [Description("四分四")]
            four_four
        }
        /// <summary>
        /// 打印文字的类型
        /// </summary>
        public enum PrintStrType
        {
            [Description("标题")]
            title,
            [Description("商品")]
            good,
            [Description("做法")]
            method,
            [Description("其他")]
            other,
            [Description("最大")]
            max
        }
        /// <summary>
        /// 打印纸的类型
        /// </summary>
        public enum PrintPageSize
        {
            [Description("s_58")]
            s_58,
            [Description("s_80")]
            s_80
        }
        /// <summary>
        /// 串口打印的类型
        /// </summary>
        public enum ComPrintType
        {
            [Description("下单商户联")]
            payMerchant = 1,
            [Description("下单顾客联")]
            payCustomer = 2,
            [Description("退单商家联")]
            refundMerchant = 3,
            [Description("退单顾客联")]
            refundCustomer = 4,
            [Description("详情商户联")]
            detailMerchant = 5,
            [Description("详情顾客联")]
            detailCustomer
        }
    }
}
