using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace self_syt.model.print
{
    /*
     * 打印商户的存根
     */
    public class PrintMerchantModel
    {
        /// <summary>
        /// 商户名称
        /// </summary>
        public string merchantName { get; set; }
        /// <summary>
        ///门店名称
        /// </summary>
        public string storeName { get; set; }
        /// <summary>
        /// 操作者
        /// </summary>
        public string operaUser { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public string order_no { get; set; }
        /// <summary>
        /// 交易类型
        /// </summary>
        public string tradeType { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public string orderStatus { get; set; }
        /// <summary>
        /// 订单金额
        /// </summary>
        public string tradeMoney { get; set; }
        /// <summary>
        /// 优惠金额
        /// </summary>
        public string discountMoney { get; set; }
        /// <summary>
        /// 收款金额
        /// </summary>
        public string payMoney { get; set; }
        /// <summary>
        /// 支付宝优惠
        /// </summary>
        public string zfbDiscount { get; set; }
        /// <summary>
        /// 用户实付
        /// </summary>
        public string realPayMoney { get; set; }
        /// <summary>
        /// 支付时间
        /// </summary>
        public string payTime { get; set; }
        /// <summary>
        /// 系统流水号
        /// </summary>
        public string system_flow_no { get; set; }
        /// <summary>
        /// 银行流水号
        /// </summary>
        public string bank_flow_no { get; set; }
        /// <summary>
        ///支付流水号
        /// </summary>
        public string pay_flow_no { get; set; }
        /// <summary>
        /// 退款金额
        /// </summary>
        public string refund_money { get; set; }
        /// <summary>
        /// 退款的流水的类
        /// </summary>
        public List<PrintFlowNoModel> refundFlowList { get; set; }

    }
}
