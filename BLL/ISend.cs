/**
 * 文件：ISend.cs
 * 作者：朱孟峰
 * 日期：2009-03-16
 * 描述：短信发送接口
*/

using System;
using System.Collections.Generic;

using Com.Yuantel.MobileMsg.Model;

namespace Com.Yuantel.MobileMsg.BLL
{
    /// <summary>
    /// 短信发送接口
    /// </summary>
    public interface ISend
    {

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="msg"></param>
        void Send(IMsg msg);

        /// <summary>
        /// 接收发送报告
        /// </summary>
        /// <returns>查询到的短信发送结果集合</returns>
        IList<IMsg> ReportSend();

    }
}
