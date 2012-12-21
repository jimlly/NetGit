/**
 * 文件：MobileMsg.cs
 * 作者：朱孟峰
 * 日期：2009-03-16
 * 描述：短信业务层实现
*/

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using System.Data;

using Com.Yuantel.MobileMsg.Model;
using Com.Yuantel.MobileMsg.DAL;

namespace Com.Yuantel.MobileMsg.BLL
{
    public class MobileMsg
    {

        #region Send Strategy

        // Using a static variable will cache the Send Message strategy object for all instances of business
        // We implement it this way to improve performance, so that the code will only load the instance once
        private static readonly ISend sendStrategy = LoadSendStrategy();

        /// <summary>
        /// This method determines which Send Message Strategy to use based on user's configuration.
        /// </summary>
        /// <returns>An instance of ISend</returns>
        private static ISend LoadSendStrategy()
        {
            // Maybe using the evidence given in the config file load the appropriate assembly and class
            // Look up which strategy to use from config file
            // <add key="SendMsgStrategyAssembly" value="Com.Yuantel.MobileMsg.BLL">
            // <add key="SendMsgStrategyClass" value="ModemPool">
            string path = ConfigurationManager.AppSettings["SendMsgStrategyAssembly"];
            string className = ConfigurationManager.AppSettings["SendMsgStrategyClass"];
            //return (ISend)Assembly.Load(path).CreateInstance(className);
            return (ISend)(new ModemPool());
        }

        #endregion

        #region Constructor

        public MobileMsg() { }

        #endregion

        #region Methods

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="msg"></param>
        public void Send(IMsg msg)
        {
            // 验证计费信息

            // 数据持久化

            // 发送
            sendStrategy.Send(msg);
        }

        /// <summary>
        /// 查询短信发送结果并持久化
        /// </summary>
        public void ReportSend()
        {
            // 查询返回状态
            IList<IMsg> msgs = sendStrategy.ReportSend();

            // 持久化状态
        }

        /// <summary>
        /// 获取指定页的短信列表
        /// </summary>
        /// <param name="sender">发送用户，如果空值，查询所有短信</param>
        /// <param name="page">页码</param>
        /// <returns></returns>
        public IList<IMsg> GetMsgs(string sender, int page)
        {
            return null;
        }

        public DataSet GetMsgs(Msg msg, out string totalRows)
        {
            totalRows = "";
            return null;
        }

        #endregion

    }
}
