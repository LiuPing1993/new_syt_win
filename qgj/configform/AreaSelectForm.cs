using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace qgj
{
    public partial class AreaSelectForm : Form
    {
        loadconfigClass lcc = new loadconfigClass("picareasetting");
        public bool IsStart = false;
        public Rectangle RectSelect;
        string sPicsetting = "";
        Image imageCach;
        Point pointFirst = new Point(0, 0);
        Point pointSecond = new Point(0, 0);

        public AreaSelectForm(Image _image)
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            InitializeComponent();
            imageCach = _image;
        }
        private void AreaSelectForm_Load(object sender, EventArgs e)
        {
            BackgroundImage = imageCach;
            string[] _settingtemp;
            try
            {
                sPicsetting = lcc.readfromConfig();
                if (sPicsetting == "")
                {
                    picClass.PicPointX = "0";
                    picClass.PicPointY = "0";
                    picClass.PicPointW = "0";
                    picClass.PicPointH = "0";
                }
                else
                {
                    _settingtemp = PublicMethods.SplitByChar(sPicsetting, ',');
                    picClass.PicPointX = _settingtemp[0];
                    picClass.PicPointY = _settingtemp[1];
                    picClass.PicPointW = _settingtemp[2];
                    picClass.PicPointH = _settingtemp[3];
                }
                int _areaX = Convert.ToInt32(picClass.PicPointX);
                int _areaY = Convert.ToInt32(picClass.PicPointY);
                int _areaW = Convert.ToInt32(picClass.PicPointW);
                int _areaH = Convert.ToInt32(picClass.PicPointH);

                Image _tempimage = new Bitmap(imageCach);
                Graphics _g = Graphics.FromImage(_tempimage);
                _g.DrawRectangle(new Pen(Color.Red), _areaX, _areaY, _areaW, _areaH);
                RectSelect = new Rectangle(_areaX, _areaY, _areaW, _areaH);
                this.BackgroundImage = _tempimage;
            }
            catch (Exception ee)
            {
                //Console.WriteLine(ee.ToString());
                PublicMethods.WriteLog(ee);
                this.Dispose();
            }
        }
        private void AreaSelectForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 1 && e.Button == System.Windows.Forms.MouseButtons.Left)
            {
                IsStart = true;
                pointFirst = new Point(e.X, e.Y);
            }
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                this.Dispose();
            }
        }
        private void AreaSelectForm_MouseMove(object sender, MouseEventArgs e)
        {
             try
            {
                if (IsStart)
                {
                    pointSecond = new Point(e.X, e.Y);
                    int _minX = Math.Min(pointFirst.X, pointSecond.X);
                    int _minY = Math.Min(pointFirst.Y, pointSecond.Y);
                    int _maxX = Math.Max(pointFirst.X, pointSecond.X);
                    int _maxY = Math.Max(pointFirst.Y, pointSecond.Y);
                    Image _tempimage = new Bitmap(imageCach);
                    Graphics _g = Graphics.FromImage(_tempimage);
                    _g.DrawRectangle(new Pen(Color.Red), _minX, _minY, _maxX - _minX, _maxY - _minY);
                    RectSelect.X = _minX;
                    RectSelect.Y = _minY;
                    RectSelect.Width = _maxX - _minX;
                    RectSelect.Height = _maxY - _minY;
                    this.BackgroundImage = _tempimage;
                }
            }
            catch (Exception ee)
            {
                //MessageBox.Show(ee.Message.ToString());
                Console.WriteLine("自定义选择区域异常："+ee.Message.ToString());
            }
        }
        private void AreaSelectForm_MouseUp(object sender, MouseEventArgs e)
        {
            IsStart = false;
            picClass.PicPointX = RectSelect.X.ToString();
            picClass.PicPointY = RectSelect.Y.ToString();
            picClass.PicPointW = RectSelect.Width.ToString();
            picClass.PicPointH = RectSelect.Height.ToString();
            lcc.writetoConfig(picClass.PicPointX + ',' + picClass.PicPointY + ',' + picClass.PicPointW + ',' + picClass.PicPointH);
            imageCach.Dispose();
            Dispose();
        }
    }
}
