/**
 * 文件：DataAccess.cs
 * 作者：朱孟峰
 * 日期：2009-04-18
 * 描述：数据访问层工厂模式实现
*/

using System.Reflection;
using System.Configuration;

namespace Com.Yuantel.Msg.DAL
{
    public sealed class DataAccess
    {
        private DataAccess() { }

        public static ILogin CreateLoginDAL()
        {
            return new MsgLogin();
        }

        public static IMobileMsg CreateMobileMsgDAL()
        {
            return new MobileMsg();
        }

        public static IXmlReturn CreatXmlReturnDal()
        {
            return new XmlReturn();
        }
    }
}
