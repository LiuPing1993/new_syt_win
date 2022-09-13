using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using System.IO; 

namespace qgj
{
    class SecurityClass
    {
        static string encryptKey = "oyes";    //密钥  

        #region 加密字符串
        /// <summary> /// 加密字符串   
        /// </summary>  
        /// <param name="str">要加密的字符串</param>  
        /// <returns>加密后的字符串</returns>  
        static string Encrypt(string str)
        {
            DESCryptoServiceProvider descsp = new DESCryptoServiceProvider();
            byte[] key = Encoding.Unicode.GetBytes(encryptKey);
            byte[] data = Encoding.Unicode.GetBytes(str);
            MemoryStream MStream = new MemoryStream();
            CryptoStream CStream = new CryptoStream(MStream, descsp.CreateEncryptor(key, key), CryptoStreamMode.Write);
            CStream.Write(data, 0, data.Length);
            CStream.FlushFinalBlock();
            return Convert.ToBase64String(MStream.ToArray());
        }
        #endregion

        #region 解密字符串
        /// <summary>  
        /// 解密字符串   
        /// </summary>  
        /// <param name="str">要解密的字符串</param>  
        /// <returns>解密后的字符串</returns>  
        static string Decrypt(string str)
        {
            DESCryptoServiceProvider descsp = new DESCryptoServiceProvider();
            byte[] key = Encoding.Unicode.GetBytes(encryptKey);
            byte[] data = Convert.FromBase64String(str);
            MemoryStream MStream = new MemoryStream();
            CryptoStream CStream = new CryptoStream(MStream, descsp.CreateDecryptor(key, key), CryptoStreamMode.Write);
            CStream.Write(data, 0, data.Length);
            CStream.FlushFinalBlock();
            return Encoding.Unicode.GetString(MStream.ToArray());
        }
        #endregion

        /// <summary>
        /// 加密入口
        /// </summary>
        /// <param name="str">待加密串</param>
        /// <returns>加密完成的字符串</returns>
        public static string inCode(string str)
        {
            return Encrypt(str);
        }
        /// <summary>
        /// 解密出口
        /// </summary>
        /// <param name="str">待解密串</param>
        /// <returns>解密完成的字符串</returns>
        public static string outCode(string str)
        {
            return Decrypt(str);
        }
    }
}
