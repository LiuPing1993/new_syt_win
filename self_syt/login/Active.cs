using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace self_syt.login
{
    /*
     * 功能：收银插件注册
     */
    public class Active : BaseRequest
    {
        public string code = "";
        public string app_id = "1528202001";
        public Active()
            : base("active：")
        {
        }
        public override bool http()
        {
            untils.Umurl url = new untils.Umurl(Url.activate);
            url.addParameter("app_id", app_id);
            url.addParameter("activation_code", code);
            url.addParameter("device_type", "1");
            url.addParameter("version", "1.0.0");
            string requestUrl = url.requestUrl();
            Log(requestUrl);

            untils.UhttpUntils http = new untils.UhttpUntils();
            string result = "";
            result = http.HttpGet(requestUrl, true);
            Log(result);
            if (isRequestSuccess(result))
            {
                activateSuccess loginS = (activateSuccess)JsonConvert.DeserializeObject(result, typeof(activateSuccess));
    
                BaseValue.merchant_code = loginS.data.merchant_code;
                BaseValue.app_id = loginS.data.app_id;

                untils.UoperaConfig uc = new untils.UoperaConfig("app_id");
                uc.WriteConfig(loginS.data.app_id);

                uc = new untils.UoperaConfig("merchant_code");
                uc.WriteConfig(loginS.data.merchant_code);

                untils.UcomUntils.writeIn(loginS.data.app_key);

                iRHttp = 0;
                sRHttp = "登录成功";
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
}
