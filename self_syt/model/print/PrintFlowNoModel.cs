using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace self_syt.model.print
{
    /*
     * 打印流水的类
     */
    public class PrintFlowNoModel
    {
        public string trade_time { get; set; }
        public string pay_channel { get; set; }
        public string pay_channel_type { get; set; }
        public string flow_no { get; set; }
        public string trade_type { get; set; }
        public string pay_money { get; set; }
        public string refund_money { get; set; }
        public string employee_name { get; set; }
        public string employee_account { get; set; }
        public string discount_money { get; set; }
        public string merchant_discount_money { get; set; }
    }
}
