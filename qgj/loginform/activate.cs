using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace qgj.loginform
{
    class activate :baseRequest
    {
        public activateSuccess resultS;
        public string activation_code = "";
        public activate()
            : base("activate:::") { }

        public override bool http()
        {
            UrlClass url = new UrlClass();
            url.addParameter("app_id", BaseValue.app_id);
            url.addParameter("device_type", "1");
            url.addParameter("version", "1.0.0");
            url.addParameter("activation_code", activation_code);

            string requestUrl = url.requestUrl();
            Log("url: " + requestUrl);//请求url
            HttpClass http = new HttpClass();
            string result = http.HttpGet(requestUrl);
            Log("返回:" + result + " 检查 " + result.IndexOf("\"errCode\":10000"));

            if (result.IndexOf(success_str) != -1)
            {
                resultS = (activateSuccess)JsonConvert.DeserializeObject(result, typeof(activateSuccess));
                iRHttp = 0;
                sRHttp = "成功";
                return true;
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

    public class activateData
    {
        public string app_id { get; set; }
        public string app_key { get; set; }
        public string merchant_code { get; set; }
        public string merchant_name { get; set; }
        public string merchant_short_name { get; set; }
    }

    public class activateSuccess
    {
        public string errCode { get; set; }
        public string errMsg { get; set; }
        public activateData data { get; set; }
    }
}
