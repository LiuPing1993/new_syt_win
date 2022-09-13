using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace qgj.untils
{
    public class OperaGraphics
    {
        /// <summary>
        /// 圆角矩形边框
        /// </summary>
        /// <param name="rectangle">原始矩形</param>
        /// <param name="g">Graphics 对象</param>
        /// <param name="radius">圆角半径</param>
        /// <param name="backColor">颜色</param>
        public static void FrameRoundRectangle(Rectangle rectangle, Graphics g, int radius, Color backColor)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            if (radius == 0)
            {
                g.DrawRectangle(new Pen(backColor), rectangle);
            }
            else
            {
                g.DrawPath(new Pen(backColor), DrawRoundRect(rectangle.X, rectangle.Y, rectangle.Width - 2, rectangle.Height - 1, radius));
            }
        }
        /// <summary>
        /// 圆角边框【可设置四个角不同的圆角】
        /// </summary>
        /// <param name="rectangle">控件的范围</param>
        /// <param name="g">g</param>
        /// <param name="top_left_radius">左上角的圆角</param>
        /// <param name="top_right_radius">右上角的圆角</param>
        /// <param name="bottom_left_radius">底部左边的圆角</param>
        /// <param name="bottom_right_radius">底部右边的圆角</param>
        /// <param name="backColor">要描述的边框的背景的颜色</param>
        public static void FrameRoundRectangle_Four(Rectangle rectangle, Graphics g, int top_left_radius, int top_right_radius, int bottom_left_radius, int bottom_right_radius, Color backColor)
        {

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            if (top_left_radius == 0 && top_right_radius == 0 && bottom_left_radius == 0 && bottom_right_radius == 0)
            {
                g.DrawRectangle(new Pen(backColor), rectangle);
            }
            else
            {
                g.DrawPath(new Pen(backColor), DrawRoundRect_four(rectangle.X, rectangle.Y, rectangle.Width - 1, rectangle.Height - 1, top_left_radius, top_right_radius, bottom_left_radius, bottom_right_radius));
            }
        }
        /// <summary>
        /// 圆角矩形
        /// </summary>
        /// <param name="rectangle">原始矩形</param>
        /// <param name="g">Graphics 对象</param>
        /// <param name="radius">圆角半径</param>
        /// <param name="backColor">颜色</param>
        public static void FillRoundRectangle(Rectangle rectangle, Graphics g, int radius, Color backColor)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            if (radius == 0)
            {
                g.FillRectangle(new SolidBrush(backColor), rectangle);
            }
            else
            {
                g.FillPath(new SolidBrush(backColor), DrawRoundRect(rectangle.X, rectangle.Y, rectangle.Width - 2, rectangle.Height - 1, radius));
            }
        }

        /// <summary>
        /// 带原角的粗型边框
        /// </summary>
        /// <param name="rectangle"></param>
        /// <param name="g"></param>
        /// <param name="radius"></param>
        /// <param name="backColor"></param>
        /// <param name="linewide"></param>
        public static void FrameBoldRoundRectangle(Rectangle rectangle, Graphics g, int radius, Color backColor, int linewide)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            if (radius == 0)
            {
                g.DrawRectangle(new Pen(backColor, linewide), rectangle);
            }
            else
            {
                g.DrawPath(new Pen(backColor, linewide), DrawRoundRect(rectangle.X, rectangle.Y, rectangle.Width - 2, rectangle.Height - 1, radius));
            }
        }
        public static GraphicsPath DrawRoundRect(int x, int y, int width, int height, int radius)
        {
            GraphicsPath gp = new GraphicsPath();
            gp.AddArc(x, y, radius, radius, 180, 90);
            gp.AddArc(x + width - radius, y, radius, radius, 270, 90);
            gp.AddArc(x + width - radius, y + height - radius, radius, radius, 0, 90);
            gp.AddArc(x, y + height - radius, radius, radius, 90, 90);
            gp.CloseAllFigures();
            return gp;
        }
        /// <summary>
        /// 设置四个角不同的圆角的矩形背景
        /// </summary>
        /// <param name="rectangle">圆角阴影部分的位置和范围</param>
        /// <param name="g">graphics对象</param>
        /// <param name="radius_topleft">左上角的圆角</param>
        /// <param name="radius_topright">右上角的圆角</param>
        /// <param name="radius_bottom_left">左下角的圆角</param>
        /// <param name="radius_bottomright">右下角的圆角</param>
        /// <param name="backColor">背景的颜色</param>
        public static void FillRoundRectangle_four(Rectangle rectangle, Graphics g, int radius_topleft, int radius_topright, int radius_bottom_left, int radius_bottomright, Color backColor)
        {
            g.SmoothingMode = SmoothingMode.AntiAlias;
            if (radius_topleft == 0 && radius_topright == 0 && radius_bottom_left == 0 && radius_bottomright == 0)
            {
                g.FillRectangle(new SolidBrush(backColor), rectangle);
            }
            else
            {
                g.FillPath(new SolidBrush(backColor), DrawRoundRect_four(rectangle.X, rectangle.Y, rectangle.Width, rectangle.Height, radius_topleft, radius_topright, radius_bottom_left, radius_bottomright));
            }
        }
        public static GraphicsPath DrawRoundRect_four(int x, int y, int width, int height, int radius_topleft, int radius_topright, int radius_bottomleft, int radius_bottomright)
        {
            GraphicsPath gp = new GraphicsPath();
            //左上角
            if (radius_topleft > 0)
            {
                gp.AddArc(x, y, radius_topleft, radius_topleft, 180, 90);
            }
            else
            {
                //画两条直线

                gp.AddLine(new Point(x, y), new Point(x, y + 5));
                gp.AddLine(new Point(x, y), new Point(x + 5, y));
            }
            //右上角
            if (radius_topright > 0)
            {
                gp.AddArc(x + width - radius_topright, y, radius_topright, radius_topright, 270, 90);
            }
            else
            {
                gp.AddLine(new Point(x + width - 5, y), new Point(x + width, y));
                gp.AddLine(new Point(x + width, y), new Point(x + width, y + 10));
            }
            if (radius_bottomright > 0)
            {
                gp.AddArc(x + width - radius_bottomright, y + height - radius_bottomright, radius_bottomright, radius_bottomright, 0, 90);
            }
            else
            {
                gp.AddLine(new Point(x + width, y + height), new Point(x + width, y + height - 5));
                gp.AddLine(new Point(x + width, y + height), new Point(x + width - 5, y + height));
            }

            if (radius_bottomleft > 0)
            {
                gp.AddArc(x, y + height - radius_bottomleft, radius_bottomleft, radius_bottomleft, 90, 90);
            }
            else
            {
                //画个直角
                gp.AddLine(new Point(x, y + height), new Point(x + 5, y + height));
                gp.AddLine(new Point(x, y + height), new Point(x, y + height - 5));
            }
            gp.CloseAllFigures();
            return gp;
        }

    }
}
