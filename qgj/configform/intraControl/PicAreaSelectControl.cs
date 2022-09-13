using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;


namespace qgj
{
    public partial class PicAreaSelectControl : UserControl
    {
        Label lblInfo = new Label();
        string sInfo = "选择识别区域";
        Bitmap _tempBitmap;

        Rectangle rectPicAreaSelectShow = new Rectangle(5, 5, 120, 30);
        Rectangle rectPicDetailShow = new Rectangle(140, 5, 120, 30);
        
        public PicAreaSelectControl()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            InitializeComponent();
            BackColor = Defcolor.MainBackColor;

            lblInfo.BackColor = Defcolor.MainBackColor;
            lblInfo.Text = "在收银系统按完[结算]后,选取您要获取的金额区域,就会自动识别到金额框中";
            lblInfo.Font = new Font(UserClass.fontName, 9);
            lblInfo.ForeColor = Defcolor.FontGrayColor;
            lblInfo.SetBounds(5, 40, 400, 20);
            Controls.Add(lblInfo);
        }

        private void PicAreaSelectControl_Load(object sender, EventArgs e)
        {

        }
        private void PicAreaSelectControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (rectPicAreaSelectShow.Contains(e.Location))
            {
                ((JointSetControl)Parent).fnPicAreaSelect();
            }
            else if (rectPicDetailShow.Contains(e.Location))
            {
                loadconfigClass _lcc = new loadconfigClass("picareasetting");
                loadconfigClass _lccType = new loadconfigClass("recognitionType");
                if (_lcc.readfromConfig() == "")
                {
                    return;
                }
                else if (_lccType.readfromConfig() == "text")
                {
                    errorinformationForm _errorF = new errorinformationForm("提示", "文本框识别方式无需进行详细设置");
                    _errorF.TopMost = true;
                    _errorF.StartPosition = FormStartPosition.CenterParent;
                    _errorF.ShowDialog();
                    Refresh();
                    return;
                }
                else if (_lccType.readfromConfig() == "clip")
                {
                    errorinformationForm _errorF = new errorinformationForm("提示", "剪切板模式无需进行详细设置");
                    _errorF.TopMost = true;
                    _errorF.StartPosition = FormStartPosition.CenterParent;
                    _errorF.ShowDialog();
                    Refresh();
                    return;
                }
                else if (_lccType.readfromConfig() == "")
                {
                    return;
                }
                else
                {
                    try
                    {
                        pictureClass _pictureC = new pictureClass();
                        ((SetForm)Parent.Parent).Hide();
                        ((SetForm)Parent.Parent).MainForm.Hide();
                        pictureClass.Delay(250);

                        //_pictureC.getscreenPicture();
                        _tempBitmap = new Bitmap(_pictureC.getscreenPictureWork());
                        _pictureC.myImage.Dispose();

                        pictureClass.Delay(250);
                        ((SetForm)Parent.Parent).MainForm.Show();
                        ((SetForm)Parent.Parent).Show();
                    }
                    catch (Exception e1)
                    {
                        PublicMethods.WriteLog(e1);
                    }

                    try
                    {
                        PicDetailForm _PicDetailF = new PicDetailForm();
                        _PicDetailF.picsetC.bitmapWork = _tempBitmap;
                        _PicDetailF.TopMost = true;
                        _PicDetailF.StartPosition = FormStartPosition.CenterParent;
                        _PicDetailF.ShowDialog();
                    }
                    catch (Exception e2) 
                    {
                        PublicMethods.WriteLog(e2);
                    }
                }
            }
        }
        private void PicAreaSelectControl_Paint(object sender, PaintEventArgs e)
        {
            StringFormat _drawFormat = new StringFormat();
            _drawFormat.Alignment = StringAlignment.Center;
            _drawFormat.LineAlignment = StringAlignment.Center;

            Font _myFont = new Font(UserClass.fontName, 9);
            string _strLine = string.Format(sInfo);
            e.Graphics.DrawString(_strLine, _myFont, new SolidBrush(Defcolor.FontLiteGrayColor), rectPicAreaSelectShow, _drawFormat);
            PublicMethods.FrameRoundRectangle(new Rectangle(5, 5, 125, 35), e.Graphics, 7, Defcolor.MainGrayLineColor);
            
            _myFont = new Font(UserClass.fontName, 9);
            _strLine = String.Format("详细设置");
            e.Graphics.DrawString(_strLine, _myFont, new SolidBrush(Defcolor.FontLiteGrayColor), rectPicDetailShow, _drawFormat);
            PublicMethods.FrameRoundRectangle(new Rectangle(140, 5, 260, 35), e.Graphics, 7, Defcolor.MainGrayLineColor);
        }
    }
}
