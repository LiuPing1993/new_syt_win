using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace qgj
{
    public partial class dicountselectControl : UserControl
    {
        string sSelectDiscountTitle = "选择会员/优惠";
        string sShowMemberName = "";
        string sShowMemberNum = "";

        Rectangle rectDelete = new Rectangle(294, 14, 12, 12);
        public TextBox tbxInsert = new TextBox();
        bool IsInsert = false;
        bool IsHasInfo = false;
        public dicountselectControl()
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);

            InitializeComponent();

            BackColor = Defcolor.MainBackColor;

            tbxInsert.BorderStyle = System.Windows.Forms.BorderStyle.None;
            tbxInsert.ImeMode = System.Windows.Forms.ImeMode.Off;
            tbxInsert.SetBounds(35, 8, 250, 30);
            tbxInsert.TextAlign = HorizontalAlignment.Center;
            tbxInsert.Font = new Font(UserClass.fontName, 14);
            tbxInsert.BackColor = Defcolor.DiscountColor;
            tbxInsert.ForeColor = Defcolor.FontLiteGrayColor;
            tbxInsert.KeyPress += new KeyPressEventHandler(tbxInsert_insert);
            tbxInsert.MouseUp += new MouseEventHandler(tbxInsert_MouseUp);
            tbxInsert.Leave += new EventHandler(tbxInsert_Leave);
            tbxInsert.VisibleChanged += new EventHandler(tbxInsert_visible);
            tbxInsert.KeyDown += new KeyEventHandler(tbxInsert_down);
            tbxInsert.Leave += new EventHandler(tbxInsert_leave);
            Controls.Add(tbxInsert);
            tbxInsert.Visible = false;
        }

        private void dicountselectControl_Paint(object sender, PaintEventArgs e)
        {
            PublicMethods.FillRoundRectangle(e.ClipRectangle, e.Graphics, 10, Defcolor.DiscountColor);

            if(IsHasInfo)
            {
                Font _Font1 = new Font(UserClass.fontName, 11);
                Font _Font2 = new Font(UserClass.fontName, 16);

                string _Line1 = String.Format(sShowMemberName);
                string _Line2 = String.Format(sShowMemberNum);

                SizeF _sizeF1 = e.Graphics.MeasureString(_Line1, _Font1);
                SizeF _sizeF2 = e.Graphics.MeasureString(_Line2, _Font2);

                float _x1 = (320 - _sizeF1.Width - _sizeF2.Width - 10) / 2;
                float _x2 = _x1 + _sizeF1.Width + 10;

                float _y1 = (40 - _sizeF1.Height) / 2;
                float _y2 = (40 - _sizeF2.Height) / 2;

                PointF _lefttopPoint1 = new PointF(_x1, _y1);
                PointF _lefttopPoint2 = new PointF(_x2, _y2);

                e.Graphics.DrawString(_Line1, _Font1, new SolidBrush(Defcolor.FontLiteGrayColor), _lefttopPoint1);
                e.Graphics.DrawString(_Line2, _Font2, new SolidBrush(Defcolor.FontLiteGrayColor), _lefttopPoint2);

                e.Graphics.DrawLine(new Pen(new SolidBrush(Defcolor.FontLiteGrayColor), 2), new Point(295, 14), new Point(307, 26));
                e.Graphics.DrawLine(new Pen(new SolidBrush(Defcolor.FontLiteGrayColor), 2), new Point(295, 26), new Point(307, 14));

            }
            else
            {
                Font _Font = new Font(UserClass.fontName, 12);
                SizeF _sizeF = e.Graphics.MeasureString(sSelectDiscountTitle, _Font);
                string _strLine = String.Format(sSelectDiscountTitle);
                PointF _strPoint = new PointF((e.ClipRectangle.Width - _sizeF.Width) / 2, (e.ClipRectangle.Height - _sizeF.Height) / 2);
                e.Graphics.DrawString(_strLine, _Font, new SolidBrush(Defcolor.FontLiteGrayColor), _strPoint);
            }
        }

        private void dicountselectControl_MouseUp(object sender, MouseEventArgs e)
        {
            ((gatherControl)Parent).fnFocusIsChange("discountselect");
            if (IsHasInfo) 
            {
                if(rectDelete.Contains(e.Location))
                {
                    fnClearDiscountSelect();
                    return;
                }
                else if(IsInsert)
                {
                    return;
                }
                else
                {
                    fnGetmemberInfo();
                    return;
                }
            }
            else
            {
                if(IsInsert)
                {
                    return;
                }
                else
                {
                    tbxInsert.Visible = true;
                    tbxInsert.Focus();
                }
            }
        }
        private void tbxInsert_MouseUp(object sender, MouseEventArgs e)
        {
            ((gatherControl)Parent).fnFocusIsChange("discountselect");
        }
        private void tbxInsert_insert(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                fnGetmemberInfo();
                e.Handled = true;
                return;
            }
            if (!(Char.IsNumber(e.KeyChar)) && !(e.KeyChar == (char)8))
            {
                e.Handled = true;
                return;
            }
        }
        private void tbxInsert_down(object sender, KeyEventArgs  e)
        {
            if(tbxInsert.Text == "请输入手机号/会员卡号/券编号")
            {
                tbxInsert.Text = "";
                tbxInsert.Font = new Font(UserClass.fontName, 14);
                tbxInsert.ForeColor = Defcolor.FontLiteGrayColor;
            }
            if (tbxInsert.Text.Length == 1 && e.KeyCode == Keys.Back)
            {
                tbxInsert.Font = new Font(UserClass.fontName, 12);
                tbxInsert.Text = "请输入手机号/会员卡号/券编号 ";
                tbxInsert.ForeColor = Color.Gray;
                tbxInsert.SelectionStart = tbxInsert.Text.Length;
                Refresh();
            }

        }
        private void tbxInsert_Leave(object sender, EventArgs e)
        {
            if(tbxInsert.Text.Trim() == "")
            {
                tbxInsert.Visible = false;
                Refresh();
            }
        }
        public void fnGetmemberInfo()
        {
            if (PublicMethods.hasChinese(tbxInsert.Text.Trim()))
            {
                return;
            }
            try
            {
                if (((gatherControl)Parent).fnMemberCouponInfo(tbxInsert.Text.Trim(), ref sShowMemberName, ref sShowMemberNum))
                {
                    ((gatherControl)Parent).fnSetFocus();
                    tbxInsert.Visible = false;
                    IsHasInfo = true;
                    gatherMouse.hasmember = true;
                    Refresh();
                }
                else
                {
                    IsHasInfo = false;
                    ((gatherControl)Parent).fnClearMemberCouponInfo();
                    Refresh();
                }
            }
            catch
            {
                fnClearDiscountSelect();
            }
        }
        private void tbxInsert_visible(object sender, EventArgs e)
        {
            if (tbxInsert.Visible == true)
            {
                tbxInsert.Font = new Font(UserClass.fontName, 12);
                tbxInsert.Text = "请输入手机号/会员卡号/券编号";
                tbxInsert.ForeColor = Color.Gray;
                tbxInsert.SelectionStart = tbxInsert.Text.Length;
                Refresh();
            }
            else
            {
                tbxInsert.Font = new Font(UserClass.fontName, 14);
                tbxInsert.ForeColor = Defcolor.FontLiteGrayColor;
            }
        }
        public void tbxInsert_leave(object sender, EventArgs e)
        {
            if (tbxInsert.Text == "请输入手机号/会员卡号/券编号" || tbxInsert.Text == "")
            {
                tbxInsert.Hide();
                Refresh();
            }
        }
        public void fnClearDiscountSelect()
        {
            ((gatherControl)Parent).fnClearMemberCouponInfo();
            tbxInsert.Text = "";
            tbxInsert.Visible = false;
            sShowMemberName = "";
            sShowMemberNum = "";
            IsHasInfo = false;
            IsInsert = false;
            Refresh();
        }

    }
}
