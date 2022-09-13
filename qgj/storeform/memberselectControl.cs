using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace qgj
{
    public partial class memberselectControl : UserControl
    {
        string sDiscountTitle = "选择会员";
        string sShowMemberName = "";
        string sShowMemberNum = "";

        Rectangle rectDelete = new Rectangle(294, 14, 12, 12);
        public TextBox tbxInsert = new TextBox();
        bool IsInsert = false;
        bool IsHasInfo = false;

        public memberselectControl()
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
            tbxInsert.KeyPress += new KeyPressEventHandler(insertTextBox_insert);
            tbxInsert.MouseUp += new MouseEventHandler(insertTextBox_MouseUp);
            tbxInsert.Leave += new EventHandler(insertTextBox_Leave);
            tbxInsert.KeyDown += new KeyEventHandler(inserttext_down);
            tbxInsert.VisibleChanged += new EventHandler(inserttext_visible);
            Controls.Add(tbxInsert);
            tbxInsert.Visible = false;
        }
        private void insertTextBox_MouseUp(object sender, MouseEventArgs e)
        {
            ((storeControl)Parent).fnFocusIsChange("membertselect");
        }
        private void insertTextBox_insert(object sender, KeyPressEventArgs e)
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
        private void inserttext_down(object sender, KeyEventArgs e)
        {
            if (tbxInsert.Text == "请输入手机号/会员卡号")
            {
                tbxInsert.Text = "";
                tbxInsert.Font = new Font(UserClass.fontName, 14);
                tbxInsert.ForeColor = Defcolor.FontLiteGrayColor;
            }
            if (tbxInsert.Text.Length == 1 && e.KeyCode == Keys.Back)
            {
                tbxInsert.Font = new Font(UserClass.fontName, 12);
                tbxInsert.Text = "请输入手机号/会员卡号 ";
                tbxInsert.ForeColor = Color.Gray;
                tbxInsert.SelectionStart = tbxInsert.Text.Length;
                Refresh();
            }

        }
        private void inserttext_visible(object sender, EventArgs e)
        {
            if (tbxInsert.Visible == true)
            {
                tbxInsert.Font = new Font(UserClass.fontName, 12);
                tbxInsert.Text = "请输入手机号/会员卡号";
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
        private void insertTextBox_Leave(object sender, EventArgs e)
        {
            if (tbxInsert.Text == "请输入手机号/会员卡号" || tbxInsert.Text == "")
            {
                tbxInsert.Hide();
                Refresh();
            }
        }
        private void memberselectControl_MouseUp(object sender, MouseEventArgs e)
        {
            ((storeControl)Parent).fnFocusIsChange("membertselect");
            if (IsHasInfo)
            {
                if (rectDelete.Contains(e.Location))
                {
                    fnClearMemberSelect();
                    return;
                }
                else if (IsInsert)
                {
                    return;
                }
                else
                {
                    return;
                }
            }
            else
            {
                if (IsInsert)
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
        public void fnGetmemberInfo()
        {
            try
            {
                if (IsHasInfo)
                {
                    return;
                }
                if (((storeControl)Parent).fnMemberStoreInfo(tbxInsert.Text.Trim(), ref sShowMemberName, ref sShowMemberNum))
                {
                    
                    tbxInsert.Visible = false;
                    IsHasInfo = true;
                    Refresh();
                }
                else
                {
                    //insertTextBox.Visible = false;
                    //insertTextBox.Text = "";
                    IsHasInfo = false;
                    ((storeControl)Parent).fnClearMemberStoreInfo();
                    Refresh();
                }
            }
            catch
            {
                fnClearMemberSelect();
            }
        }
        public void fnClearMemberSelect()
        {
            ((storeControl)Parent).fnClearMemberStoreInfo();
            UserClass.storeInfoC.code = "";
            tbxInsert.Text = "";
            tbxInsert.Visible = false;
            sShowMemberName = "";
            sShowMemberNum = "";
            IsHasInfo = false;
            IsInsert = false;
            Refresh();
        }
        private void memberselectControl_Paint(object sender, PaintEventArgs e)
        {
            PublicMethods.FillRoundRectangle(e.ClipRectangle, e.Graphics, 10, Defcolor.DiscountColor);

            if (IsHasInfo)
            {
                Font Font1 = new Font(UserClass.fontName, 11);
                Font Font2 = new Font(UserClass.fontName, 16);

                string Line1 = String.Format(sShowMemberName);
                string Line2 = String.Format(sShowMemberNum);

                SizeF sizeF1 = e.Graphics.MeasureString(Line1, Font1);
                SizeF sizeF2 = e.Graphics.MeasureString(Line2, Font2);

                float x1 = (320 - sizeF1.Width - sizeF2.Width - 10) / 2;
                float x2 = x1 + sizeF1.Width + 10;

                float y1 = (40 - sizeF1.Height) / 2;
                float y2 = (40 - sizeF2.Height) / 2;

                PointF lefttopPoint1 = new PointF(x1, y1);
                PointF lefttopPoint2 = new PointF(x2, y2);

                e.Graphics.DrawString(Line1, Font1, new SolidBrush(Defcolor.FontLiteGrayColor), lefttopPoint1);
                e.Graphics.DrawString(Line2, Font2, new SolidBrush(Defcolor.FontLiteGrayColor), lefttopPoint2);

                e.Graphics.DrawLine(new Pen(new SolidBrush(Defcolor.FontLiteGrayColor), 2), new Point(295, 14), new Point(307, 26));
                e.Graphics.DrawLine(new Pen(new SolidBrush(Defcolor.FontLiteGrayColor), 2), new Point(295, 26), new Point(307, 14));

            }
            else
            {
                Font Font = new Font(UserClass.fontName, 12);
                SizeF sizeF = e.Graphics.MeasureString(sDiscountTitle, Font);
                string strLine = String.Format(sDiscountTitle);
                PointF strPoint = new PointF((e.ClipRectangle.Width - sizeF.Width) / 2, (e.ClipRectangle.Height - sizeF.Height) / 2);
                e.Graphics.DrawString(strLine, Font, new SolidBrush(Defcolor.FontLiteGrayColor), strPoint);
            }
        }
    }
}
