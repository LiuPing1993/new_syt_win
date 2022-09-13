using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace qgj
{
    class summaryPrint
    {
        string sStartTime;
        string sEndTime;
        int iHttpResult = 0;
        string sHttpResult = "";
        orderCollectSucccess orderCollectS;

        public summaryPrint()
        {
            DateTime today = DateTime.Now;
            sStartTime = today.ToString("yyyy-MM-dd");
            sStartTime += " 00:00:00";
            sEndTime = today.ToString("yyyy-MM-dd");
            sEndTime += " 23:59:59";
        }

        public void print()
        {
            try
            {
                if (getSummary())
                {
                    summaryprintClass summaryprintC = new summaryprintClass();
                    summaryprintC.p = orderCollectS;
                    summaryprintC.PrintNow();
                }
                else
                {
                    errorinformationForm errorF = new errorinformationForm("提示", "获取数据失败：" + sHttpResult.ToString());
                    errorF.TopMost = true;
                    errorF.ShowDialog();
                }
            }
            catch (Exception e)
            {
                errorinformationForm errorF = new errorinformationForm("提示", "打印故障：" + e.ToString());
                errorF.TopMost = true;
                errorF.ShowDialog();
            }
        }

        public bool getSummary()
        {
            loadconfigClass _lcc = new loadconfigClass("terminal_sn");
            UrlClass _urlC = new UrlClass(Url.flowtotalsummary);
            _urlC.addParameter("terminal_sn", _lcc.readfromConfig());
            _urlC.addParameter("token", UserClass.Token);

            _urlC.addParameter("start_time", sStartTime);
            _urlC.addParameter("end_time", sEndTime);

            string _sRequestUrl = _urlC.requestUrl();
            Console.WriteLine("url:" + _sRequestUrl);

            HttpClass _httpC = new HttpClass();
            string _sRequestMsg = _httpC.HttpGet(_sRequestUrl);
            Console.WriteLine("result:" + _sRequestMsg);
            if (_sRequestMsg.IndexOf("\"errCode\":0") != -1)
            {
                orderCollectS = (orderCollectSucccess)JsonConvert.DeserializeObject(_sRequestMsg, typeof(orderCollectSucccess));
                iHttpResult = 0; ;
                sHttpResult = "请求成功";
                return true;
            }
            else
            {
                errorClass errorC = (errorClass)JsonConvert.DeserializeObject(_sRequestMsg, typeof(errorClass));
                iHttpResult = 1;
                sHttpResult = errorC.errMsg;
                return false;
            }
        }

    }
}
