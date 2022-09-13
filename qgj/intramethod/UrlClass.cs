using System;
using System.Collections.Generic;
using System.Text;

namespace qgj
{
    class UrlClass
    {
        StringBuilder sbUrl = new StringBuilder();
        StringBuilder sbSign = new StringBuilder();

        StringBuilder sbStrToSgin = new StringBuilder();

        private Dictionary<string, string> dicParameter = new Dictionary<string, string>();
        string sKey;
        public UrlClass()
        {
            sbUrl.Append(Url.activate);
            sKey = "";
        }
        public UrlClass(string _url)
        {
            sbUrl.Append(_url);
            sKey = PublicMethods.readOut();
        }
        public void addParameter(string valuename, string value)
        {
            dicParameter.Add(valuename, value);
        }
        public string requestUrl()
        {
            sbUrl.Append(DictionarySort(dicParameter));

            if(sKey == "")
            {
                return sbUrl.ToString();
            }

            sbStrToSgin.Append("key=" + sKey);

            sbSign.Append(PublicMethods.md5(sbStrToSgin.ToString()).ToLower());

            return sbUrl.Append("&sign=" + sbSign.ToString()).ToString();
        }

        private string DictionarySort(Dictionary<string, string> _dic)
        {
            if (_dic.Count > 0)
            {
                List<KeyValuePair<string, string>> _lst = new List<KeyValuePair<string, string>>(_dic);
                _lst.Sort(delegate(KeyValuePair<string, string> s1, KeyValuePair<string, string> s2)
                {
                    if (string.CompareOrdinal(s1.Key, s2.Key) < 0)
                    {
                        return -1;
                    }
                    else if (string.CompareOrdinal(s1.Key, s2.Key) > 0)
                    {
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                });
                StringBuilder _param = new StringBuilder();
                foreach (KeyValuePair<string, string> kvp in _lst)
                {
                    _param.Append(kvp.Key + "=" + kvp.Value + "&");
                    if (kvp.Value != "")
                    {
                        sbStrToSgin.Append(kvp.Key + "=" + kvp.Value + "&");
                    }
                }
                return _param.ToString().Remove(_param.ToString().Length - 1, 1);
            }
            return "";
        } 
    }
}
