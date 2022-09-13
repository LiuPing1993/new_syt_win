using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace qgj
{
    class recognitionClass
    {
        /// <summary>
        /// 默认识别方式OCR
        /// </summary>
        string sRecognitionType = "ocr";

        public recognitionClass() { }

        /// <summary>
        /// 根据设置的识别方式进行从选择的区域内进行识别
        /// </summary>
        /// <returns>识别结果</returns>
        public string recognitionFormSelectArea(bool is_auto = false)
        {
            loadconfigClass _lcc = new loadconfigClass("recognitionType");

            sRecognitionType = _lcc.readfromConfig();
            //ocr识别
            if (sRecognitionType == "ocr")
            {
                return ocrRecognition(is_auto);
            }
            else if (sRecognitionType == "text")
            {
                return textRecognition();
            }
            else if (sRecognitionType == "clip")
            {
                setClipborad();
                Application.DoEvents();
                return textClipborad();
            }
            else if (sRecognitionType == "ocrex")
            {
                return ocrRecognitionEx(is_auto);
            }
            else if (sRecognitionType == "customer")//客显
            {
                return UserClass.com_money;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// OCR识别具体实现函数(模式1)
        /// </summary>
        /// <returns>识别结果</returns>
        private string ocrRecognition(bool is_auto)
        {
            loadconfigClass _areaC = new loadconfigClass("picareasetting");
            loadconfigClass _manageC = new loadconfigClass("picmanagesetting");
            string _sPicSetting = "";
            string _sPicmanage = "";
            bool _IsInverse = false;
            int _iTempValue = 120;
            double dZoom = 1;
            int iZoomH = 1;
            int iZoomW = 1;
            Bitmap _tempBitmap;
            Rectangle _areaRect = new Rectangle();

            try
            {
                //得到区域
                _sPicSetting = _areaC.readfromConfig();
                if (_sPicSetting == "")
                {
                    picClass.PicPointX = "0";
                    picClass.PicPointY = "0";
                    picClass.PicPointW = "0";
                    picClass.PicPointH = "0";
                    return "";
                }
                else
                {
                    string[] _settingtemp = PublicMethods.SplitByChar(_sPicSetting, ',');
                    picClass.PicPointX = _settingtemp[0];
                    picClass.PicPointY = _settingtemp[1];
                    picClass.PicPointW = _settingtemp[2];
                    picClass.PicPointH = _settingtemp[3];
                    _areaRect.X = Convert.ToInt32(_settingtemp[0]);
                    _areaRect.Y = Convert.ToInt32(_settingtemp[1]);
                    _areaRect.Width = Convert.ToInt32(_settingtemp[2]);
                    _areaRect.Height = Convert.ToInt32(_settingtemp[3]);
                }
                //得到二值化的值和是否取反的值
                _sPicmanage = _manageC.readfromConfig();
                if (_sPicmanage == "")
                {
                    picClass.PicPointZ = "1";
                    picClass.PicValue = "120";
                    picClass.PicInverse = "False";
                    _iTempValue = 120;
                }
                else
                {
                    string[] _settingtemp = PublicMethods.SplitByChar(_sPicmanage, ',');
                    picClass.PicPointZ = _settingtemp[0];
                    picClass.PicValue = _settingtemp[1];
                    picClass.PicInverse = _settingtemp[2];
                    dZoom = Convert.ToDouble(_settingtemp[0]);
                    _iTempValue = Convert.ToInt32(_settingtemp[1]);
                }
                //得到照片的缩放的倍数
                iZoomW = Convert.ToInt32(Convert.ToInt32(picClass.PicPointW) * dZoom);
                iZoomH = Convert.ToInt32(Convert.ToInt32(picClass.PicPointH) * dZoom);

                if (picClass.PicInverse == "False")
                {
                    _IsInverse = false;
                }
                else
                {
                    _IsInverse = true;
                }

                if(is_auto)
                {
                    pictureClass _pictureC = new pictureClass();
                    return _pictureC.autoOrc(_areaRect, _IsInverse, _iTempValue, dZoom, iZoomW, iZoomH);
                }
                else
                {
                    pictureClass _pictureC = new pictureClass();

                    _tempBitmap = new Bitmap(_pictureC.getscreenPictureWork());

                    _tempBitmap = _pictureC.CutImage(_tempBitmap, _areaRect);

                    _tempBitmap = _pictureC.binaryzation(_tempBitmap, _IsInverse, _iTempValue);

                    _tempBitmap = _pictureC.picSize(_tempBitmap, (float)dZoom);

                    _tempBitmap.Save(@"C:\tempWork.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

                    _tempBitmap.Dispose();

                    _pictureC.disPic();

                    return _pictureC.getStringOCR(_pictureC.ocrImageWork(iZoomW, iZoomH));
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.ToString());
                return "";
            }
        }

        /// <summary>
        /// 根据文本框句柄,直接从中读取字符串信息
        /// </summary>
        /// <returns>获取的字符串信息</returns>
        private string textRecognition()
        {
            getHandle _gethandelmessage = new getHandle();
            string _sTemp = _gethandelmessage.getWindowsMessage();
            _sTemp = _sTemp.Replace(",", "");
            Console.WriteLine("抓取的字符串" + _sTemp);
            if (PublicMethods.moneyCheck(_sTemp))
            {
                return _sTemp;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 从剪切板获取金额
        /// </summary>
        /// <returns></returns>
        private string textClipborad()
        {
            IDataObject _idata = Clipboard.GetDataObject();
            string _sTemp = "";
            if (_idata.GetDataPresent(DataFormats.Text))
            {
                _sTemp = (String)_idata.GetData(DataFormats.Text);
                Console.WriteLine("剪切板中的字符串" + _sTemp);
            }
            return _sTemp;
        }

        /// <summary>
        /// 复制快捷键操作
        /// </summary>
        private void setClipborad()
        {
            string hotkey_value = "C";
            Keys getType = (Keys)Enum.Parse(typeof(Keys), hotkey_value);
            NativeMethods.keybd_event(17, 0, 0, 0);
            NativeMethods.keybd_event((byte)getType, 0, 0, 0);
            NativeMethods.keybd_event((byte)getType, 0, 2, 0);
            NativeMethods.keybd_event(17, 0, 2, 0);
        }

        /// <summary>
        /// OCR识别具体实现函数(模式2)
        /// </summary>
        /// <returns></returns>
        private string ocrRecognitionEx(bool is_auto)
        {
            loadconfigClass _areaC = new loadconfigClass("picareasetting");
            loadconfigClass _manageC = new loadconfigClass("picmanagesetting");
            string _sPicSetting = "";
            string _sPicmanage = "";
            bool _IsInverse = false;
            int _iTempValue = 120;
            double dZoom = 1;
            int iZoomH = 1;
            int iZoomW = 1;
            Bitmap _tempBitmap;
            Rectangle _areaRect = new Rectangle();
            try
            {
                _sPicSetting = _areaC.readfromConfig();
                if (_sPicSetting == "")
                {
                    picClass.PicPointX = "0";
                    picClass.PicPointY = "0";
                    picClass.PicPointW = "0";
                    picClass.PicPointH = "0";
                    return "";
                }
                else
                {
                    string[] _settingtemp = PublicMethods.SplitByChar(_sPicSetting, ',');
                    picClass.PicPointX = _settingtemp[0];
                    picClass.PicPointY = _settingtemp[1];
                    picClass.PicPointW = _settingtemp[2];
                    picClass.PicPointH = _settingtemp[3];
                    _areaRect.X = Convert.ToInt32(_settingtemp[0]);
                    _areaRect.Y = Convert.ToInt32(_settingtemp[1]);
                    _areaRect.Width = Convert.ToInt32(_settingtemp[2]);
                    _areaRect.Height = Convert.ToInt32(_settingtemp[3]);
                }

                _sPicmanage = _manageC.readfromConfig();
                if (_sPicmanage == "")
                {
                    picClass.PicPointZ = "1";
                    picClass.PicValue = "120";
                    picClass.PicInverse = "False";
                    _iTempValue = 120;
                }
                else
                {
                    string[] _settingtemp = PublicMethods.SplitByChar(_sPicmanage, ',');
                    picClass.PicPointZ = _settingtemp[0];
                    picClass.PicValue = _settingtemp[1];
                    picClass.PicInverse = _settingtemp[2];
                    dZoom = Convert.ToDouble(_settingtemp[0]);
                    _iTempValue = Convert.ToInt32(_settingtemp[1]);
                }
                iZoomW = Convert.ToInt32(Convert.ToInt32(picClass.PicPointW) * dZoom);
                iZoomH = Convert.ToInt32(Convert.ToInt32(picClass.PicPointH) * dZoom);

                if (picClass.PicInverse == "False")
                {
                    _IsInverse = false;
                }
                else
                {
                    _IsInverse = true;
                }

                //if (is_auto)
                //{
                //    pictureClass _pictureC = new pictureClass();
                //    return _pictureC.autoOrc(_areaRect, _IsInverse, _iTempValue, dZoom, iZoomW, iZoomH);
                //}
                //else
                {
                    pictureClass _pictureC = new pictureClass();
                    _tempBitmap = new Bitmap(_pictureC.getscreenPictureWork());
                    _tempBitmap = _pictureC.CutImage(_tempBitmap, _areaRect);

                    UnCodebase picbase = new UnCodebase(_tempBitmap);
                    picbase.GrayByPixels();
                    picbase.GetPicValidByValue(_iTempValue);

                    picbase.bmpobj = _pictureC.binaryzation(picbase.bmpobj, _IsInverse, _iTempValue);
                    picbase.bmpobj = _pictureC.picSize(picbase.bmpobj, (float)dZoom);

                    iZoomH = picbase.bmpobj.Height;
                    iZoomW = picbase.bmpobj.Width;

                    picbase.bmpobj.Save(@"C:\tempWork.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    picbase.bmpobj.Dispose();
                    _tempBitmap.Dispose();
                    _pictureC.disPic();
                    return _pictureC.getStringOCR(_pictureC.ocrImageWork(iZoomW, iZoomH));
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.ToString());
                return "";
            }
        }
    }
}
