/**
 * 文件：ISend.cs
 * 作者：朱孟峰
 * 日期：2009-03-16
 * 描述：短信发送猫池实现
*/

using System;
using System.Collections.Generic;

using Com.Yuantel.MobileMsg.Model;

namespace Com.Yuantel.MobileMsg.BLL
{
    public class ModemPool : ISend
    {
        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="msg"></param>
        public void Send(IMsg msg)
        {
            Com.Yuantel.MobileMsg.DAL.ISend dal = Com.Yuantel.MobileMsg.DAL.DataAccess.CreateSend();
            dal.Send(msg);
        }

        /// <summary>
        /// 接收发送报告
        /// </summary>
        /// <returns>查询到的短信发送结果集合</returns>
        public IList<IMsg> ReportSend()
        {
            return null;
        }

    }
}
