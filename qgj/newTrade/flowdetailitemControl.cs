using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace qgj
{
    public partial class flowdetailitemControl : UserControl
    {
        string title;
        string contant;
        bool first = false;
        public bool isFlowDetail = true;

        public Label orderdetail = new Label();

        public flowdetailitemControl(string title, string contant, bool first = false)
        {
            this.title = title;
            this.contant = contant;
            this.first = first;
            InitializeComponent();
            this.BackColor = Defcolor.MainBackColor;

        }

        private void flowdetailitemControl_Load(object sender, EventArgs e)
        {
            this.titleLabel.Text = title;
            this.contantLabel.Text = contant;
            if(title == "订单号" && isFlowDetail)
            {
                orderdetail.Text = "查看订单详情";
                orderdetail.Size = new Size(100, 20);
                orderdetail.BorderStyle = System.Windows.Forms.BorderStyle.None;
                orderdetail.Font = new Font(UserClass.fontName, 9);
                orderdetail.ForeColor = Defcolor.FontBlueColor;
                orderdetail.Location = new Point(contantLabel.Right + 50, 7);
                this.Controls.Add(orderdetail);
            }
        }
        private void flowdetailitemControl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            if (first)
            {
                ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid
            );
            }
            else
            {
                ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 0, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid
            );
            }
            
            e.Graphics.DrawLine(new Pen(Defcolor.MainGrayLineColor), new Point(170, 0), new Point(170, 30));
        }

        
    }
}
