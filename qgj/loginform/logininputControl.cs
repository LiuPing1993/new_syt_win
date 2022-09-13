using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace qgj
{
    public partial class logininputControl : UserControl
    {
        StringBuilder sbValue = new StringBuilder();
        public TextBox tbxValue = new TextBox();
        InType type;
        public logininputControl(InType _type)
        {
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.DoubleBuffer, true);
            InitializeComponent();
            BackColor = Defcolor.MainBackColor;

            tbxValue.Font = new Font(UserClass.fontName, 12);
            tbxValue.ForeColor = Defcolor.FontGrayColor;
            if (_type == InType.password)
            {
                tbxValue.SetBounds(5, 5, ClientRectangle.Width - 50, ClientRectangle.Height - 10);
            }
            else
            {
                tbxValue.SetBounds(5, 5, ClientRectangle.Width - 10, ClientRectangle.Height - 10);
            }
            tbxValue.BorderStyle = System.Windows.Forms.BorderStyle.None;
            tbxValue.TabIndex = 0;
            tbxValue.BackColor = Defcolor.MainBackColor;
            tbxValue.KeyPress += new KeyPressEventHandler(valueTextBox_KeyPress);
            tbxValue.Enter += new EventHandler(textBox_Enter);
            tbxValue.MouseDown += new MouseEventHandler(textBox_MouseDown);
            tbxValue.MouseMove += new MouseEventHandler(textBox_MouseMove);

            Controls.Add(tbxValue);
            type = _type;
        }
        public string fnGetValue()
        {
            if (type == InType.password)
            {
                return sbValue.ToString();
            }
            return tbxValue.Text.ToString();
        }
        public void fnSetValue(string _insert)
        {
            sbValue = new StringBuilder();
            tbxValue.Text = "";
            sbValue.Append(_insert);
            if (type == InType.account)
            {
                tbxValue.Text = sbValue.ToString();
            }
            else
            {
                int _length = _insert.Length;
                for (int i = 0; i < _length; i++)
                {
                    tbxValue.Text += "*";
                }
            }
        }
        private void accountinsertControl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.DrawLine(new Pen(Defcolor.MainGrayLineColor, 3), new Point(5, e.ClipRectangle.Height), new Point(e.ClipRectangle.Width - 5, e.ClipRectangle.Height));
            if (type == InType.password && tbxValue.TextLength > 0)
            {
                e.Graphics.DrawEllipse(new Pen(Color.FromArgb(180, 180, 180), 2), new Rectangle(255, 5, 16, 16)); ;
                e.Graphics.DrawLine(new Pen(Color.FromArgb(180, 180, 180), 2), new Point(260, 10), new Point(266, 16));
                e.Graphics.DrawLine(new Pen(Color.FromArgb(180, 180, 180), 2), new Point(260, 16), new Point(266, 10));
            }            
        }
        private void valueTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (type == InType.password)
            {
                if (e.KeyChar == 8 )
                {
                    if (tbxValue.Text.Length > 0)
                    {
                        tbxValue.Text = tbxValue.Text.Remove(tbxValue.Text.Length - 1, 1);
                        tbxValue.Select(tbxValue.Text.Length, 0);
                        sbValue.Remove(sbValue.Length - 1, 1);
                        Console.WriteLine("sbvalue:" + sbValue.ToString());
                    }
                }
                else if (e.KeyChar == 13 || e.KeyChar == 27)
                {
                    //e.Handled = true;
                }
                else
                {
                    tbxValue.Text += "*";
                    tbxValue.Select(tbxValue.Text.Length, 0);
                    sbValue.Append(e.KeyChar);
                }
                Refresh();
                e.Handled = true;
            }
        }
        private void textBox_Enter(object sender, EventArgs e)
        {
            tbxValue.Select(tbxValue.Text.Length, 0);
            keyboardClass.showKeyBoard();
        }
        private void textBox_MouseDown(object sender, MouseEventArgs e)
        {
            tbxValue.Select(tbxValue.TextLength, 0);
        }
        private void textBox_MouseMove(object sender, MouseEventArgs e)
        {
            tbxValue.Select(tbxValue.TextLength, 0);
        }

        private void logininputControl_MouseUp(object sender, MouseEventArgs e)
        {
            Rectangle rect = new Rectangle(255, 5, 16, 16);
            if (rect.Contains(e.Location) && tbxValue.TextLength != 0)
            {
                tbxValue.Clear();
                sbValue = new StringBuilder();
                Refresh();
            }
        }
    }
}
