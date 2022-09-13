using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace self_syt
{
    /*
     * 收银插件的全局变量
     * 
     */
    public class BaseModel
    {

        #region 主界面的全局变量
        public static mainform.MainForm mainFormModel;
        #endregion

        #region 全局打印日志的类
        public static untils.UwriteLogUntils baseLog;
        #endregion

        #region 登录界面的全局变量
        public static login.LoginBaseForm loginFormModel;
        #endregion

        #region 支付的全局变量
        public static pay.PayBaseForm payBaseValue;
        #endregion

        #region 菜单的全局变量
        public static menubar.MenuBarBaseForm menuBarFormValue;
        //支付方式的全局
        public static pay.paychanel.PayChannelForm payChannelValue;
        #endregion

        #region  支付相关
        //支付方式
        public static pay.paychanel.PayChannelBaseControl payChannelBaseValue;
        //收银
        public static pay.PayBaseControl payBaseControlValue;
        #endregion

        #region 订单相关
        //订单详细
        public static order.OrderBaseForm orderBaseFormValue;
        //交易订单
        public static order.transorder.TransOrderControl transOrderControlValue;
        //订单的basecontrl
        public static order.OrderBaseControl orderBaseControlValue;
        //流水
        public static order.moneyflow.MoneyFlowBaseControl moneyFlowBaseValue;

        #endregion

        #region 退款相关
        public static order.refund.RefundBaseForm refundFormValue;
        #endregion

        #region 设置相关
        public static setting.SetBaseForm setFormValue;
        #endregion

        #region 悬浮窗相关
        public static suspend.SuspendForm suspendFormValue;
        #endregion

        #region 设置热键相关
        public static untils.UhotKey hotKeySet;
        #endregion

        #region  虚拟串口接收数据
        public static untils.UserialPortHelpher baseSerialPortHelpher;
        #endregion

        #region 订单流水界面的选择框
        public static order.moneyflow.typeselect.TypeSelectForm typeSelectFormModel;
        #endregion


    }
}
