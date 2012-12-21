/**
 * 文件：Exceptions.cs
 * 作者：朱孟峰
 * 日期：2009-04-14
 * 描述：自定义异常类
*/

using System;

namespace Com.Yuantel.Msg.DAL
{
    /// <summary>
    /// 登录异常
    /// </summary>
    public class PermissionException : Exception
    {
        public PermissionException(string message)
            : base(message)
        {
        }
    }

    /// <summary>
    /// 账号不可用
    /// </summary>
    public class AccountExpiredException : Exception
    {
        public AccountExpiredException(string message)
            : base(message)
        {
        }
    }

    /// <summary>
    /// 账号密码错误
    /// </summary>
    public class PwdErrorException : Exception
    {
        public PwdErrorException(string message)
            : base(message)
        {
        }
    }

}
