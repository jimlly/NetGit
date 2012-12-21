using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Yuantel.Msg.DAL
{
    public class XmlReturn : IXmlReturn
    {
        #region XmlReturn
        //private const string XML_
        public string GetXml(string ErrCode, string SmsID)
        {
            StringBuilder sb = new StringBuilder();
            string ErrMsg = "";
            string ReturnXml = "";

            //判断手机格式
            if (SmsID == "104")
            {
                ErrCode = "104";
                SmsID = "";
            }

            switch (ErrCode)
            {
                case "0": //操作成功
                    ErrMsg = "操作成功";
                    break;
                case "101": //权限不足
                    ErrMsg = "权限不足";
                    break;
                case "102": // 帐号过期 
                    ErrMsg = "帐号过期 ";
                    break;
                case "103": // 帐号密码错误
                    ErrMsg = "帐号密码错误";
                    break;
                case "104": // 帐号密码错误
                    ErrMsg = "手机格式不正确";
                    break;
                case "107": // 该账号发送的内容不合法
                    ErrMsg = "该账号发送的内容有不合法词汇";
                    break;
                default:
                    ErrMsg = "操作失败";
                    break;
            }
            ReturnXml = sb.Append("<ReturnInfo>")
                        .Append("<ErrCode>")
                        .Append(ErrCode)
                        .Append("</ErrCode>")
                        .Append("<ErrMsg>")
                        .Append(ErrMsg)
                        .Append("</ErrMsg>")
                        .Append("<SmsId>")
                        .Append(SmsID)
                        .Append("</SmsId>")
                        .Append("</ReturnInfo>").ToString();
            return ReturnXml;
        }
        #endregion
    }
}
