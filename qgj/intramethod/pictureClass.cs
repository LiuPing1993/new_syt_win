using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace qgj
{
    class pictureClass
    {
        public Image myImage;
        Bitmap bitmapNew;
        Bitmap bitmap;
        Bitmap b;

        public pictureClass() { }
        public Image getscreenPicture()
        {
            Screen scr = Screen.PrimaryScreen;
            Rectangle rc = scr.Bounds;
            int iWidth = rc.Width;
            int iHeight = rc.Height;
            Image myImage = new Bitmap(iWidth, iHeight);
            Graphics g = Graphics.FromImage(myImage);
            g.CopyFromScreen(new Point(0, 0), new Point(0, 0), new Size(iWidth, iHeight));
            myImage.Save(@"C:\screen.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            return myImage;
        }
        public void disPic()
        {
            try
            {
                myImage.Dispose();//原图
                myImage = null;
                bitmapNew.Dispose();//二值化
                bitmapNew = null;
                bitmap.Dispose();//大小
                bitmap = null;
                b.Dispose();//裁剪
                b = null;
            }
            catch { }
        }
        public Image getscreenPictureWork()
        {

            try
            {
                IntPtr ptr = NativeMethods.CreateDC("DISPLAY", null, null, (IntPtr)null);

                Graphics currentG = Graphics.FromHdc(ptr);

                myImage = new Bitmap(Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height, currentG);

                IntPtr screenPtr = currentG.GetHdc();

                Graphics imageG = Graphics.FromImage(myImage);
                
                IntPtr imagePtr = imageG.GetHdc();
                
                NativeMethods.BitBlt(imagePtr, 0, 0, Screen.PrimaryScreen.WorkingArea.Width, Screen.PrimaryScreen.WorkingArea.Height, screenPtr, 0, 0, 13369376);//截图

                currentG.ReleaseHdc(screenPtr);
                imageG.ReleaseHdc(imagePtr);

                NativeMethods.DeleteObject(screenPtr);
                NativeMethods.DeleteObject(imagePtr);

                imageG.Dispose();
                currentG.Dispose();

                return myImage;
            }
            catch
            {
                throw (new Exception("获取失败"));
            }
        }
        /// <summary>
        /// 延时函数
        /// </summary>
        /// <param name="ms">延时时长（ms）</param>
        public static void Delay(uint ms)
        {
            uint start = NativeMethods.GetTickCount();
            while (NativeMethods.GetTickCount() - start < ms)
            {
                Application.DoEvents();
            }
        }

        /// <summary>
        /// 二值化图像
        /// </summary>
        /// <param name="mybitmap">原始图像</param>
        /// <param name="_inverse">是否需要取反色</param>
        /// <param name="_range">值域</param>
        /// <returns>处理完成后的图像</returns>
        public Bitmap binaryzation(Bitmap mybitmap, bool _inverse ,int _range,bool is_set = false)
        {
            bitmapNew = new Bitmap(mybitmap, mybitmap.Width, mybitmap.Height);
            int resultR, resultG, resultB;
            for (int x = 0; x < bitmapNew.Width; x++)
            {
                for (int y = 0; y < bitmapNew.Height; y++)
                {
                    //转灰度
                    Color c = bitmapNew.GetPixel(x, y);
                    int luma = (int)(c.R * 0.3 + c.G * 0.59 + c.B * 0.11);
                    bitmapNew.SetPixel(x, y, Color.FromArgb(luma, luma, luma));

                    //二值化
                    if (!_inverse)
                    {
                        if (((c.R + c.B + c.G) / 3 + 1) <= _range)
                        {
                            resultR = 0;
                            resultG = 0;
                            resultB = 0;
                        }
                        else
                        {
                            resultR = 255;
                            resultG = 255;
                            resultB = 255;
                        }
                    }
                    else
                    {
                        if (((c.R + c.B + c.G) / 3 + 1) >= _range)
                        {
                            resultR = 0;
                            resultG = 0;
                            resultB = 0;
                        }
                        else
                        {
                            resultR = 255;
                            resultG = 255;
                            resultB = 255;
                        }
                    }
                    bitmapNew.SetPixel(x, y, Color.FromArgb(resultR, resultG, resultB));//绘图
                }
            }
            if(!is_set)
            {
                mybitmap.Dispose();
            }
            return bitmapNew;
        }

        /// <summary>
        /// 重设图片大小
        /// </summary>
        /// <param name="originBmp">原始图像</param>
        /// <param name="isize">缩放倍率</param>
        /// <returns>处理完成后的图像</returns>
        public  Bitmap picSize(Bitmap originBmp, float isize)
        {
            int newHeight = Convert.ToInt32(originBmp.Height * isize);
            int newWidth = Convert.ToInt32(originBmp.Width * isize);

            bitmap = new Bitmap(newWidth, newHeight);
            Graphics graphics = Graphics.FromImage(bitmap);

            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.DrawImage(originBmp, new Rectangle(0, 0, newWidth, newHeight),
                new Rectangle(0, 0, originBmp.Width, originBmp.Height), GraphicsUnit.Pixel);
            graphics.Dispose();
            originBmp.Dispose();
            return bitmap;
        }
        /// <summary>
        /// 图片裁剪
        /// </summary>
        /// <param name="img">原始图像</param>
        /// <param name="rect">裁剪矩形</param>
        /// <returns>处理完成后的图像</returns>
        public Bitmap CutImage(Bitmap img, Rectangle rect)
        {
            b = new Bitmap(rect.Width, rect.Height, PixelFormat.Format32bppArgb);
            Graphics g = Graphics.FromImage(b);
            g.DrawImage(img, 0, 0, rect, GraphicsUnit.Pixel);
            g.Dispose();
            img.Dispose();
            return b;
        }

        public string ocrImage(int _w,int _h)
        {
            try
            {
                string getinfo = Marshal.PtrToStringAnsi(NativeMethods.OCRpart(@"C:/temp.jpg", -1, 0, 0, _w, _h));
                return getinfo.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return null;
            }
        }
        public string ocrImageWork(int _w, int _h)
        {
            try
            {
                string getinfo = Marshal.PtrToStringAnsi(NativeMethods.OCRpart(@"C:/tempWork.jpg", -1, 0, 0, _w, _h));
                return getinfo.ToString();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message.ToString());
                return null;
            }
        }

        public string getStringOCR(string inset = "")
        {
            try
            {
                if (inset != "")
                {
                    string s_string = inset.Replace("\n", "").Replace(" ", "").Replace("\t", "").Replace("\r", "").Replace("T", "7").Replace("O", "0").Replace("l", "1")
                        .Replace("o", "0").Replace("(PIC)", "").Replace(",", ".").Replace("I", "1").Replace("Z", "2").Replace("D", "2");

                    loadconfigClass _lcc = new loadconfigClass("recognitionType");
                    if(_lcc.readfromConfig() == "ocrex")
                    {
                        s_string = s_string.Replace("g", "9").Replace("z","2").Replace("s","5").Replace("S","5");
                    }

                    //Console.WriteLine("替换：" + s_string.ToString());

                    int first = s_string.IndexOf(".");
                    int last = s_string.LastIndexOf("."); 
                    
                    if(first != last)
                    {
                        s_string = s_string.Replace(".", "");
                        s_string = s_string.Insert(last - 1, ".");
                    }
                    //Console.WriteLine("第一次处理：" + s_string);

                    if (s_string.Length > last + 3 && last != -1)
                    {
                        s_string = s_string.Remove(last + 3);
                    }

                    //Console.WriteLine("处理完成的字符串：" + s_string);
                    if (Regex.IsMatch(s_string, @"^\d{0,10}\.{0,1}(\d{0,2})?$"))
                    {
                        return s_string;
                    }
                    else
                    {
                        return "";
                    }
                }
                else
                {
                    return "";
                }
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message.ToString());
                return "";
            }
        }


        public string autoOrc(Rectangle _areaRect, bool _IsInverse, int _iTempValue, double dZoom, int iZoomW, int iZoomH)
        {
            try
            {
                getscreenPictureWork();
                //Bitmap _tempBitmap = new Bitmap(myImage);
                //裁剪
                b = new Bitmap(_areaRect.Width, _areaRect.Height, PixelFormat.Format32bppArgb);
                Graphics g = Graphics.FromImage(b);
                g.DrawImage(myImage, 0, 0, _areaRect, GraphicsUnit.Pixel);
                g.Dispose();

                //二值化
                int resultR, resultG, resultB;
                for (int x = 0; x < b.Width; x++)
                {
                    for (int y = 0; y < b.Height; y++)
                    {
                        //转灰度
                        Color c = b.GetPixel(x, y);
                        int luma = (int)(c.R * 0.3 + c.G * 0.59 + c.B * 0.11);
                        b.SetPixel(x, y, Color.FromArgb(luma, luma, luma));

                        //二值化
                        if (!_IsInverse)
                        {
                            if (((c.R + c.B + c.G) / 3 + 1) <= _iTempValue)
                            {
                                resultR = 0;
                                resultG = 0;
                                resultB = 0;
                            }
                            else
                            {
                                resultR = 255;
                                resultG = 255;
                                resultB = 255;
                            }
                        }
                        else
                        {
                            if (((c.R + c.B + c.G) / 3 + 1) >= _iTempValue)
                            {
                                resultR = 0;
                                resultG = 0;
                                resultB = 0;
                            }
                            else
                            {
                                resultR = 255;
                                resultG = 255;
                                resultB = 255;
                            }
                        }
                        b.SetPixel(x, y, Color.FromArgb(resultR, resultG, resultB));//绘图
                    }
                }

                //重设大小
                int newHeight = Convert.ToInt32(b.Height * dZoom);
                int newWidth = Convert.ToInt32(b.Width * dZoom);
                bitmap = new Bitmap(newWidth, newHeight);

                Graphics graphics = Graphics.FromImage(bitmap);

                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;

                graphics.DrawImage(b, new Rectangle(0, 0, newWidth, newHeight),
                    new Rectangle(0, 0, b.Width, b.Height), GraphicsUnit.Pixel);
                graphics.Dispose();

                //保存
                bitmap.Save(@"C:\tempWork.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);

                //销毁
                myImage.Dispose();//原图
                bitmap.Dispose();//大小
                b.Dispose();//裁剪

                return getStringOCR(ocrImageWork(iZoomW, iZoomH));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message.ToString() + e.StackTrace.ToString());
                return "0.00";
            }
        }

    }

    class UnCodebase
    {
        public Bitmap bmpobj;
        public UnCodebase(Bitmap pic)
        {
            bmpobj = new Bitmap(pic);  //转换为Format32bppRgb
        }

        /// <summary>
        /// 根据RGB，计算灰度值
        /// </summary>
        /// <param name="posClr">Color值</param>
        /// <returns>灰度值，整型</returns>
        private int GetGrayNumColor(System.Drawing.Color posClr)
        {
            return (posClr.R * 19595 + posClr.G * 38469 + posClr.B * 7472) >> 16;
        }

        /// <summary>
        /// 灰度转换,逐点方式
        /// </summary>
        public void GrayByPixels()
        {
            for (int i = 0; i < bmpobj.Height; i++)
            {
                for (int j = 0; j < bmpobj.Width; j++)
                {
                    int tmpValue = GetGrayNumColor(bmpobj.GetPixel(j, i));
                    bmpobj.SetPixel(j, i, Color.FromArgb(tmpValue, tmpValue, tmpValue));
                }
            }
        }

        /// <summary>
        /// 去图形边框
        /// </summary>
        /// <param name="borderWidth"></param>
        public void ClearPicBorder(int borderWidth)
        {
            for (int i = 0; i < bmpobj.Height; i++)
            {
                for (int j = 0; j < bmpobj.Width; j++)
                {
                    if (i < borderWidth || j < borderWidth || j > bmpobj.Width - 1 - borderWidth || i > bmpobj.Height - 1 - borderWidth)
                        bmpobj.SetPixel(j, i, Color.FromArgb(255, 255, 255));
                }
            }
        }

        /// <summary>
        /// 灰度转换,逐行方式
        /// </summary>
        public void GrayByLine()
        {
            Rectangle rec = new Rectangle(0, 0, bmpobj.Width, bmpobj.Height);
            BitmapData bmpData = bmpobj.LockBits(rec, ImageLockMode.ReadWrite, bmpobj.PixelFormat);// PixelFormat.Format32bppPArgb);
            //  bmpData.PixelFormat = PixelFormat.Format24bppRgb;
            IntPtr scan0 = bmpData.Scan0;
            int len = bmpobj.Width * bmpobj.Height;
            int[] pixels = new int[len];
            Marshal.Copy(scan0, pixels, 0, len);

            //对图片进行处理
            int GrayValue = 0;
            for (int i = 0; i < len; i++)
            {
                GrayValue = GetGrayNumColor(Color.FromArgb(pixels[i]));
                pixels[i] = (byte)(Color.FromArgb(GrayValue, GrayValue, GrayValue)).ToArgb();   //Color转byte
            }

            bmpobj.UnlockBits(bmpData);
        }

        /// <summary>
        /// 得到有效图形并调整为可平均分割的大小
        /// </summary>
        /// <param name="dgGrayValue">灰度背景分界值</param>
        /// <param name="CharsCount">有效字符数</param>
        /// <returns></returns>
        public void GetPicValidByValue(int dgGrayValue, int CharsCount)
        {
            int posx1 = bmpobj.Width; int posy1 = bmpobj.Height;
            int posx2 = 0; int posy2 = 0;
            for (int i = 0; i < bmpobj.Height; i++)   //找有效区
            {
                for (int j = 0; j < bmpobj.Width; j++)
                {
                    int pixelValue = bmpobj.GetPixel(j, i).R;
                    if (pixelValue < dgGrayValue)   //根据灰度值
                    {
                        if (posx1 > j) posx1 = j;
                        if (posy1 > i) posy1 = i;

                        if (posx2 < j) posx2 = j;
                        if (posy2 < i) posy2 = i;
                    };
                };
            };
            // 确保能整除
            int Span = CharsCount - (posx2 - posx1 + 1) % CharsCount;  //可整除的差额数
            if (Span < CharsCount)
            {
                int leftSpan = Span / 2;  //分配到左边的空列 ，如span为单数,则右边比左边大1
                if (posx1 > leftSpan)
                    posx1 = posx1 - leftSpan;
                if (posx2 + Span - leftSpan < bmpobj.Width)
                    posx2 = posx2 + Span - leftSpan;
            }
            //复制新图
            Rectangle cloneRect = new Rectangle(posx1, posy1, posx2 - posx1 + 1, posy2 - posy1 + 1);
            bmpobj = bmpobj.Clone(cloneRect, bmpobj.PixelFormat);
        }

        /// <summary>
        /// 得到有效图形,图形为类变量
        /// </summary>
        /// <param name="dgGrayValue">灰度背景分界值</param>
        /// <param name="CharsCount">有效字符数</param>
        /// <returns></returns>
        public void GetPicValidByValue(int dgGrayValue)
        {
            int posx1 = bmpobj.Width; int posy1 = bmpobj.Height;
            int posx2 = 0; int posy2 = 0;
            for (int i = 0; i < bmpobj.Height; i++)   //找有效区
            {
                for (int j = 0; j < bmpobj.Width; j++)
                {
                    int pixelValue = bmpobj.GetPixel(j, i).R;
                    if (pixelValue < dgGrayValue)   //根据灰度值
                    {
                        if (posx1 > j) posx1 = j;
                        if (posy1 > i) posy1 = i;

                        if (posx2 < j) posx2 = j;
                        if (posy2 < i) posy2 = i;
                    };
                };
            };
            //复制新图
            Rectangle cloneRect = new Rectangle(posx1, posy1, posx2 - posx1 + 1, posy2 - posy1 + 1);
            bmpobj = bmpobj.Clone(cloneRect, bmpobj.PixelFormat);
        }

        /// <summary>
        /// 得到有效图形,图形由外面传入
        /// </summary>
        /// <param name="dgGrayValue">灰度背景分界值</param>
        /// <param name="CharsCount">有效字符数</param>
        /// <returns></returns>
        public Bitmap GetPicValidByValue(Bitmap singlepic, int dgGrayValue)
        {
            int posx1 = singlepic.Width; int posy1 = singlepic.Height;
            int posx2 = 0; int posy2 = 0;
            for (int i = 0; i < singlepic.Height; i++)   //找有效区
            {
                for (int j = 0; j < singlepic.Width; j++)
                {
                    int pixelValue = singlepic.GetPixel(j, i).R;
                    if (pixelValue < dgGrayValue)   //根据灰度值
                    {
                        if (posx1 > j) posx1 = j;
                        if (posy1 > i) posy1 = i;

                        if (posx2 < j) posx2 = j;
                        if (posy2 < i) posy2 = i;
                    };
                };
            };
            //复制新图
            Rectangle cloneRect = new Rectangle(posx1, posy1, posx2 - posx1 + 1, posy2 - posy1 + 1);
            return singlepic.Clone(cloneRect, singlepic.PixelFormat);
        }

        /// <summary>
        /// 平均分割图片
        /// </summary>
        /// <param name="RowNum">水平上分割数</param>
        /// <param name="ColNum">垂直上分割数</param>
        /// <returns>分割好的图片数组</returns>
        public Bitmap[] GetSplitPics(int RowNum, int ColNum)
        {
            if (RowNum == 0 || ColNum == 0)
                return null;
            int singW = bmpobj.Width / RowNum;
            int singH = bmpobj.Height / ColNum;
            Bitmap[] PicArray = new Bitmap[RowNum * ColNum];

            Rectangle cloneRect;
            for (int i = 0; i < ColNum; i++)   //找有效区
            {
                for (int j = 0; j < RowNum; j++)
                {
                    cloneRect = new Rectangle(j * singW, i * singH, singW, singH);
                    PicArray[i * RowNum + j] = bmpobj.Clone(cloneRect, bmpobj.PixelFormat);//复制小块图
                }
            }
            return PicArray;
        }

        /// <summary>
        /// 返回灰度图片的点阵描述字串，1表示灰点，0表示背景
        /// </summary>
        /// <param name="singlepic">灰度图</param>
        /// <param name="dgGrayValue">背前景灰色界限</param>
        /// <returns></returns>
        public string GetSingleBmpCode(Bitmap singlepic, int dgGrayValue)
        {
            Color piexl;
            string code = "";
            for (int posy = 0; posy < singlepic.Height; posy++)
                for (int posx = 0; posx < singlepic.Width; posx++)
                {
                    piexl = singlepic.GetPixel(posx, posy);
                    if (piexl.R < dgGrayValue)  // Color.Black )
                        code = code + "1";
                    else
                        code = code + "0";
                }
            return code;
        }
    }

}
