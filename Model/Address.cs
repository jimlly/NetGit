using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Yuantel.MobileMsg.Model
{
    /// <summary>
    /// 联系人模型
    /// </summary>
    public class Contactor
    {
        public Contactor()
        { }

        private string _contactorID = "";
        public string ContactorID
        {
            get { return _contactorID; }
            set { _contactorID = value; }
        }

        private string _seqNo = "";
        public string SeqNo
        {
            get { return _seqNo; }
            set { _seqNo = value; }
        }

        private string _compID = "";
        public string CompID
        {
            get { return _compID; }
            set { _compID = value; }
        }

        private string _company = "";
        public string Company
        {
            get { return _company; }
            set { _company = value; }
        }

        private string _jobFax = "";
        public string JobFax
        {
            get { return _jobFax; }
            set { _jobFax = value; }
        }

        private string _name = "";
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _jobPhone = "";
        public string JobPhone
        {
            get { return _jobPhone; }
            set { _jobPhone = value; }
        }

        private string _homePhone = "";
        public string HomePhone
        {
            get { return _homePhone; }
            set { _homePhone = value; }
        }

        private string _mobile = "";
        public string Mobile
        {
            get { return _mobile; }
            set { _mobile = value; }
        }

        private string _shareType = "";
        public string ShareType
        {
            get { return _shareType; }
            set { _shareType = value; }
        }
    }

    /// <summary>
    /// 群发组模型
    /// </summary>
    public class Group
    {
        public Group()
        { }

        private string _sendGroupID = "";
        public string SendGroupID
        {
            get { return _sendGroupID; }
            set { _sendGroupID = value; }
        }

        private string _seqNo = "";
        public string SeqNo
        {
            get { return _seqNo; }
            set { _seqNo = value; }
        }

        private string _compID = "";
        public string CompID
        {
            get { return _compID; }
            set { _compID = value; }
        }

        private string _groupName = "";
        public string GroupName
        {
            get { return _groupName; }
            set { _groupName = value; }
        }

        private string _menberCount = "";
        public string MemberCount
        {
            get { return _menberCount; }
            set { _menberCount = value; }
        }

        private string _shareType = "";
        public string ShareType
        {
            get { return _shareType; }
            set { _shareType = value; }
        }
    }

    /// <summary>
    /// 群发组明细模型
    /// </summary>
    public class GroupDetail
    {
        public GroupDetail()
        { }

        private string _memberID = "";
        public string MemberID
        {
            get { return _memberID; }
            set { _memberID = value; }
        }

        private string _seqNo = "";
        public string SeqNo
        {
            get { return _seqNo; }
            set { _seqNo = value; }
        }

        private string _compID = "";
        public string CompID
        {
            get { return _compID; }
            set { _compID = value; }
        }

        private string _name = "";
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _company = "";
        public string Company
        {
            get { return _company; }
            set { _company = value; }
        }

        private string _phoneNo = "";
        public string PhoneNo
        {
            get { return _phoneNo; }
            set { _phoneNo = value; }
        }

        private string _property = "";
        public string Property
        {
            get { return _property; }
            set { _property = value; }
        }
    }
}
