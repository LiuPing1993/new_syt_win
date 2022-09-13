using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using System.Threading;

namespace qgj.newpay
{
    class toQuery : baseRequest
    {
        public querySuccess resultS;
        string order_no = "";

        public string discount_name = "";
        public string discount_money = "";
        public toQuery(string value)
            : base("query:::") 
         {
             order_no = value;
         }

        public override bool http()
        {
            UrlClass url = new UrlClass(Url.trade_open);
            url.addParameter("app_id", BaseValue.app_id);
            url.addParameter("token", BaseValue.token);
            url.addParameter("method", "trade.order.detail");
            url.addParameter("sign_type", "MD5");
            url.addParameter("version", "1.0.0");
            url.addParameter("order_no", order_no);

            string requestUrl = url.requestUrl();
            Log("url: " + requestUrl);//请求url

            HttpClass http = new HttpClass();
            string result = http.HttpGet(requestUrl);
            if (result.IndexOf(success_str) != -1)
            {
                resultS = (querySuccess)JsonConvert.DeserializeObject(result, typeof(querySuccess));
                iRHttp = 0;
                sRHttp = "success";
                if (resultS.data.order_status == "2")
                {
                    if (resultS.data.discount_money != "0")
                    {
                        discount_name = "平台优惠";
                        discount_money = resultS.data.discount_money;
                    }
                    if (resultS.data.merchant_discount_money != "0")
                    {
                        discount_name = "商家优惠";
                        discount_money = resultS.data.merchant_discount_money;
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else if (result != "")
            {
                errorClass _errorC = (errorClass)JsonConvert.DeserializeObject(result, typeof(errorClass));
                iRHttp = 1;
                sRHttp = _errorC.errMsg;
                return false;
            }
            else
            {
                return false;
            }
        }

        public override bool get()
        {
            while(true)
            {
                if(http())
                {
                    break;
                }
                else
                {
                    //1.5秒循环调取一次
                    Thread.Sleep(1500);
                }
            } 
            return true;
        }
    }

    public class queryData
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
        public string discount_money { get; set; }
        public string merchant_discount_money { get; set; }
        public string out_trade_no { get; set; }
        public string flow_no { get; set; }
    }

    public class querySuccess
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
        public queryData data { get; set; }
    }

}
