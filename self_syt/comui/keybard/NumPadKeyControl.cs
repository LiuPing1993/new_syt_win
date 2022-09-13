using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace self_syt.comui.keybard
{
    public partial class NumPadKeyControl : UserControl
    {
        Color colorString = Color.FromArgb(77, 77, 77);

        public NumControl[,] nnumC;
        public List<NumControl> numList;

        Keys[,] keys = { 
                            { Keys.D1, Keys.D2, Keys.D3,Keys.Back },
                            { Keys.D4, Keys.D5, Keys.D6,Keys.Delete }, 
                            { Keys.D7, Keys.D8, Keys.D9,Keys.Enter }, 
                            { Keys.Decimal, Keys.D0, Keys.Add,Keys.Enter }
                       };
        string[,] sInfo = {
                             {"1","2","3","删除"},
                             {"4","5","6","清空"},
                             {"7","8","9","收款"},
                             {".","0","+",""},
                         };

        int wid = 100;
        int hei = 60;
        public NumPadKeyControl()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            InitializeComponent();        
            Dock = DockStyle.Fill;
            BackColor = Color.FromArgb(224, 224, 224);
            numList = new List<NumControl>();
        }

        private void NumPadKeyControl_Load(object sender, EventArgs e)
        {
            NumPadKeyControl_SizeChanged(null, null);
            nnumC = new NumControl[4, 4];
            for (int _i = 0; _i < 4; _i++)
            {
                for (int _j = 0; _j < 4; _j++)
                {
                    nnumC[_i, _j] = new NumControl();
                    nnumC[_i, _j].sShowText = sInfo[_i, _j];
                    nnumC[_i, _j].NumKey = keys[_i, _j];
                    nnumC[_i, _j].Size = new System.Drawing.Size(wid, hei);
                    if (_i == 2 && _j == 3)
                    {
                        Console.WriteLine(sInfo[_i, _j]);
                        nnumC[_i, _j].Size = new System.Drawing.Size(wid, hei * 2);
                    }
                    nnumC[_i, _j].Location = new Point(_j * wid, _i * hei);
                    if (_i == 3 && _j == 3)
                    {
                        nnumC[_i, _j].Visible = false;
                    }
                    Controls.Add(nnumC[_i, _j]);
                    numList.Add(nnumC[_i,_j]);
                }
            }
        }

        private void NumPadKeyControl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        }

        private void NumPadKeyControl_SizeChanged(object sender, EventArgs e)
        {
            wid = ClientRectangle.Width / 4;
            hei = ClientRectangle.Height / 4;
        }
    }
}
