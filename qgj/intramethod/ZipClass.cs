using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.IO.Compression;
using System.Collections;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace qgj
{
    class ZipClass
    {
        /// <summary>  
        /// 将指定的文件解压,返回解压后的数据  
        /// </summary>  
        /// <param name="srcFile">指定的源文件</param>  
        /// <returns>解压后得到的数据</returns>  
        public static byte[] DecompressData(string srcFile)
        {
            if (false == File.Exists(srcFile))
                throw new FileNotFoundException(String.Format("找不到指定的文件{0}", srcFile));
            FileStream sourceStream = null;
            GZipStream decompressedStream = null;
            byte[] quartetBuffer = null;
            try
            {
                sourceStream = new FileStream(srcFile, FileMode.Open, FileAccess.Read, FileShare.Read);

                decompressedStream = new GZipStream(sourceStream, CompressionMode.Decompress, true);

                // Read the footer to determine the length of the destiantion file  
                //GZIP文件格式说明:  
                //10字节的头，包含幻数、版本号以及时间戳   
                //可选的扩展头，如原文件名   
                //文件体，包括DEFLATE压缩的数据   
                //8字节的尾注，包括CRC-32校验和以及未压缩的原始数据长度(4字节) 文件大小不超过4G   

                //为Data指定byte的长度，故意开大byte数据的范围  
                //读取未压缩的原始数据长度  
                quartetBuffer = new byte[4];
                long position = sourceStream.Length - 4;
                sourceStream.Position = position;
                sourceStream.Read(quartetBuffer, 0, 4);

                int checkLength = BitConverter.ToInt32(quartetBuffer, 0);
                byte[] data;
                if (checkLength <= sourceStream.Length)
                {
                    data = new byte[Int16.MaxValue];
                }
                else
                {
                    data = new byte[checkLength + 100];
                }
                //每100byte从解压流中读出数据，并将读出的数据Copy到Data byte[]中，这样就完成了对数据的解压  
                byte[] buffer = new byte[100];

                sourceStream.Position = 0;

                int offset = 0;
                int total = 0;

                while (true)
                {
                    int bytesRead = decompressedStream.Read(buffer, 0, 100);

                    if (bytesRead == 0)
                        break;

                    buffer.CopyTo(data, offset);

                    offset += bytesRead;
                    total += bytesRead;
                }
                //剔除多余的byte  
                byte[] actualdata = new byte[total];

                for (int i = 0; i < total; i++)
                    actualdata[i] = data[i];

                return actualdata;
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("从文件{0}解压数据时发生错误", srcFile), ex);
            }
            finally
            {
                if (sourceStream != null)
                    sourceStream.Close();

                if (decompressedStream != null)
                    decompressedStream.Close();
            }
        }  
        public static void CompressData(byte[] srcBuffer, string destFile)
        {
            FileStream destStream = null;
            GZipStream compressedStream = null;
            try
            {
                //打开文件流  
                destStream = new FileStream(destFile, FileMode.OpenOrCreate, FileAccess.Write);
                //指定压缩的目的流（这里是文件流）  
                compressedStream = new GZipStream(destStream, CompressionMode.Compress, true);
                //往目的流中写数据，而流将数据写到指定的文件  
                compressedStream.Write(srcBuffer, 0, srcBuffer.Length);
            }
            catch (Exception ex)
            {
                throw new Exception(String.Format("压缩数据写入文件{0}时发生错误", destFile), ex);
            }
            finally
            {             
                if (null != compressedStream)
                {
                    compressedStream.Close();
                    compressedStream.Dispose();
                }

                if (null != destStream)
                    destStream.Close();
            }
        }

        public static void Unzip(string srcFile,string destFile)
        {
            try
            {
                using (FileStream fs = new FileStream(srcFile, FileMode.Open, FileAccess.Read))
                {
                    //目录文件写入流  
                    using (FileStream save = new FileStream(destFile, FileMode.Create, FileAccess.Write))
                    {
                        //创建包含压缩文件流的GZipStream流  
                        using (GZipStream zipStream = new GZipStream(fs, CompressionMode.Decompress, true))
                        {
                            //创建byte[]数组中转数据  
                            byte[] buf = new byte[1024];
                            int len;
                            //循环将解压流中数据写入到byte[]数组中  
                            while ((len = zipStream.Read(buf, 0, buf.Length)) > 0)
                            {
                                //向目标文件流写入byte[]中转数组  
                                save.Write(buf, 0, len);
                            }
                        }
                    }
                }
                Console.WriteLine("解压完毕");
                Console.ReadKey();  
            }
            catch (System.Exception)
            {
                throw;
            }
        }
        public static void Zip(string srcFile, string destFile)
        {
            try
            {
                using (FileStream fs = new FileStream(srcFile, FileMode.Open, FileAccess.Read))
                {
                    //创建写入流  
                    using (FileStream save = new FileStream(destFile, FileMode.Create, FileAccess.Write))
                    {
                        //创建包含写入流的压缩流  
                        using (GZipStream gs = new GZipStream(save, CompressionMode.Compress))
                        {
                            //创建byte[]数组中转数据  
                            byte[] b = new byte[1024 * 1024];
                            int count = 0;
                            //循环将读取流中数据写入到byte[]数组中  
                            while ((count = fs.Read(b, 0, b.Length)) > 0)
                            {
                                //将byte[]数组中数据写入到压缩流  
                                gs.Write(b, 0, b.Length);
                            }
                        }
                    }
                }
                Console.WriteLine("压缩完毕!");
            }
            catch (System.Exception)
            {
                throw;
            }
        }
    }
    public class GZipCompress
    {
        /// <summary>
        /// 对目标文件夹进行压缩，将压缩结果保存为指定文件
        /// </summary>
        /// <param name="dirPath">目标文件夹</param>
        /// <param name="fileName">压缩文件</param>
        
        public static void Compress(string dirPath, string fileName)
        {
            ArrayList list = new ArrayList();
            foreach (string f in Directory.GetFiles(dirPath))
            {
                byte[] destBuffer = File.ReadAllBytes(f);
                SerializeFileInfo sfi = new SerializeFileInfo(f, destBuffer);
                list.Add(sfi);
            }
            IFormatter formatter = new BinaryFormatter();
            using (Stream s = new MemoryStream())
            {
                formatter.Serialize(s, list);
                s.Position = 0;
                CreateCompressFile(s, fileName);
            }
        }

        /// <summary>
        /// 对目标压缩文件解压缩，将内容解压缩到指定文件夹
        /// </summary>
        /// <param name="fileName">压缩文件</param>
        /// <param name="dirPath">解压缩目录</param>
        public static void DeCompress(string fileName, string dirPath)
        {
            using (Stream source = File.OpenRead(fileName))
            {
                using (Stream destination = new MemoryStream())
                {
                    using (GZipStream input = new GZipStream(source, CompressionMode.Decompress, true))
                    {
                        byte[] bytes = new byte[4096];
                        int n;
                        while ((n = input.Read(bytes, 0, bytes.Length)) != 0)
                        {
                            destination.Write(bytes, 0, n);
                        }
                    }
                    destination.Flush();
                    destination.Position = 0;
                    DeSerializeFiles(destination, dirPath);
                }
            }
        }

        private static void DeSerializeFiles(Stream s, string dirPath)
        {
            BinaryFormatter b = new BinaryFormatter();
            ArrayList list = (ArrayList)b.Deserialize(s);

            foreach (SerializeFileInfo f in list)
            {
                string newName = dirPath + Path.GetFileName(f.FileName);
                using (FileStream fs = new FileStream(newName, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(f.FileBuffer, 0, f.FileBuffer.Length);
                    fs.Close();
                }
            }
        }

        private static void CreateCompressFile(Stream source, string destinationName)
        {
            using (Stream destination = new FileStream(destinationName, FileMode.Create, FileAccess.Write))
            {
                using (GZipStream output = new GZipStream(destination, CompressionMode.Compress))
                {
                    byte[] bytes = new byte[4096];
                    int n;
                    while ((n = source.Read(bytes, 0, bytes.Length)) != 0)
                    {
                        output.Write(bytes, 0, n);
                    }
                }
            }
        }

        [Serializable]
        class SerializeFileInfo
        {
            public SerializeFileInfo(string name, byte[] buffer)
            {
                fileName = name;
                fileBuffer = buffer;
            }

            string fileName;
            public string FileName
            {
                get
                {
                    return fileName;
                }
            }

            byte[] fileBuffer;
            public byte[] FileBuffer
            {
                get
                {
                    return fileBuffer;
                }
            }
        }
    }
}
