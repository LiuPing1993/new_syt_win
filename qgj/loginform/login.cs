using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace qgj.loginform
{
    class login : baseRequest
    {
        public loginSuccess resultS;
        public string account = "";
        public string pwd = "";
        public login()
            : base("login:::") { }

        public override bool http()
        {
            UrlClass url = new UrlClass(Url.login);
            url.addParameter("app_id", BaseValue.app_id);
            url.addParameter("method", "merchant.employee.login");
            url.addParameter("sign_type", "MD5");
            url.addParameter("version", "1.0.0");
            url.addParameter("login_type", "2");
            url.addParameter("merchant_code", BaseValue.merchant_code);
            url.addParameter("account",account);
            url.addParameter("pwd", PublicMethods.md5(pwd).ToLower());

            string requestUrl = url.requestUrl();
            Log("url: " + requestUrl);//请求url
            HttpClass http = new HttpClass();
            string result = http.HttpGet(requestUrl);
            if (result.IndexOf(success_str) != -1)
            {
                resultS = (loginSuccess)JsonConvert.DeserializeObject(result, typeof(loginSuccess));
                iRHttp = 0;
                sRHttp = "success";
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
    }

    public class loginSuccess
    {
        public string errCode { get; set; }
        public string errMsg { get; set; }
        public loginData data { get; set; }
    }
}
