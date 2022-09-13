using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Drawing.Text;
using System.IO;
using System.Drawing.Imaging;
using System.Diagnostics;

namespace qgj
{
    class printClass
    {
        PrintDocument printDocument;
        //得到任意参数的类的大小
        loadconfigClass lcc;

        public string printName = "";

        public bool IshandPrint = false;

        public bool IsMerchant = true;

        public bool IsBold = false;

        public int typeofCompatible = 0;

        public int iPageWide = 50;

        public string sLine;
        public Font myFont = new Font("宋体", 8);
        public int iPointY = 175;//Y轴起始位置
        public int iLeftPointX = 0;//左起始位置
        public int iRightPointX = 70;//右起始位置
        public int iLeftWide = 70;//左宽
        public int iRightWide = 110;//右宽
        public int iLineWide = 1;//线宽
        public int iFontSpacing = 15;//空行像素
        public int iRightEndX = 190;//右结束位置

        public int iLogeX = 50;
        public int iLogeY = 65;
        public int iLogeW = 90;
        public int iLogeH = 30;

        public int iQrX = 20;
        public int iQrY = 100;
        public int iQrW = 150;
        public int iQrH = 150;

        public string sLPTprint = "";

        public printClass(string _str)
        {
            printDocument = new PrintDocument();
            lcc = new loadconfigClass("driveprintname");
            try
            {
                printName = lcc.readfromConfig();
                printDocument.PrinterSettings.PrinterName = printName;
            }
            catch
            {
                printDocument.PrinterSettings.PrinterName = "";
            }
            printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrintPage);
            printDocument.PrintController = new System.Drawing.Printing.StandardPrintController();

            User.drawFormatTitle.Alignment = StringAlignment.Center;
            User.drawFormatTitle.LineAlignment = StringAlignment.Center;
            User.drawFormatLeft.Alignment = StringAlignment.Near;
            User.drawFormatLeft.LineAlignment = StringAlignment.Center;
            User.drawFormatRight.Alignment = StringAlignment.Far;
            User.drawFormatRight.LineAlignment = StringAlignment.Center;

            User.drawFormatLeftTop.Alignment = StringAlignment.Near;
            User.drawFormatLeftTop.LineAlignment = StringAlignment.Near;
        }
        public void PrintNow()
        {
            try
            {
                printToFile();
                //如果是通过按钮点击进行的打印则跳过自动打印设置检查
                if (!IshandPrint)
                {
                    lcc = new loadconfigClass("autoprint");

                    if (lcc.readfromConfig() == "false")
                    {
                        return;
                    }
                }

                lcc = new loadconfigClass("printbold");

                if (lcc.readfromConfig() == "true")
                {

                    IsBold = true;

                }

                lcc = new loadconfigClass("printcompatible");

                if (lcc.readfromConfig() == "1")
                {

                    typeofCompatible = 1;

                }
                else if(lcc.readfromConfig() == "2")
                {

                    typeofCompatible = 2;

                }

                lcc = new loadconfigClass("printmode");
                if (lcc.readfromConfig() == "mer" && !IsMerchant)
                {
                    return;
                }
                if (lcc.readfromConfig() == "cust" && IsMerchant)
                {
                    return;
                }
                lcc = new loadconfigClass("printpagewide");

                if (lcc.readfromConfig() == "80")
                {
                    iPageWide = 80;

                    iFontSpacing = 20;

                    iLogeX = 75;
                    iLogeW = 120;
                    iLogeH = 40;

                    iQrX = 46;
                    iQrY = 100;
                    iQrW = 200;
                    iQrH = 200;

                    iLeftPointX = 10;
                    iLeftWide = 100;
                    iRightPointX = 100;
                    iRightWide = 170;
                    iRightEndX = 270;
                }
                lcc = new loadconfigClass("userprint");
                if (lcc.readfromConfig() == "drive")
                {
                    if(typeofCompatible == 2)
                    {
                        RawPrinterHelper.SendStringToPrinter(printName, System.Text.Encoding.Default.GetString(new byte[] { 0x1b, 0x40 }));
                        RawPrinterHelper.SendFileToPrinter(printName, Application.StartupPath + @"\printinfo.txt");
                        RawPrinterHelper.SendStringToPrinter(printName, System.Text.Encoding.Default.GetString(new byte[] { 0x0a, 0x0a, 0x1d, 0x56, 0x01 }));
                    }
                    else
                    {
                        printDocument.Print();
                    }
                }
                else if (lcc.readfromConfig() == "lpt")
                {
                    loadconfigClass _lcc = new loadconfigClass("lptprintname");
                    if (sLPTprint != "" && _lcc.readfromConfig() != "")
                    {
                        LPTClass _LPTC = new LPTClass(_lcc.readfromConfig());
                        string _sTemp = _LPTC.fnPrintLine(sLPTprint, IsMerchant);
                        if (_sTemp != "打印成功!")
                        {
                            MessageBox.Show(_sTemp);
                        }
                    }
                }
                else
                {
                    return;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                MessageBox.Show(e.Message);
            }
        }
        public void PrintView()
        {
            PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
            printPreviewDialog.Document = printDocument;
            StringReader lineReader = new StringReader("");
            try
            {
                lcc = new loadconfigClass("printpagewide");
                if (lcc.readfromConfig() == "80")
                {
                    iPageWide = 80;

                    iFontSpacing = 20;

                    iLogeX = 70;
                    iLogeW = 120;
                    iLogeH = 40;

                    iQrX = 50;
                    iQrY = 100;
                    iQrW = 200;
                    iQrH = 200;

                    iLeftPointX = 10;
                    iLeftWide = 100;
                    iRightPointX = 100;
                    iRightWide = 170;
                    iRightEndX = 270;
                }
                printPreviewDialog.ShowDialog();
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message, "打印出现问题", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public Font fnIsBold(int size = 8)
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
            if (typeofCompatible == 1)
            {
                return new Font("Arial", size, fontstyle, GraphicsUnit.Point, 134);
            }
            else
            {
                return new Font("宋体", size, fontstyle);
            }
        }
        public virtual void printToFile() { }
        public virtual void PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e) { }
    }
    class tradeprintClass : printClass
    {
        //public bool IsMerchant = true;
        public string STRmerchantname = "";//商户名称
        public string STRstorename = "";//门店名称
        public string STRterminalnum = "";//终端编号
        public string STRemployee = "";//操作员
        public string STRordernum = "";//订单号
        public string STRpaytype = "";//交易类型
        public string STRorderstatus = "";//订单状态
        public string STRpayaccount = "";//支付账号
        public string STRtotalmoney = "";//订单金额
        public string STRundiscountmoney = "";//不打折金额
        public string STRdiscountmoney = "";///优惠金额
        public string STRpaymoney = "";//收款金额
        public string STRalipaydiscountmoney = "";//支付宝优惠
        public string STRwechatdiscountmoney = "";//微信优惠
        public string STRysfdiscountmoney = "";//云闪付优惠
        public string STRreceipt = "";//实收金额
        public string STRpaytime = "";//交易时间
        public string STRtradenum = "";//交易号
        public string STRbanktradenum = "";//银行流水号
        public string STRpaytradenum = "";//支付流水号

        public tradeprintClass(bool _ismerchant)
            : base("")
        {
            IsMerchant = _ismerchant;
        }
        public void changeMode(bool _ismerchant)
        {
            IsMerchant = _ismerchant;
        }
        public override void printToFile()
        {
            StringBuilder printinfoSB = new StringBuilder();
            printinfoSB.Append("商户名称 " + STRmerchantname + Environment.NewLine);
            printinfoSB.Append("门店名称 " + STRstorename + Environment.NewLine);
            printinfoSB.Append("终端编号 " + STRterminalnum + Environment.NewLine);
            printinfoSB.Append("操 作 员 " + STRemployee + Environment.NewLine);
            printinfoSB.Append("订 单 号 " + STRordernum + Environment.NewLine);
            printinfoSB.Append("交易类型 " + STRpaytype + Environment.NewLine);
            printinfoSB.Append("订单状态 " + STRorderstatus + Environment.NewLine);
            printinfoSB.Append("支付账号 " + STRpayaccount + Environment.NewLine);
            printinfoSB.Append("订单金额 " + STRtotalmoney + Environment.NewLine);
            if (STRundiscountmoney != "")
            {
                printinfoSB.Append("不打折金额  " + STRundiscountmoney + Environment.NewLine);
            }
            if (STRdiscountmoney != "" && STRdiscountmoney != "0.00")
            {
                printinfoSB.Append("优惠金额 " + STRdiscountmoney + Environment.NewLine);
            }
            printinfoSB.Append("收款金额 " + STRpaymoney + Environment.NewLine);
            if (STRalipaydiscountmoney != "")
            {
                printinfoSB.Append("支付宝优惠 " + STRalipaydiscountmoney + Environment.NewLine);
            }
            if (STRwechatdiscountmoney != "")
            {
                printinfoSB.Append("微信优惠 " + STRwechatdiscountmoney + Environment.NewLine);
            }
            if (STRysfdiscountmoney != "")
            {
                printinfoSB.Append("云闪付优惠 " + STRysfdiscountmoney + Environment.NewLine);
            }
            printinfoSB.Append("实收金额 " + STRreceipt + Environment.NewLine);
            printinfoSB.Append("交易时间 " + STRpaytime + Environment.NewLine);
            printinfoSB.Append("系统流水号 " + STRtradenum + Environment.NewLine);
            if (IsMerchant)
            {
                printinfoSB.Append("客户签名 " + Environment.NewLine + Environment.NewLine);
            }
            else
            {
                printinfoSB.Append("谢谢惠顾 " + Environment.NewLine + Environment.NewLine);
            }
            printinfoSB.Append(Environment.NewLine);
            printinfoSB.Append(Environment.NewLine);
            printinfoSB.Append(Environment.NewLine);
            //将文字转化为text文本
            PublicMethods.writeInPrintInfo(printinfoSB.ToString());
            //得到打印的文字
            sLPTprint = printinfoSB.ToString();
        }
        public override void PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            myFont = fnIsBold(8);

            e.Graphics.DrawImage(Properties.Resources.qxprinttitle2, new RectangleF(iLogeX, iLogeY, iLogeW, iLogeH));

