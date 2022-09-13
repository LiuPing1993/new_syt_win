using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace qgj
{
    public partial class PicDetailSetControl : UserControl
    {
        loadconfigClass lccArea = new loadconfigClass("picareasetting");
        loadconfigClass lccManage = new loadconfigClass("picmanagesetting");
        loadconfigClass lccType = new loadconfigClass("recognitionType");
        string sPicSetting = "";
        string sPicManage = "";

        pictureClass pictureC = new pictureClass();
        public Bitmap bitmapWork;
        Bitmap bitMapCache;
        Rectangle rectArea = new Rectangle();

        selectControl inverseSelectC;
        confirmcancelControl confirm = new confirmcancelControl("确定");

        /// <summary>
        /// 是否取反色
        /// </summary>
        bool IsInverse = false;
        /// <summary>
        /// 二值化阈值
        /// </summary>
        int tempValue = 120;
        /// <summary>
        /// 缩放倍率
        /// </summary>
        double zoom = 1;
        int zoomH = 1;
        int zoomW = 1;

        public PicDetailSetControl()
        {
            InitializeComponent();
            //设置背景的颜色
            BackColor = Defcolor.MainBackColor;
            PICBwork.BackColor = Defcolor.MainBackColor;
            lblResultInfo.BackColor = Defcolor.MainBackColor;
            lblResult.BackColor = Defcolor.MainBackColor;
            lblTIP1.BackColor = Defcolor.MainBackColor;
            lblTIP2.BackColor = Defcolor.MainBackColor;
            lblValue.BackColor = Defcolor.MainBackColor;
            lblValueInfo.BackColor = Defcolor.MainBackColor;
            lblZoom.BackColor = Defcolor.MainBackColor;
            lblZoomInfo.BackColor = Defcolor.MainBackColor;
            lblInvInfo.BackColor = Defcolor.MainBackColor;
        }

        private void PicDetailSetControl_Load(object sender, EventArgs e)
        {
            try
            {
                sPicSetting = lccArea.readfromConfig();
                if (sPicSetting == "")
                {
                    picClass.PicPointX = "0";
                    picClass.PicPointY = "0";
                    picClass.PicPointW = "0";
                    picClass.PicPointH = "0";
                    rectArea.X = 0;
                    rectArea.Y = 0;
                    rectArea.Width = 0;
                    rectArea.Height = 0;
                }
                else
                {
                    string[] _settingtemp = PublicMethods.SplitByChar(sPicSetting, ',');
                    picClass.PicPointX = _settingtemp[0];
                    picClass.PicPointY = _settingtemp[1];
                    picClass.PicPointW = _settingtemp[2];
                    picClass.PicPointH = _settingtemp[3];
                    rectArea.X = Convert.ToInt32(_settingtemp[0]);
                    rectArea.Y = Convert.ToInt32(_settingtemp[1]);
                    rectArea.Width = Convert.ToInt32(_settingtemp[2]);
                    rectArea.Height = Convert.ToInt32(_settingtemp[3]);
                }

                sPicManage = lccManage.readfromConfig();
                if (sPicManage == "")
                {
                    picClass.PicPointZ = "1";
                    picClass.PicValue = "120";
                    picClass.PicInverse = "False";

                    hScrollZoom.Value = 10;
                    zoomW = Convert.ToInt32(rectArea.Width * zoom);
                    zoomH = Convert.ToInt32(rectArea.Height * zoom);

                    tempValue = 120;
                    hScrollValue.Value = tempValue;
                }
                else
                {
                    string[] _settingtemp = PublicMethods.SplitByChar(sPicManage, ',');
                    picClass.PicPointZ = _settingtemp[0];
                    picClass.PicValue = _settingtemp[1];
                    picClass.PicInverse = _settingtemp[2];

                    zoom = Convert.ToDouble(_settingtemp[0]);
                    hScrollZoom.Value = Convert.ToInt32(zoom * 10);
                    zoomW = Convert.ToInt32(rectArea.Width * zoom);
                    zoomH = Convert.ToInt32(rectArea.Height * zoom);

                    tempValue = Convert.ToInt32(_settingtemp[1]);
                    hScrollValue.Value = tempValue;
                }


                lblValue.Text = tempValue.ToString();
                lblZoom.Text = zoom.ToString();

                if (picClass.PicInverse == "False")
                {
                    IsInverse = false;
                    inverseSelectC = new selectControl(false);
                    inverseSelectC.Location = new Point(90, 233);
                    Controls.Add(inverseSelectC);
                }
                else
                {
                    IsInverse = true;
                    inverseSelectC = new selectControl(true);
                    inverseSelectC.Location = new Point(90, 233);
                    Controls.Add(inverseSelectC);
                }
                //修改为内存传递设置图片
                //bitmapWork = new Bitmap(Image.FromFile(@"C:\screen.jpg"));
                bitmapWork = pictureC.CutImage(bitmapWork, rectArea);
                PICBwork.SizeMode = PictureBoxSizeMode.Zoom;
                fnLoadPicture();
                
            }
            catch (Exception ee)
            {
                PublicMethods.WriteLog(ee);
                Console.WriteLine(ee.ToString());
                ((PicDetailForm)Parent).Dispose();
            }

            confirm.Location = new Point(400, 245);
            confirm.MouseUp += new MouseEventHandler(confirm_Click);
            Controls.Add(confirm);

            inverseSelectC.MouseUp += new MouseEventHandler(inverseselect_Click);
        }
        private void fnLoadPicture()
        {
            try
            {
                if (lccType.readfromConfig() == "ocrex")
                {
                    UnCodebase picbase = new UnCodebase(bitmapWork);
                    picbase.GrayByPixels();
                    picbase.GetPicValidByValue(tempValue);

                    picbase.bmpobj = pictureC.binaryzation(picbase.bmpobj, IsInverse, tempValue);
                    picbase.bmpobj = pictureC.picSize(picbase.bmpobj, (float)zoom);

                    zoomW = picbase.bmpobj.Width;
                    zoomH = picbase.bmpobj.Height;
                    picbase.bmpobj.Save(@"C:\temp.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    PICBwork.Image = picbase.bmpobj;
                    PICBwork.Refresh();
                }
                else
                {
                    bitMapCache = pictureC.binaryzation(bitmapWork, IsInverse, tempValue, true);
                    bitMapCache = pictureC.picSize(bitMapCache, (float)zoom);
                    bitMapCache.Save(@"C:\temp.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    PICBwork.Image = bitMapCache;
                    PICBwork.Refresh();
                }

                lblResult.Text = pictureC.getStringOCR(pictureC.ocrImage(zoomW, zoomH));
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message.ToString() + e.StackTrace.ToString());
            }
        }
        private void PicDetailSetControl_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid
            );
        }

        private void hScrollValue_Scroll(object sender, ScrollEventArgs e)
        {
            lblValue.Text = hScrollValue.Value.ToString();
            tempValue = hScrollValue.Value;
            try
            {
                fnLoadPicture();
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.Message);
            }
        }

        private void hScrollZoom_Scroll(object sender, ScrollEventArgs e)
        {
            try
            {
                zoom = Convert.ToDouble(hScrollZoom.Value) / 10;
                zoom = Math.Round(zoom, 2);
                zoomW = Convert.ToInt32(rectArea.Width * zoom);
                zoomH = Convert.ToInt32(rectArea.Height * zoom);
                lblZoom.Text = zoom.ToString();
                fnLoadPicture();
            }
            catch (Exception ee)
            {
                Console.WriteLine(ee.ToString());
            }

        }
        private void confirm_Click(object sender, MouseEventArgs e)
        {
            fnSavePictureSetting();
            ((PicDetailForm)Parent).Dispose();
        }
        private void inverseselect_Click(object sender, MouseEventArgs e)
        {
            IsInverse = IsInverse ? false : true;
            fnLoadPicture();
            inverseSelectC.fnSelectChange(IsInverse);
        }
        private bool fnSavePictureSetting()
        {
            picClass.PicPointZ = zoom.ToString();
            picClass.PicValue = tempValue.ToString();
            picClass.PicInverse = IsInverse.ToString();
            return lccManage.writetoConfig(picClass.PicPointZ + "," + picClass.PicValue + "," + picClass.PicInverse);
        }
    }
}
