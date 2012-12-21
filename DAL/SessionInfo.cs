using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Yuantel.Msg.DAL
{
    public class SessionInfo
    {
        private string sessionId;
        private string account;
        private string userName;
        private string companyName;
        private string appCode;
        private int seqNo;
        private int compId;
        private DateTime expiredTime;

        public SessionInfo()
        {
        }

        public string SessionId
        {
            get { return sessionId; }
            set { sessionId = value; }
        }

        public string Account
        {
            get { return account; }
            set { account = value; }
        }

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        public string CompanyName
        {
            get { return companyName; }
            set { companyName = value; }
        }

        public string AppCode
        {
            get { return appCode; }
            set { appCode = value; }
        }

        public int SeqNo
        {
            get { return seqNo; }
            set { seqNo = value; }
        }

        public int CompId
        {
            get { return compId; }
            set { compId = value; }
        }

        public DateTime ExpiredTime
        {
            get { return expiredTime; }
            set { expiredTime = value; }
        }
    }
}
