using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace self_syt
{
    public class BaseRequest
    {
        public int iRHttp = 0;
        public string sRHttp = "";

        string log;

        public Dictionary<string, string> dic;

        public BaseRequest(string str = "base:::")
        {
            this.log = str;
            dic = new Dictionary<string, string>();

        }

        public virtual bool http()
        {
            return false;
        }


        public virtual bool get()
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

        public void Log(string str)
        {

            untils.UcomUntils.ConsoleMsg(log+str);
        }


        public void setPublicParameter(untils.Umurl url, string method)
        {
            url.addParameter("app_id", BaseValue.app_id);
            url.addParameter("method", method);
            url.addParameter("version", "1.0.0");
            url.addParameter("sign_type", "MD5");
        }
       

        public bool isRequestSuccess(string result)
        {
            if (result.IndexOf("\"errCode\":\"10000\"") != -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void addDic(string key, string value)
        {
            if (dic != null)
            {
                if (!dic.ContainsKey(key))
                {
                    dic.Add(key, value);
                }
            }
        }
    }
}
