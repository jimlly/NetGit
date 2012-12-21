using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Yuantel.MobileMsg.DAL
{
    public class Global
    {
        #region 获取格式化后的金额(从DB取出)
        public static Decimal ChangeRateFromDb(long _lMoney)
        {
            Decimal _dMoney = 0;

            try
            {
                _dMoney = Convert.ToDecimal(Convert.ToDouble(_lMoney) / 10000);
            }
            catch
            {                
            }
            return _dMoney;
        }
        #endregion

        #region 获取格式化后的金额(存入DB)
        public static long ChangeRateToDb(Decimal _dMoney)
        {
            long _lMoney = 0;

            try
            {
                _lMoney = Convert.ToInt64(_dMoney * 10000);
            }
            catch
            {
            }
            return _lMoney;
        }
        #endregion
    }
}
