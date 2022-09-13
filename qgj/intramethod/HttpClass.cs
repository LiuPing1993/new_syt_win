using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using System.Threading;
using System.Web;
using System.Net.Security;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace qgj
{
    class HttpClass
    {
        public HttpClass()
        {
            System.Net.ServicePointManager.DefaultConnectionLimit = 50;
        }
        /// <summary>
        /// get方式的http访问
        /// </summary>
        /// <param name="Url">目标url</param>
        /// <returns>返回接收的到的字符流</returns>
        public string HttpGet(string Url)
        {
            string retString = "";
            string sProxyUrl = "";

            int statusCode = 200;

            try
            {
                loadconfigClass _lcc = new loadconfigClass("proxy");
                sProxyUrl = _lcc.readfromConfig();
                if (sProxyUrl == "")
                {
                    System.Net.HttpWebRequest.DefaultWebProxy = null;
                }
                else
                {
                    WebProxy proxy = new WebProxy(sProxyUrl, false);
                    System.Net.HttpWebRequest.DefaultWebProxy = proxy;
                }
            }
            catch { }
            WriteHttpLog(Url);//输出请求URL到文件
            try
            {
                ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Method = "GET";
                //request.Timeout = 5000;
                request.ContentType = "text/html;charset=UTF-8";
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));

                int httpStatusCode = (int)response.StatusCode;

                retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();
                request.Abort();
                request = null;
                WriteHttpLog(retString + "(" + httpStatusCode.ToString()+")");//输出请求返回的内容到文件
                statusCode = httpStatusCode;
                return retString;
            }
            catch (WebException we)
            {
                PublicMethods.WriteLog(we);
                throw new Exception("网络异常,请检查防火墙等相关设置(" + statusCode + ")");
            }
            catch (Exception se)
            {
                PublicMethods.WriteLog(se.ToString());
                return retString;
            }
        }

        public string HttpPost(string Url, string postDataStr)
        {
            string retString = "";
            WriteHttpLog(Url);//输出请求URL到文件
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);
                request.Method = "POST";
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = Encoding.UTF8.GetByteCount(postDataStr);
                Stream myRequestStream = request.GetRequestStream();
                StreamWriter myStreamWriter = new StreamWriter(myRequestStream, Encoding.GetEncoding("gb2312"));
                myStreamWriter.Write(postDataStr);
                myStreamWriter.Close();

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();
                int httpStatusCode = (int)response.StatusCode;
                WriteHttpLog(retString + "(" + httpStatusCode.ToString() + ")");//输出请求返回的内容到文件
                return retString;
            }
            catch (Exception we)
            {
                PublicMethods.WriteLog(we);
                return retString;
            }
           
            
        }

        public bool CheckValidationResult(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors)
        {   
            // Always accept
            // Console.WriteLine("accept " + certificate.GetName());
            return true; //总是接受
        }
        public static void WriteHttpLog(string _text, string _LogAddress = "")
        {
            //todo 日志等级
            if (Url.url.IndexOf("test") != -1 || Url.url.IndexOf("master") != -1 || Url.url.IndexOf("alittle-tea") != -1)
            {
                #region http请求日志
                try
                {
                    //如果日志文件为空，则默认目录下新建 YYYY-mm-dd_Log.log文件
                    if (_LogAddress == "")
                    {
                        _LogAddress = System.Windows.Forms.Application.StartupPath + '\\' +
                            DateTime.Now.Year + '-' +
                            DateTime.Now.Month + "_HttpLog.log";
                    }
                    //把异常信息输出到文件
                    StreamWriter _fs = new StreamWriter(_LogAddress, true);
                    _fs.WriteLine("当前时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff"));
                    _fs.WriteLine("网络信息：" + _text);
                    _fs.WriteLine("系统信息：" + PublicMethods.cpuAndMemory());
                    _fs.WriteLine();
                    _fs.Close();
                }
                catch { }
                #endregion
            }
        }
    }

    class Url
    {
        //todo 新收银台接口域名
        public static string url = "";//默认
        public static string mch_url = "https://mchapitest.qinguanjia.com";
        public static string trade_url = "https://tradetest.qinguanjia.com";
        public static string pay_url = "https://paytest.qinguanjia.com";

        /// <summary>
        /// 判断是否使用IP访问
        /// </summary>
        public static string getUrl(string _url)
        {
            loadconfigClass _lcc1 = new loadconfigClass("server_ip");
            string sIP = _lcc1.readfromConfig();

            if (sIP != "")
            {
                loadconfigClass _lcc2 = new loadconfigClass("ssl");
                string sHttp = _lcc2.readfromConfig();
                if (sHttp == "https")
                {
                    url = "https://" + sIP;
                    return "https://" + sIP;
                }
                else
                {
                    url = "http://" + sIP;
                    return "http://" + sIP;
                }
            }
            else
            {
                return _url;
            }
        }

        public static string mch_open = getUrl(url) + "/open?";//商户接口入口
        public static string trade_open = getUrl(trade_url) + "/open?";//交易接口入口
        public static string pay_open = getUrl(pay_url) + "/open?";//支付接口入口

        public static string activate = getUrl(mch_url) + "/device-activate/activate?";//激活
        public static string login = getUrl(mch_url) + "/open?";//登录




        public static string updatepwd = getUrl(url) + "/cashier/home/update-pwd?";//修改密码

        public static string getmember = getUrl(url) + "/cashier/member/get-member-info?";//获取会员、卡券等信息
        public static string countneedpay = getUrl(url) + "/cashier/member/count-need-pay?";//计算优惠

        public static string storelist = getUrl(url) + "/cashier/recharge/recharge-activity-list?";//获取储值列表

        //普通收银
        public static string create = getUrl(url) + "/open?";//创建订单
        public static string pay = getUrl(url) + "/cashier/trade/pay?";//微信支付宝条码支付
        public static string precreate = getUrl(url) + "/cashier/trade/precreate?";//微信支付宝扫码支付
        public static string query = getUrl(url) + "/cashier/trade/query?";//未支付状态订单查询
        public static string cashcard = getUrl(url) + "/cashier/trade/charge?";//现金银联记账
        public static string store = getUrl(url) + "/cashier/trade/balance-pay?";//储值支付
        public static string tradecsv = getUrl(url) + "/cashier/data/trade-csv-download?";//导出订单明细

        //储值
        public static string storepay = getUrl(url) + "/cashier/recharge/barpay-for-recharge?";//微信支付宝条码支付
        public static string storeprecreate = getUrl(url) + "/cashier/recharge/qrpay-for-recharge?";//微信支付宝扫码支付
        public static string storequery = getUrl(url) + "/cashier/recharge/query-for-recharge?";//未支付状态订单查询
        public static string storecashcard = getUrl(url) + "/cashier/recharge/cash-pay-for-recharge?";//现金银联记账

        public static string storetradelist = getUrl(url) + "/cashier/recharge/recharge-order-list?";//储值明细
        public static string storedetail = getUrl(url) + "/cashier/recharge/recharge-order-detail?";//储值详情
        public static string storetradecsv = getUrl(url) + "/cashier/data/recharge-order-csv-download?";//储值明细导出
        public static string storetradetotal = getUrl(url) + "/cashier/recharge/recharge-summary?";//储值汇总
        public static string storesummary = getUrl(url) + "/cashier/data/recharge-trade-summary?";//储值汇总统计

        public static string tradelist = getUrl(url) + "/cashier/trade/trade-list?";//订单列表
        public static string tradetotal = getUrl(url) + "/cashier/trade/trade-total?";//订单汇总
        public static string orderdetail = getUrl(url) + "/cashier/trade/trade-view?";//订单详情
        public static string refund = getUrl(url) + "/cashier/trade/refund?";//退款
        public static string refundpwdshow = getUrl(url) + "/cashier/trade/is-refund-pwd?";//退款是否需要输入密码
        public static string tradesummary = getUrl(url) + "/cashier/data/trade-summary?";//汇总

        public static string membersearch = getUrl(url) + "/cashier/member/search-member-info?";//会员界面获取会员详细信息
        public static string userechargecode = getUrl(url) + "/cashier/recharge/use-recharge-code?";//使用储值码
        public static string addmember = getUrl(url) + "/cashier/member/add-member?";//添加会员

        //新财务系统
        public static string flowlist = getUrl(url) + "/mdata/trade/flow-list?";//流水列表
        public static string flowlistcsv = getUrl(url) + "/mdata/trade/flow-list-csv?";//流水列表csv
        public static string flowsummary = getUrl(url) + "/mdata/trade/flow-summary?";//流水汇总
        public static string flowtotalsummary = getUrl(url) + "/mdata/trade/flow-total-summary?";//流水详细汇总
        public static string flowdetail = getUrl(url) + "/mdata/trade/flow-detail?";//流水详情

        public static string orderlist = getUrl(url) + "/mdata/trade/order-list?";//订单列表
        public static string ordersummary = getUrl(url) + "/mdata/trade/order-summary?";//订单汇总
        public static string orderlistcsv = getUrl(url) + "/mdata/trade/order-list-csv?";//订单列表csv

        //卡券相关
        public static string couponsearch = getUrl(url) + "/mtrade/coupon/search?";//查找单张优惠券
        public static string couponconsume = getUrl(url) + "/mtrade/coupon/consume?";//核销单张优惠券
        public static string couponlist = getUrl(url) + "/mtrade/coupon/consume-list?";//优惠券核销列表
        public static string couponview = getUrl(url) + "/mtrade/coupon/consume-view?";//优惠券核销详情
        public static string couponlistcsv = getUrl(url) + "/mtrade/coupon/consume-csv?";//导出优惠券核销明细csv

        //口碑券
        public static string koubeisearch = getUrl(url) + "/mtrade/koubei-coupon/search?";//查找口碑券
        public static string koubeiconsume = getUrl(url) + "/mtrade/coupon/consume?";//核销单张优惠券(12位券码位口碑券)
        public static string koubeilist = getUrl(url) + "/mdata/coupon/koubei-list?";//口碑券核销列表
        public static string koubeidetail = getUrl(url) + "/mdata/coupon/koubei-detail?";//口碑券核销详情
        public static string koubeisummary = getUrl(url) + "/mdata/coupon/koubei-summary?";//口碑券核销汇总
        public static string koubeicsv = getUrl(url) + "/mdata/coupon/koubei-list-csv?";//导出口碑核销明细csv
    }

    class download
    {
        public static string packagePath = @"package";//拓展包文件夹
        public static string paiaiPath = @"paipai";//派派驱动文件夹
        public static string paipaiZipFileUrl = "https://upload.qinguanjia.com/client/exe/paipai.zip";//派派小盒拓展包下载地址
        public static string paipaiZipFilePath = @"package\paipai.zip";//派派小盒拓展包保存路径
        public static string paipaiExeFilePath = @"paipai\Paipaibox.exe";//派派小盒可执行程序路径
        public static string paipaiZipToPath = @"paipai\";//派派小盒解压目录
    }
    class logPath
    {
        public static string paipaiLogFilePath = @"paipai\log";//派派小盒日志路径
    }
}
