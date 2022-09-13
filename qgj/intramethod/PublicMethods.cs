using System;
using System.Windows.Forms;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Collections;
using System.Reflection;
using System.Collections.Specialized;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Security.Cryptography;
using IWshRuntimeLibrary;
using System.Globalization;
using System.Media;
using System.Diagnostics;
using Microsoft.Win32;

namespace qgj
{
    class PublicMethods
    {
        /// <summary>
        /// 金额检查
        /// </summary>
        /// <param name="_value">金额字符串</param>
        /// <returns>是否符合金额正则验证,且金额小于1百万</returns>
        public static bool moneyCheck(string _value)
        {
            try
            {
                if (Regex.IsMatch(_value, @"^\d{0,9}\.{0,1}(\d{0,2})?$"))
                {
                    if (Convert.ToDouble(_value) < 1000000)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 验证字符串中是否包含中文
        /// </summary>
        /// <param name="_data">待检查字符串</param>
        /// <returns>返回检查结果</returns>
        public static bool hasChinese(string _data)
        {
            if (Regex.IsMatch(_data, @"[\u4e00-\u9fa5]+"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 验证字符串是否是数字
        /// </summary>
        /// <param name="_value">待检查字符串</param>
        /// <returns>返回结果</returns>
        public static bool IsNumeric(string _value)
        {
            return Regex.IsMatch(_value, @"^[+-]?\d*[.]?\d*$");
        }

        /// <summary>
        /// 验证字符串是否整型数字
        /// </summary>
        /// <param name="_value">待检查字符串</param>
        /// <returns>返回检查结果</returns>
        public static bool IsInt(string _value)
        {
            return Regex.IsMatch(_value, @"^[+-]?\d*$");
        }

        /// <summary>
        /// urlencode 编码方法
        /// </summary>
        /// <param name="_str">待编码字符串</param>
        /// <returns>编码结果</returns>
        public static string UrlEncode(string _str)
        {
            StringBuilder _sbTemp = new StringBuilder();
            byte[] _byStr = System.Text.Encoding.UTF8.GetBytes(_str);
            for (int _i = 0; _i < _byStr.Length; _i++)
            {
                _sbTemp.Append(@"%" + Convert.ToString(_byStr[_i], 16));
            }

            return (_sbTemp.ToString());
        }

        /// <summary>
        /// 检查字符串是否为中文字符串
        /// </summary>
        /// <param name="CString">待检查字符串</param>
        /// <returns>返回检查结果</returns>
        public static bool IsChinese(string CString)
        {
            return Regex.IsMatch(CString, @"^[\u4e00-\u9fa5]+$");
        }

        /// <summary>
        /// 将keycode类型值转为对应char值
        /// </summary>
        /// <param name="k">keycode类型值</param>
        /// <returns>char值</returns>
        public static char KeyCodeToChar(System.Windows.Forms.Keys k)
        {
            int nonVirtualKey = NativeMethods.MapVirtualKey((uint)k, 2);
            char mappedChar = Convert.ToChar(nonVirtualKey);
            return mappedChar;
        }

        /// <summary>
        /// 金额格式化
        /// </summary>
        /// <param name="_insert">待格式化的金额字符串</param>
        /// <returns>已经格式化的金额</returns>
        public static string moneyFormater(string _insert)
        {
            try
            {
                if (_insert == "")
                {
                    return "0.00";
                }
                else if (_insert.IndexOf(".") == -1)
                {
                    return _insert + ".00";
                }
                else if(_insert.Length - _insert.IndexOf(".") == 1)
                {
                    return _insert + "00";
                }
                else if (_insert.Length - _insert.IndexOf(".") < 3)
                {
                    return _insert + "0";
                }

                return _insert;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return _insert;
            }

        }

        /// <summary>
        /// 打折金额计算及格式化
        /// </summary>
        /// <param name="_memberdiscout">会员折扣金额</param>
        /// <param name="_coupon">优惠券折扣金额</param>
        /// <param name="_alipay">支付宝优惠金额</param>
        /// <returns>总优惠金额</returns>
        public static string discountmoneyFormater(string _memberdiscout,string _coupon,string _alipay)
        {
            double _discountmoney = 0;
            if (_alipay != null && _alipay != "")
            {
                _discountmoney += Convert.ToDouble(_alipay);
            }
            if (_coupon != null && _coupon != "")
            {
                _discountmoney += Convert.ToDouble(_coupon);
            }
            if (_memberdiscout != null && _memberdiscout != "")
            {
                _discountmoney += Convert.ToDouble(_memberdiscout);
            }
            return moneyFormater(_discountmoney.ToString());
        }

        public static string cashierPrintPaymoney(string _money,string _ali_merchant_discount)
        {
            double paymoney = 0;
            if (_money != null && _money != "")
            {
                paymoney = Convert.ToDouble(_money);
            }
            if (_ali_merchant_discount != null && _ali_merchant_discount != "")
            {
                paymoney = paymoney - Convert.ToDouble(_ali_merchant_discount);
            }
            if( paymoney <0)
            {
                return "0.00";
            }
            return moneyFormater(paymoney.ToString());
        }

        /// <summary>
        /// 精简详细错误信息
        /// </summary>
        /// <param name="_insert">错误信息内容元字符串</param>
        /// <returns>精简后的字符串</returns>
        public static string errormsgFormater(string _insert)
        {
            try
            {
                if (_insert.Length > 50)
                {
                    return _insert.Substring(0, 50) + ".....";
                }
            }
            catch { }
            return _insert;
        }

        /// <summary>
        /// 转换带千分位的数字
        /// </summary>
        /// <param name="_thousandthStr">元字符串</param>
        /// <returns>转换后的整型数字</returns>
        public static int ParseThousandthString(string _thousandthStr)
        {
            int _value = 0;
            if (!string.IsNullOrEmpty(_thousandthStr))
            {
                try
                {
                    _value = int.Parse(_thousandthStr, NumberStyles.AllowThousands | NumberStyles.AllowDecimalPoint | NumberStyles.AllowLeadingSign);
                }
                catch (Exception ex)
                {
                    _value = 0;
                    Console.WriteLine(string.Format("将千分位字符串{0}转换成数字异常，原因:{0}", _thousandthStr, ex.Message));
                }
            }
            return _value;
        }

        /// <summary>
        /// md5字符串
        /// </summary>
        /// <param name="_str">待md5的字符串</param>
        /// <returns>md5结果字符串</returns>
        public static string md5(string _str)
        {

            byte[] _result = Encoding.Default.GetBytes(_str.Trim());
            MD5 _md5 = new MD5CryptoServiceProvider();
            byte[] _output = _md5.ComputeHash(_result);
            string _returnMd5String = BitConverter.ToString(_output).Replace("-", "");
            return _returnMd5String;
        }

        /// <summary>
        /// 从枚举类型和它的特性读出并返回一个键值对
        /// </summary>
        /// <param name="_enumType">Type,该参数的格式为typeof(需要读的枚举类型)</param>
        /// <returns>键值对</returns>
        public static NameValueCollection GetNVCFromEnumValue(Type _enumType)
        {
            NameValueCollection _nvc = new NameValueCollection();
            Type _typeDescription = typeof(DescriptionAttribute);
            System.Reflection.FieldInfo[] _fields = _enumType.GetFields();
            string _strText = string.Empty;
            string _strValue = string.Empty;
            foreach (FieldInfo _field in _fields)
            {
                if (_field.FieldType.IsEnum)
                {
                    _strValue = ((int)_enumType.InvokeMember(_field.Name, BindingFlags.GetField, null, null, null)).ToString();
                    object[] _arr = _field.GetCustomAttributes(_typeDescription, true);
                    if (_arr.Length > 0)
                    {
                        DescriptionAttribute aa = (DescriptionAttribute)_arr[0];
                        _strText = aa.Description;
                    }
                    else
                    {
                        _strText = _field.Name;
                    }
                    _nvc.Add(_strText, _strValue);
                }
            }
            return _nvc;
        }

        public static string GetEnumDesc<Ttype>(Ttype _Enumtype)
        {
            if (_Enumtype == null) throw new ArgumentNullException("Enumtype");
            if (!_Enumtype.GetType().IsEnum) throw new Exception("参数类型不正确");

            FieldInfo[] _fieldinfo = _Enumtype.GetType().GetFields();
            foreach (FieldInfo _item in _fieldinfo)
            {
                Object[] _obj = _item.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (_obj != null && _obj.Length != 0)
                {
                    DescriptionAttribute _des = (DescriptionAttribute)_obj[0];
                    Console.WriteLine(_des.Description);
                    return _des.Description;
                }
            }
            return null;
        }

        public static Font pringFontSet(int iPageWide, bool IsBold,bool IsCompatible, int size = 8)
        {
            if (iPageWide == 80)
            {
                size += 2;
            }
            FontStyle fontstyle = FontStyle.Regular;
            if (IsBold)
            {
                fontstyle = FontStyle.Bold;
            }
            if (IsCompatible)
            {
                return new Font("Arial", size, fontstyle, GraphicsUnit.Point, 134);
            }
            else
            {
                return new Font("宋体", size, fontstyle);
            }
        }

        /// <summary>
        /// 圆角矩形边框
        /// </summary>
        /// <param name="_rectangle">原始矩形</param>
        /// <param name="_g">Graphics 对象</param>
        /// <param name="_radius">圆角半径</param>
        /// <param name="backColor">颜色</param>
        public static void FrameRoundRectangle(Rectangle _rectangle, Graphics _g, int _radius, Color backColor)
        {
            _g.SmoothingMode = SmoothingMode.AntiAlias;
            _g.DrawPath(new Pen(backColor), DrawRoundRect(_rectangle.X, _rectangle.Y, _rectangle.Width - 2, _rectangle.Height - 1, _radius));
        }
        /// <summary>
        /// 圆角矩形
        /// </summary>
        /// <param name="_rectangle">原始矩形</param>
        /// <param name="_g">Graphics 对象</param>
        /// <param name="_radius">圆角半径</param>
        /// <param name="backColor">颜色</param>
        public static void FillRoundRectangle(Rectangle _rectangle, Graphics _g, int _radius, Color backColor)
        {
            _g.SmoothingMode = SmoothingMode.AntiAlias;
            _g.FillPath(new SolidBrush(backColor), DrawRoundRect(_rectangle.X, _rectangle.Y, _rectangle.Width - 2, _rectangle.Height - 1, _radius));
        }
        public static GraphicsPath DrawRoundRect(int _x, int _y, int _width, int _height, int _radius)
        {
            GraphicsPath _gp = new GraphicsPath();
            _gp.AddArc(_x, _y, _radius, _radius, 180, 90);
            _gp.AddArc(_width - _radius, _y, _radius, _radius, 270, 90);
            _gp.AddArc(_width - _radius, _height - _radius, _radius, _radius, 0, 90);
            _gp.AddArc(_x, _height - _radius, _radius, _radius, 90, 90);
            _gp.CloseAllFigures();
            return _gp;
        }
        /// <summary>
        /// 圆角矩形路径绘制
        /// </summary>
        /// <param name="_rect"></param>
        /// <param name="_arcRadius"></param>
        /// <returns></returns>
        public static GraphicsPath CreateRoundPath(Rectangle _rect, ArcRadius _arcRadius)
        {
            var _path = new GraphicsPath();
            if (_rect.Width == 0 || _rect.Height == 0)
            {
                return _path;
            }

            if (_arcRadius.LeftTop > 0)
            {
                _path.AddArc(
                    _rect.Left, _rect.Top, _arcRadius.LeftTop, _arcRadius.LeftTop, 180, 90);
            }

            _path.AddLine(new Point(_rect.Left + _arcRadius.LeftTop, _rect.Top),
                         new Point(_rect.Right - _arcRadius.RightTop, _rect.Top));

            if (_arcRadius.RightTop > 0)
            {
                _path.AddArc(_rect.Right - _arcRadius.RightTop, _rect.Top,
                    _arcRadius.RightTop, _arcRadius.RightTop, -90, 90);
            }

            _path.AddLine(new Point(_rect.Right, _rect.Top + _arcRadius.RightTop),
                         new Point(_rect.Right, _rect.Bottom - _arcRadius.RightBottom));

            if (_arcRadius.RightBottom > 0)
            {
                _path.AddArc(_rect.Right - _arcRadius.RightBottom, _rect.Bottom - _arcRadius.RightBottom,
                    _arcRadius.RightBottom, _arcRadius.RightBottom, 0, 90);
            }

            _path.AddLine(new Point(_rect.Right - _arcRadius.RightBottom, _rect.Bottom),
                         new Point(_rect.Left + _arcRadius.LeftBottom, _rect.Bottom));

            if (_arcRadius.LeftBottom > 0)
            {
                _path.AddArc(_rect.Left, _rect.Bottom - _arcRadius.LeftBottom,
                    _arcRadius.LeftBottom, _arcRadius.LeftBottom, 90, 90);
            }

            _path.AddLine(new Point(_rect.Left, _rect.Bottom - _arcRadius.LeftBottom),
                         new Point(_rect.Left, _rect.Top + _arcRadius.LeftTop));
            
            _path.CloseFigure();

            return _path;
        }

        /// <summary>
        /// 分割自定义字符串
        /// </summary>
        /// <param name="_str">待分割的串</param>
        /// <param name="_point">分割标志</param>
        /// <returns>分割完成的数组</returns>
        public static string[] SplitByChar(string _str, char _point)
        {
            string[] _sArray = _str.Split(_point);
            return _sArray;
        }

        /// <summary>
        /// 内存清理
        /// </summary>
        public static void FlushMemory()
        {
            try
            {
                GC.Collect();
                GC.WaitForPendingFinalizers();
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                {
                    NativeMethods.SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
                }
            }
            catch { }
            
        }

        public static void writeIn(string _str)
        {
            try
            {
                _str = SecurityClass.inCode(_str);
                StreamWriter _sw = new StreamWriter("c:\\Windows\\qinpro.qgj", false);
                _sw.WriteLine(_str);
                _sw.Close();
            }
            catch
            {
                errorinformationForm _errorF = new errorinformationForm("可能存在问题", "程序文件写入失败");
                _errorF.TopMost = true;
                _errorF.StartPosition = FormStartPosition.CenterScreen;
                _errorF.ShowDialog();
            }

        }
        public static string readOut()
        {
            string _str = "";
            try
            {
                StreamReader _sr = new StreamReader("c:\\Windows\\qinpro.qgj", false);
                _str = _sr.ReadLine().ToString();
                _sr.Close();
                _str = SecurityClass.outCode(_str);
            }
            catch
            {
                errorinformationForm _errorF = new errorinformationForm("错误(文件丢失)", "丢失系统重要文件");
                _errorF.TopMost = true;
                _errorF.StartPosition = FormStartPosition.CenterScreen;
                _errorF.ShowDialog();
                PublicMethods.exitSystem();
            }
            return _str;
        }

        public static void WriteLog(Exception _ex, string _LogAddress = "")
        {
            try
            {
                //如果日志文件为空，则默认目录下新建 YYYY-mm-dd_Log.log文件
                if (_LogAddress == "")
                {
                    _LogAddress = Application.StartupPath + '\\' +
                        DateTime.Now.Year + '-' +
                        DateTime.Now.Month + "_Log.log";
                }
                //把异常信息输出到文件
                StreamWriter _fs = new StreamWriter(_LogAddress, true);
                _fs.WriteLine("当前时间：" + DateTime.Now.ToString());
                _fs.WriteLine("异常信息：" + _ex.Message);
                _fs.WriteLine("异常对象：" + _ex.Source);
                _fs.WriteLine("调用堆栈：\n" + _ex.StackTrace.Trim());
                _fs.WriteLine("触发方法：" + _ex.TargetSite);
                _fs.WriteLine();
                _fs.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString());
            }
        }
        public static void WriteLog(string _text,string _LogAddress = "")
        {
            try
            {
                //如果日志文件为空，则默认目录下新建 YYYY-mm-dd_Log.log文件
                if (_LogAddress == "")
                {
                    _LogAddress = Application.StartupPath + '\\' +
                        DateTime.Now.Year + '-' +
                        DateTime.Now.Month + "_Log.log";
                }
                //把异常信息输出到文件
                StreamWriter _fs = new StreamWriter(_LogAddress, true);
                _fs.WriteLine("当前时间：" + DateTime.Now.ToString());
                _fs.WriteLine("异常信息：" + _text);
                _fs.WriteLine();
                _fs.Close();
            }
            catch { }
        }
        /// <summary>
        /// 一维码code39
        /// </summary>
        /// <param name="sourceCode"></param>
        /// <param name="barCodeHeight"></param>
        /// <param name="sf"></param>
        /// <returns></returns>
        public static Bitmap GetCode39(string sourceCode, int barCodeHeight, StringFormat sf)
        {
            int leftMargin = 5;
            int topMargin = 0;
            int thickLength = 2;
            int narrowLength = 1;
            int intSourceLength = sourceCode.Length;
            string strEncode = "010010100"; //添加起始码“ *”.
            var font = new System.Drawing.Font("Segoe UI", 5);
            string AlphaBet = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ-. $/+%*";
            string[] Code39 =
             {
                 /* 0 */ "000110100" , 
                 /* 1 */ "100100001" , 
                 /* 2 */ "001100001" , 
                 /* 3 */ "101100000" ,
                 /* 4 */ "000110001" , 
                 /* 5 */ "100110000" , 
                 /* 6 */ "001110000" , 
                 /* 7 */ "000100101" ,
                 /* 8 */ "100100100" , 
                 /* 9 */ "001100100" , 
                 /* A */ "100001001" , 
                 /* B */ "001001001" ,
                 /* C */ "101001000" , 
                 /* D */ "000011001" , 
                 /* E */ "100011000" , 
                 /* F */ "001011000" ,
                 /* G */ "000001101" , 
                 /* H */ "100001100" , 
                 /* I */ "001001100" , 
                 /* J */ "000011100" ,
                 /* K */ "100000011" , 
                 /* L */ "001000011" , 
                 /* M */ "101000010" , 
                 /* N */ "000010011" ,
                 /* O */ "100010010" , 
                 /* P */ "001010010" , 
                 /* Q */ "000000111" , 
                 /* R */ "100000110" ,
                 /* S */ "001000110" , 
                 /* T */ "000010110" , 
                 /* U */ "110000001" , 
                 /* V */ "011000001" ,
                 /* W */ "111000000" , 
                 /* X */ "010010001" , 
                 /* Y */ "110010000" , 
                 /* Z */ "011010000" ,
                 /* - */ "010000101" , 
                 /* . */ "110000100" , 
                 /*' '*/ "011000100" ,
                 /* $ */ "010101000" ,
                 /* / */ "010100010" , 
                 /* + */ "010001010" , 
                 /* % */ "000101010" , 
                 /* * */ "010010100"  
             };
            sourceCode = sourceCode.ToUpper();
            Bitmap objBitmap = new Bitmap(((thickLength * 3 + narrowLength * 7) * (intSourceLength + 2)) +
                                           (leftMargin * 2), barCodeHeight + (topMargin * 2));
            Graphics objGraphics = Graphics.FromImage(objBitmap);
            objGraphics.FillRectangle(Brushes.White, 0, 0, objBitmap.Width, objBitmap.Height);
            for (int i = 0; i < intSourceLength; i++)
            {
                //非法字符校验
                if (AlphaBet.IndexOf(sourceCode[i]) == -1 || sourceCode[i] == '*')
                {
                    objGraphics.DrawString("Invalid Bar Code", SystemFonts.DefaultFont, Brushes.Red, leftMargin, topMargin);
                    return objBitmap;
                }
                //编码
                strEncode = string.Format("{0}0{1}", strEncode,
                Code39[AlphaBet.IndexOf(sourceCode[i])]);
            }
            strEncode = string.Format("{0}0010010100", strEncode); //添加结束码“*”
            int intEncodeLength = strEncode.Length;
            int intBarWidth;
            for (int i = 0; i < intEncodeLength; i++) //绘制 Code39 barcode
            {
                intBarWidth = strEncode[i] == '1' ? thickLength : narrowLength;
                objGraphics.FillRectangle(i % 2 == 0 ? Brushes.Black : Brushes.White, leftMargin, topMargin, intBarWidth, barCodeHeight);
                leftMargin += intBarWidth;
            }
            //绘制明码         
            //Font barCodeTextFont = new Font("黑体", 10F);
            //RectangleF rect = new RectangleF(2, barCodeHeight - 20, objBitmap.Width - 4, 20);
            //objGraphics.FillRectangle(Brushes.White, rect);
            ////文本对齐
            //objGraphics.DrawString(BarCodeText, barCodeTextFont, Brushes.Black, rect, sf);
            return objBitmap;
        }
        /// <summary>
        /// 快捷方式
        /// </summary>
        public static void newFastLnk()
        {
            try
            {
                string _desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
                if (System.IO.File.Exists(_desktopPath + "\\亲管家.lnk"))
                {
                    System.IO.File.Delete(_desktopPath + "\\亲管家.lnk");
                }

                WshShell _shell = new WshShell();
                IWshShortcut _shortcut = (IWshShortcut)_shell.CreateShortcut(
                        Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) +
                        "\\" + "亲享.lnk"
                        );
                _shortcut.TargetPath = Application.StartupPath + "\\" + "qgj.exe";
                _shortcut.WorkingDirectory = System.Environment.CurrentDirectory;
                _shortcut.WindowStyle = 1;
                _shortcut.Description = "qgj";
                _shortcut.IconLocation = Application.ExecutablePath;
                _shortcut.Save();
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee);
                //throw new Exception(ee.ToString());
            }
        }

        /// <summary>
        /// 输出到打印文件
        /// </summary>
        /// <param name="_str">输出内容</param>
        public static void writeInPrintInfo(string _str)
        {
            //string strPath = System.Environment.CurrentDirectory;
            string _strPath = Application.StartupPath;
            //MessageBox.Show(strPath);
            try
            {
                if (System.IO.File.Exists(_strPath + "\\printinfo.txt"))
                {
                    System.IO.File.Delete(_strPath + "\\printinfo.txt");
                }
                FileStream _fs = new FileStream(_strPath + "\\printinfo.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite);
                StreamWriter _sw = new StreamWriter(_fs, System.Text.Encoding.Default);
                _sw.BaseStream.Seek(0, SeekOrigin.End);
                //StreamWriter sw = new StreamWriter(strPath + "\\printinfo.txt", false, System.Text.Encoding.Default);
                _sw.WriteLine(_str);
                _sw.Flush();
                _sw.Close();
                _fs.Close();
            }
            catch (Exception e)
            {
                errorinformationForm _errorF = new errorinformationForm("未写入(print)", e.ToString());
                _errorF.TopMost = true;
                _errorF.StartPosition = FormStartPosition.CenterParent;
                _errorF.ShowDialog();
            }
        }
        public static void gfnVoiceToInform()
        {
            try
            {
                loadconfigClass _lcc = new loadconfigClass("usevoice");
                if (_lcc.readfromConfig() != "true")
                {
                    return;
                }
                SoundPlayer _soundP;
                _soundP = new SoundPlayer();
                _soundP.Stream = Properties.Resources.cash_register;
                _soundP.Play(); 

            }
            catch(Exception e)
            {
                Console.Write(e);
            }
        }
        public static void exitSystem()
        {
            try
            {
                UserClass.ppbC.setSence(0);
                UserClass.ppbC.disposePaipai();
            }
            catch { }
            System.Environment.Exit(0);
        }

        /// <summary>
        /// 获取当前cpu及内存信息
        /// </summary>
        /// <returns></returns>
        public static string cpuAndMemory()
        {
            return "不记录";
            /*
            string cpu = "0";
            string memory = "0";
            try
            {
                Process CurrentProcess = Process.GetCurrentProcess();
                cpu = ((Double)(CurrentProcess.TotalProcessorTime.TotalMilliseconds - CurrentProcess.UserProcessorTime.TotalMilliseconds) / 1000 / Environment.ProcessorCount).ToString();//CPU
                var p1 = new PerformanceCounter("Process", "Working Set - Private", CurrentProcess.ProcessName);
                memory = (p1.NextValue() / 1024).ToString();//私有工作集内存
                //(CurrentProcess.WorkingSet64 / 1024 / 1024).ToString();//工作类内存
            }
            catch { }
            return "CPU:" + cpu + " M:" + memory;
             * */
        }

        public static string codeChannel (string code)
        {
            string temp = code.Substring(0, 2);
            if(temp == "25"||temp == "26"||temp == "27"||temp == "28"||temp == "29"||temp == "30"||temp == "88")
            {
                return "支付宝付款码";
            }
            if (temp == "10" || temp == "11" || temp == "12" || temp == "13" || temp == "14" || temp == "15")
            {
                return "微信付款码";
            }
            return "付款码";
        }

        public static List<string> GetComlist(bool isUseReg)
        {
            List<string> list = new List<string>();
            try
            {
                if (isUseReg)
                {
                    RegistryKey RootKey = Registry.LocalMachine;
                    RegistryKey Comkey = RootKey.OpenSubKey(@"HARDWARE\DEVICEMAP\SERIALCOMM");
 
                    String[] ComNames = Comkey.GetValueNames();
 
                    foreach (String ComNamekey in ComNames)
                    {
                        string TemS = Comkey.GetValue(ComNamekey).ToString();
                        list.Add(TemS);
                    }
                }
                else
                {
                    foreach (string com in System.IO.Ports.SerialPort.GetPortNames())  //自动获取串行口名称
                        list.Add(com);
                }
            }
            catch
            {
            }
            return list;
        }

        public static void getVirtualMemory()
        {
            Win32.MEMORY_INFO m = new Win32.MEMORY_INFO();
            Win32.GlobalMemoryStatus(ref m);
            Console.WriteLine("可用虚拟内存" + m.dwAvailVirtual);
        }
    }

    class getHandle
    {
        int _x = 0;
        int _y = 0;

        public string getWindowsMessage()
        {
            IntPtr _controlId = IntPtr.Zero;
            StringBuilder _strB = new StringBuilder(256);

            try
            {
                getPos();
                IntPtr _hwnd = NativeMethods.WindowFromPoint(_x, _y);//获取指定坐标处窗口的句柄 
                _controlId = _hwnd;

                if (_controlId != IntPtr.Zero)
                {
                    //得到文本框句柄
                    NativeMethods.SendMessage(_controlId, 0x000D, new IntPtr(255), _strB);
                    //string a = strB.ToString().Trim();
                    //GetWindowText(controlId, strB, 256);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return "0";
            }
            return _strB.ToString();
        }

        public void getPos()
        {
            int _read_x = 0;
            int _read_y = 0;
            int _read_w = 20;
            int _read_h = 20;
            //配置文档读取位置信息
            loadconfigClass _areaC = new loadconfigClass("picareasetting");
            string sPicSetting = _areaC.readfromConfig();

            if (sPicSetting == "")
            {
                _x = 0;
                _y = 0;
            }
            else
            {
                string[] _settingtemp = PublicMethods.SplitByChar(sPicSetting, ',');
                _read_x = Convert.ToInt32(_settingtemp[0]);
                _read_y = Convert.ToInt32(_settingtemp[1]);
                _read_w = Convert.ToInt32(_settingtemp[2]);
                _read_h = Convert.ToInt32(_settingtemp[3]);
                _x = _read_x + _read_w / 2;
                _y = _read_y + _read_h / 2;
            }
        }

    }

    
}
