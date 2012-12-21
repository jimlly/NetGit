/**
 * 文件：ISend.cs
 * 作者：朱孟峰
 * 日期：2009-03-16
 * 描述：短信发送专线实现
*/

using System;
using System.Collections.Generic;

using Com.Yuantel.MobileMsg.Model;

namespace Com.Yuantel.MobileMsg.BLL
{
    public class DirectLine : ISend
    {
        public void Send(IMsg msg)
        { }

        public IList<IMsg> ReportSend()
        {
            return null;
        }
    }
}
