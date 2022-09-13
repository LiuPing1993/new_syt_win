using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace qgj.newpay
{
    class toPay : baseRequest
    {
        public barSuccess resultS;
        public string auth_code = "";
        public string out_order_no = "";
        public string collection_account = "";
        public string pay_money = "";

        public toPay()
            : base("pay:::") { }

        public override bool http()
        {
            UrlClass url = new UrlClass(Url.pay_open);
            url.addParameter("app_id", BaseValue.app_id);
            url.addParameter("method", "pay.pay.barcode");
            url.addParameter("token", BaseValue.token);
            url.addParameter("sign_type", "MD5");
            url.addParameter("version", "1.0.0");
            url.addParameter("pay_scene", "1");
            url.addParameter("auth_code", auth_code);
            url.addParameter("out_order_no", out_order_no);
            url.addParameter("collection_account", collection_account);
            url.addParameter("pay_money", pay_money);

            string requestUrl = url.requestUrl();
            Log("url: " + requestUrl);//请求url

            HttpClass http = new HttpClass();
            string result = http.HttpGet(requestUrl);
            if (result.IndexOf(success_str) != -1)
            {
                Log("result: " + result);//请求url
                resultS = (barSuccess)JsonConvert.DeserializeObject(result, typeof(barSuccess));

                if(resultS.data.resultCode == "30001")
                {
                    iRHttp = 1;
                    sRHttp = resultS.data.resultMsg;
                    return false;
                }
                else
                {
                    iRHttp = 0;
                    sRHttp = "success";
                    return true;
                }
            }
            else
            {
                errorClass errorC = (errorClass)JsonConvert.DeserializeObject(result, typeof(errorClass));
                iRHttp = 1;
                sRHttp = errorC.errMsg;
                return false;
            }
        }

        public override bool get()
        {
            http();
            if (iRHttp == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class payOrderData
    {
        public string resultCode { get; set; }
        public string resultMsg { get; set; }
        public string app_id { get; set; }
        public string pay_scene { get; set; }
        public string out_order_no { get; set; }
        public string flow_no { get; set; }
        public string status { get; set; }
        public string pay_time { get; set; }
        public string close_time { get; set; }
        public string reverse_time { get; set; }
        public string pay_money { get; set; }
        public string pay_channel { get; set; }
        public string pay_channel_type { get; set; }
        public string pay_type { get; set; }
        public string out_trade_no { get; set; }
        public string channel_trade_no { get; set; }
        public string attach { get; set; }
        public string userid { get; set; }
        public string collection_account { get; set; }
        public string wechat_mchid { get; set; }
        public string wechat_appid { get; set; }
        public string alipay_appid { get; set; }
        public string alipay_koubei_store_no { get; set; }
        public string alipay_out_store_no { get; set; }
        public string mybank_mchid { get; set; }
        public string newland_mchid { get; set; }
        public string newland_store { get; set; }
        public string newland_trmno { get; set; }
        public string discount_money { get; set; }
        public string merchant_discount_money { get; set; }
        public string rate { get; set; }
        public string rate_money { get; set; }
    }

    public class barSuccess
    {
        public string errCode { get; set; }
        public string errMsg { get; set; }
        public payOrderData data { get; set; }
    }

}
