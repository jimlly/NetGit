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

            //�ж��ֻ���ʽ
            if (SmsID == "104")
            {
                ErrCode = "104";
                SmsID = "";
            }

            switch (ErrCode)
            {
                case "0": //�����ɹ�
                    ErrMsg = "�����ɹ�";
                    break;
                case "101": //Ȩ�޲���
                    ErrMsg = "Ȩ�޲���";
                    break;
                case "102": // �ʺŹ��� 
                    ErrMsg = "�ʺŹ��� ";
                    break;
                case "103": // �ʺ��������
                    ErrMsg = "�ʺ��������";
                    break;
                case "104": // �ʺ��������
                    ErrMsg = "�ֻ���ʽ����ȷ";
                    break;
                case "107": // ���˺ŷ��͵����ݲ��Ϸ�
                    ErrMsg = "���˺ŷ��͵������в��Ϸ��ʻ�";
                    break;
                default:
                    ErrMsg = "����ʧ��";
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
