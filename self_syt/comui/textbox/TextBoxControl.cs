using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace self_syt.comui.textbox
{
    public partial class TextBoxControl : UserControl
    {
        public Color backColor = Color.Gray;

        public TextBox textBox;
        public Label label;

        public string labelTips = "请输入用户名";
        public string title = "";

        Rectangle rect;
        Rectangle rect_title;

        public bool isPassWord = false;

        public bool hasBorder = false;

        public string textName = "";

        public TextBoxControl()
        {
            InitializeComponent();
            BackColor = BaseColor.color_White;

        }

        private void TextBoxControl_Load(object sender, EventArgs e)
        {
            textBox = new TextBox();
            textBox.Size = new Size(ClientRectangle.Width - 40, ClientRectangle.Height - 20);
            textBox.Location = new Point((ClientRectangle.Width - textBox.Width) / 2, (ClientRectangle.Height - textBox.Height) / 2);
            textBox.TextAlign = HorizontalAlignment.Center;
            textBox.BorderStyle = BorderStyle.None;
            textBox.LostFocus += textBox_LostFocus;
            textBox.GotFocus += textBox_GotFocus;
            textBox.Font = new System.Drawing.Font(BaseValue.baseFont, 15);

            if (isPassWord)
            {
                textBox.PasswordChar = '*';
            }
            Controls.Add(textBox);

            label = new Label();
            label.AutoSize = false;
            label.Text = labelTips;
            label.Dock = DockStyle.Fill;
            label.TextAlign = ContentAlignment.MiddleCenter;
            label.Font = new System.Drawing.Font(BaseValue.baseFont, 10);
            label.ForeColor = BaseColor.deep_Gray;
            label.MouseUp += label_MouseUp;
            textBox.Controls.Add(label);
            if (textName != "")
            {
                label.Visible = false;
            }


            rect_title = new Rectangle(0, 2, 80, ClientRectangle.Height + 2);

            rect = new Rectangle(this.textBox.Location.X - 8, this.textBox.Location.Y - 8, this.textBox.Width + 16, this.textBox.Height + 16);

            if (textName != "")
            {
                textBox.Text = textName;
            }
        }

        void textBox_GotFocus(object sender, EventArgs e)
        {
            if (!label.Visible)
            {
                hasBorder = true;
                Refresh();
            }
        }

        void textBox_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.textBox.Text))
            {
                this.label.Visible = true;
            }
            hasBorder = false;
            Refresh();

        }

        void label_MouseUp(object sender, MouseEventArgs e)
        {
            this.label.Visible = false;
            this.textBox.Focus();
        }

        private void TextBoxControl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            if (hasBorder)
            {
                untils.UcomUntils.FrameRoundRectangle(rect, e.Graphics, 35, BaseColor.color_Red);
            }
            else
            {
                untils.UcomUntils.FrameRoundRectangle(rect, e.Graphics, 35, BaseColor.line);
            }
        }

        public string GetTextValue()
        {
            return this.textBox.Text;
        }

        public void SetValue(string value)
        {
            this.label.Visible = false;

            this.textBox.Text = value;
        }
    }
}
