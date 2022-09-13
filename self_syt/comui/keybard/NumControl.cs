using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace self_syt
{
    public partial class NumControl : Panel
    {
        Rectangle region = new Rectangle(0, 0, 0, 0);
        Color colorString = Color.FromArgb(77, 77, 77);
        public NumControl()
        {
            BackColor = BaseColor.color_White;
            OnSizeChanged(null);
        }

        private string sShowtext;
        [Category("显示属性"), Description("按钮显示的数值")]
        public string sShowText
        {
            get { return sShowtext; }
            set { sShowtext = value; }
        }

        private Keys key = Keys.None;
        [Category("按钮值属性"), Description("当前按钮代表的键")]
        public Keys NumKey
        {
            set
            {
                key = value;
            }
            get
            {
                return key;
            }
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            region = new Rectangle(0, 0, ClientRectangle.Width, ClientRectangle.Height);
            base.OnSizeChanged(e);
        }
        protected override void OnPaint(PaintEventArgs pevent)
        {
            pevent.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            ControlPaint.DrawBorder(pevent.Graphics, ClientRectangle,
                BaseColor.line, 1, ButtonBorderStyle.Solid,
                BaseColor.line, 1, ButtonBorderStyle.Solid,
                BaseColor.line, 0, ButtonBorderStyle.Solid,
                BaseColor.line, 0, ButtonBorderStyle.Solid
            );
            if (sShowtext == "收款" || sShowtext == "=")
            {
                BackColor = BaseColor.color_Red;
                pevent.Graphics.DrawString(sShowtext, new Font("微软雅黑", 12), new SolidBrush(BaseColor.color_White), ClientRectangle, BaseValue.drawFormatTitle);
            }
            else
            {
                if (sShowtext == "删除" || sShowtext == "清空")
                {
                    pevent.Graphics.DrawString(sShowtext, new Font("微软雅黑", 12), new SolidBrush(Color.Black), ClientRectangle, BaseValue.drawFormatTitle);
                }
                else
                {
                    pevent.Graphics.DrawString(sShowtext, new Font("微软雅黑", 14), new SolidBrush(Color.Black), ClientRectangle, BaseValue.drawFormatTitle);
                }
            }
            base.OnPaint(pevent);
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            BackColor = BaseColor.color_Red;
            base.OnMouseDown(e);
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            BackColor = BaseColor.color_White;
            Keys getType = key;
            untils.UmouseUntils.keybd_event((byte)getType, 0, 0, 0);
            untils.UmouseUntils.keybd_event((byte)getType, 0, 2, 0);
            base.OnMouseUp(e);
        }
    }

}
