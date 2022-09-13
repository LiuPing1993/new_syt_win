using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace self_syt
{

    /*
     * 全局的变量
     */
    public class BaseConfigValue
    {

        /// <summary>
        /// 是否记录密码
        /// </summary>
        public static string remberPwd = "remberPwd";
        /// <summary>
        /// 是否自动登录
        /// </summary>
        public static string autoLogin = "autoLogin";
        /// <summary>
        /// 用户名
        /// </summary>
        public static string userName = "userName";
        /// <summary>
        /// 密码
        /// </summary>
        public static string passwWord = "passWord";
        /// <summary>
        /// 开机自动启动
        /// </summary>
        public static string startIsAutoOpen = "startIsAutoOpen";
        /// <summary>
        /// 是否直接退出或者最小化
        /// </summary>
        public static string exitOrMin = "exitormin";
        /// <summary>
        ///是否显示悬浮窗
        /// </summary>
        public static string isShowSuperForm = "isShowSuperForm";
        /// <summary>
        /// 快捷键唤起收银界面
        /// </summary>
        public static string fastKeyShowPayForm = "fastKeyShowPayForm";
        /// <summary>
        /// 唤起收银界面的具体额值的大小
        /// </summary>
        public static string fastKeyShowPayFormValue = "fastKeyShowPayFormValue";
        /// <summary>
        /// 收款后最小化收款界面
        /// </summary>
        public static string payFinMinPay = "payFinMinPay";
        /// <summary>
        /// 启动金额自动识别
        /// </summary>
        public static string startMoneyAutoRecongtion = "startMoneyAutoRecongtion";
        /// <summary>
        /// 实时获取收款金额
        /// </summary>
        public static string getMoneyInRealTime = "getMoneyInRealTime";
        /// <summary>
        /// 识别后唤起的支付方式
        /// </summary>
        public static string recogFinStartPayChannel = "recogFinStartPayChannel";
        /// <summary>
        /// 识别方式
        /// </summary>
        public static string recongtionWay = "recongtionWay";
        /// <summary>
        /// 实时获取付款码
        /// </summary>
        public static string getMostNewPayCode = "getMostNewPayCode";
        /// <summary>
        /// 获取付款码后确认
        /// </summary>
        public static string getPayCodeNeedCnfirm = "getPayCodeNeedCnfirm";
        /// <summary>
        /// 自动识别的区域
        /// </summary>
        public static string recongtionRectangleArea = "recongtionRectangleArea";
        /// <summary>
        /// 是否取反色
        /// </summary>
        public static string isQuFanColor = "isQuFanColor";
        /// <summary>
        /// 照片的缩放的倍数默认值为1
        /// </summary>
        public static string zoomMultiples = "zoomMultiples";
        /// <summary>
        /// 照片二值化的阈值 默认值为120
        /// </summary>
        public static string threshold = "threshold";
        /// <summary>
        /// 支付后第一步的时间
        /// </summary>
        public static string one_step_time = "one_step_time";
        /// <summary>
        /// 支付后第二步的时间
        /// </summary>
        public static string two_step_time = "two_step_time";
        /// <summary>
        /// 支付后第三步的时间
        /// </summary>
        public static string three_step_time = "three_step_time";
        /// <summary>
        /// 支付后第四步的时间
        /// </summary>
        public static string four_step_time = "four_step_time";
        /// <summary>
        /// 支付后第五步的时间
        /// </summary>
        public static string five_step_time = "five_step_time";
        /// <summary>
        /// 当前的部署的步骤
        /// </summary>
        public static string curretStep = "curretStep";
        /// <summary>
        /// 第一步的区域
        /// </summary>
        public static string one_step_area = "one_step_area";
        /// <summary>
        /// 第二步的区域
        /// </summary>
        public static string two_step_area = "two_step_area";
        /// <summary>
        /// 第三步的区域
        /// </summary>
        public static string three_step_area = "three_step_area";
        /// <summary>
        /// 第四步的区域
        /// </summary>
        public static string four_step_area = "four_step_area";
        /// <summary>
        /// 第五步的区域
        /// </summary>
        public static string five_step_area = "five_step_area";
        /// <summary>
        ///开启自动打印
        /// </summary>
        public static string startAutoPrint = "startAutoPrint";
        /// <summary>
        /// 打印机的类型
        /// </summary>
        public static string printType = "printType";
        /// <summary>
        /// 打印机名称
        /// </summary>
        public static string printName = "printName";
        /// <summary>
        /// 打印小票
        /// </summary>
        public static string printCashier = "printCashier";
        /// <summary>
        /// 代理地址的参数
        /// </summary>
        public static string proxyPara = "proxyPara";
        /// <summary>
        /// 虚拟串口的串口名称
        /// </summary>
        public static string virtualPortName = "virtualPortName";
        /// <summary>
        /// 虚拟串口的波特率
        /// </summary>
        public static string virtualBuadRate = "virtualBuadRate";
        /// <summary>
        /// 串口打印机串口名称
        /// </summary>
        public static string printPort = "printPort";
        /// <summary>
        /// 串口打印机波特率
        /// </summary>
        public static string printBuadRate = "printBuadRate";
        /// <summary>
        /// 串口打印机字符编码
        /// </summary>
        public static string printChar = "printChar";
        /// <summary>
        /// 是否是从主界面退出
        /// </summary>
        public static string IsExitFromMain = "IsExitFromMain";
        /// <summary>
        /// 是否是调试模式
        /// </summary>
        public static string IsDebug = "IsDebug";

    }
    public class BaseValue
    {

        /// <summary>
        /// token
        /// </summary>
        public static string token = "";
        /// <summary>
        /// 商户code
        /// </summary>
        public static string merchant_code = "";
        /// <summary>
        /// app_id
        /// </summary>
        public static string app_id = "";
        /// <summary>
        /// 员工名称
        /// </summary>
        public static string employee_name = "";
        /// <summary>
        /// 员工号
        /// </summary>
        public static string employee_code = "";
        /// <summary>
        /// 商户名称
        /// </summary>
        public static string merchant_name = "";
        /// <summary>
        /// 门店名称
        /// </summary>
        public static string store_name = "";
        /// <summary>
        /// 分门店名称
        /// </summary>
        public static string store_branch_name = "";
        /// <summary>
        /// 微软雅黑字体
        /// </summary>
        public static string baseFont = "微软雅黑";
        /// <summary>
        /// 思源雅黑字体
        /// </summary>
        public static string baseFont_St = "思源雅黑";
        /// <summary>
        /// 串口接收的数据的值的大小
        /// </summary>
        public static string serialPortValue = "";
        /// <summary>
        /// 对齐方式居中
        /// </summary>
        public static StringFormat drawFormatTitle = new StringFormat();
        /// <summary>
        /// 对齐方式居左
        /// </summary>
        public static StringFormat drawFormatLeft = new StringFormat();
        /// <summary>
        /// 对齐方式居右
        /// </summary>
        public static StringFormat drawFormatRight = new StringFormat();
        /// <summary>
        /// 对齐方式居右中
        /// </summary>
        public static StringFormat drawFormatRightMid = new StringFormat();
        /// <summary>
        /// 对齐方式居左中
        /// </summary>
        public static StringFormat drawFormatLeftMid = new StringFormat();
        /// <summary>
        /// 对齐方式居右
        /// </summary>
        public static StringFormat drawFormatRightFar = new StringFormat();
        /// <summary>
        /// 对齐方式居左
        /// </summary>
        public static StringFormat drawFormatLeftFar = new StringFormat();
        /// <summary>
        /// 对齐方式顶部
        /// </summary>
        public static StringFormat drawFormatTop = new StringFormat();
        /// <summary>
        /// 对齐方式顶部中间
        /// </summary>
        public static StringFormat drawFormatTopTitle = new StringFormat();
        /// <summary>
        /// 截取的照片的路径
        /// </summary>
        public static string imagePath = System.Environment.CurrentDirectory + "\\temp.bmp";
        /// <summary>
        /// 要对比的照片的路径
        /// </summary>
        public static string dbImagePath = System.Environment.CurrentDirectory + "\\tempCopy.bmp";

    }

    public class Url
    {

        #region 测试的URL
        //public static string url_dc_open = "https://cposapi-test.qinguanjia.com/open?";
        //public static string url_data_open = "https://dataapi-test.qinguanjia.com/open?";
        //public static string url_mch = "https://mchapi-test.qinguanjia.com/";
        //public static string url_marketing_open = "https://marketingapi-test.qinguanjia.com/open?";
        //public static string url_crm_open = "https://crmapi-test.qinguanjia.com/open?";
        //public static string url_trade = "https://tradeapi-test.qinguanjia.com/open?";
        #endregion

        #region 预发布URL
        //public static string url_dc_open = "https://cposapi-pre.qinguanjia.com/open?";
        //public static string url_data_open = "https://dataapi-pre.qinguanjia.com/open?";
        //public static string url_mch = "https://mchapi-pre.qinguanjia.com/";
        //public static string url_marketing_open = "https://marketingapi-pre.qinguanjia.com/open?";
        //public static string url_crm_open = "https://crmapi-pre.qinguanjia.com/open?";
        //public static string url_trade = "https://tradeapi-pre.qinguanjia.com/open?";
        #endregion

        #region 正式的URL
        public static string url_dc_open = "https://cposapi.qinguanjia.com/open?";
        public static string url_data_open = "https://dataapi.qinguanjia.com/open?";
        public static string url_mch = "https://mchapi.qinguanjia.com/";
        public static string url_marketing_open = "https://marketingapi.qinguanjia.com/open?";
        public static string url_crm_open = "https://crmapi.qinguanjia.com/open?";
        public static string url_trade = "https://tradeapi.qinguanjia.com/open?";
        #endregion

        #region 接口的具体的URL
        /// <summary>
        /// 系统激活
        /// </summary>
        public static string activate = url_mch + "/device-activate/activate?";
        /// <summary>
        /// 系统登录，方法名：trade.employee.login
        /// </summary>
        public static string login = url_trade;
        /// <summary>
        /// 创建订单
        /// </summary>
        public static string createOrder = url_trade;
        /// <summary>
        /// 现金支付
        /// </summary>
        public static string crashPay = url_trade;
        /// <summary>
        /// 支付订单的查询
        /// </summary>
        public static string queryOrder = url_trade;
        /// <summary>
        /// 条码支付
        /// </summary>
        public static string codeBarPay = url_trade;
       /// <summary>
       /// 财务流水
       /// </summary>
        public static string moneyFlow = url_trade;
        /// <summary>
        /// 交易订单
        /// </summary>
        public static string transOrder = url_trade;
        /// <summary>
        /// 订单退款
        /// </summary>
        public static string orderRefund = url_trade;
        /// <summary>
        /// 订单详情
        /// </summary>
        public static string orderDetail = url_trade;
        /// <summary>
        /// 交易汇总
        /// </summary>
        public static string transSummary = url_data_open;
        /// <summary>
        /// 修改密码
        /// </summary>
        public static string changePwd = url_trade;
        #endregion

    }

    /// <summary>
    /// 接口调取的异常类
    /// </summary>
    public class errorClass
    {
        public string errCode { get; set; }
        public string errMsg { get; set; }
    }
}
