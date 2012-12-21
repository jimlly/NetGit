using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Yuantel.Msg.DAL
{
    public interface IXmlReturn
    {
        string GetXml(string ErrCode,string MsgID);
    }
}
