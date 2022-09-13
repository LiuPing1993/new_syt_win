using System;
using System.Collections.Generic;
using System.Text;
using System.IO.Ports;

namespace qgj
{
    class CustomerDisplay
    {
        public SerialPort serialPort;   //串口

        public CustomerDisplay(string portName, string rate, string parity, string stop)
        {
            try
            {
                serialPort = new SerialPort();
                serialPort.PortName = portName;
                serialPort.BaudRate = Convert.ToInt32(rate);//波特率

                if (parity == "1")
                {
                    serialPort.Parity = System.IO.Ports.Parity.Odd;//奇校验
                }
                else if (parity == "2")
                {
                    serialPort.Parity = System.IO.Ports.Parity.Even;//偶校验
                }
                else
                {
                    serialPort.Parity = System.IO.Ports.Parity.None;//无
                }

                if (stop == "1")
                {
                    serialPort.StopBits = System.IO.Ports.StopBits.One;//停止位1
                }
                else if (stop == "2")
                {
                    serialPort.StopBits = System.IO.Ports.StopBits.OnePointFive;//停止位1.5
                }
                else if (stop == "2")
                {
                    serialPort.StopBits = System.IO.Ports.StopBits.Two;//停止位2
                }
                else
                {
                    serialPort.StopBits = System.IO.Ports.StopBits.One;//停止位1
                    //serialPort.StopBits = System.IO.Ports.StopBits.None;//停止位 无
                }
                serialPort.DataBits = 8;

                serialPort.DataReceived += new SerialDataReceivedEventHandler(DataReceivedHandler);

                serialPort.Encoding = System.Text.Encoding.GetEncoding("GB2312");

                OpenPort();
            }
            catch (Exception e)
            {
                throw (e);
                //MessageBox.Show(e.ToString());
            }
        }

        private void OpenPort()
        {
            if (null != serialPort)
            {
                try
                {
                    if (!serialPort.IsOpen)
                        serialPort.Open();
                    else
                    {
                        ClosePort();
                        serialPort.Open();
                    }

                }
                catch (Exception e)
                {
                    throw (e);
                    //MessageBox.Show(e.ToString());
                }
            }
        }

        public void ClosePort()
        {
            try
            {
                if (serialPort.IsOpen)
                    serialPort.Close();
                serialPort.Dispose();
            }
            catch (Exception e)
            {
                throw (e);
                //MessageBox.Show(e.ToString());
            }
        }

        /// <summary>
        /// 发送字符串
        /// </summary>
        public void Write(string strBuf)
        {
            try
            {
                byte[] data = ToHex(strBuf, "GB2312");
                serialPort.Write(data, 0, data.Length);
                //serialPort.WriteLine(strBuf);
            }
            catch (Exception e)
            {
                throw (e);
                //MessageBox.Show(e.ToString());
            }
        }

        /// <summary>
        /// 根据编码方式转换字符串为byte[]
        /// </summary>
        private byte[] ToHex(string str, string charset)
        {
            if (str.Length % 2 != 0)
                str += "";
            Encoding enc = Encoding.GetEncoding(charset);
            return enc.GetBytes(str);
        }

        private static void DataReceivedHandler(object sender, SerialDataReceivedEventArgs e)
        {
            SerialPort sp = (SerialPort)sender;
            string indata = sp.ReadExisting();
            Console.WriteLine("Data Received:" + indata);

        }
        public void ConsoleMsg(string result)
        {
            Console.WriteLine(result.ToString());
        }
    }
}
