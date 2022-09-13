using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace qgj.newTrade
{
    class order_list:baseRequest
    {
        public orderListSuccess resultS;
        public string start_time = "";
        public string end_time = "";
        public string order_status = "";
        public string page = "";
        public string limit = "";

        public bool is_reload = true;
        public order_list()
            : base("create:::") { }

        public override bool http()
        {
            UrlClass url = new UrlClass(Url.trade_open);
            url.addParameter("app_id", BaseValue.app_id);
            url.addParameter("method", "trade.order.list");
            url.addParameter("sign_type", "MD5");
            url.addParameter("version", "1.0.0");
            url.addParameter("start_time", start_time);
            url.addParameter("end_time", end_time);
            url.addParameter("order_status", order_status);
            url.addParameter("page", page);
            url.addParameter("limit", limit);

            string requestUrl = url.requestUrl();
            Log("url: " + requestUrl);//请求url
            HttpClass http = new HttpClass();
            string result = http.HttpGet(requestUrl);
            if (result.IndexOf("\"errCode\":0") != -1)
            {
                resultS = (orderListSuccess)JsonConvert.DeserializeObject(result, typeof(orderListSuccess));
                iRHttp = 0;
                sRHttp = "success";
                return true;
            }
            else
            {
                errorClass _errorC = (errorClass)JsonConvert.DeserializeObject(result, typeof(errorClass));
                iRHttp = 1;
                sRHttp = _errorC.errMsg;
                return false;
            }
        }

        public override bool get()
        {
            http();
            if(iRHttp == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class orderListItem
    {
        public string app_id { get; set; }
        public string merchant_code { get; set; }
        public string merchant_name { get; set; }
        public string store_code { get; set; }
        public string store_name { get; set; }
        public string store_branch_name { get; set; }
        public string employee_code { get; set; }
        public string employee_name { get; set; }
        public string employee_account { get; set; }
        public string user_code { get; set; }
        public string order_no { get; set; }
        public string pay_type { get; set; }
        public string pay_channel { get; set; }
        public string pay_channel_type { get; set; }
        public string trade_money { get; set; }
        public string undiscount_trade_money { get; set; }
        public string coupon_discount_money { get; set; }
        public string member_discount_money { get; set; }
        public string order_status { get; set; }
        public string remark { get; set; }
        public string create_time { get; set; }
    }

    public class orderListData
    {
        public List<orderListItem> list { get; set; }
        public string total_num { get; set; }
        public string page { get; set; }
        public string limit { get; set; }
    }

    public class orderListSuccess
    {
        public string errCode { get; set; }
        public string errMsg { get; set; }
        public orderListData data { get; set; }
    }

}
