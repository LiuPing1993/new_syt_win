using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace qgj
{
    public partial class HotKeySetControl : UserControl
    {
        Label lblInfo = new Label();
        string sKeyValue = "单击设置快捷键";
        bool IsSelect = false;
        Rectangle rectHotKeyShow = new Rectangle(5, 5, 120, 30);
        hotkeyClass HotKeyC = new hotkeyClass();

        public int type = 1;

        public HotKeySetControl()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            InitializeComponent();

            BackColor = Defcolor.MainBackColor;

            lblInfo.BackColor = Defcolor.MainBackColor;
            lblInfo.Text = "调出收款软件热键，直接输入热键组合，或单一热键，例如Ctrl+A或F8";
            lblInfo.Font = new Font(UserClass.fontName, 9);
            lblInfo.ForeColor = Defcolor.FontGrayColor;
            lblInfo.SetBounds(5, 40, 400, 20);
            Controls.Add(lblInfo);
        }

        public HotKeySetControl(int value)
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            InitializeComponent();

            BackColor = Defcolor.MainBackColor;

            type = value;
            HotKeyC.type = value;

            lblInfo.BackColor = Defcolor.MainBackColor;
            lblInfo.Text = "快捷打印当日汇总热键，直接输入热键组合，或单一热键，例如Ctrl+A或F8";
            lblInfo.Font = new Font(UserClass.fontName, 9);
            lblInfo.ForeColor = Defcolor.FontGrayColor;
            lblInfo.SetBounds(5, 40, 400, 20);
            Controls.Add(lblInfo);
        }

        private void HotKeySetControl_Load(object sender, EventArgs e)
        {
            string config_name = "hotkey";
            if (type == 2)
            {
                lblInfo.Text = "快捷打印当日汇总热键，直接输入热键组合，或单一热键，例如Ctrl+A或F8";
                config_name = "print_hotkey";
            }
            loadconfigClass _lcc = new loadconfigClass(config_name);
            string _keyvaluc = _lcc.readfromConfig();
            if (_keyvaluc != "")
            {
                sKeyValue = _keyvaluc;
            }
            Refresh();
        }
        private void HotKeySetControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (rectHotKeyShow.Contains(e.Location))
            {
                IsSelect = true;
                Refresh();
            }
            else
            {
                HotKeySetControl_Leave(null, null);
            }
        }
        public void HotKeySetControl_Leave(object sender, EventArgs e)
        {
            if (IsSelect)
            {
                if (!HotKeyC.fnSetHotKeyValue(sKeyValue))
                {
                    MessageBox.Show("设置失败");
                }
                else
                {
                    if(this.type == 1)
                    {
                        HotKeyC.fnSetHotKey(sKeyValue, 100);
                    }
                    else
                    {
                        HotKeyC.fnSetHotKey(sKeyValue, 101);
                    }
                }
                IsSelect = false;
                Refresh();
            }
            else
            {
                return;
            }
        }
        private void HotKeySetControl_Paint(object sender, PaintEventArgs e)
        {
            if (IsSelect)
            {
                PublicMethods.FrameRoundRectangle(new Rectangle(5, 5, 125, 35), e.Graphics, 7, Defcolor.MainRadColor);
            }
            else
            {
                PublicMethods.FrameRoundRectangle(new Rectangle(5, 5, 125, 35), e.Graphics, 7, Defcolor.MainGrayLineColor);
            }
            StringFormat _drawFormat = new StringFormat();
            _drawFormat.Alignment = StringAlignment.Center;
            _drawFormat.LineAlignment = StringAlignment.Center;

            Font _myFont = new Font(UserClass.fontName, 9);
            string _strLine = string.Format(sKeyValue);
            e.Graphics.DrawString(_strLine, _myFont, new SolidBrush(Defcolor.FontLiteGrayColor), rectHotKeyShow, _drawFormat);
        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            string _getKey = keyData.ToString();
            if (!IsSelect)
            {
                return false;
            }
            if (_getKey.IndexOf("ShiftKey") != -1 || _getKey.IndexOf("ControlKey") != -1 || _getKey.IndexOf("Menu") != -1)
            {
                //排除单独shift、ctrl、特殊功能键等
                return true;
            }
            else
            {
                if (_getKey.IndexOf("Shift") != -1 || _getKey.IndexOf("Alt") != -1)
                {
                    //排除shift、alt组合的快捷键
                    return true;
                }
                else
                {
                    if (_getKey.IndexOf("Control") == -1)
                    {
                        sKeyValue = _getKey;
                        Refresh();
                        return true;
                    }
                    else
                    {
                        sKeyValue = "Ctrl + " + _getKey.Replace(" ", "").Replace("Control", "").Replace(",", "");
                        Refresh();
                        return true;
                    }
                }
            }
        }
    }
}
