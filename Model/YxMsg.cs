using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Yuantel.MobileMsg.Model
{
    public class YxMsg
    {
        public YxMsg()
        { }

        private int _seqNo = -1;
        private string _message = "";
        private string _areaIndustry = "";
        private DateTime _sendTime = DateTime.Now;
        private int _startID = 0;
        private int _endID = 0;
        private int _sendCounts = 0;
        private int _clsID = 0;
        private string _beginTime = "";
        private string _endTime = "";
        private int _pageIndex = 1;
        private int _pageSize = 10;

        /// <summary>
        /// 用户编号
        /// </summary>
        public int SeqNo
        {
            get { return _seqNo; }
            set { _seqNo = value; }
        }

        /// <summary>
        /// 短信内容
        /// </summary>
        public string Message
        {
            get { return _message; }
            set { _message = value; }
        }

        /// <summary>
        /// 地区行业信息,格式为"省市编号|地区编号|行业编号"，多个用分号隔开
        /// </summary>
        public string AreaIndustry
        {
            get { return _areaIndustry; }
            set { _areaIndustry = value; }
        }

        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime SendTime
        {
            get { return _sendTime; }
            set { _sendTime = value; }
        }

        /// <summary>
        /// 开始编号
        /// </summary>
        public int StartID
        {
            get { return _startID; }
            set { _startID = value; }
        }

        /// <summary>
        /// 结束编号
        /// </summary>
        public int EndID
        {
            get { return _endID; }
            set { _endID = value; }
        }

        /// <summary>
        /// 发送条数
        /// </summary>
        public int SendCounts
        {
            get { return _sendCounts; }
            set { _sendCounts = value; }
        }

        #region 查询用
        /// <summary>
        /// 时间段查询类型
        /// </summary>
        public int ClsID
        {
            get { return _clsID; }
            set { _clsID = value; }
        }

        /// <summary>
        /// 查询开始时间
        /// </summary>
        public string BeginTime
        {
            get { return _beginTime; }
            set { _beginTime = value; }
        }

        /// <summary>
        /// 查询结束时间
        /// </summary>
        public string EndTime
        {
            get { return _endTime; }
            set { _endTime = value; }
        }

        /// <summary>
        /// 页索引
        /// </summary>
        public int PageIndex
        {
            get { return _pageIndex; }
            set { _pageIndex = value; }
        }

        /// <summary>
        /// 页面显示记录数
        /// </summary>
        public int PageSize
        {
            get { return _pageSize; }
            set { _pageSize = value; }
        }
        #endregion
    }
}
