using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace self_syt.login
{
    public class Login : BaseRequest
    {
        public string account = "";
        public string pwd = "";
        untils.UoperaConfig config = new untils.UoperaConfig(BaseConfigValue.remberPwd);

        public Login() : base("login：") { }

        public override bool http()
        {
            untils.Umurl url = new untils.Umurl(Url.login);
            //设置方法名称
            setPublicParameter(url, "trade.employee.login");
            url.addParameter("merchant_code", BaseValue.merchant_code);
            url.addParameter("account", account);
            url.addParameter("pwd", untils.UcomUntils.Md5(pwd).ToLower());

            string requestUrl = url.requestUrl();
            Log(requestUrl);

            untils.UhttpUntils http = new untils.UhttpUntils();
            string result = http.HttpGet(requestUrl, true);
            Log(result);

            if (isRequestSuccess(result))
            {
                loginSuccess logsuccs = (loginSuccess)JsonConvert.DeserializeObject(result, typeof(loginSuccess));
                BaseValue.token = logsuccs.data.token;
                BaseValue.merchant_code = logsuccs.data.merchant_code;
                BaseValue.employee_name = logsuccs.data.employee_name;
                BaseValue.employee_code = logsuccs.data.employee_code;
                BaseValue.merchant_name = logsuccs.data.merchant_name;
                BaseValue.store_name = logsuccs.data.store_name;
                BaseValue.store_branch_name = logsuccs.data.store_branch_name;
                if (config.ReadConfig() == "1")
                {
                    config = new untils.UoperaConfig(BaseConfigValue.userName);
                    config.WriteConfig(account);

                    config = new untils.UoperaConfig(BaseConfigValue.passwWord);
                    config.WriteConfig(untils.Usecurity.inCode(pwd));
                }
                iRHttp = 0;
                sRHttp = "登陆成功";
                return true;
            }
            else
            {
                errorClass err = (errorClass)JsonConvert.DeserializeObject(result, typeof(errorClass));
                iRHttp = 1;
                sRHttp = err.errMsg;
                return false;
            }
        }

        public class loginData
        {
            public string app_id { get; set; }
            public string token { get; set; }
            public string merchant_code { get; set; }
            public string merchant_short_name { get; set; }
            public string merchant_name { get; set; }
            public string store_code { get; set; }
            public string store_name { get; set; }
            public string store_branch_name { get; set; }
            public string employee_code { get; set; }
            public string employee_account { get; set; }
            public string employee_role { get; set; }
            public string employee_name { get; set; }
            public List<string> employee_right_list { get; set; }
        }

        public class loginSuccess
        {
            public string errCode { get; set; }
            public string errMsg { get; set; }
            public loginData data { get; set; }
        }
    }
}
