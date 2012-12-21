using System;
using System.Collections.Generic;
using System.Text;

using Com.Yuantel.MobileMsg.DAL;

namespace Com.Yuantel.MobileMsg.BLL
{
    public class Common
    {
        public static string GetBatchNo(int _clsid)
        {
            return DAL.Common.GetBatchNo(_clsid);
        }
    }
}
