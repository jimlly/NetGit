/**
 * 文件：Msg.cs
 * 作者：朱翔
 * 日期：2009-03-23
 * 描述：短信实体模型类
*/

using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Yuantel.MobileMsg.Model
{
    public class Msg 
    {
        /// <summary>
        /// 短信编号
        /// </summary>
        private string _msgId = "";
        public string MsgId
        {
            get { return _msgId; }
            set { _msgId = value; }
        }

        /// <summary>
        /// 发送人
        /// </summary>
        private string _sender = "";
        public string Sender
        {
            get { return _sender; }
            set { _sender = value; }
        }

        /// <summary>
        /// 优先级
        /// </summary>
        private MsgPriority _priority;
        public MsgPriority Priority
        {
            get { return _priority; }
            set { _priority = value; }
        }

        /// <summary>
        /// 接收人
        /// </summary>
        private IList<Receiver> _receivers;
        public IList<Receiver> Receivers
        {
            get { return _receivers; }
            set { _receivers = value; }
        }

        /// <summary>
        /// 提交时间
        /// </summary>
        private DateTime _submitTime = DateTime.Now;
        public DateTime SubmitTime
        {
            get { return _submitTime; }
            set { _submitTime = value; }
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

        /// <summary>
        /// 短信内容
        /// </summary>
        private string _message = "";
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        /// <summary>
        /// 短信批次编号
        /// </summary>
        private BatchState _state;
        public BatchState State
        {
            get { return _state; }
            set { _state = value; }
        }

        /// <summary>
        /// 用户编号
        /// </summary>
        private int _seqNo = -1;
        public int SeqNo
        {
            get { return _seqNo; }
            set { _seqNo = value; }
        }

        #region  查询用
        /// <summary>
        /// 查询时间段
        /// </summary>
        private int _clsID = 0;
        public int ClsID
        {
            get { return _clsID; }
            set { _clsID = value; }
        }

        /// <summary>
        /// 开始时间
        /// </summary>
        private string _beginTime = "";
        public string BeginTime
        {
            get { return _beginTime; }
            set { _beginTime = value; }
        }

        /// <summary>
        /// 结束时间
        /// </summary>
        private string _endTime = "";
        public string EndTime
        {
            get { return _endTime; }
            set { _endTime = value; }
        }

        /// <summary>
        /// 每页显示记录数
        /// </summary>
        private int _pageSize = 0;
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value; }
        }

        /// <summary>
        /// 查询页码
        /// </summary>
        private int _pageIndex = 0;
        public int PageIndex
        {
            get { return _pageIndex; }
            set { _pageIndex = value; }
        }

        /// <summary>
        /// 是否删除
        /// </summary>
        private int _isDelete = 0;
        public int IsDelete
        {
            get { return _isDelete; }
            set { _isDelete = value; }
        }

        /// <summary>
        /// 是否取全部记录
        /// </summary>
        private int _isTotal = 0;
        public int IsTotal
        {
            get { return _isTotal; }
            set { _isTotal = value; }
        }

        /// <summary>
        /// 总记录数
        /// </summary>
        private int _totalRows = 0;
        public int TotalRows
        {
            get { return _totalRows; }
            set { _totalRows = value; }
        }

        /// <summary>
        /// 审核状态
        /// </summary>
        private int _status = -1;
        public int Status
        {
            get { return _status; }
            set { _status = value; }
        }

        private string _userName = "";
        public string UserName
        {
            get { return _userName; }
            set { _userName = value; }
        }
        #endregion
    }
}
