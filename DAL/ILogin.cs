/**
 * 文件：IMsgDAL.cs
 * 作者：朱孟峰
 * 日期：2009-04-18
 * 描述：短信客户端数据访问层接口
*/

using System;

namespace Com.Yuantel.Msg.DAL
{
    public interface ILogin
    {
        SessionInfo Login(string userName, string pwd);
    }
}
