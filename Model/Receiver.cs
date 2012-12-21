/**
 * 文件：Receiver.cs
 * 作者：朱翔
 * 日期：2009-03-23
 * 描述：收件人实体模型类
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Yuantel.MobileMsg.Model
{
    public class Receiver
    {
        /// <summary>
        /// 接收人手机号码
        /// </summary>
        private string _mobile = "";
        public string Mobile
        {
            get { return _mobile; }
            set { _mobile = value; }
        }

        /// <summary>
        /// 接收人姓名
        /// </summary>
        private string _name = "";
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        /// <summary>
        /// 具体短信状态
        /// </summary>
        private MsgState _state;
        public MsgState State
        {
            get { return _state; }
            set { _state = value; }
        }

        /// <summary>
        /// 发送时间
        /// </summary>
        private DateTime _sendTime = DateTime.Now;
        public DateTime SendTime
        {
            get { return _sendTime; }
            set { _sendTime = value; }
        }
    }
}
