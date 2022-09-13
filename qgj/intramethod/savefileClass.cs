using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace qgj
{
    [Serializable]
    class savefileClass
    {
        string value = "rtyui23jh4g5j32kjh4g5";
        public savefileClass(string _insert) 
        {
            value = _insert;
            
        }
        public void setkey()
        {
            Save(this);
        }
        public string getkey()
        {
            Open();
            return value;
        }

        private void Save(savefileClass _insert)
        {
            SerializeObjectToFile("c:\\Windows\\qinroot.qgj", _insert);
            //SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            //if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            //{
            //    SerializeObjectToFile(saveFileDialog1.FileName);
            //}
        }
        private savefileClass Open()
        {
            return UnSerializeObjectFromFile("c:\\Windows\\qinroot.qgj");
            //OpenFileDialog openFileDialog1 = new OpenFileDialog();
            //if (openFileDialog1.ShowDialog() == DialogResult.OK)
            //{
            //    return UnSerializeObjectFromFile(openFileDialog1.FileName);
            //}
            //return null;
        }
        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="storefilename">文件位置</param>
        internal void SerializeObjectToFile(string storefilename,savefileClass _k)
        {
            IFormatter formatter = new BinaryFormatter();

            Stream writer = new FileStream(storefilename, FileMode.Create);

            formatter.Serialize(writer, _k);

            byte[] objbuffer = new byte[writer.Length];
            writer.Seek(0, SeekOrigin.Begin);
            writer.Read(objbuffer, 0, objbuffer.Length);
            writer.Close();
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="storefilename">文件位置</param>
        /// <returns></returns>
        internal savefileClass UnSerializeObjectFromFile(string storefilename)
        {
            savefileClass offlineobject = null;

            IFormatter formatter = new BinaryFormatter();

            Stream writer = new FileStream(storefilename, FileMode.Open);
            writer.Seek(0, SeekOrigin.Begin);
            offlineobject = (savefileClass)formatter.Deserialize(writer);

            writer.Close();
            return offlineobject;
        }
    }

}