            e.Graphics.DrawLine(new Pen(Color.Black, iLineWide), new Point(iLeftPointX, iPointY - 45), new Point(iRightEndX, iPointY - 45));
            if (IsMerchant)
            {
                sLine = String.Format("商户存根");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY - 45, iLeftWide, 35), User.drawFormatLeft);
            }
            else
            {
                sLine = String.Format("用户存根");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY - 45, iLeftWide, 35), User.drawFormatLeft);
            }
            e.Graphics.DrawLine(new Pen(Color.Black, iLineWide), new Point(iLeftPointX, iPointY - 10), new Point(iRightEndX, iPointY - 10));

            #region 商户-门店-多行
            sLine = String.Format("商户名称");
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);

            if (STRmerchantname.Length > 10 && iPageWide == 80)
            {
                int multi_line_hight = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(STRmerchantname.Length) / 11)) * (iFontSpacing - 2);
                sLine = String.Format(STRmerchantname);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, multi_line_hight), User.drawFormatLeftTop);
                iPointY += multi_line_hight;
            }
            else if (STRmerchantname.Length > 9)
            {
                int multi_line_hight = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(STRmerchantname.Length) / 9)) * (iFontSpacing - 2);
                sLine = String.Format(STRmerchantname);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, multi_line_hight), User.drawFormatLeftTop);
                iPointY += multi_line_hight;
            }
            else
            {
                sLine = String.Format(STRmerchantname);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatLeft);
                iPointY += iFontSpacing;
            }

            sLine = String.Format("门店名称");
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);

            if (STRstorename.Length > 10 && iPageWide == 80)
            {
                int multi_line_hight = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(STRstorename.Length) / 11)) * (iFontSpacing - 2);
                sLine = String.Format(STRstorename);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, multi_line_hight), User.drawFormatLeftTop);
                iPointY += multi_line_hight;
            }
            else if (STRstorename.Length > 9)
            {
                int multi_line_hight = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(STRstorename.Length) / 9)) * (iFontSpacing - 2);
                sLine = String.Format(STRstorename);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, multi_line_hight), User.drawFormatLeftTop);
                iPointY += multi_line_hight;
            }
            else
            {
                sLine = String.Format(STRstorename);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatLeft);
                iPointY += iFontSpacing;
            }
            #endregion

            sLine = String.Format("终端编号");
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
            sLine = String.Format(STRterminalnum);
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatLeft);

            iPointY += iFontSpacing;
            sLine = String.Format("操作员");
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
            sLine = String.Format(STRemployee);
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatLeft);

            iPointY += iFontSpacing;
            sLine = String.Format("订单号");
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
            sLine = String.Format(STRordernum);
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatLeft);

            iPointY += iFontSpacing;
            sLine = String.Format("交易类型");
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
            sLine = String.Format(STRpaytype);
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatLeft);

            iPointY += iFontSpacing;
            sLine = String.Format("订单状态");
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
            sLine = String.Format(STRorderstatus);
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatLeft);

            iPointY += iFontSpacing;
            sLine = String.Format("支付账号");
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
            sLine = String.Format(STRpayaccount);
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatLeft);

            iPointY += iFontSpacing;
            sLine = String.Format("订单金额");
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
            sLine = String.Format(STRtotalmoney);
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

            if (STRundiscountmoney != "")
            {
                iPointY += iFontSpacing;
                sLine = String.Format("不打折金额");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format(STRundiscountmoney);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);
            }

            if (STRdiscountmoney != "" && STRdiscountmoney != "0.00")
            {
                iPointY += iFontSpacing;
                sLine = String.Format("优惠金额");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format(STRdiscountmoney);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);
            }

            iPointY += iFontSpacing;
            sLine = String.Format("收款金额");
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
            sLine = String.Format(STRpaymoney);
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);



            if (STRalipaydiscountmoney != "")
            {
                iPointY += iFontSpacing;
                e.Graphics.DrawLine(new Pen(Color.Black, iLineWide), new Point(iLeftPointX, iPointY), new Point(iRightEndX, iPointY));
                iPointY += 5;

                sLine = String.Format("支付宝优惠");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format(STRalipaydiscountmoney);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);
                iPointY += iFontSpacing;

                sLine = String.Format("用户实付");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format(STRreceipt);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

                iPointY += iFontSpacing;
                e.Graphics.DrawLine(new Pen(Color.Black, iLineWide), new Point(iLeftPointX, iPointY), new Point(iRightEndX, iPointY));
                iPointY += 5;
            }
            else if (STRwechatdiscountmoney != "")
            {
                iPointY += iFontSpacing;
                e.Graphics.DrawLine(new Pen(Color.Black, iLineWide), new Point(iLeftPointX, iPointY), new Point(iRightEndX, iPointY));
                iPointY += 5;

                sLine = String.Format("微信优惠");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format(STRwechatdiscountmoney);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);
                iPointY += iFontSpacing;

                sLine = String.Format("用户实付");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format(STRreceipt);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

                iPointY += iFontSpacing;
                e.Graphics.DrawLine(new Pen(Color.Black, iLineWide), new Point(iLeftPointX, iPointY), new Point(iRightEndX, iPointY));
                iPointY += 5;
            }
            else if(STRysfdiscountmoney != "")
            {
                iPointY += iFontSpacing;
                e.Graphics.DrawLine(new Pen(Color.Black, iLineWide), new Point(iLeftPointX, iPointY), new Point(iRightEndX, iPointY));
                iPointY += 5;

                sLine = String.Format("云闪付优惠");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format(STRysfdiscountmoney);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);
                iPointY += iFontSpacing;

                sLine = String.Format("用户实付");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format(STRreceipt);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

                iPointY += iFontSpacing;
                e.Graphics.DrawLine(new Pen(Color.Black, iLineWide), new Point(iLeftPointX, iPointY), new Point(iRightEndX, iPointY));
                iPointY += 5;
            }
            else
            {
                iPointY += iFontSpacing;
            }

            sLine = String.Format("交易时间");
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
            sLine = String.Format(STRpaytime);
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatLeft);

            if (STRtradenum != "")
            {
                iPointY += iFontSpacing;
                myFont = fnIsBold(7);
                sLine = String.Format("系统流水号");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iRightEndX, iFontSpacing), User.drawFormatLeft);

                if (iPageWide == 80)
                {
                    myFont = fnIsBold(6);
                    sLine = String.Format(STRtradenum);
                    e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatLeft);

                }
                else
                {
                    iPointY += (iFontSpacing - 5);
                    myFont = fnIsBold(8);
                    sLine = String.Format(STRtradenum);
                    e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iRightEndX, iFontSpacing), User.drawFormatLeft);
                }
            }
            if (STRbanktradenum != "")
            {
                iPointY += iFontSpacing;
                myFont = fnIsBold(7);
                sLine = String.Format("银行流水号");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iRightEndX, iFontSpacing), User.drawFormatLeft);

                if (iPageWide == 80)
                {
                    myFont = fnIsBold(6);
                    sLine = String.Format(STRbanktradenum);
                    e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatLeft);
                }
                else
                {
                    iPointY += (iFontSpacing - 5);
                    myFont = fnIsBold(8);
                    sLine = String.Format(STRbanktradenum);
                    e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iRightEndX, iFontSpacing), User.drawFormatLeft);
                }
            }
            if (STRpaytradenum != "")
            {
                iPointY += iFontSpacing;
                myFont = fnIsBold(7);
                sLine = String.Format("支付流水号");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iRightEndX, iFontSpacing), User.drawFormatLeft);

                if (iPageWide == 80)
                {
                    myFont = fnIsBold(6);
                    sLine = String.Format(STRpaytradenum);
                    e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatLeft);
                }
                else
                {
                    iPointY += (iFontSpacing - 5);
                    myFont = fnIsBold(8);
                    sLine = String.Format(STRpaytradenum);
                    e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iRightEndX, iFontSpacing), User.drawFormatLeft);
                }

            }

            if (IsMerchant)
            {
                iPointY += iFontSpacing;
                myFont = fnIsBold(6);
                sLine = String.Format("用户签名");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iRightEndX, iFontSpacing), User.drawFormatLeft);

                iPointY += 50;
                e.Graphics.DrawLine(new Pen(Color.Black, iLineWide), new Point(iLeftPointX, iPointY), new Point(iRightEndX, iPointY));
                iPointY += 5;

                sLine = String.Format("本人确认以上交易，对交易无任何交易纠纷");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iRightEndX, iFontSpacing), User.drawFormatLeft);
            }

            try
            {
                zxingbarcodeClass zxingBar = new zxingbarcodeClass(150, 5);
                e.Graphics.DrawImage(zxingBar.getBarcode(STRordernum.ToString().Trim()), new RectangleF(iLeftPointX + 20, iPointY + 20, iRightEndX - 40, 55));
            }
            catch { }

        }
    }
    class refundprintClass : printClass
    {
        //bool IsMerchant = true;

        public string STRmerchantname = "";//商户名称
        public string STRstorename = "";//门店名称
        public string STRterminalnum = "";//终端编号
        public string STRemployee = "";//操作员
        public string STRordernum = "";//订单号
        public string STRpaytype = "";//交易类型
        public string STRorderstatus = "";//订单状态
        public string STRpayaccount = "";//支付账号
        public string STRtotalmoney = "";//订单金额
        public string STRpaymoney = "";//收款金额
        public string STRrefundmoney = "";//退款金额
        public string STRreceipt = "";//剩余实收
        public string STRrefundtime = "";//退款时间
        public string STRtradenum = "";//交易号
        public string STRbanktradenum = "";//银行流水号
        public string STRpaytradenum = "";//支付流水号

        public refundprintClass(bool _ismerchant)
            : base("")
        {
            IsMerchant = _ismerchant;
        }
        public void changeMode(bool _ismerchant)
        {
            IsMerchant = _ismerchant;
        }
        public override void printToFile()
        {
            StringBuilder printinfoSB = new StringBuilder();
            printinfoSB.Append("商户名称 " + STRmerchantname + Environment.NewLine);
            printinfoSB.Append("门店名称 " + STRstorename + Environment.NewLine);
            printinfoSB.Append("终端编号 " + STRterminalnum + Environment.NewLine);
            printinfoSB.Append("操 作 员 " + STRemployee + Environment.NewLine);
            printinfoSB.Append("订 单 号 " + STRordernum + Environment.NewLine);
            printinfoSB.Append("交易类型 " + STRpaytype + Environment.NewLine);
            printinfoSB.Append("订单状态 " + STRorderstatus + Environment.NewLine);
            printinfoSB.Append("支付账号 " + STRpayaccount + Environment.NewLine);
            printinfoSB.Append("订单金额 " + STRtotalmoney + Environment.NewLine);
            printinfoSB.Append("收款金额 " + STRpaymoney + Environment.NewLine);
            printinfoSB.Append("退款金额 " + STRrefundmoney + Environment.NewLine);
            printinfoSB.Append("剩余实收 " + STRreceipt + Environment.NewLine);
            printinfoSB.Append("退款时间 " + STRrefundtime + Environment.NewLine);
            printinfoSB.Append("系统流水号 " + STRtradenum + Environment.NewLine);
            printinfoSB.Append(Environment.NewLine);
            printinfoSB.Append(Environment.NewLine);
            printinfoSB.Append(Environment.NewLine);
            PublicMethods.writeInPrintInfo(printinfoSB.ToString());
            sLPTprint = printinfoSB.ToString();
        }
        public override void PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            myFont = fnIsBold(8);

            e.Graphics.DrawImage(Properties.Resources.qxprinttitle2, new RectangleF(iLogeX, iLogeY, iLogeW, iLogeH));

            e.Graphics.DrawLine(new Pen(Color.Black, iLineWide), new Point(iLeftPointX, iPointY - 45), new Point(iRightEndX, iPointY - 45));
            if (IsMerchant)
            {
                sLine = String.Format("商户存根");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY - 45, iLeftWide, 35), User.drawFormatLeft);
            }
            else
            {
                sLine = String.Format("用户存根");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY - 45, iLeftWide, 35), User.drawFormatLeft);
            }
            e.Graphics.DrawLine(new Pen(Color.Black, iLineWide), new Point(iLeftPointX, iPointY - 10), new Point(iRightEndX, iPointY - 10));

            #region 商户-门店-多行
            sLine = String.Format("商户名称");
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);

            if (STRmerchantname.Length > 10 && iPageWide == 80)
            {
                int multi_line_hight = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(STRmerchantname.Length) / 11)) * (iFontSpacing - 2);
                sLine = String.Format(STRmerchantname);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, multi_line_hight), User.drawFormatLeftTop);
                iPointY += multi_line_hight;
            }
            else if (STRmerchantname.Length > 9)
            {
                int multi_line_hight = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(STRmerchantname.Length) / 9)) * (iFontSpacing - 2);
                sLine = String.Format(STRmerchantname);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, multi_line_hight), User.drawFormatLeftTop);
                iPointY += multi_line_hight;
            }
            else
            {
                sLine = String.Format(STRmerchantname);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatLeft);
                iPointY += iFontSpacing;
            }

            sLine = String.Format("门店名称");
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);

            if (STRstorename.Length > 10 && iPageWide == 80)
            {
                int multi_line_hight = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(STRstorename.Length) / 11)) * (iFontSpacing - 2);
                sLine = String.Format(STRstorename);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, multi_line_hight), User.drawFormatLeftTop);
                iPointY += multi_line_hight;
            }
            else if (STRstorename.Length > 9)
            {
                int multi_line_hight = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(STRstorename.Length) / 9)) * (iFontSpacing - 2);
                sLine = String.Format(STRstorename);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, multi_line_hight), User.drawFormatLeftTop);
                iPointY += multi_line_hight;
            }
            else
            {
                sLine = String.Format(STRstorename);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatLeft);
                iPointY += iFontSpacing;
            }
            #endregion
            //sLine = String.Format("商户名称");
            //e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
            //if (STRmerchantname.Length > 9)
            //{
            //    sLine = String.Format(STRmerchantname);
            //    e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY - 10, iRightWide, iFontSpacing * 2), User.drawFormatLeft);
            //}
            //else
            //{
            //    sLine = String.Format(STRmerchantname);
            //    e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatLeft);
            //}

            //iPointY += iFontSpacing;
            //sLine = String.Format("门店名称");
            //e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
            //sLine = String.Format(STRstorename);
            //e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatLeft);

            //iPointY += iFontSpacing;

            sLine = String.Format("终端编号");
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
            sLine = String.Format(STRterminalnum);
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatLeft);

            iPointY += iFontSpacing;
            sLine = String.Format("操作员");
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
            sLine = String.Format(STRemployee);
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatLeft);

            iPointY += iFontSpacing;
            sLine = String.Format("订单号");
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
            sLine = String.Format(STRordernum);
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatLeft);

            iPointY += iFontSpacing;
            sLine = String.Format("交易类型");
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
            sLine = String.Format(STRpaytype);
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatLeft);

            iPointY += iFontSpacing;
            sLine = String.Format("订单状态");
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
            sLine = String.Format(STRorderstatus);
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatLeft);

            iPointY += iFontSpacing;
            sLine = String.Format("支付账号");
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
            sLine = String.Format(STRpayaccount);
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatLeft);

            iPointY += iFontSpacing;
            sLine = String.Format("订单金额");
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
            sLine = String.Format(STRtotalmoney);
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

            iPointY += iFontSpacing;
            sLine = String.Format("收款金额");
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
            sLine = String.Format(STRpaymoney);
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

            iPointY += iFontSpacing;
            e.Graphics.DrawLine(new Pen(Color.Black, iLineWide), new Point(iLeftPointX, iPointY), new Point(iRightEndX, iPointY));
            iPointY += 5;

            sLine = String.Format("退款金额");
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
            sLine = String.Format(STRrefundmoney);
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);
            iPointY += iFontSpacing;

            sLine = String.Format("剩余实收");
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
            sLine = String.Format(STRreceipt);
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

            iPointY += iFontSpacing;
            e.Graphics.DrawLine(new Pen(Color.Black, iLineWide), new Point(iLeftPointX, iPointY), new Point(iRightEndX, iPointY));
            iPointY += 5;

            sLine = String.Format("退款时间");
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
            sLine = String.Format(STRrefundtime);
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatLeft);

            if (STRtradenum != "")
            {
                iPointY += iFontSpacing;
                myFont = fnIsBold(7);
                sLine = String.Format("系统流水号");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iRightEndX, iFontSpacing), User.drawFormatLeft);

                if (iPageWide == 80)
                {
                    myFont = fnIsBold(6);
                    sLine = String.Format(STRtradenum);
                    e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatLeft);
                }
                else
                {
                    iPointY += (iFontSpacing - 5);
                    myFont = fnIsBold(8);
                    sLine = String.Format(STRtradenum);
                    e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iRightEndX, iFontSpacing), User.drawFormatLeft);
                }
            }
            if (STRbanktradenum != "")
            {
                iPointY += iFontSpacing;
                myFont = fnIsBold(7);
                sLine = String.Format("银行流水号");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iRightEndX, iFontSpacing), User.drawFormatLeft);

                if (iPageWide == 80)
                {
                    myFont = fnIsBold(6);
                    sLine = String.Format(STRbanktradenum);
                    e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatLeft);
                }
                else
                {
                    iPointY += (iFontSpacing - 5);
                    myFont = fnIsBold(8);
                    sLine = String.Format(STRbanktradenum);
                    e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iRightEndX, iFontSpacing), User.drawFormatLeft);
                }
            }
            if (STRpaytradenum != "")
            {
                iPointY += iFontSpacing;
                myFont = fnIsBold(7);
                sLine = String.Format("支付流水号");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iRightEndX, iFontSpacing), User.drawFormatLeft);

                if (iPageWide == 80)
                {
                    myFont = fnIsBold(6);
                    sLine = String.Format(STRpaytradenum);
                    e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatLeft);
                }
                else
                {
                    iPointY += (iFontSpacing - 5);
                    myFont = fnIsBold(8);
                    sLine = String.Format(STRpaytradenum);
                    e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iRightEndX, iFontSpacing), User.drawFormatLeft);
                }
            }

            if (IsMerchant)
            {
                iPointY += iFontSpacing;
                myFont = fnIsBold(6);
                sLine = String.Format("用户签名");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iRightEndX, iFontSpacing), User.drawFormatLeft);

                iPointY += 50;
                e.Graphics.DrawLine(new Pen(Color.Black, iLineWide), new Point(iLeftPointX, iPointY), new Point(iRightEndX, iPointY));
                iPointY += 5;

                sLine = String.Format("本人确认以上交易，对交易无任何交易纠纷");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iRightEndX, iFontSpacing), User.drawFormatLeft);
            }

            try
            {
                zxingbarcodeClass zxingBar = new zxingbarcodeClass(150, 5);
                e.Graphics.DrawImage(zxingBar.getBarcode(STRordernum.ToString().Trim()), new RectangleF(iLeftPointX + 20, iPointY + 20, iRightEndX - 40, 55));
            }
            catch { }
        }

    }
    class detailprintClass : printClass
    {

        public string STRmerchantname = "";//商户名称
        public string STRstorename = "";//门店名称
        public string STRterminalnum = "";//终端编号
        public string STRemployee = "";//操作员
        public string STRordernum = "";//订单号
        public string STRpaytype = "";//交易类型
        public string STRorderstatus = "";//订单状态
        public string STRpayaccount = "";//支付账号
        public string STRtotalmoney = "";//订单金额
        public string STRundiscountmoney = "";//不打折金额
        public string STRdiscountmoney = "";///优惠金额
        public string STRpaymoney = "";//收款金额
        public string STRalipaydiscountmoney = "";//支付宝优惠
        public string STRwechatdiscountmoney = "";//微信优惠
        public string STRysfdiscountmoney = "";//云闪付优惠
        public string STRreceipt = "";//实收金额
        public string STRpaytime = "";//交易时间
        public string STRtradenum = "";//交易号
        public string STRbanktradenum = "";//银行流水号
        public string STRpaytradenum = "";//支付流水号

        public refunddetailPrint[] refundedetailC;//退款详情

        public string STRresiduereceipt = "";//剩余实收

        public detailprintClass(bool _ismerchant)
            : base("")
        {
            IsMerchant = _ismerchant;
        }
        public void changeMode(bool _ismerchant)
        {
            IsMerchant = _ismerchant;
        }
        public override void printToFile()
        {
            bool hasRefund = false;
            StringBuilder printinfoSB = new StringBuilder();
            printinfoSB.Append("商户名称 " + STRmerchantname + Environment.NewLine);
            printinfoSB.Append("门店名称 " + STRstorename + Environment.NewLine);
            printinfoSB.Append("终端编号 " + STRterminalnum + Environment.NewLine);
            printinfoSB.Append("操 作 员 " + STRemployee + Environment.NewLine);
            printinfoSB.Append("订 单 号 " + STRordernum + Environment.NewLine);
            printinfoSB.Append("交易类型 " + STRpaytype + Environment.NewLine);
            printinfoSB.Append("订单状态 " + STRorderstatus + Environment.NewLine);
            printinfoSB.Append("支付账号 " + STRpayaccount + Environment.NewLine);
            printinfoSB.Append("订单金额 " + STRtotalmoney + Environment.NewLine);
            if (STRundiscountmoney != "")
            {
                printinfoSB.Append("不打折金额  " + STRundiscountmoney + Environment.NewLine);
            }
            if (STRdiscountmoney != "" && STRdiscountmoney != "0.00")
            {
                printinfoSB.Append("优惠金额 " + STRdiscountmoney + Environment.NewLine);
            }
            printinfoSB.Append("收款金额 " + STRpaymoney + Environment.NewLine);
            if (STRalipaydiscountmoney != "")
            {
                printinfoSB.Append("支付宝优惠 " + STRalipaydiscountmoney + Environment.NewLine);
            }
            if (STRwechatdiscountmoney != "")
            {
                printinfoSB.Append("微信优惠 " + STRwechatdiscountmoney + Environment.NewLine);
            }
            if (STRysfdiscountmoney != "")
            {
                printinfoSB.Append("云闪付优惠 " + STRysfdiscountmoney + Environment.NewLine);
            }
            printinfoSB.Append("实收金额 " + STRreceipt + Environment.NewLine);
            printinfoSB.Append("交易时间 " + STRpaytime + Environment.NewLine);
            printinfoSB.Append("系统流水号 " + STRtradenum + Environment.NewLine);

            foreach (refunddetailPrint rv in refundedetailC)
            {
                printinfoSB.Append("--------------------------------" + Environment.NewLine);
                printinfoSB.Append("退款金额 " + rv.refundmoney + Environment.NewLine);
                printinfoSB.Append("退款时间 " + rv.refundtime + Environment.NewLine);
                hasRefund = true;
            }
            if(hasRefund)
            {
                printinfoSB.Append("--------------------------------" + Environment.NewLine);
                printinfoSB.Append("剩余实收 " + STRresiduereceipt + Environment.NewLine);
            }
            printinfoSB.Append(Environment.NewLine);
            printinfoSB.Append(Environment.NewLine);
            printinfoSB.Append(Environment.NewLine);
            PublicMethods.writeInPrintInfo(printinfoSB.ToString());
            sLPTprint = printinfoSB.ToString();
        }
        public override void PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            myFont = fnIsBold(8);

            e.Graphics.DrawImage(Properties.Resources.qxprinttitle2, new RectangleF(iLogeX, iLogeY, iLogeW, iLogeH));
            iPointY = 175;
            e.Graphics.DrawLine(new Pen(Color.Black, iLineWide), new Point(iLeftPointX, iPointY - 45), new Point(iRightEndX, iPointY - 45));
            if (IsMerchant)
            {
                sLine = String.Format("商户存根");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY - 45, iLeftWide, 35), User.drawFormatLeft);
            }
            else
            {
                sLine = String.Format("用户存根");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY - 45, iLeftWide, 35), User.drawFormatLeft);
            }
            e.Graphics.DrawLine(new Pen(Color.Black, iLineWide), new Point(iLeftPointX, iPointY - 10), new Point(iRightEndX, iPointY - 10));

            #region 商户-门店-多行
            sLine = String.Format("商户名称");
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);

            if (STRmerchantname.Length > 10 && iPageWide == 80)
            {
                int multi_line_hight = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(STRmerchantname.Length) / 11)) * (iFontSpacing - 2);
                sLine = String.Format(STRmerchantname);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, multi_line_hight), User.drawFormatLeftTop);
                iPointY += multi_line_hight;
            }
            else if (STRmerchantname.Length > 9)
            {
                int multi_line_hight = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(STRmerchantname.Length) / 9)) * (iFontSpacing - 2);
                sLine = String.Format(STRmerchantname);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, multi_line_hight), User.drawFormatLeftTop);
                iPointY += multi_line_hight;
            }
            else
            {
                sLine = String.Format(STRmerchantname);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatLeft);
                iPointY += iFontSpacing;
            }

            sLine = String.Format("门店名称");
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);

            if (STRstorename.Length > 10 && iPageWide == 80)
            {
                int multi_line_hight = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(STRstorename.Length) / 11)) * (iFontSpacing - 2);
                sLine = String.Format(STRstorename);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, multi_line_hight), User.drawFormatLeftTop);
                iPointY += multi_line_hight;
            }
            else if (STRstorename.Length > 9)
            {
                int multi_line_hight = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(STRstorename.Length) / 9)) * (iFontSpacing - 2);
                sLine = String.Format(STRstorename);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, multi_line_hight), User.drawFormatLeftTop);
                iPointY += multi_line_hight;
            }
            else
            {
                sLine = String.Format(STRstorename);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatLeft);
                iPointY += iFontSpacing;
            }
            #endregion
            sLine = String.Format("终端编号");
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
            sLine = String.Format(STRterminalnum);
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatLeft);

            iPointY += iFontSpacing;
            sLine = String.Format("操作员");
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
            sLine = String.Format(STRemployee);
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatLeft);

            iPointY += iFontSpacing;
            sLine = String.Format("订单号");
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
            sLine = String.Format(STRordernum);
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatLeft);

            iPointY += iFontSpacing;
            sLine = String.Format("交易类型");
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
            sLine = String.Format(STRpaytype);
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatLeft);

            iPointY += iFontSpacing;
            sLine = String.Format("订单状态");
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
            sLine = String.Format(STRorderstatus);
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatLeft);

            iPointY += iFontSpacing;
            sLine = String.Format("支付账号");
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
            sLine = String.Format(STRpayaccount);
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatLeft);

            iPointY += iFontSpacing;
            sLine = String.Format("订单金额");
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
            sLine = String.Format(STRtotalmoney);
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

            if (STRundiscountmoney != "")
            {
                iPointY += iFontSpacing;
                sLine = String.Format("不打折金额");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format(STRundiscountmoney);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);
            }
            if (STRdiscountmoney != "" && STRdiscountmoney != "0.00")
            {
                iPointY += iFontSpacing;
                sLine = String.Format("优惠金额");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format(STRdiscountmoney);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);
            }

            iPointY += iFontSpacing;
            sLine = String.Format("收款金额");
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
            sLine = String.Format(STRpaymoney);
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

            if (STRalipaydiscountmoney != "")
            {
                iPointY += iFontSpacing;
                e.Graphics.DrawLine(new Pen(Color.Black, iLineWide), new Point(iLeftPointX, iPointY), new Point(iRightEndX, iPointY));
                iPointY += 5;

                sLine = String.Format("支付宝优惠");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format(STRalipaydiscountmoney);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);
                iPointY += iFontSpacing;

                sLine = String.Format("用户实付");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format(STRreceipt);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

                iPointY += iFontSpacing;
                e.Graphics.DrawLine(new Pen(Color.Black, iLineWide), new Point(iLeftPointX, iPointY), new Point(iRightEndX, iPointY));
                iPointY += 5;
            }
            else if (STRwechatdiscountmoney != "")
            {
                iPointY += iFontSpacing;
                e.Graphics.DrawLine(new Pen(Color.Black, iLineWide), new Point(iLeftPointX, iPointY), new Point(iRightEndX, iPointY));
                iPointY += 5;

                sLine = String.Format("微信优惠");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format(STRwechatdiscountmoney);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);
                iPointY += iFontSpacing;

                sLine = String.Format("用户实付");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format(STRreceipt);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

                iPointY += iFontSpacing;
                e.Graphics.DrawLine(new Pen(Color.Black, iLineWide), new Point(iLeftPointX, iPointY), new Point(iRightEndX, iPointY));
                iPointY += 5;
            }
            else if (STRysfdiscountmoney != "")
            {
                iPointY += iFontSpacing;
                e.Graphics.DrawLine(new Pen(Color.Black, iLineWide), new Point(iLeftPointX, iPointY), new Point(iRightEndX, iPointY));
                iPointY += 5;

                sLine = String.Format("云闪付优惠");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format(STRysfdiscountmoney);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);
                iPointY += iFontSpacing;

                sLine = String.Format("用户实付");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format(STRreceipt);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

                iPointY += iFontSpacing;
                e.Graphics.DrawLine(new Pen(Color.Black, iLineWide), new Point(iLeftPointX, iPointY), new Point(iRightEndX, iPointY));
                iPointY += 5;
            }
            else
            {
                iPointY += iFontSpacing;
            }


            sLine = String.Format("交易时间");
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
            sLine = String.Format(STRpaytime);
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatLeft);

            if (STRtradenum != "")
            {
                iPointY += iFontSpacing;
                myFont = fnIsBold(7);
                sLine = String.Format("系统流水号");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iRightEndX, iFontSpacing), User.drawFormatLeft);

                if (iPageWide == 80)
                {
                    myFont = fnIsBold(6);
                    sLine = String.Format(STRtradenum);
                    e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatLeft);
                }
                else
                {
                    iPointY += (iFontSpacing - 5);
                    myFont = fnIsBold(8);
                    sLine = String.Format(STRtradenum);
                    e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iRightEndX, iFontSpacing), User.drawFormatLeft);
                }
            }
            if (STRbanktradenum != "")
            {
                iPointY += iFontSpacing;
                myFont = fnIsBold(7);
                sLine = String.Format("银行流水号");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iRightEndX, iFontSpacing), User.drawFormatLeft);

                if (iPageWide == 80)
                {
                    myFont = fnIsBold(6);
                    sLine = String.Format(STRbanktradenum);
                    e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatLeft);
                }
                else
                {
                    iPointY += (iFontSpacing - 5);
                    myFont = fnIsBold(8);
                    sLine = String.Format(STRbanktradenum);
                    e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iRightEndX, iFontSpacing), User.drawFormatLeft);
                }
            }
            if (STRpaytradenum != "")
            {
                iPointY += iFontSpacing;
                myFont = fnIsBold(7);
                sLine = String.Format("支付流水号");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iRightEndX, iFontSpacing), User.drawFormatLeft);

                if (iPageWide == 80)
                {
                    myFont = fnIsBold(6);
                    sLine = String.Format(STRpaytradenum);
                    e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatLeft);
                }
                else
                {
                    iPointY += (iFontSpacing - 5);
                    myFont = fnIsBold(8);
                    sLine = String.Format(STRpaytradenum);
                    e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iRightEndX, iFontSpacing), User.drawFormatLeft);
                }
            }

            iPointY += iFontSpacing;
            myFont = fnIsBold(8);
            foreach (refunddetailPrint rv in refundedetailC)
            {
                iPointY += iFontSpacing;
                e.Graphics.DrawLine(new Pen(Color.Black, iLineWide), new Point(iLeftPointX, iPointY), new Point(iRightEndX, iPointY));
                iPointY += 5;
                sLine = String.Format("退款金额");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format(rv.refundmoney);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);
                iPointY += iFontSpacing;
                sLine = String.Format("退款时间");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format(rv.refundtime.ToString());
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatLeft);
            }

            iPointY += iFontSpacing;
            e.Graphics.DrawLine(new Pen(Color.Black, iLineWide), new Point(iLeftPointX, iPointY), new Point(iRightEndX, iPointY));
            iPointY += 5;

            if (refundedetailC.Length != 0)
            {
                sLine = String.Format("剩余实收");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format(STRresiduereceipt);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);
            }

            try
            {
                zxingbarcodeClass zxingBar = new zxingbarcodeClass(150, 5);
                e.Graphics.DrawImage(zxingBar.getBarcode(STRordernum.ToString().Trim()), new RectangleF(iLeftPointX + 20, iPointY + 20, iRightEndX - 40, 55));
            }
            catch { }

        }
    }
    class qrpayprintClass : printClass
    {
        //public bool IsMerchant = true;
        public string STRmerchantname = "";//商户名称
        public string STRstorename = "";//门店名称
        public string STRterminalnum = "";//终端编号
        public string STRemployee = "";//操作员
        public string STRordertime = "";//下单时间
        public string STRordermoney = "";//订单金额
        public string STRpaymoney = "";//应收金额

        public Image qrcodeImage;//二维码图 

        public qrpayprintClass(bool _ismerchant)
            : base("")
        {
            IsMerchant = _ismerchant;
        }
        public void changeMode(bool _ismerchant)
        {
            IsMerchant = _ismerchant;
        }
        public override void printToFile()
        {
            base.printToFile();
        }
        public override void PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            iPointY = 375;
            myFont = fnIsBold(8);
            sLine = String.Format("请使用支付宝或微信扫描支付");
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, 65, iRightEndX, 30), User.drawFormatTitle);

            e.Graphics.DrawImage(qrcodeImage, new RectangleF(iQrX, iQrY, iQrW, iQrH));

            if (iPageWide == 80)
            {
                iLogeY = 320;
                e.Graphics.DrawImage(Properties.Resources.qxprinttitle2, new RectangleF(iLogeX, iLogeY, iLogeW, iLogeH));
                e.Graphics.DrawLine(new Pen(Color.Black, iLineWide), new Point(iLeftPointX, iPointY - 5), new Point(iRightEndX, iPointY - 5));
            }
            else
            {
                iLogeY = 300;
                e.Graphics.DrawImage(Properties.Resources.qxprinttitle2, new RectangleF(iLogeX, iLogeY, iLogeW, iLogeH));
                e.Graphics.DrawLine(new Pen(Color.Black, iLineWide), new Point(iLeftPointX, iPointY - 35), new Point(iRightEndX, iPointY - 35));
            }

            #region 商户-门店-多行
            sLine = String.Format("商户名称");
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);

            if (STRmerchantname.Length > 10 && iPageWide == 80)
            {
                int multi_line_hight = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(STRmerchantname.Length) / 11)) * (iFontSpacing - 2);
                sLine = String.Format(STRmerchantname);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, multi_line_hight), User.drawFormatLeftTop);
                iPointY += multi_line_hight;
            }
            else if (STRmerchantname.Length > 9)
            {
                int multi_line_hight = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(STRmerchantname.Length) / 9)) * (iFontSpacing - 2);
                sLine = String.Format(STRmerchantname);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, multi_line_hight), User.drawFormatLeftTop);
                iPointY += multi_line_hight;
            }
            else
            {
                sLine = String.Format(STRmerchantname);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatLeft);
                iPointY += iFontSpacing;
            }

            sLine = String.Format("门店名称");
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);

            if (STRstorename.Length > 10 && iPageWide == 80)
            {
                int multi_line_hight = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(STRstorename.Length) / 11)) * (iFontSpacing - 2);
                sLine = String.Format(STRstorename);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, multi_line_hight), User.drawFormatLeftTop);
                iPointY += multi_line_hight;
            }
            else if (STRstorename.Length > 9)
            {
                int multi_line_hight = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(STRstorename.Length) / 9)) * (iFontSpacing - 2);
                sLine = String.Format(STRstorename);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, multi_line_hight), User.drawFormatLeftTop);
                iPointY += multi_line_hight;
            }
            else
            {
                sLine = String.Format(STRstorename);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatLeft);
                iPointY += iFontSpacing;
            }
            #endregion

            sLine = String.Format("终端编号");
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
            sLine = String.Format(STRterminalnum);
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatLeft);

            iPointY += iFontSpacing;
            sLine = String.Format("操作员");
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
            sLine = String.Format(STRemployee);
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatLeft);

            iPointY += iFontSpacing;
            sLine = String.Format("下单时间");
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
            sLine = String.Format(STRordertime);
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatLeft);

            iPointY += iFontSpacing;
            sLine = String.Format("订单金额");
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
            sLine = String.Format(STRordermoney);
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

            iPointY += iFontSpacing;
            sLine = String.Format("应收金额");
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
            sLine = String.Format(STRpaymoney);
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);


            iPointY += iFontSpacing;
            e.Graphics.DrawLine(new Pen(Color.Black, iLineWide), new Point(iLeftPointX, iPointY), new Point(iRightEndX, iPointY));
            iPointY += 5;

            sLine = String.Format("请确认金额并扫描付款");
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iRightEndX, iFontSpacing), User.drawFormatLeft);

        }
    }
    class summaryprintClass
    {
        //bool IsMerchant = true;
        public bool IsBold = false;
        public string printName = "";
        public int typeofCompatible = 0;
        public int iPageWide = 50;
        public orderCollectSucccess p;//汇总返回结构体

        PrintDocument printDocument;
        loadconfigClass lcc;

        public string sLine;
        public Font myFont = new Font("宋体", 8);
        public int iPointY = 120;
        public int iLeftPointX = 0;
        public int iRightPointX = 70;
        public int iLeftWide = 70;
        public int iRightWide = 110;
        public int iLineWide = 1;
        public int iFontSpacing = 15;
        public int iRightEndX = 190;

        public int iLogeX = 50;
        public int iLogeY = 65;
        public int iLogeW = 90;
        public int iLogeH = 30;

        public string sLPTprint = "";

        public summaryprintClass()
        {
            printDocument = new PrintDocument();
            lcc = new loadconfigClass("driveprintname");
            try
            {
                printName = lcc.readfromConfig();
                printDocument.PrinterSettings.PrinterName = printName;
            }
            catch
            {
                printDocument.PrinterSettings.PrinterName = "";
            }
            printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrintPage);
            printDocument.PrintController = new System.Drawing.Printing.StandardPrintController();

            User.drawFormatTitle.Alignment = StringAlignment.Center;
            User.drawFormatTitle.LineAlignment = StringAlignment.Center;
            User.drawFormatLeft.Alignment = StringAlignment.Near;
            User.drawFormatLeft.LineAlignment = StringAlignment.Center;
            User.drawFormatRight.Alignment = StringAlignment.Far;
            User.drawFormatRight.LineAlignment = StringAlignment.Center;
        }
        public Font fnIsBold(int size = 8)
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
            if (typeofCompatible == 1)
            {
                return new Font("Arial", size, fontstyle, GraphicsUnit.Point, 134);
            }
            else
            {
                return new Font("宋体", size, fontstyle);
            }
        }
        public void PrintNow()
        {
            try
            {
                lcc = new loadconfigClass("printbold");
                if (lcc.readfromConfig() == "true")
                {
                    IsBold = true;
                }
                lcc = new loadconfigClass("printcompatible");
                if (lcc.readfromConfig() == "1")
                {
                    typeofCompatible = 1;
                }
                else if (lcc.readfromConfig() == "2")
                {
                    typeofCompatible = 2;
                }
                lcc = new loadconfigClass("printpagewide");
                if (lcc.readfromConfig() == "80")
                {
                    iPageWide = 80;

                    iFontSpacing = 20;

                    iLogeX = 75;
                    iLogeW = 120;
                    iLogeH = 40;

                    iLeftPointX = 10;
                    iLeftWide = 120;
                    iRightPointX = 120;
                    iRightWide = 150;
                    iRightEndX = 280;
                }
                printToFile();
                lcc = new loadconfigClass("userprint");
                if (lcc.readfromConfig() == "drive")
                {
                    if (typeofCompatible == 2)
                    {
                        RawPrinterHelper.SendStringToPrinter(printName, System.Text.Encoding.Default.GetString(new byte[] { 0x1b, 0x40 }));
                        RawPrinterHelper.SendFileToPrinter(printName, Application.StartupPath + @"\printinfo.txt");
                        RawPrinterHelper.SendStringToPrinter(printName, System.Text.Encoding.Default.GetString(new byte[] { 0x0a, 0x0a, 0x1d, 0x56, 0x01 }));
                    }
                    else
                    {
                        //Usb打印
                        printDocument.Print();
                    }
                }
                else if (lcc.readfromConfig() == "lpt")
                {
                    loadconfigClass _lcc = new loadconfigClass("lptprintname");
                    if (sLPTprint != "" && _lcc.readfromConfig() != "")
                    {
                        LPTClass _LPTC = new LPTClass(_lcc.readfromConfig());
                        string _sTemp = _LPTC.fnPrintLine(sLPTprint, true, true);
                        if (_sTemp != "打印成功!")
                        {
                            MessageBox.Show(_sTemp);
                        }
                    }
                }
                else
                {
                    return;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                MessageBox.Show(e.Message);
            }
        }
        public void PrintView()
        {
            printToFile();
            PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
            printPreviewDialog.Document = printDocument;
            StringReader lineReader = new StringReader("");
            try
            {
                printPreviewDialog.ShowDialog();
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message, "打印出现问题", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void printToFile()
        {
            StringBuilder printinfoSB = new StringBuilder();
            printinfoSB.Append("商户名称 " + p.data.merchant_name + Environment.NewLine);
            printinfoSB.Append("门店名称 " + p.data.store_name + Environment.NewLine);
            printinfoSB.Append("操作员 " + UserClass.Employee + Environment.NewLine);
            printinfoSB.Append("时间 " + p.data.date + Environment.NewLine);
            if (p.data.data.alipay.num != "0" || p.data.data.alipay.refund_num != "0")
            {
                printinfoSB.Append("--------------------------------" + Environment.NewLine);
                printinfoSB.Append("支付宝 " + Environment.NewLine);
                printinfoSB.Append("交易笔数 " + p.data.data.alipay.num + Environment.NewLine);
                printinfoSB.Append("交易金额 " + p.data.data.alipay.money + Environment.NewLine);
                printinfoSB.Append("退款笔数 " + p.data.data.alipay.refund_num + Environment.NewLine);
                printinfoSB.Append("退款金额 " + p.data.data.alipay.refund_money + Environment.NewLine);
                printinfoSB.Append("优惠金额 " + p.data.data.alipay.discount_money + Environment.NewLine);
                printinfoSB.Append("实收金额 " + p.data.data.alipay.pay_money + Environment.NewLine);
            }
            if (p.data.data.wechat.num != "0" || p.data.data.wechat.refund_num != "0")
            {
                printinfoSB.Append("--------------------------------" + Environment.NewLine);
                printinfoSB.Append("微信 " + Environment.NewLine);
                printinfoSB.Append("交易笔数 " + p.data.data.wechat.num + Environment.NewLine);
                printinfoSB.Append("交易金额 " + p.data.data.wechat.money + Environment.NewLine);
                printinfoSB.Append("退款笔数 " + p.data.data.wechat.refund_num + Environment.NewLine);
                printinfoSB.Append("退款金额 " + p.data.data.wechat.refund_money + Environment.NewLine);
                printinfoSB.Append("优惠金额 " + p.data.data.wechat.discount_money + Environment.NewLine);
                printinfoSB.Append("实收金额 " + p.data.data.wechat.pay_money + Environment.NewLine);
            }
            if (p.data.data.ysf.num != "0" || p.data.data.ysf.refund_num != "0")
            {
                printinfoSB.Append("--------------------------------" + Environment.NewLine);
                printinfoSB.Append("云闪付 " + Environment.NewLine);
                printinfoSB.Append("交易笔数 " + p.data.data.ysf.num + Environment.NewLine);
                printinfoSB.Append("交易金额 " + p.data.data.ysf.money + Environment.NewLine);
                printinfoSB.Append("退款笔数 " + p.data.data.ysf.refund_num + Environment.NewLine);
                printinfoSB.Append("退款金额 " + p.data.data.ysf.refund_money + Environment.NewLine);
                printinfoSB.Append("优惠金额 " + p.data.data.ysf.discount_money + Environment.NewLine);
                printinfoSB.Append("实收金额 " + p.data.data.ysf.pay_money + Environment.NewLine);
            }
            if (p.data.data.unionpay.num != "0" || p.data.data.unionpay.refund_num != "0")
            {
                printinfoSB.Append("--------------------------------" + Environment.NewLine);
                printinfoSB.Append("银联 " + Environment.NewLine);
                printinfoSB.Append("交易笔数 " + p.data.data.unionpay.num + Environment.NewLine);
                printinfoSB.Append("交易金额 " + p.data.data.unionpay.money + Environment.NewLine);
                printinfoSB.Append("退款笔数 " + p.data.data.unionpay.refund_num + Environment.NewLine);
                printinfoSB.Append("退款金额 " + p.data.data.unionpay.refund_money + Environment.NewLine);
                printinfoSB.Append("优惠金额 " + p.data.data.unionpay.discount_money + Environment.NewLine);
                printinfoSB.Append("实收金额 " + p.data.data.unionpay.pay_money + Environment.NewLine);
            }
            if (p.data.data.cash.num != "0" || p.data.data.cash.refund_num != "0")
            {
                printinfoSB.Append("--------------------------------" + Environment.NewLine);
                printinfoSB.Append("现金 " + Environment.NewLine);
                printinfoSB.Append("交易笔数 " + p.data.data.cash.num + Environment.NewLine);
                printinfoSB.Append("交易金额 " + p.data.data.cash.money + Environment.NewLine);
                printinfoSB.Append("退款笔数 " + p.data.data.cash.refund_num + Environment.NewLine);
                printinfoSB.Append("退款金额 " + p.data.data.cash.refund_money + Environment.NewLine);
                printinfoSB.Append("优惠金额 " + p.data.data.cash.discount_money + Environment.NewLine);
                printinfoSB.Append("实收金额 " + p.data.data.cash.pay_money + Environment.NewLine);
            }
            if (p.data.data.balance.num != "0" || p.data.data.balance.refund_num != "0")
            {
                printinfoSB.Append("--------------------------------" + Environment.NewLine);
                printinfoSB.Append("储值 " + Environment.NewLine);
                printinfoSB.Append("交易笔数 " + p.data.data.balance.num + Environment.NewLine);
                printinfoSB.Append("交易金额 " + p.data.data.balance.money + Environment.NewLine);
                printinfoSB.Append("退款笔数 " + p.data.data.balance.refund_num + Environment.NewLine);
                printinfoSB.Append("退款金额 " + p.data.data.balance.refund_money + Environment.NewLine);
                printinfoSB.Append("优惠金额 " + p.data.data.balance.discount_money + Environment.NewLine);
                printinfoSB.Append("实收金额 " + p.data.data.balance.pay_money + Environment.NewLine);
            }
            if (p.data.data.total.num != "0" || p.data.data.total.refund_num != "0")
            {
                printinfoSB.Append("--------------------------------" + Environment.NewLine);
                printinfoSB.Append("汇总统计 " + Environment.NewLine);
                printinfoSB.Append("交易笔数 " + p.data.data.total.num + Environment.NewLine);
                printinfoSB.Append("交易金额 " + p.data.data.total.money + Environment.NewLine);
                printinfoSB.Append("退款笔数 " + p.data.data.total.refund_num + Environment.NewLine);
                printinfoSB.Append("退款金额 " + p.data.data.total.refund_money + Environment.NewLine);
                printinfoSB.Append("优惠金额 " + p.data.data.total.discount_money + Environment.NewLine);
                printinfoSB.Append("实收金额 " + p.data.data.total.pay_money + Environment.NewLine);
            }
            printinfoSB.Append(Environment.NewLine);
            printinfoSB.Append(Environment.NewLine);
            printinfoSB.Append(Environment.NewLine);
            PublicMethods.writeInPrintInfo(printinfoSB.ToString());
            sLPTprint = printinfoSB.ToString();
        }
        public void PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            myFont = fnIsBold(12);
            e.Graphics.DrawImage(Properties.Resources.qxprinttitle2, new RectangleF(iLogeX, iLogeY, iLogeW, iLogeH));
            sLine = String.Format("交易汇总");
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iRightEndX, 30), User.drawFormatTitle);
            myFont = fnIsBold(8);
            iPointY += iFontSpacing * 3;

            #region 商户-门店-多行
            sLine = String.Format("商户名称");
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);

            if (p.data.merchant_name.Length > 10 && iPageWide == 80)
            {
                int multi_line_hight = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(p.data.merchant_name.Length) / 11)) * (iFontSpacing - 2);
                sLine = String.Format(p.data.merchant_name);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, multi_line_hight), User.drawFormatLeftTop);
                iPointY += multi_line_hight;
            }
            else if (p.data.merchant_name.Length > 9)
            {
                int multi_line_hight = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(p.data.merchant_name.Length) / 9)) * (iFontSpacing - 2);
                sLine = String.Format(p.data.merchant_name);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, multi_line_hight), User.drawFormatLeftTop);
                iPointY += multi_line_hight;
            }
            else
            {
                sLine = String.Format(p.data.merchant_name);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatLeft);
                iPointY += iFontSpacing;
            }

            sLine = String.Format("门店名称");
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);

            if (p.data.store_name.Length > 10 && iPageWide == 80)
            {
                int multi_line_hight = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(p.data.store_name.Length) / 11)) * (iFontSpacing - 2);
                sLine = String.Format(p.data.store_name);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, multi_line_hight), User.drawFormatLeftTop);
                iPointY += multi_line_hight;
            }
            else if (p.data.store_name.Length > 9)
            {
                int multi_line_hight = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(p.data.store_name.Length) / 9)) * (iFontSpacing - 2);
                sLine = String.Format(p.data.store_name);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, multi_line_hight), User.drawFormatLeftTop);
                iPointY += multi_line_hight;
            }
            else
            {
                sLine = String.Format(p.data.store_name);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatLeft);
                iPointY += iFontSpacing;
            }
            #endregion

            sLine = String.Format("操作员");
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
            sLine = String.Format(UserClass.Employee);
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatLeft);

            iPointY += iFontSpacing;
            sLine = String.Format("时间");
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
            sLine = String.Format(p.data.date);
            myFont = fnIsBold(5);
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatLeft);
            myFont = fnIsBold(8);

            iPointY += iFontSpacing * 2;
            e.Graphics.DrawLine(new Pen(Color.Black, iLineWide), new Point(iLeftPointX, iPointY), new Point(iRightEndX, iPointY));

            if (iPageWide == 80)
            {
                iLeftWide = 110;
                iRightPointX = 110;
                iRightWide = 160;
            }
            else
            {
                iLeftWide = 90;
                iRightPointX = 90;
                iRightWide = 90;
            }

            if (p.data.data.alipay.num != "0" || p.data.data.alipay.refund_num != "0")
            {
                #region 支付宝汇总
                iPointY += iFontSpacing;
                sLine = String.Format("支付宝");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format("笔数");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatRight);
                sLine = String.Format("金额");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

                iPointY += iFontSpacing;
                sLine = String.Format("交易");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format(p.data.data.alipay.num);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatRight);
                sLine = String.Format(p.data.data.alipay.money);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

                iPointY += iFontSpacing;
                sLine = String.Format("退款");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format(p.data.data.alipay.refund_num);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatRight);
                sLine = String.Format(p.data.data.alipay.refund_money);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

                iPointY += iFontSpacing;
                sLine = String.Format("优惠");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format(p.data.data.alipay.discount_money);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

                iPointY += iFontSpacing;
                sLine = String.Format("实收");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format(p.data.data.alipay.pay_money);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

                iPointY += iFontSpacing * 2;
                e.Graphics.DrawLine(new Pen(Color.Black, iLineWide), new Point(iLeftPointX, iPointY), new Point(iRightEndX, iPointY));
                #endregion
            }

            if (p.data.data.wechat.num != "0" || p.data.data.wechat.refund_num != "0")
            {
                #region 微信汇总
                iPointY += iFontSpacing;
                sLine = String.Format("微信");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format("笔数");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatRight);
                sLine = String.Format("金额");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

                iPointY += iFontSpacing;
                sLine = String.Format("交易");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format(p.data.data.wechat.num);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatRight);
                sLine = String.Format(p.data.data.wechat.money);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

                iPointY += iFontSpacing;
                sLine = String.Format("退款");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format(p.data.data.wechat.refund_num);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatRight);
                sLine = String.Format(p.data.data.wechat.refund_money);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

                iPointY += iFontSpacing;
                sLine = String.Format("优惠");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format(p.data.data.wechat.discount_money);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

                iPointY += iFontSpacing;
                sLine = String.Format("实收");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format(p.data.data.wechat.pay_money);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

                iPointY += iFontSpacing * 2;
                e.Graphics.DrawLine(new Pen(Color.Black, iLineWide), new Point(iLeftPointX, iPointY), new Point(iRightEndX, iPointY));
                #endregion
            }

            if (p.data.data.ysf.num != "0" || p.data.data.ysf.refund_num != "0")
            {
                #region 云闪付汇总
                iPointY += iFontSpacing;
                sLine = String.Format("云闪付");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format("笔数");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatRight);
                sLine = String.Format("金额");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

                iPointY += iFontSpacing;
                sLine = String.Format("交易");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format(p.data.data.ysf.num);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatRight);
                sLine = String.Format(p.data.data.ysf.money);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

                iPointY += iFontSpacing;
                sLine = String.Format("退款");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format(p.data.data.ysf.refund_num);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatRight);
                sLine = String.Format(p.data.data.ysf.refund_money);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

                iPointY += iFontSpacing;
                sLine = String.Format("优惠");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format(p.data.data.ysf.discount_money);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

                iPointY += iFontSpacing;
                sLine = String.Format("实收");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format(p.data.data.ysf.pay_money);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

                iPointY += iFontSpacing * 2;
                e.Graphics.DrawLine(new Pen(Color.Black, iLineWide), new Point(iLeftPointX, iPointY), new Point(iRightEndX, iPointY));
                #endregion
            }

            if (p.data.data.unionpay.num != "0" || p.data.data.unionpay.refund_num != "0")
            {
                #region 银联汇总
                iPointY += iFontSpacing;
                sLine = String.Format("银联");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format("笔数");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatRight);
                sLine = String.Format("金额");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

                iPointY += iFontSpacing;
                sLine = String.Format("交易");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format(p.data.data.unionpay.num);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatRight);
                sLine = String.Format(p.data.data.unionpay.money);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

                iPointY += iFontSpacing;
                sLine = String.Format("退款");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format(p.data.data.unionpay.refund_num);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatRight);
                sLine = String.Format(p.data.data.unionpay.refund_money);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

                iPointY += iFontSpacing;
                sLine = String.Format("优惠");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format(p.data.data.unionpay.discount_money);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

                iPointY += iFontSpacing;
                sLine = String.Format("实收");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format(p.data.data.unionpay.pay_money);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

                iPointY += iFontSpacing * 2;
                e.Graphics.DrawLine(new Pen(Color.Black, iLineWide), new Point(iLeftPointX, iPointY), new Point(iRightEndX, iPointY));
                #endregion
            }

            if (p.data.data.cash.num != "0" || p.data.data.cash.refund_num != "0")
            {
                #region 现金汇总
                iPointY += iFontSpacing;
                sLine = String.Format("现金");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format("笔数");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatRight);
                sLine = String.Format("金额");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

                iPointY += iFontSpacing;
                sLine = String.Format("交易");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format(p.data.data.cash.num);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatRight);
                sLine = String.Format(p.data.data.cash.money);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

                iPointY += iFontSpacing;
                sLine = String.Format("退款");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format(p.data.data.cash.refund_num);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatRight);
                sLine = String.Format(p.data.data.cash.refund_money);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

                iPointY += iFontSpacing;
                sLine = String.Format("优惠");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format(p.data.data.cash.discount_money);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

                iPointY += iFontSpacing;
                sLine = String.Format("实收");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format(p.data.data.cash.pay_money);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

                iPointY += iFontSpacing * 2;
                e.Graphics.DrawLine(new Pen(Color.Black, iLineWide), new Point(iLeftPointX, iPointY), new Point(iRightEndX, iPointY));
                #endregion
            }

            if (p.data.data.balance.num != "0" || p.data.data.balance.refund_num != "0")
            {
                #region 储值汇总
                iPointY += iFontSpacing;
                sLine = String.Format("储值");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format("笔数");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatRight);
                sLine = String.Format("金额");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

                iPointY += iFontSpacing;
                sLine = String.Format("交易");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format(p.data.data.balance.num);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatRight);
                sLine = String.Format(p.data.data.balance.money);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

                iPointY += iFontSpacing;
                sLine = String.Format("退款");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format(p.data.data.balance.refund_num);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatRight);
                sLine = String.Format(p.data.data.balance.refund_money);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

                iPointY += iFontSpacing;
                sLine = String.Format("优惠");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format(p.data.data.balance.discount_money);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

                iPointY += iFontSpacing;
                sLine = String.Format("实收");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format(p.data.data.balance.pay_money);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

                iPointY += iFontSpacing * 2;
                e.Graphics.DrawLine(new Pen(Color.Black, iLineWide), new Point(iLeftPointX, iPointY), new Point(iRightEndX, iPointY));
                #endregion
            }

            if (p.data.data.total.num != "0" || p.data.data.total.refund_num != "0")
            {
                #region 汇总
                iPointY += iFontSpacing;
                sLine = String.Format("汇总统计");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format("笔数");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatRight);
                sLine = String.Format("金额");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

                iPointY += iFontSpacing;
                sLine = String.Format("交易");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format(p.data.data.total.num);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatRight);
                sLine = String.Format(p.data.data.total.money);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

                iPointY += iFontSpacing;
                sLine = String.Format("退款");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format(p.data.data.total.refund_num);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatRight);
                sLine = String.Format(p.data.data.total.refund_money);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

                iPointY += iFontSpacing;
                sLine = String.Format("优惠");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format(p.data.data.total.discount_money);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

                iPointY += iFontSpacing;
                sLine = String.Format("实收");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format(p.data.data.total.pay_money);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

                iPointY += iFontSpacing * 2;
                e.Graphics.DrawLine(new Pen(Color.Black, iLineWide), new Point(iLeftPointX, iPointY), new Point(iRightEndX, iPointY));
                #endregion
            }
        }
    }
    class rechargesummaryprintClass
    {
        public rechargeTradeSummarySuccess p;//汇总返回结构体
        public bool IsBold = false;
        public string printName = "";
        public int typeofCompatible = 0;
        public int iPageWide = 50;
        PrintDocument printDocument;
        loadconfigClass lcc;

        public string sLine;
        public Font myFont = new Font("宋体", 8);
        public int iPointY = 120;
        public int iLeftPointX = 0;
        public int iRightPointX = 70;
        public int iLeftWide = 70;
        public int iRightWide = 110;
        public int iLineWide = 1;
        public int iFontSpacing = 15;
        public int iRightEndX = 190;

        public int iLogeX = 50;
        public int iLogeY = 65;
        public int iLogeW = 90;
        public int iLogeH = 30;

        public string sLPTprint = "";

        public rechargesummaryprintClass()
        {
            printDocument = new PrintDocument();
            lcc = new loadconfigClass("driveprintname");
            try
            {
                printName = lcc.readfromConfig();
                printDocument.PrinterSettings.PrinterName = printName;
            }
            catch
            {
                printDocument.PrinterSettings.PrinterName = "";
            }
            printDocument.PrintPage += new System.Drawing.Printing.PrintPageEventHandler(PrintPage);
            printDocument.PrintController = new System.Drawing.Printing.StandardPrintController();

            User.drawFormatTitle.Alignment = StringAlignment.Center;
            User.drawFormatTitle.LineAlignment = StringAlignment.Center;
            User.drawFormatLeft.Alignment = StringAlignment.Near;
            User.drawFormatLeft.LineAlignment = StringAlignment.Center;
            User.drawFormatRight.Alignment = StringAlignment.Far;
            User.drawFormatRight.LineAlignment = StringAlignment.Center;
        }
        public Font fnIsBold(int size = 8)
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
            if (typeofCompatible == 1)
            {
                return new Font("Arial", size, fontstyle, GraphicsUnit.Point, 134);
            }
            else
            {
                return new Font("宋体", size, fontstyle);
            }
        }
        public void PrintNow()
        {
            try
            {
                lcc = new loadconfigClass("printbold");
                if (lcc.readfromConfig() == "true")
                {
                    IsBold = true;
                }
                lcc = new loadconfigClass("printcompatible");
                if (lcc.readfromConfig() == "1")
                {
                    typeofCompatible = 1;
                }
                else if (lcc.readfromConfig() == "2")
                {
                    typeofCompatible = 2;
                }
                lcc = new loadconfigClass("printpagewide");
                if (lcc.readfromConfig() == "80")
                {
                    iPageWide = 80;

                    iFontSpacing = 20;

                    iLogeX = 75;
                    iLogeW = 120;
                    iLogeH = 40;

                    iLeftPointX = 10;
                    iLeftWide = 120;
                    iRightPointX = 120;
                    iRightWide = 150;
                    iRightEndX = 280;
                }
                printToFile();
                lcc = new loadconfigClass("userprint");
                if (lcc.readfromConfig() == "drive")
                {
                    if (typeofCompatible == 2)
                    {
                        RawPrinterHelper.SendStringToPrinter(printName, System.Text.Encoding.Default.GetString(new byte[] { 0x1b, 0x40 }));
                        RawPrinterHelper.SendFileToPrinter(printName, Application.StartupPath + @"\printinfo.txt");
                        RawPrinterHelper.SendStringToPrinter(printName, System.Text.Encoding.Default.GetString(new byte[] { 0x0a, 0x0a, 0x1d, 0x56, 0x01 }));
                    }
                    else
                    {
                        printDocument.Print();
                    }
                }
                else if (lcc.readfromConfig() == "lpt")
                {
                    loadconfigClass _lcc = new loadconfigClass("lptprintname");
                    if (sLPTprint != "" && _lcc.readfromConfig() != "")
                    {
                        LPTClass _LPTC = new LPTClass(_lcc.readfromConfig());
                        string _sTemp = _LPTC.fnPrintLine(sLPTprint, true, true);
                        if (_sTemp != "打印成功!")
                        {
                            MessageBox.Show(_sTemp);
                        }
                    }
                }
                else
                {
                    return;
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public void PrintView()
        {
            printToFile();
            PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog();
            printPreviewDialog.Document = printDocument;
            StringReader lineReader = new StringReader("");
            try
            {
                printPreviewDialog.ShowDialog();
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message, "打印出现问题", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public void printToFile()
        {
            StringBuilder printinfoSB = new StringBuilder();
            printinfoSB.Append("商户名称 " + p.data.merchant_name + Environment.NewLine);
            printinfoSB.Append("门店名称 " + p.data.store_name + Environment.NewLine);
            printinfoSB.Append("操作员 " + UserClass.Employee + Environment.NewLine);
            printinfoSB.Append("时间 " + p.data.date + Environment.NewLine);
            if (p.data.data.alipay.num != "0")
            {
                printinfoSB.Append("--------------------------------" + Environment.NewLine);
                printinfoSB.Append("支付宝 " + Environment.NewLine);
                printinfoSB.Append("储值笔数 " + p.data.data.alipay.num + Environment.NewLine);
                printinfoSB.Append("储值金额 " + p.data.data.alipay.money + Environment.NewLine);
                if (p.data.data.alipay.discount_money != "0")
                {
                    printinfoSB.Append("优惠金额 " + p.data.data.alipay.discount_money + Environment.NewLine);
                }
                printinfoSB.Append("实收金额 " + p.data.data.alipay.pay_money + Environment.NewLine);
            }
            if (p.data.data.wechat.num != "0")
            {
                printinfoSB.Append("--------------------------------" + Environment.NewLine);
                printinfoSB.Append("微信 " + Environment.NewLine);
                printinfoSB.Append("储值笔数 " + p.data.data.wechat.num + Environment.NewLine);
                printinfoSB.Append("储值金额 " + p.data.data.wechat.money + Environment.NewLine);
                if (p.data.data.wechat.discount_money != "0")
                {
                    printinfoSB.Append("优惠金额 " + p.data.data.wechat.discount_money + Environment.NewLine);
                }
                printinfoSB.Append("实收金额 " + p.data.data.wechat.pay_money + Environment.NewLine);
            }
            if (p.data.data.unionpay.num != "0")
            {
                printinfoSB.Append("--------------------------------" + Environment.NewLine);
                printinfoSB.Append("银联 " + Environment.NewLine);
                printinfoSB.Append("储值笔数 " + p.data.data.unionpay.num + Environment.NewLine);
                printinfoSB.Append("储值金额 " + p.data.data.unionpay.money + Environment.NewLine);
                if (p.data.data.unionpay.discount_money != "0")
                {
                    printinfoSB.Append("优惠金额 " + p.data.data.unionpay.discount_money + Environment.NewLine);
                }
                printinfoSB.Append("实收金额 " + p.data.data.unionpay.pay_money + Environment.NewLine);
            }
            if (p.data.data.cash.num != "0")
            {
                printinfoSB.Append("--------------------------------" + Environment.NewLine);
                printinfoSB.Append("现金 " + Environment.NewLine);
                printinfoSB.Append("储值笔数 " + p.data.data.cash.num + Environment.NewLine);
                printinfoSB.Append("储值金额 " + p.data.data.cash.money + Environment.NewLine);
                if (p.data.data.cash.discount_money != "0")
                {
                    printinfoSB.Append("优惠金额 " + p.data.data.cash.discount_money + Environment.NewLine);
                }
                printinfoSB.Append("实收金额 " + p.data.data.cash.pay_money + Environment.NewLine);
            }
            if (p.data.data.total.num != "0")
            {
                printinfoSB.Append("--------------------------------" + Environment.NewLine);
                printinfoSB.Append("汇总 " + Environment.NewLine);
                printinfoSB.Append("储值笔数 " + p.data.data.total.num + Environment.NewLine);
                printinfoSB.Append("储值金额 " + p.data.data.total.money + Environment.NewLine);
                if (p.data.data.cash.discount_money != "0")
                {
                    printinfoSB.Append("优惠金额 " + p.data.data.total.discount_money + Environment.NewLine);
                }
                printinfoSB.Append("实收金额 " + p.data.data.total.pay_money + Environment.NewLine);
            }
            printinfoSB.Append(Environment.NewLine);
            printinfoSB.Append(Environment.NewLine);
            printinfoSB.Append(Environment.NewLine);
            PublicMethods.writeInPrintInfo(printinfoSB.ToString());
            sLPTprint = printinfoSB.ToString();
        }
        public void PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

            myFont = fnIsBold(12);
            e.Graphics.DrawImage(Properties.Resources.qxprinttitle2, new RectangleF(iLogeX, iLogeY, iLogeW, iLogeH));
            sLine = String.Format("储值汇总");
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iRightEndX, 30), User.drawFormatTitle);
            myFont = fnIsBold(8);

            iPointY += iFontSpacing * 3;

            #region 商户-门店-多行
            sLine = String.Format("商户名称");
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);

            if (p.data.merchant_name.Length > 10 && iPageWide == 80)
            {
                int multi_line_hight = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(p.data.merchant_name.Length) / 11)) * (iFontSpacing - 2);
                sLine = String.Format(p.data.merchant_name);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, multi_line_hight), User.drawFormatLeftTop);
                iPointY += multi_line_hight;
            }
            else if (p.data.merchant_name.Length > 9)
            {
                int multi_line_hight = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(p.data.merchant_name.Length) / 9)) * (iFontSpacing - 2);
                sLine = String.Format(p.data.merchant_name);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, multi_line_hight), User.drawFormatLeftTop);
                iPointY += multi_line_hight;
            }
            else
            {
                sLine = String.Format(p.data.merchant_name);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatLeft);
                iPointY += iFontSpacing;
            }

            sLine = String.Format("门店名称");
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);

            if (p.data.store_name.Length > 10 && iPageWide == 80)
            {
                int multi_line_hight = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(p.data.store_name.Length) / 11)) * (iFontSpacing - 2);
                sLine = String.Format(p.data.store_name);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, multi_line_hight), User.drawFormatLeftTop);
                iPointY += multi_line_hight;
            }
            else if (p.data.store_name.Length > 9)
            {
                int multi_line_hight = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(p.data.store_name.Length) / 9)) * (iFontSpacing - 2);
                sLine = String.Format(p.data.store_name);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, multi_line_hight), User.drawFormatLeftTop);
                iPointY += multi_line_hight;
            }
            else
            {
                sLine = String.Format(p.data.store_name);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatLeft);
                iPointY += iFontSpacing;
            }
            #endregion

            sLine = String.Format("操作员");
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
            sLine = String.Format(UserClass.Employee);
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatLeft);

            iPointY += iFontSpacing;
            sLine = String.Format("时间");
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
            sLine = String.Format(p.data.date);
            myFont = fnIsBold(5);
            e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatLeft);
            myFont = fnIsBold(8);

            iPointY += iFontSpacing * 2;
            e.Graphics.DrawLine(new Pen(Color.Black, iLineWide), new Point(iLeftPointX, iPointY), new Point(iRightEndX, iPointY));

            if (iPageWide == 80)
            {
                iLeftWide = 110;
                iRightPointX = 110;
                iRightWide = 160;
            }
            else
            {
                iLeftWide = 90;
                iRightPointX = 90;
                iRightWide = 90;
            }

            if (p.data.data.alipay.num != "0")
            {
                #region 支付宝汇总
                iPointY += iFontSpacing;
                sLine = String.Format("支付宝");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format("笔数");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatRight);
                sLine = String.Format("金额");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

                iPointY += iFontSpacing;
                sLine = String.Format("储值");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format(p.data.data.alipay.num);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatRight);
                sLine = String.Format(p.data.data.alipay.money);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

                if (p.data.data.alipay.discount_money != "0.00")
                {
                    iPointY += iFontSpacing;
                    sLine = String.Format("优惠");
                    e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                    sLine = String.Format(p.data.data.alipay.discount_money);
                    e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);
                }

                iPointY += iFontSpacing;
                sLine = String.Format("实收");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format(p.data.data.alipay.pay_money);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

                iPointY += iFontSpacing * 2;
                e.Graphics.DrawLine(new Pen(Color.Black, iLineWide), new Point(iLeftPointX, iPointY), new Point(iRightEndX, iPointY));
                #endregion
            }

            if (p.data.data.wechat.num != "0")
            {
                #region 微信汇总
                iPointY += iFontSpacing;
                sLine = String.Format("微信");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format("笔数");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatRight);
                sLine = String.Format("金额");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

                iPointY += iFontSpacing;
                sLine = String.Format("储值");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format(p.data.data.wechat.num);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatRight);
                sLine = String.Format(p.data.data.wechat.money);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

                if (p.data.data.wechat.discount_money != "0.00")
                {
                    iPointY += iFontSpacing;
                    sLine = String.Format("优惠");
                    e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                    sLine = String.Format(p.data.data.wechat.discount_money);
                    e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);
                }

                iPointY += iFontSpacing;
                sLine = String.Format("实收");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format(p.data.data.wechat.pay_money);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

                iPointY += iFontSpacing * 2;
                e.Graphics.DrawLine(new Pen(Color.Black, iLineWide), new Point(iLeftPointX, iPointY), new Point(iRightEndX, iPointY));
                #endregion
            }

            if (p.data.data.unionpay.num != "0")
            {
                #region 银联汇总
                iPointY += iFontSpacing;
                sLine = String.Format("银联");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format("笔数");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatRight);
                sLine = String.Format("金额");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

                iPointY += iFontSpacing;
                sLine = String.Format("储值");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format(p.data.data.unionpay.num);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatRight);
                sLine = String.Format(p.data.data.unionpay.money);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

                if (p.data.data.unionpay.discount_money != "0.00")
                {
                    iPointY += iFontSpacing;
                    sLine = String.Format("优惠");
                    e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                    sLine = String.Format(p.data.data.unionpay.discount_money);
                    e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);
                }

                iPointY += iFontSpacing;
                sLine = String.Format("实收");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format(p.data.data.unionpay.pay_money);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

                iPointY += iFontSpacing * 2;
                e.Graphics.DrawLine(new Pen(Color.Black, iLineWide), new Point(iLeftPointX, iPointY), new Point(iRightEndX, iPointY));
                #endregion
            }

            if (p.data.data.cash.num != "0")
            {
                #region 现金汇总
                iPointY += iFontSpacing;
                sLine = String.Format("现金");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format("笔数");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatRight);
                sLine = String.Format("金额");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

                iPointY += iFontSpacing;
                sLine = String.Format("储值");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format(p.data.data.cash.num);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatRight);
                sLine = String.Format(p.data.data.cash.money);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

                if (p.data.data.cash.discount_money != "0.00")
                {
                    iPointY += iFontSpacing;
                    sLine = String.Format("优惠");
                    e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                    sLine = String.Format(p.data.data.cash.discount_money);
                    e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);
                }

                iPointY += iFontSpacing;
                sLine = String.Format("实收");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format(p.data.data.cash.pay_money);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

                iPointY += iFontSpacing * 2;
                e.Graphics.DrawLine(new Pen(Color.Black, iLineWide), new Point(iLeftPointX, iPointY), new Point(iRightEndX, iPointY));
                #endregion
            }

            if (p.data.data.total.num != "0")
            {
                #region 汇总
                iPointY += iFontSpacing;
                sLine = String.Format("汇总统计");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format("笔数");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatRight);
                sLine = String.Format("金额");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

                iPointY += iFontSpacing;
                sLine = String.Format("储值");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format(p.data.data.total.num);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatRight);
                sLine = String.Format(p.data.data.total.money);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

                if (p.data.data.total.discount_money != "0.00")
                {
                    iPointY += iFontSpacing;
                    sLine = String.Format("优惠");
                    e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                    sLine = String.Format(p.data.data.total.discount_money);
                    e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);
                }

                iPointY += iFontSpacing;
                sLine = String.Format("实收");
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iLeftPointX, iPointY, iLeftWide, iFontSpacing), User.drawFormatLeft);
                sLine = String.Format(p.data.data.total.pay_money);
                e.Graphics.DrawString(sLine, myFont, Brushes.Black, new Rectangle(iRightPointX, iPointY, iRightWide, iFontSpacing), User.drawFormatRight);

                iPointY += iFontSpacing * 2;
                e.Graphics.DrawLine(new Pen(Color.Black, iLineWide), new Point(iLeftPointX, iPointY), new Point(iRightEndX, iPointY));
                #endregion
            }
        }
    }


    public enum printType
    {
        [Description("收款小票")]
        trade = 0,
        [Description("退款小票")]
        refund = 1,
        [Description("详情页小票")]
        detail = 2,
        [Description("其他")]
        other = 3,
    };
    public class refunddetailPrint
    {
        public string refundmoney = "";
        public string refundtime = "";
    }

}
