using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace qgj
{
    public partial class numPadControl : UserControl
    {
        //Panel[,] numPanel;
        Color colorString = Color.FromArgb(77, 77, 77);

        NumControl[,] nnumC;

        Keys[,] keys = { 
                            { Keys.D1, Keys.D2, Keys.D3 },
                            { Keys.D4, Keys.D5, Keys.D6 }, 
                            { Keys.D7, Keys.D8, Keys.D9 }, 
                            { Keys.Back, Keys.D0, Keys.Decimal }
                       };
        string[,] sInfo = {
                             {"1","2","3"},
                             {"4","5","6"},
                             {"7","8","9"},
                             {"←","0","."},
                         };
        public numPadControl()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            InitializeComponent();
            BackColor = Defcolor.MainBackColor;
        }
        private void NumControl_Load(object sender, EventArgs e)
        {
            #region 旧实现方式
            //numPanel = new Panel[4, 3];
            //int temp = 1;
            //for (int i = 0; i < 4; i++)
            //{
            //    for (int j = 0; j < 3; j++, temp++)
            //    {
            //        numPanel[i, j] = new Panel();
            //        numPanel[i, j].MouseUp += new MouseEventHandler(NumPanel_MouseUp);
            //        //numPanel[i, j].MouseDown += new MouseEventHandler(NumPanel_MouseDown);
            //        //numPanel[i, j].MouseEnter += new EventHandler(NumPanel_MouseEnter);
            //        //numPanel[i, j].MouseLeave += new EventHandler(NumPanel_MouseLeave);
            //        numPanel[i, j].Paint += new PaintEventHandler(NumPanel_Paint);
            //        numPanel[i, j].SetBounds(j * 133, i * 62, 133, 62);
            //        numPanel[i, j].Name = temp.ToString();
            //        numPanel[i, j].BackColor = Defcolor.NumboardBackColor;
            //        this.Controls.Add(numPanel[i, j]);
            //    }
            //}
            #endregion

            nnumC = new NumControl[4, 3];
            for (int _i = 0; _i < 4; _i++)
            {
                for (int _j = 0; _j < 3; _j++)
                {
                    nnumC[_i, _j] = new NumControl();
                    nnumC[_i, _j].sShowText = sInfo[_i, _j];
                    nnumC[_i, _j].NumKey = keys[_i, _j];
                    if (_i == 3 && _j == 2)
                    {
                        nnumC[_i, _j].Font = new System.Drawing.Font(UserClass.fontName, 36);
                    }
                    else
                    {
                        nnumC[_i, _j].Font = new System.Drawing.Font(UserClass.fontName, 22);
                    }
                    nnumC[_i, _j].SetBounds(_j * 133, _i * 62, 133, 62);
                    Controls.Add(nnumC[_i, _j]);
                }
            }
        }

        private void NumControl_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid
            );
        }
        #region 旧实现方式
        //private void NumPanel_Paint(object sender, PaintEventArgs e)
        //{
        //    e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        //    ControlPaint.DrawBorder(e.Graphics, ClientRectangle,
        //        Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
        //        Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
        //        Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
        //        Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid
        //    );

        //    Panel p = (Panel)sender;
        //    string numPaneltemp = "";
        //    switch (p.Name)
        //    {
        //        case "10": numPaneltemp = "←"; e.Graphics.DrawLine(new Pen(Defcolor.MainGrayLineColor, 1), new Point(0, 61), new Point(133, 61)); break;
        //        case "11": numPaneltemp = "0"; e.Graphics.DrawLine(new Pen(Defcolor.MainGrayLineColor, 1), new Point(0, 61), new Point(133, 61)); break;
        //        case "12": numPaneltemp = "·"; e.Graphics.DrawLine(new Pen(Defcolor.MainGrayLineColor, 1), new Point(0, 61), new Point(133, 61)); break;
        //        //case "13": numPaneltemp = "确定"; e.Graphics.DrawLine(new Pen(Defcolor.MainGrayLineColor, 1), new Point(0, 61), new Point(133, 61)); break;
        //        default: numPaneltemp = p.Name.ToString(); break;
        //    }
        //    Font numPanelFont;
        //    if (numPaneltemp == "←")
        //    {

        //        e.Graphics.DrawLine(new Pen(strColor, 2), new Point(55, 31), new Point(80, 31));
        //        e.Graphics.DrawLine(new Pen(strColor, 2), new Point(55, 31), new Point(66, 22));
        //        e.Graphics.DrawLine(new Pen(strColor, 2), new Point(55, 31), new Point(66, 39));
        //        return;
        //    }
        //    else if (numPaneltemp == "·")
        //    {
        //        numPanelFont = new Font(UserClass.fontName, 36);
        //    }
        //    else
        //    {
        //        numPanelFont = new Font(UserClass.fontName, 22);
        //    }
        //    SizeF sizeF = e.Graphics.MeasureString(numPaneltemp, numPanelFont);
        //    string strLine = String.Format(numPaneltemp);
        //    PointF strPoint = new PointF((133 - sizeF.Width) / 2, (62 - sizeF.Height) / 2);
        //    e.Graphics.DrawString(strLine, numPanelFont, new SolidBrush(strColor), strPoint);
        //}

        //private void NumPanel_MouseUp(object sender, MouseEventArgs e)
        //{
        //    Application.DoEvents();
        //    Panel p = (Panel)sender;
        //    Keys getType = Keys.B;
        //    switch (p.Name)
        //    {
        //        case "10": getType = Keys.Back; break;
        //        case "11": getType = Keys.D0; break;
        //        case "12": getType = Keys.Decimal; break;
        //        case "13": return;
        //        default: getType = (Keys)Enum.Parse(typeof(Keys), "D" + p.Name); break;
        //    }
        //    NativeMethods.keybd_event((byte)getType, 0, 0, 0);
        //    NativeMethods.keybd_event((byte)getType, 0, 2, 0); 
        //}

        //private void NumPanel_MouseLeave(object sender, EventArgs e)
        //{

        //}
        #endregion
    }
}
