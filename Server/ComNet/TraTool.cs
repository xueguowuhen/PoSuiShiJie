/****************************************************
    文件：TraTool
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-04-12 9:36:31
	功能：Nothing
*****************************************************/
using System.IO;
using System;
using System.IO.Compression;
//using System.Xml.Serialization;
using ProtoBuf; // 使用 protobuf-net 的命名空间
namespace ComNet
{
    public class TraTool
    {
        public static byte[] PackLenInfo(byte[] data)
        {
            int len = data.Length;
            byte[] pkg = new byte[len + 4];//+4为了存储长度信息，空的
            byte[] head = BitConverter.GetBytes(len);//将长度转化为字节流，放在开头
            head.CopyTo(pkg, 0);//将长度放在开头
            data.CopyTo(pkg, 4);//内容放在后面
            return pkg;
        }
        /// <summary>
        /// 序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="msg"></param>
        /// <returns></returns>

        public static byte[] Serialize<T>(T msg) // 这里的 T 应该是具体的消息类型
        {
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    Serializer.Serialize(ms, msg); // 使用 protobuf-net 进行序列化
                    return Compress(ms.ToArray()); // 压缩序列化数据
                }
            }
            catch (Exception ex)
            {
                // 记录异常信息
                LogMsg($"Serialization failed for type {typeof(T).Name}: {ex.Message}");
                // 如果可以，记录 msg 的详细信息
                LogMsg($"Message details: {msg}");
                throw; // 重新抛出异常，以便外部处理
            }
        }

        /// <summary>
        /// 压缩数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static byte[] Compress(byte[] input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (GZipStream gZip = new GZipStream(ms, CompressionMode.Compress, true))//实例化一个用于对数据进行压缩的对象，compress表示压缩，true表示使用基础流
                {
                    gZip.Write(input, 0, input.Length);//将input的数据写入压缩
                    //gZip.Close();
                }
                    return ms.ToArray();// 将压缩后的数据转化为字节返回
            }
        }
        /// <summary>
        /// 反序列化
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="msg"></param>
        /// <returns></returns>
        public static T DeSerialize<T>(byte[] msg) // T 代表反序列化后的类型
        {
            using (MemoryStream ms = new MemoryStream(DeCompress(msg)))
            {
                return Serializer.Deserialize<T>(ms); // 使用 protobuf-net 进行反序列化
            }
        }

        /// <summary>
        /// 解压数据
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static byte[] DeCompress(byte[] input)
        {
            using (MemoryStream ms = new MemoryStream(input))
            {
                using (MemoryStream outs = new MemoryStream())
                {
                    using (GZipStream gZip = new GZipStream(ms, CompressionMode.Decompress))//实例化一个用于对数据进行压缩的对象，compress表示压缩，true表示使用基础流
                    {
                        gZip.CopyTo(outs);//将解压的数据复制到outs
                       // gZip.Close();
                    }
                        return outs.ToArray();// 将压缩后的数据转化为字节返回
                }
            }
        }
        #region Log
        public static bool log = true;
        public static Action<string, int> logCB;
        public static void LogMsg(string msg, LogLevel lv = LogLevel.None)
        {
            if (log != true)
            {
                return;
            }
            //消息添加当前时间
            msg = DateTime.Now.ToLongTimeString() + " >> " + msg;
            if (logCB != null)//判断是否有委托
            {
                logCB(msg, (int)lv);
            }
            else
            {
                if (lv == LogLevel.None)
                {
                    Console.WriteLine(msg);
                }
                else if (lv == LogLevel.Warn)
                {
                    Console.WriteLine("//--------------------Warn--------------------//");
                    Console.WriteLine(msg);
                }
                else if (lv == LogLevel.Error)
                {
                    Console.WriteLine("//--------------------Error--------------------//");
                    Console.WriteLine(msg);
                }
                else if (lv == LogLevel.Info)
                {
                    Console.WriteLine("//--------------------Info--------------------//");
                    Console.WriteLine(msg);
                }
                else
                {
                    Console.WriteLine("//--------------------Error--------------------//");
                    Console.WriteLine(msg + " >> 未知日志类型\n");
                }
            }
        }
        #endregion
    }
    /// <summary>
    /// Log Level
    /// </summary>
    public enum LogLevel
    {
        None = 0,// None
        Warn = 1,//Yellow
        Error = 2,//Red
        Info = 3//Green
    }
}
