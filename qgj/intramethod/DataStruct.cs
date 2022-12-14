using System;
using System.Collections.Generic;
using System.Text;

namespace qgj
{
    class DataStruct
    {

    }
    public struct ArcRadius
    {
        private int _rightBottom;
        private int _rightTop;
        private int _leftBottom;
        private int _leftTop;

        public static readonly ArcRadius Empty = new ArcRadius(0);

        public ArcRadius(int radiusLength)
        {
            if (radiusLength < 0)
            {
                radiusLength = 0;
            }

            this._rightBottom = this._rightTop = this._leftBottom = this._leftTop = radiusLength;
        }

        public ArcRadius(int leftTop, int rightTop, int leftBottom, int rightBottom)
        {
            this._rightBottom = rightBottom < 0 ? 0 : rightBottom;
            this._rightTop = rightTop < 0 ? 0 : rightTop;
            this._leftBottom = leftBottom < 0 ? 0 : leftBottom;
            this._leftTop = leftTop < 0 ? 0 : leftTop;
        }

        private bool IsAllEqual()
        {
            return ((this.RightBottom == this.RightTop)
                 && (this.RightBottom == this.LeftBottom))
                 && (this.RightBottom == this.LeftTop);
        }

        public int All
        {
            get
            {
                if (!IsAllEqual())
                {
                    return -1;
                }

                return this.RightBottom;
            }
            set
            {
                if (value < 0)
                {
                    value = 0;
                }

                this.RightBottom = this.RightTop = this.LeftBottom = this.LeftTop = value;
            }
        }

        public int LeftTop
        {
            get
            {
                return this._leftTop;
            }
            set
            {
                if (value < 0)
                {
                    value = 0;
                }

                this._leftTop = value;
            }
        }

        public int RightTop
        {
            get
            {
                return this._rightTop;
            }
            set
            {
                if (value < 0)
                {
                    value = 0;
                }

                this._rightTop = value;
            }
        }

        public int LeftBottom
        {
            get
            {
                return this._leftBottom;
            }
            set
            {
                if (value < 0)
                {
                    value = 0;
                }

                this._leftBottom = value;
            }
        }

        public int RightBottom
        {
            get
            {
                return this._rightBottom;
            }
            set
            {
                if (value < 0)
                {
                    value = 0;
                }

                this._rightBottom = value;
            }
        }

        public static bool operator ==(ArcRadius p1, ArcRadius p2)
        {
            return ((((p1.RightTop == p2.RightTop)
                && (p1.RightBottom == p2.RightBottom))
                && (p1.LeftBottom == p2.LeftBottom))
                && (p1.LeftTop == p2.LeftTop));
        }

        public static bool operator !=(ArcRadius p1, ArcRadius p2)
        {
            return !(p1 == p2);
        }

        public override string ToString()
        {
            return LeftTop + ", " + RightTop + ", " + LeftBottom + ", " + RightBottom;
        }
    }
}
