using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace self_syt.mainform
{
    public partial class MainTopControl : UserControl
    {
        Rectangle rect_border;

        public PictureBox main_logo_pic;
        public PictureBox main_close;
        public MainTopControl()
        {
            InitializeComponent();
            BackColor = Color.Red;
            Dock = DockStyle.Fill;
        
        }

        public void Main_Close_MouseUp(object sender, MouseEventArgs e)
        {
            BaseModel.mainFormModel.CloseForm();
        }

        private void MainTopControl_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            untils.UcomUntils.FillRoundRectangle_four(rect_border, e.Graphics, 15, 15, 0, 0, BaseColor.mainColor);
        }

        private void MainTopControl_MouseDown(object sender, MouseEventArgs e)
        {
            BaseModel.mainFormModel.Frm_MouseDown(sender, e);
        }

        private void MainTopControl_MouseUp(object sender, MouseEventArgs e)
        {
            BaseModel.mainFormModel.Frm_MouseUp(sender, e);
        }

        private void MainTopControl_MouseMove(object sender, MouseEventArgs e)
        {
            BaseModel.mainFormModel.Frm_MouseMove(sender, e);
        }

        private void MainTopControl_SizeChanged(object sender, EventArgs e)
        {
           // rect_border = new Rectangle(0, 0, ClientRectangle.Width, ClientRectangle.Height);
        }

        private void MainTopControl_Load(object sender, EventArgs e)
        {
            rect_border = new Rectangle(0, 0, ClientRectangle.Width, ClientRectangle.Height);
            main_logo_pic = new PictureBox();
            main_logo_pic.Image = Properties.Resources.main_logo;
            main_logo_pic.BackColor = Color.Transparent;
            main_logo_pic.Size = new Size(40, 40);
            main_logo_pic.SizeMode = PictureBoxSizeMode.StretchImage;
            main_logo_pic.Location = new Point(30, (ClientRectangle.Height - main_logo_pic.Height) / 2);
            Controls.Add(main_logo_pic);

            main_close = new PictureBox();
            main_close.BackColor = Color.Transparent;
            main_close.MouseUp += new MouseEventHandler(Main_Close_MouseUp);
            main_close.Size = new Size(30, 35);
            main_close.SizeMode = PictureBoxSizeMode.StretchImage;
            main_close.Image = Properties.Resources.close;
            main_close.Location = new Point(ClientRectangle.Width - main_close.Width - 20, (ClientRectangle.Height - main_close.Height) / 2);
            Controls.Add(main_close);
        }
    }
}
