using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace qgj
{
    public class discountHttpClass
    {
        public bool discountHttp(ref string _errormsg)
        {
            string _money = UserClass.orderInfoC.getMoney() == "" ? "0" : UserClass.orderInfoC.getMoney();
            string _undiscount = UserClass.orderInfoC.getNotDiscount();
            string _coupon = UserClass.orderInfoC.coupon;
            string _member = UserClass.orderInfoC.member;
            string _store = UserClass.orderInfoC.usestore;
            try
            {
                if (_coupon != "")
                {
                    _coupon = _coupon.Remove(_coupon.Length - 1);
                }
                loadconfigClass lcc = new loadconfigClass("terminal_sn");
                UrlClass _urlC = new UrlClass(Url.countneedpay);
                _urlC.addParameter("terminal_sn", lcc.readfromConfig());
                _urlC.addParameter("token", UserClass.Token);
                if (UserClass.orderInfoC.UseMemberDiscount)
                {
                    _urlC.addParameter("code", _member);
                }
                else
                {
                    _urlC.addParameter("code", "");
                }
                _urlC.addParameter("order_money", _money);
                _urlC.addParameter("undiscountable_money", _undiscount);
                _urlC.addParameter("coupon_id_list", _coupon);

                string _sRequestUrl = _urlC.requestUrl();
                Console.WriteLine("url:" + _sRequestUrl);

                HttpClass http = new HttpClass();
                string requestmsg = http.HttpGet(_sRequestUrl);
                Console.WriteLine("result:" + requestmsg);
                if (requestmsg.IndexOf("\"errCode\":0") != -1)
                {
                    countneedpaySuccess tp = (countneedpaySuccess)JsonConvert.DeserializeObject(requestmsg, typeof(countneedpaySuccess));

                    UserClass.orderInfoC.setShowReceipt(tp.data.pay_money);
                    UserClass.orderInfoC.setShowDiscount(tp.data.discount_money);
                    _errormsg = "";
                    return true;
                }
                else
                {
                    errorClass errorp = (errorClass)JsonConvert.DeserializeObject(requestmsg, typeof(errorClass));
                    _errormsg = errorp.errMsg;
                    return false;
                }
            }
            catch(Exception e)
            {
                _errormsg = e.Message.ToString();
                return false;
            }
            //调试输出
            //Console.WriteLine("money:" + UserClass.orderInfoC.getMoney() + "-------------------------------------");
            //Console.WriteLine("undiscount:" + UserClass.orderInfoC.getNotDiscount());
            //Console.WriteLine("coupon:" + UserClass.orderInfoC.coupon);
            //Console.WriteLine("member:" + UserClass.orderInfoC.member);
            //Console.WriteLine("receipt:" + UserClass.orderInfoC.getShowReceipt());
            //Console.WriteLine("discount:" + UserClass.orderInfoC.getShowDiscount() + "-------------------------------------");
            //return true;
        }
    }
    public class Member_info
    {
        /// <summary>
        /// 头像
        /// </summary>
        public string avatar { get; set; }
        /// <summary>
        /// 会员名
        /// </summary>
        public string user_name { get; set; }
        /// <summary>
        /// 会员账号（手机）
        /// </summary>
        public string account { get; set; }
        /// <summary>
        /// 会员号
        /// </summary>
        public string member_code { get; set; }
        /// <summary>
        /// 储余额
        /// </summary>
        public string balance { get; set; }
        /// <summary>
        /// 积分
        /// </summary>
        public string bonus { get; set; }
        /// <summary>
        /// 会员等级名
        /// </summary>
        public string member_level { get; set; }
        /// <summary>
        /// 会员折扣
        /// </summary>
        public string member_discount { get; set; }
    }
    public class Coupon_listItem
    {
        /// <summary>
        /// -无视-
        /// </summary>
        public string coupon_id { get; set; }
        /// <summary>
        /// 核销id
        /// </summary>
        public string id { get; set; }
        /// <summary>
        /// 券码
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 起始时间
        /// </summary>
        public string start_time { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public string end_time { get; set; }
        /// <summary>
        /// 券标题
        /// </summary>
        public string title { get; set; }
        /// <summary>
        /// 券类型
        /// </summary>
        public string type { get; set; }
        /// <summary>
        /// 最小使用金额
        /// </summary>
        public string least_cost { get; set; }
        /// <summary>
        /// 优惠金额
        /// </summary>
        public string reduce_cost { get; set; }
        /// <summary>
        /// 折扣金额
        /// </summary>
        public string discount { get; set; }
        /// <summary>
        /// 兑换券
        /// </summary>
        public string gift { get; set; }
        /// <summary>
        /// 颜色
        /// </summary>
        public string color { get; set; }
        /// <summary>
        /// 是否能与会员同用
        /// </summary>
        public string can_use_with_member_card { get; set; }
        /// <summary>
        /// 能否与其他折扣同用
        /// </summary>
        public string can_use_with_other_discount { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string least_cost_title { get; set; }
        /// <summary>
        /// 0.1折折扣券,满100元可用
        /// </summary>
        public string sub_title { get; set; }
        /// <summary>
        /// 券0.1折
        /// </summary>
        public string tips { get; set; }
    }
    public class memberData
    {
        /// <summary>
        /// 会员信息
        /// </summary>
        public Member_info member_info { get; set; }
        /// <summary>
        /// 优惠券列表
        /// </summary>
        public List<Coupon_listItem> coupon_list { get; set; }
        /// <summary>
        /// 请求类型
        /// </summary>
        public string input_type { get; set; }
    }
    public class memberSuccess
    {
        /// <summary>
        /// 错误码
        /// </summary>
        public string errCode { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        public string errMsg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public memberData data { get; set; }
    }
    public class countneedpayData
    {
        /// <summary>
        /// 优惠金额
        /// </summary>
        public string discount_money { get; set; }

        /// <summary>
        /// 应收金额
        /// </summary>
        public string pay_money { get; set; }

        /// <summary>
        /// 不可选卡券
        /// </summary>
        public string disable_coupon { get; set; }

        /// <summary>
        /// 接下来能否使用会员优惠
        /// </summary>
        public string can_use_member_discount { get; set; }
    }
    public class countneedpaySuccess
    {
        /// <summary>
        /// 
        /// </summary>
        public string errCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string errMsg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public countneedpayData data { get; set; }
    }
}
