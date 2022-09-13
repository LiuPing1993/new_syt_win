using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace qgj
{
    public partial class couponControl : UserControl
    {
        int iHttpResult = 0;
        string sHttpResult = "";

        public bool IsHasCoupon = false;

        public bool IsKoubeiCoupon = false; //是否是口碑券
        public bool IsHasQuantity = false;  //是否是多份数的口碑券
        public int quantity = 1;            //核销份数
        public int total_quantity = 1;      //可核销的总份数
        public Rectangle minus = new Rectangle(370, 330, 20, 20); //减少按钮
        public Rectangle plus = new Rectangle(460, 330, 20, 20); //增加按钮

        Color koubeiCouponColor = Color.FromArgb(215, 83, 41);

        PictureBox searchPic = new PictureBox();
        public TextBox codeinsert = new TextBox();
        public TextBox moneyinsert = new TextBox();
        Label tipmoney = new Label();

        couponSearchSuccess couponS; 
        koubeicouponSuccess koubeiS;

        CouponUseControl couponUseC = new CouponUseControl();

        string coupon_id = "";
        string gift_goods_money = "0";
        string total_money = "0";
        string undiscountable_money = "0";
        string coupon_money = "0";

        string couponType = "CASH";

        public couponControl()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);

            InitializeComponent();
            BackColor = Defcolor.MainBackColor;

        }

        private void couponControl_Load(object sender, EventArgs e)
        {
            codeinsert.SetBounds(137, 35, 470, 30);
            codeinsert.Font = new Font(UserClass.fontName, 12);
            codeinsert.ForeColor = Defcolor.FontLiteGrayColor;
            codeinsert.ImeMode = System.Windows.Forms.ImeMode.Off;
            codeinsert.BorderStyle = System.Windows.Forms.BorderStyle.None;
            codeinsert.Enter += new EventHandler(codeinsert_enter);
            Controls.Add(codeinsert);

            moneyinsert.SetBounds(350, 400, 120, 30);
            moneyinsert.Font = new Font(UserClass.fontName, 12);
            moneyinsert.ForeColor = Defcolor.FontLiteGrayColor;
            moneyinsert.ImeMode = System.Windows.Forms.ImeMode.Off;
            moneyinsert.BorderStyle = System.Windows.Forms.BorderStyle.None;
            moneyinsert.Enter += new EventHandler(codeinsert_enter);
            moneyinsert.Visible = false;
            Controls.Add(moneyinsert);

            tipmoney.SetBounds(300,402,50,30);
            tipmoney.Font = new Font(UserClass.fontName, 9);
            tipmoney.ForeColor = Defcolor.FontLiteGrayColor;
            tipmoney.Text = "金 额";
            tipmoney.Visible = false;
            Controls.Add(tipmoney);

            searchPic.SetBounds(610, 38, 20, 20);
            searchPic.BackColor = Color.White;
            searchPic.Image = Properties.Resources.search;
            searchPic.SizeMode = PictureBoxSizeMode.Zoom;
            searchPic.MouseUp += new MouseEventHandler(search_Click);
            Controls.Add(searchPic);

            couponUseC.Location = new Point(320, 450);
            couponUseC.Visible = false;
            couponUseC.MouseUp += new MouseEventHandler(use_Click);
            Controls.Add(couponUseC);

            iHttpResult = 0;
            codeinsert.Focus();
            ((main)Parent).fnSetCouponMouse();
        }

        public void search_Click(object sender, MouseEventArgs e)
        {
            if (codeinsert.Text == "")
            {
                return;
            }
            if (fnCouponinfoHttp())
            {
                Refresh();
            }
            else
            {
                IsHasCoupon = false;
                couponUseC.Visible = false;
                moneyinsert.Visible = false;
                tipmoney.Visible = false;
                IsKoubeiCoupon = false;
                IsHasQuantity = false;
                quantity = 1;
                total_quantity = 1;
                Refresh();
            }
        }

        public void use_Click(object sender, MouseEventArgs e)
        {
            total_money = moneyinsert.Text.Trim();
            if(!PublicMethods.IsNumeric(total_money))
            {
                total_money = "0";
            }
            if(fnCountNeedPay())
            {
                if(fnCouponuseHttp())
                {
                    couponUseSuccessForm successF = new couponUseSuccessForm(0, "");
                    successF.TopMost = true;
                    successF.StartPosition = FormStartPosition.CenterParent;
                    successF.ShowDialog();
                    IsHasCoupon = false;
                    couponUseC.Visible = false;
                    moneyinsert.Visible = false;
                    tipmoney.Visible = false;
                    coupon_id = "";
                    gift_goods_money = "0";
                    total_money = "0";
                    undiscountable_money = "0";
                    coupon_money = "0";
                    couponType = "CASH";

                    IsKoubeiCoupon = false;
                    IsHasQuantity = false;
                    quantity = 1;
                    total_quantity = 1;

                    Refresh();
                    return;
                }
            }
            couponUseSuccessForm errorF = new couponUseSuccessForm(1, sHttpResult);
            errorF.TopMost = true;
            errorF.StartPosition = FormStartPosition.CenterParent;
            errorF.ShowDialog();
            Refresh();
        }

        private void couponControl_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 0, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 0, ButtonBorderStyle.Solid
            );
            PublicMethods.FillRoundRectangle(new Rectangle(132, 28, 640, 66), e.Graphics, 15, Color.White);

            User.drawFormatTitle.Alignment = StringAlignment.Center;
            User.drawFormatTitle.LineAlignment = StringAlignment.Center;

            string sLine = "";
            Color infoColor = Color.Gray;
            if (iHttpResult == 1)
            {
                sLine = "优惠券编号有误 , 请核对后再输入";
                infoColor = Defcolor.MainRadColor;
                if(codeinsert.Text.Trim().Length == 12)
                {
                    sLine = sHttpResult;
                }
                else if (sHttpResult != null)
                {
                    sLine = sHttpResult;
                }
            }
            else
            {
                sLine = "请扫描或输入优惠券编号";
            }
            e.Graphics.DrawString(sLine, new Font(UserClass.fontName, 9), new SolidBrush(infoColor), new Rectangle(235, 75, 300, 30), User.drawFormatTitle);

            if(IsHasCoupon)
            {
                if(IsKoubeiCoupon)
                {
                    #region 口碑券
                    Pen mainLinePen = new Pen(Defcolor.MainGrayLineColor);
                    Brush whiteBrush = new SolidBrush(Color.White);
                    Brush fontBrush = new SolidBrush(Defcolor.FontGrayColor);

                    e.Graphics.DrawRectangle(mainLinePen, 200, 120, 383, 180);
                    e.Graphics.FillRectangle(whiteBrush, 200, 120, 383, 180);

                    Color couponColor = koubeiCouponColor;
                    e.Graphics.FillRectangle(new SolidBrush(couponColor), 200, 120, 104, 84);

                    e.Graphics.DrawLine(mainLinePen, new Point(200, 204), new Point(583, 204));

                    #region 券左上部分绘制
                    int _fontsize = 24;
                    Font _lefttopFont1 = new Font(UserClass.fontName, _fontsize);
                    Font _lefttopFont2 = new Font(UserClass.fontName, 10);
                    string _lefttopLine1 = String.Format("");
                    string _lefttopLine2 = String.Format("");
                    float _y = 145;

                    _lefttopLine1 = String.Format("口碑券");
                    _lefttopFont1 = new Font(UserClass.fontName, 16);
                    
                    SizeF _lefttopsizeF1 = e.Graphics.MeasureString(_lefttopLine1, _lefttopFont1);
                    SizeF _lefttopsizeF2 = e.Graphics.MeasureString(_lefttopLine2, _lefttopFont2);

                    float _x1 = (100 - _lefttopsizeF1.Width - _lefttopsizeF2.Width) / 2 + 205;
                    float _x2 = _x1 + _lefttopsizeF1.Width;
                    PointF _lefttopPoint1 = new PointF(_x1, _y);
                    
                    e.Graphics.DrawString(_lefttopLine1, _lefttopFont1, new SolidBrush(Color.White), _lefttopPoint1);
                    #endregion

                    #region 券其他信息
                    User.drawFormatLeftTop.Alignment = StringAlignment.Near;
                    User.drawFormatLeftTop.LineAlignment = StringAlignment.Near;

                    Font _myFont = new Font(UserClass.fontName, 9);
                    string _strLine = String.Format(" ");
                    SizeF _sizeF = e.Graphics.MeasureString(_strLine, _myFont);
                    PointF _pointF = new PointF((100 - _sizeF.Width) / 2 + 205, 175);

                    _myFont = new Font(UserClass.fontName, 10);
                    _strLine = String.Format(koubeiS.data.name);
                    _pointF = new PointF(315, 130);
                    e.Graphics.DrawString(_strLine, _myFont, fontBrush, _pointF);

                    string start_time = PublicMethods.SplitByChar(koubeiS.data.start_time, ' ')[0];
                    string end_time = PublicMethods.SplitByChar(koubeiS.data.end_time, ' ')[0];
                    _myFont = new Font(UserClass.fontName, 9);
                    _strLine = String.Format("有效期 " + start_time + " - " + end_time);
                    _pointF = new PointF(315, 152);
                    e.Graphics.DrawString(_strLine, _myFont, new SolidBrush(Defcolor.MainGrayLineColor), _pointF);

                    _strLine = String.Format("券编号 " + koubeiS.data.code);
                    _pointF = new PointF(315, 172);
                    e.Graphics.DrawString(_strLine, _myFont, new SolidBrush(Defcolor.MainGrayLineColor), _pointF);

                    _myFont = new Font(UserClass.fontName, 10);
                    _strLine = String.Format("优惠说明");
                    _pointF = new PointF(220, 220);
                    e.Graphics.DrawString(_strLine, _myFont, fontBrush, _pointF);

                    _strLine = String.Format(koubeiS.data.goods_name);
                    e.Graphics.DrawString(_strLine, new Font(UserClass.fontName, 10), fontBrush, new Rectangle(300, 220, 260, 20), User.drawFormatLeftTop);

                    _strLine = String.Format("商品原价: " + koubeiS.data.goods_original_price);
                    e.Graphics.DrawString(_strLine, new Font(UserClass.fontName, 10), fontBrush, new Rectangle(300, 240, 200, 20), User.drawFormatLeftTop);

                    _strLine = String.Format("商品现价: " + koubeiS.data.goods_current_price);
                    e.Graphics.DrawString(_strLine, new Font(UserClass.fontName, 10), fontBrush, new Rectangle(300, 260, 200, 20), User.drawFormatLeftTop);

                    if(IsHasQuantity)
                    {
                        #region 数量选择
                        _myFont = new Font(UserClass.fontName, 10);
                        _strLine = String.Format("使用数量:");
                        _pointF = new PointF(300, 330);
                        e.Graphics.DrawString(_strLine, _myFont, fontBrush, _pointF);

                        //减少按钮
                        e.Graphics.DrawEllipse(mainLinePen, minus);
                        e.Graphics.FillEllipse(whiteBrush, minus);
                        e.Graphics.DrawLine(mainLinePen, new Point(378, 340), new Point(382, 340));

                        //份数展示
                        _strLine = String.Format(quantity.ToString() + "/" + total_quantity.ToString());
                        e.Graphics.DrawString(_strLine, new Font(UserClass.fontName, 10), fontBrush, new Rectangle(395, 330, 66, 20), User.drawFormatTitle);

                        //增加按钮
                        e.Graphics.DrawEllipse(mainLinePen, plus);
                        e.Graphics.FillEllipse(whiteBrush, plus);
                        e.Graphics.DrawLine(mainLinePen, new Point(468, 340), new Point(472, 340));
                        e.Graphics.DrawLine(mainLinePen, new Point(470, 338), new Point(470, 342));
                        #endregion
                    }
                    #endregion

                    #region 是否可用

                    _myFont = new Font(UserClass.fontName, 10);
                    if (koubeiS.data.can_use == "YES")
                    {
                        _strLine = String.Format("有效券");
                    }
                    else
                    {
                        _strLine = String.Format("失效券");
                    }
                    _pointF = new PointF(520, 150);
                    e.Graphics.DrawString(_strLine, _myFont, new SolidBrush(Defcolor.FontBlueColor), _pointF);

                    #endregion

                    #endregion
                }
                else
                {
                    #region 普通优惠券
                    PublicMethods.FillRoundRectangle(new Rectangle(350, 400, 470, 420), e.Graphics, 15, Color.White);

                    e.Graphics.DrawRectangle(new Pen(Defcolor.MainGrayLineColor), 200, 120, 383, 239);
                    e.Graphics.FillRectangle(new SolidBrush(Color.White), 200, 120, 383, 239);

                    Color couponColor = ColorTranslator.FromHtml(couponS.data[0].color);
                    e.Graphics.FillRectangle(new SolidBrush(couponColor), 200, 120, 104, 84);

                    e.Graphics.DrawLine(new Pen(Defcolor.MainGrayLineColor), new Point(200, 204), new Point(583, 204));

                    #region 券左上部分绘制
                    int _fontsize = 24;
                    Font _lefttopFont1 = new Font(UserClass.fontName, _fontsize);
                    Font _lefttopFont2 = new Font(UserClass.fontName, 10);
                    string _lefttopLine1 = String.Format("");
                    string _lefttopLine2 = String.Format("");
                    float _y = 135;
                    if (couponS.data[0].type == "CASH")
                    {
                        _lefttopLine1 = String.Format(couponS.data[0].reduce_cost);
                        if (_lefttopLine1.Length > 4)
                        {
                            _fontsize = 18;
                        }
                        _lefttopLine2 = String.Format("元");
                    }
                    else if (couponS.data[0].type == "DISCOUNT")
                    {
                        _lefttopLine1 = String.Format(couponS.data[0].discount);
                        if (_lefttopLine1.Length > 4)
                        {
                            _fontsize = 18;
                        }
                        _lefttopLine2 = String.Format("折");
                    }
                    else if (couponS.data[0].type == "GIFT")
                    {
                        _lefttopLine1 = String.Format("兑换券");
                        _lefttopFont1 = new Font(UserClass.fontName, 16);
                        _y = 135;
                    }
                    SizeF _lefttopsizeF1 = e.Graphics.MeasureString(_lefttopLine1, _lefttopFont1);
                    SizeF _lefttopsizeF2 = e.Graphics.MeasureString(_lefttopLine2, _lefttopFont2);

                    float _x1 = (100 - _lefttopsizeF1.Width - _lefttopsizeF2.Width) / 2 + 205;
                    float _x2 = _x1 + _lefttopsizeF1.Width;
                    PointF _lefttopPoint1 = new PointF(_x1, _y);
                    PointF _lefttopPoint2;
                    if (_fontsize == 18)
                    {
                        _lefttopPoint2 = new PointF(_x2 - 5, 145);
                    }
                    else
                    {
                        _lefttopPoint2 = new PointF(_x2 - 5, 155);
                    }
                    e.Graphics.DrawString(_lefttopLine1, _lefttopFont1, new SolidBrush(Color.Black), _lefttopPoint1);
                    e.Graphics.DrawString(_lefttopLine2, _lefttopFont2, new SolidBrush(Color.Black), _lefttopPoint2);
                    #endregion

                    #region 券其他信息
                    User.drawFormatLeftTop.Alignment = StringAlignment.Near;
                    User.drawFormatLeftTop.LineAlignment = StringAlignment.Near;

                    string sLeftBottom = couponS.data[0].least_cost_title;
                    Font _myFont = new Font(UserClass.fontName, 9);
                    string _strLine = String.Format(sLeftBottom);
                    SizeF _sizeF = e.Graphics.MeasureString(_strLine, _myFont);
                    PointF _pointF = new PointF((100 - _sizeF.Width) / 2 + 205, 175);
                    e.Graphics.DrawString(_strLine, _myFont, new SolidBrush(Color.Black), _pointF);

                    _myFont = new Font(UserClass.fontName, 10);
                    _strLine = String.Format(couponS.data[0].title);
                    _pointF = new PointF(315, 130);
                    e.Graphics.DrawString(_strLine, _myFont, new SolidBrush(Defcolor.FontGrayColor), _pointF);

                    _myFont = new Font(UserClass.fontName, 9);
                    _strLine = String.Format("有效期 " + couponS.data[0].start_time + " - " + couponS.data[0].end_time);
                    _pointF = new PointF(315, 152);
                    e.Graphics.DrawString(_strLine, _myFont, new SolidBrush(Defcolor.MainGrayLineColor), _pointF);

                    _strLine = String.Format("券编号 " + couponS.data[0].code);
                    _pointF = new PointF(315, 172);
                    e.Graphics.DrawString(_strLine, _myFont, new SolidBrush(Defcolor.MainGrayLineColor), _pointF);

                    _myFont = new Font(UserClass.fontName, 10);
                    _strLine = String.Format("优惠说明");
                    _pointF = new PointF(220, 220);
                    e.Graphics.DrawString(_strLine, _myFont, new SolidBrush(Defcolor.FontGrayColor), _pointF);

                    sLine = couponS.data[0].sub_title;
                    e.Graphics.DrawString(sLine, new Font(UserClass.fontName, 10), new SolidBrush(Defcolor.FontGrayColor), new Rectangle(300, 220, 260, 50), User.drawFormatLeftTop);

                    _myFont = new Font(UserClass.fontName, 10);
                    _strLine = String.Format("可用时段");
                    _pointF = new PointF(220, 280);
                    e.Graphics.DrawString(_strLine, _myFont, new SolidBrush(Defcolor.FontGrayColor), _pointF);

                    sLine = couponS.data[0].use_time_info;
                    e.Graphics.DrawString(sLine, new Font(UserClass.fontName, 10), new SolidBrush(Defcolor.FontGrayColor), new Rectangle(300, 280, 260, 50), User.drawFormatLeftTop);

                    _myFont = new Font(UserClass.fontName, 10);
                    _strLine = String.Format("备 注:");
                    _pointF = new PointF(220, 380);
                    e.Graphics.DrawString(_strLine, _myFont, new SolidBrush(Defcolor.FontGrayColor), _pointF);
                    #endregion
                    #endregion
                }
                
            }

        }
        private void codeinsert_enter(object sender, EventArgs e)
        {
            keyboardClass.showKeyBoard();
        }
        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        {
            try
            {
                if (keyData == Keys.Tab)
                {
                    ((main)Parent).fnShowGatherC();
                    return true;
                }
                else if (keyData == Keys.Escape)
                {
                    ((main)Parent).toptitleC.close_MouseUp(null, null);
                    return true;
                }
                else if (keyData == Keys.Enter && Visible == true)
                {
                    search_Click(null, null);
                }

                if (IsHasCoupon && IsKoubeiCoupon && IsHasQuantity)
                {
                    if (keyData == Keys.Subtract)
                    {
                        if (quantity > 1)
                        {
                            quantity--;
                            Refresh();
                            return true;
                        }
                        else
                        {
                            return true;
                        }
                    }
                    else if (keyData == Keys.Add)
                    {
                        if (quantity < total_quantity)
                        {
                            quantity++;
                            Refresh();
                            return true;
                        }
                        else
                        {
                            return true;
                        }
                    }
                }

                return base.ProcessCmdKey(ref msg, keyData);
            }
            catch
            {
                return base.ProcessCmdKey(ref msg, keyData);
            }
        }

        public bool fnCouponinfoHttp()
        {
            try
            {
                string code = codeinsert.Text.Trim();
                if(code.Length == 12)
                {
                    loadconfigClass _lcc = new loadconfigClass("terminal_sn");
                    UrlClass _urlC = new UrlClass(Url.koubeisearch);
                    _urlC.addParameter("terminal_sn", _lcc.readfromConfig());
                    _urlC.addParameter("code", code);
                    _urlC.addParameter("token", UserClass.Token);
                    string _sRequestUrl = _urlC.requestUrl();
                    Console.WriteLine("url:" + _sRequestUrl);
                    HttpClass httpC = new HttpClass();
                    string _sRequestMsg = httpC.HttpGet(_sRequestUrl);
                    Console.WriteLine("result:" + _sRequestMsg);
                    if (_sRequestMsg.IndexOf("\"errCode\":0") != -1)
                    {
                        koubeiS = (koubeicouponSuccess)JsonConvert.DeserializeObject(_sRequestMsg, typeof(koubeicouponSuccess));
                        iHttpResult = 0;
                        sHttpResult = "成功";
                        IsHasCoupon = true;
                        IsKoubeiCoupon = true;
                        couponUseC.Visible = true;
                        moneyinsert.Visible = false;
                        tipmoney.Visible = false;
                        if (koubeiS.data.available_quantity == "-1")
                        {
                            IsHasQuantity = false;
                        }
                        else
                        {
                            IsHasQuantity = true;
                            quantity = 1;
                            total_quantity = Convert.ToInt32(koubeiS.data.available_quantity);
                        }
                        couponUseC.Location = new Point(320, 380);
                        return true;
                    }
                    else
                    {
                        errorClass _errorC = (errorClass)JsonConvert.DeserializeObject(_sRequestMsg, typeof(errorClass));
                        iHttpResult = 1;
                        sHttpResult = _errorC.errMsg;
                        return false;
                    }
                }
                else
                {
                    #region 普通优惠券
                    loadconfigClass _lcc = new loadconfigClass("terminal_sn");
                    UrlClass _urlC = new UrlClass(Url.couponsearch);
                    _urlC.addParameter("terminal_sn", _lcc.readfromConfig());
                    _urlC.addParameter("code", code);
                    _urlC.addParameter("token", UserClass.Token);
                    string _sRequestUrl = _urlC.requestUrl();
                    Console.WriteLine("url:" + _sRequestUrl);
                    HttpClass httpC = new HttpClass();
                    string _sRequestMsg = httpC.HttpGet(_sRequestUrl);
                    Console.WriteLine("result:" + _sRequestMsg);
                    if (_sRequestMsg.IndexOf("\"errCode\":0") != -1)
                    {
                        couponS = (couponSearchSuccess)JsonConvert.DeserializeObject(_sRequestMsg, typeof(couponSearchSuccess));
                        iHttpResult = 0;
                        sHttpResult = "成功";
                        IsHasCoupon = true;
                        couponUseC.Visible = true;
                        moneyinsert.Visible = true;
                        tipmoney.Visible = true;
                        coupon_id = couponS.data[0].id;
                        IsKoubeiCoupon = false;
                        IsHasQuantity = false;
                        quantity = 1;
                        total_quantity = 1;
                        couponUseC.Location = new Point(320, 450);
                        return true;
                    }
                    else
                    {
                        errorClass _errorC = (errorClass)JsonConvert.DeserializeObject(_sRequestMsg, typeof(errorClass));
                        iHttpResult = 1;
                        sHttpResult = _errorC.errMsg;
                        return false;
                    }
                    #endregion
                }
            }
            catch (Exception e)
            {
                iHttpResult = 1;
                sHttpResult = e.Message.ToString();
                return false;
            }

        }

        public bool fnCouponuseHttp()
        {
            try
            {
                loadconfigClass _lcc = new loadconfigClass("terminal_sn");
                UrlClass _urlC = new UrlClass(Url.couponconsume);
                _urlC.addParameter("terminal_sn", _lcc.readfromConfig());
                _urlC.addParameter("code", codeinsert.Text.Trim());
                _urlC.addParameter("token", UserClass.Token);
                if(couponType == "GIFT")
                {
                    gift_goods_money = moneyinsert.Text.Trim();
                    if (!PublicMethods.IsNumeric(gift_goods_money))
                    {
                        gift_goods_money = "0";
                    }
                    _urlC.addParameter("gift_goods_money", gift_goods_money);
                }
                else if (couponType == "DISCOUNT" || couponType == "CASH")
                {
                    _urlC.addParameter("total_money", total_money);
                    _urlC.addParameter("undiscountable_money", undiscountable_money);
                    _urlC.addParameter("coupon_money", coupon_money);
                }

                if(IsHasQuantity)
                {
                    _urlC.addParameter("quantity", quantity.ToString());
                }

                string _sRequestUrl = _urlC.requestUrl();
                Console.WriteLine("url:" + _sRequestUrl);
                HttpClass httpC = new HttpClass();
                string _sRequestMsg = httpC.HttpGet(_sRequestUrl);
                Console.WriteLine("result:" + _sRequestMsg);
                if (_sRequestMsg.IndexOf("\"errCode\":0") != -1)
                {
                    return true;
                }
                else
                {
                    errorClass _errorC = (errorClass)JsonConvert.DeserializeObject(_sRequestMsg, typeof(errorClass));
                    sHttpResult = _errorC.errMsg;
                    return false;
                }
            }
            catch(Exception e)
            {
                sHttpResult = e.Message.ToString();
                return false;
            }
        }

        public bool fnCountNeedPay()
        {
            if(total_money == "0")
            {
                return true;
            }
            try
            {
                loadconfigClass lcc = new loadconfigClass("terminal_sn");
                UrlClass _urlC = new UrlClass(Url.countneedpay);
                _urlC.addParameter("terminal_sn", lcc.readfromConfig());
                _urlC.addParameter("token", UserClass.Token);
                _urlC.addParameter("order_money", total_money);
                _urlC.addParameter("undiscountable_money", "0");
                _urlC.addParameter("coupon_id_list", coupon_id);

                string _sRequestUrl = _urlC.requestUrl();
                Console.WriteLine("url:" + _sRequestUrl);

                HttpClass http = new HttpClass();
                string requestmsg = http.HttpGet(_sRequestUrl);
                Console.WriteLine("result:" + requestmsg);
                if (requestmsg.IndexOf("\"errCode\":0") != -1)
                {
                    countneedpaySuccess tp = (countneedpaySuccess)JsonConvert.DeserializeObject(requestmsg, typeof(countneedpaySuccess));
                    coupon_money = tp.data.discount_money;
                    return true;
                }
                else
                {
                    errorClass errorp = (errorClass)JsonConvert.DeserializeObject(requestmsg, typeof(errorClass));
                    sHttpResult = errorp.errMsg;
                    return false;
                }
            }
            catch(Exception e)
            {
                sHttpResult = e.Message.ToString();
                return false;
            }
        }

        private void couponControl_MouseUp(object sender, MouseEventArgs e)
        {
            if(IsHasCoupon && IsKoubeiCoupon && IsHasQuantity)
            {
                if(minus.Contains(e.Location))
                {
                    if(quantity > 1)
                    {
                        quantity--;
                        Refresh();
                    }
                }
                else if (plus.Contains(e.Location))
                {
                    if (quantity < total_quantity)
                    {
                        quantity++;
                        Refresh();
                    }
                }
            }
        }

    }

    public class couponItem
    {
        public string coupon_id { get; set; }
        public string id { get; set; }
        public string code { get; set; }
        public string start_time { get; set; }
        public string end_time { get; set; }
        public string title { get; set; }
        public string type { get; set; }
        public string least_cost { get; set; }
        public string reduce_cost { get; set; }
        public string discount { get; set; }
        public string gift { get; set; }
        public string color { get; set; }
        public string time_limit_week { get; set; }
        public string time_limit_begin_time { get; set; }
        public string time_limit_end_time { get; set; }
        public string description { get; set; }
        public string can_use_with_member_card { get; set; }
        public string can_use_with_other_discount { get; set; }
        public string is_all_store { get; set; }
        public string store_ids { get; set; }
        public string least_cost_title { get; set; }
        public string sub_title { get; set; }
        public string tips { get; set; }
        public string use_time_info { get; set; }
    }

    public class couponSearchSuccess
    {
        public string errCode { get; set; }

        public string errMsg { get; set; }

        public List<couponItem> data { get; set; }
    }

    public class koubeicouponItem
    {
        /// <summary>
        /// 
        /// </summary>
        public string can_use { get; set; }
        /// <summary>
        /// 未核销，且可以在当前门店核销
        /// </summary>
        public string can_use_desc { get; set; }
        /// <summary>
        /// 4份龙虾
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string start_time { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string end_time { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 4份龙虾
        /// </summary>
        public string goods_name { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string goods_original_price { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string goods_current_price { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ticket_status { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string available_quantity { get; set; }
    }

    public class koubeicouponSuccess
    {
        /// <summary>
        /// 
        /// </summary>
        public string errCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string errMsg { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public koubeicouponItem data { get; set; }
    }
}
