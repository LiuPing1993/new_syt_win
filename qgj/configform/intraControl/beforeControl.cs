using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace qgj
{
    public partial class beforeControl : UserControl
    {
        loadconfigClass lcc = new loadconfigClass("before_key");
        loadconfigClass hotkey = new loadconfigClass("hotkey");
        loadconfigClass print_hotkey = new loadconfigClass("print_hotkey");
        string sKeyValue = "";
        bool IsSelect = false;
        Rectangle rectHotKeyShow;

        public beforeControl()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            InitializeComponent();
        }

        private void beforeControl_Load(object sender, EventArgs e)
        {
            rectHotKeyShow = new Rectangle(0, 0, Width, Height);
            sKeyValue = lcc.readfromConfig();
        }

        private void beforeControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (rectHotKeyShow.Contains(e.Location))
            {
                IsSelect = IsSelect ? false : true;
                Refresh();
            }
            else
            {
                beforeControl_Leave(null, null);
            }
        }
        public bool fnSetKeyValue(string _keyvalue)
        {
            if(_keyvalue == hotkey.readfromConfig())
            {
                return false;
            }
            else if(_keyvalue == print_hotkey.readfromConfig())
            {
                return false;
            }
            else
            {
                lcc.writetoConfig(_keyvalue);
                return true;
            }
        }
        private void beforeControl_Paint(object sender, PaintEventArgs e)
        {
            if (IsSelect)
            {
                PublicMethods.FrameRoundRectangle(rectHotKeyShow, e.Graphics, 7, Defcolor.MainRadColor);
            }
            else
            {
                PublicMethods.FrameRoundRectangle(rectHotKeyShow, e.Graphics, 7, Defcolor.MainGrayLineColor);
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
                        sKeyValue = fnSetKeyValue(_getKey) ? _getKey : sKeyValue;
                        Refresh();
                        return true;
                    }
                    else
                    {
                        string temp = "Ctrl + " + _getKey.Replace(" ", "").Replace("Control", "").Replace(",", "");
                        sKeyValue = fnSetKeyValue(temp) ? temp : sKeyValue;
                        Refresh();
                        return true;
                    }
                }
            }
        }

        private void beforeControl_Leave(object sender, EventArgs e)
        {
            if (IsSelect)
            {
                IsSelect = false;
                Refresh();
            }
            else
            {
                return;
            }
        }
    }
}
