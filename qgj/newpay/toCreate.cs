using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace qgj.newpay
{
    class toCreate : baseRequest
    {
        public createSuccess resultS;
        public string trade_money = "0";
        public string undiscount_money = "0";
        public toCreate()
            : base("create:::") { }

        public override bool http()
        {
            UrlClass url = new UrlClass(Url.trade_open);
            url.addParameter("app_id", BaseValue.app_id);
            url.addParameter("token", BaseValue.token);
            url.addParameter("method", "trade.order.create");
            url.addParameter("sign_type", "MD5");
            url.addParameter("version", "1.0.0");
            url.addParameter("login_type", "2");
            url.addParameter("merchant_code", BaseValue.merchant_code);
            url.addParameter("trade_money", trade_money);

            string requestUrl = url.requestUrl();
            Log("url: " + requestUrl);//请求url
            HttpClass http = new HttpClass();
            string result = http.HttpGet(requestUrl);
            if (result.IndexOf(success_str) != -1)
            {
                resultS = (createSuccess)JsonConvert.DeserializeObject(result, typeof(createSuccess));
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

    public class createOrder
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

    public class createData
    {
        public createOrder order { get; set; }
        public string collection_account { get; set; }
    }

    public class createSuccess
    {
        public string errCode { get; set; }
        public string errMsg { get; set; }
        public createData data { get; set; }
    }
}
