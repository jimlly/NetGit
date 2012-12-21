using System;
using System.Collections.Generic;
using System.Text;
/*金额账户实体*/
namespace Com.Yuantel.MobileMsg.Model
{
    [Serializable]
    public class IB_AccountInfo
    {
        private int accountID;
        private string nickName;
        private Double percents;
        private long balance;
        private long donatedBalance;
        private byte costType;
        private short haveTerm;
        private string endDate;
        private string feeTypeIndexDesc;

        /// <summary>
        /// 金额账户ID
        /// </summary>
        public int AccountID
        {
            get { return accountID; }
            set { accountID = value; }
        }
        /// <summary>
        /// 昵称
        /// </summary>
        public string NickName
        {
            get { return nickName; }
            set { nickName = value; }
        }
        /// <summary>
        /// 折扣
        /// </summary>
        public Double Percents
        {
            get { return percents; }
            set { percents = value; }
        }
        /// <summary>
        /// 余额
        /// </summary>
        public long Balance
        {
            get { return balance; }
            set { balance = value; }
        }
        /// <summary>
        /// 赠送金额
        /// </summary>
        public long DonatedBalance
        {
            get { return donatedBalance; }
            set { donatedBalance = value; }
        }
        /// <summary>
        /// 付费类型   0：全部  1：通信费帐户   2：非通信费帐户（租用费帐户或包年费用帐户）
        /// </summary>
        public byte CostType
        {
            get { return costType; }
            set { costType = value; }
        }
        /// <summary>
        /// 是否有期限  0无期限  1有期限
        /// </summary>
        public short HaveTerm
        {
            get { return haveTerm; }
            set { haveTerm = value; }
        }
        /// <summary>
        /// 到期日期
        /// </summary>
        public string EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }
        /// <summary>
        /// 通话类型ID  如果为-1不进行判断
        /// </summary>
        public string FeeTypeIndexDesc
        {
            get { return feeTypeIndexDesc; }
            set { feeTypeIndexDesc = value; }
        }
       
    }
}
