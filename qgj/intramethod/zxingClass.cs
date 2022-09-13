using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ZXing.QrCode;
using ZXing;
using ZXing.Common;
using ZXing.Rendering; 

namespace qgj
{
    class zxingClass
    {
        EncodingOptions options = null;
        BarcodeWriter writer = null;

        public zxingClass(int _w, int _h )
        {
            options = new QrCodeEncodingOptions
            {
                DisableECI = true,
                CharacterSet = "UTF-8",
                Width = _w,
                Height = _h,
                Margin = 0
            };
            writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.QR_CODE;
            
            writer.Options = options;
        }
        public Bitmap getQrcode(string _insert)
        {
            return writer.Write(_insert);
        }
    }
    class zxingbarcodeClass
    {
        EncodingOptions options = null;
        BarcodeWriter writer = null;
        public zxingbarcodeClass(int _w, int _h)
        {
            options = new EncodingOptions
            {
                //PureBarcode = false,
                Width = _w,
                Height = _h
            };
            writer = new BarcodeWriter();
            writer.Format = BarcodeFormat.CODE_128;
            writer.Options = options;  
        }
        public Bitmap getBarcode(string _insert)
        {
            return writer.Write(_insert);
        }
    }
}
