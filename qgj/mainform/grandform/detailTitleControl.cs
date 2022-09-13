using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace qgj
{
    public partial class detailTitleControl : UserControl
    {
        int iTabNum = 0;
        public detailControl detailC;
        public storeDetailControl storeC;
        public flowListControl flowlistC;
        public orderListControl orderlistC;
        public couponUseListControl couponuselistC;
        public koubeiUseListControl koubeiuselistC;

        Rectangle rectflowDetail = new Rectangle(0, 0, 100, 34);
        Rectangle rectorderdDetail = new Rectangle(101, 0, 100, 34);
        Rectangle rectStoreDetail = new Rectangle(201, 0, 100, 34);
        Rectangle rectCouponDetail = new Rectangle(301, 0, 100, 34);
        Rectangle rectKoubeiDetail = new Rectangle(401, 0, 100, 34);
        public detailTitleControl()
        {
            //detailC = new detailControl();
            //detailC.Location = new Point(0, 35);
            //Controls.Add(detailC);

            storeC = new storeDetailControl();
            storeC.Location = new Point(0, 35);
            storeC.Visible = false;
            Controls.Add(storeC);

            couponuselistC = new couponUseListControl();
            couponuselistC.Location = new Point(0, 35);
            couponuselistC.Visible = false;
            Controls.Add(couponuselistC);

            koubeiuselistC = new koubeiUseListControl();
            koubeiuselistC.Location = new Point(0, 35);
            koubeiuselistC.Visible = false;
            Controls.Add(koubeiuselistC);

            orderlistC = new orderListControl();
            orderlistC.Location = new Point(0, 35);
            orderlistC.Visible = false;
            Controls.Add(orderlistC);

            flowlistC = new flowListControl();
            flowlistC.Location = new Point(0, 35);
            Controls.Add(flowlistC);

            

            User.drawFormatTitle.Alignment = StringAlignment.Center;
            User.drawFormatTitle.LineAlignment = StringAlignment.Center;

            InitializeComponent();
            BackColor = ColorTranslator.FromHtml("#fffefe");
        }

        private void detailTitleControl_Load(object sender, EventArgs e)
        {

        }

        private void detailTitleControl_Paint(object sender, PaintEventArgs e)
        {
            ControlPaint.DrawBorder(e.Graphics, e.ClipRectangle,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 0, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 1, ButtonBorderStyle.Solid,
                Defcolor.MainGrayLineColor, 0, ButtonBorderStyle.Solid
            );
            Font _Font = new Font(UserClass.fontName, 10);
            string _strLine = String.Format("财务流水");
            if (iTabNum == 0)
            {
                e.Graphics.DrawString(_strLine, _Font, new SolidBrush(Color.Black), rectflowDetail, User.drawFormatTitle);
                _strLine = String.Format("交易订单");
                e.Graphics.DrawString(_strLine, _Font, new SolidBrush(Color.Gray), rectorderdDetail, User.drawFormatTitle);
                _strLine = String.Format("储值明细");
                e.Graphics.DrawString(_strLine, _Font, new SolidBrush(Color.Gray), rectStoreDetail, User.drawFormatTitle);
                _strLine = String.Format("优惠券");
                e.Graphics.DrawString(_strLine, _Font, new SolidBrush(Color.Gray), rectCouponDetail, User.drawFormatTitle);
                _strLine = String.Format("口碑券");
                e.Graphics.DrawString(_strLine, _Font, new SolidBrush(Color.Gray), rectKoubeiDetail, User.drawFormatTitle);
            }
            else if (iTabNum == 1)
            {
                e.Graphics.DrawString(_strLine, _Font, new SolidBrush(Color.Gray), rectflowDetail, User.drawFormatTitle);
                _strLine = String.Format("交易订单");
                e.Graphics.DrawString(_strLine, _Font, new SolidBrush(Color.Black), rectorderdDetail, User.drawFormatTitle);
                _strLine = String.Format("储值明细");
                e.Graphics.DrawString(_strLine, _Font, new SolidBrush(Color.Gray), rectStoreDetail, User.drawFormatTitle);
                _strLine = String.Format("优惠券");
                e.Graphics.DrawString(_strLine, _Font, new SolidBrush(Color.Gray), rectCouponDetail, User.drawFormatTitle);
                _strLine = String.Format("口碑券");
                e.Graphics.DrawString(_strLine, _Font, new SolidBrush(Color.Gray), rectKoubeiDetail, User.drawFormatTitle);
            }
            else if (iTabNum == 2)
            {
                e.Graphics.DrawString(_strLine, _Font, new SolidBrush(Color.Gray), rectflowDetail, User.drawFormatTitle);
                _strLine = String.Format("交易订单");
                e.Graphics.DrawString(_strLine, _Font, new SolidBrush(Color.Gray), rectorderdDetail, User.drawFormatTitle);
                _strLine = String.Format("储值明细");
                e.Graphics.DrawString(_strLine, _Font, new SolidBrush(Color.Black), rectStoreDetail, User.drawFormatTitle);
                _strLine = String.Format("优惠券");
                e.Graphics.DrawString(_strLine, _Font, new SolidBrush(Color.Gray), rectCouponDetail, User.drawFormatTitle);
                _strLine = String.Format("口碑券");
                e.Graphics.DrawString(_strLine, _Font, new SolidBrush(Color.Gray), rectKoubeiDetail, User.drawFormatTitle);
            }
            else if (iTabNum == 3)
            {
                e.Graphics.DrawString(_strLine, _Font, new SolidBrush(Color.Gray), rectflowDetail, User.drawFormatTitle);
                _strLine = String.Format("交易订单");
                e.Graphics.DrawString(_strLine, _Font, new SolidBrush(Color.Gray), rectorderdDetail, User.drawFormatTitle);
                _strLine = String.Format("储值明细");
                e.Graphics.DrawString(_strLine, _Font, new SolidBrush(Color.Gray), rectStoreDetail, User.drawFormatTitle);
                _strLine = String.Format("优惠券");
                e.Graphics.DrawString(_strLine, _Font, new SolidBrush(Color.Black), rectCouponDetail, User.drawFormatTitle);
                _strLine = String.Format("口碑券");
                e.Graphics.DrawString(_strLine, _Font, new SolidBrush(Color.Gray), rectKoubeiDetail, User.drawFormatTitle);
            }
            else if (iTabNum == 4)
            {
                e.Graphics.DrawString(_strLine, _Font, new SolidBrush(Color.Gray), rectflowDetail, User.drawFormatTitle);
                _strLine = String.Format("交易订单");
                e.Graphics.DrawString(_strLine, _Font, new SolidBrush(Color.Gray), rectorderdDetail, User.drawFormatTitle);
                _strLine = String.Format("储值明细");
                e.Graphics.DrawString(_strLine, _Font, new SolidBrush(Color.Gray), rectStoreDetail, User.drawFormatTitle);
                _strLine = String.Format("优惠券");
                e.Graphics.DrawString(_strLine, _Font, new SolidBrush(Color.Gray), rectCouponDetail, User.drawFormatTitle);
                _strLine = String.Format("口碑券");
                e.Graphics.DrawString(_strLine, _Font, new SolidBrush(Color.Black), rectKoubeiDetail, User.drawFormatTitle);
            }
            e.Graphics.DrawLine(new Pen(new SolidBrush(Color.Gray), 1), new Point(100, 12), new Point(100, 20));
            e.Graphics.DrawLine(new Pen(new SolidBrush(Color.Gray), 1), new Point(200, 12), new Point(200, 20));
            e.Graphics.DrawLine(new Pen(new SolidBrush(Color.Gray), 1), new Point(300, 12), new Point(300, 20));
            e.Graphics.DrawLine(new Pen(new SolidBrush(Color.Gray), 1), new Point(400, 12), new Point(400, 20));
        }

        private void detailTitleControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (rectflowDetail.Contains(e.Location) && iTabNum != 0)
            {
                iTabNum = 0;
                
                flowlistC.Visible = true;
                orderlistC.Visible = false;
                storeC.Visible = false;
                couponuselistC.Visible = false;
                koubeiuselistC.Visible = false;
                flowlistC.Focus();
                //flowlistC.setdetailMouse();
            }
            else if (rectorderdDetail.Contains(e.Location) && iTabNum != 1)
            {
                iTabNum = 1;

                flowlistC.Visible = false;
                orderlistC.Visible = true;
                storeC.Visible = false;
                couponuselistC.Visible = false;
                koubeiuselistC.Visible = false;
                orderlistC.Focus();
                //orderlistC.setdetailMouse();
            }
            else if (rectStoreDetail.Contains(e.Location) && iTabNum != 2)
            {
                iTabNum = 2;

                flowlistC.Visible = false;
                orderlistC.Visible = false;
                storeC.Visible = true;
                couponuselistC.Visible = false;
                koubeiuselistC.Visible = false;
                storeC.Focus();
                //storeC.fnSetStoreMouse();
            }
            else if (rectCouponDetail.Contains(e.Location) && iTabNum != 3)
            {
                iTabNum = 3;

                flowlistC.Visible = false;
                orderlistC.Visible = false;
                storeC.Visible = false;
                couponuselistC.Visible = true;
                koubeiuselistC.Visible = false;
                couponuselistC.Focus();
                //couponuselistC.setcouponListMouse();
            }
            else if (rectKoubeiDetail.Contains(e.Location) && iTabNum != 4)
            {
                iTabNum = 4;

                flowlistC.Visible = false;
                orderlistC.Visible = false;
                storeC.Visible = false;
                couponuselistC.Visible = false;
                koubeiuselistC.Visible = true;
                koubeiuselistC.Focus();
                //couponuselistC.setcouponListMouse();
            }
            else
            {
                return;
            }
            Refresh();
        }
    }
}
