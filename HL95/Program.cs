using System;
using System.Collections.Generic;
using System.Text;

using System.Net;

namespace HL95
{
    class Program
    {
        static void Main(string[] args)
        {
            WebClient client = new WebClient();
            client.Credentials = CredentialCache.DefaultCredentials;
            byte[] data = client.DownloadData("http://219.238.160.81/interface/limitnew.asp?username=yttx&password=123456&message=ceshi&phone=13264132010,13381401804,13691259854&epid=476&linkid=");

            string str = Encoding.UTF8.GetString(data);
        }

        /// <summary>
        /// 中文编码
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        private static string UrlEncode(string url)
        {
            byte[] bs = Encoding.GetEncoding("GB2312").GetBytes(url);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < bs.Length; i++)
            {
                if (bs[i] < 128)
                    sb.Append((char)bs[i]);
                else
                {
                    sb.Append("%" + bs[i++].ToString("x").PadLeft(2, '0'));
                    sb.Append("%" + bs[i].ToString("x").PadLeft(2, '0'));
                }
            }
            return sb.ToString();
        }
    }
}
