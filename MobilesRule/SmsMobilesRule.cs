/**
 * 文件：SmsMobilesRule.cs
 * 作者：朱孟峰
 * 日期：2009-09-17
 * 描述：短信手机号码验证
*/

using System;
using System.Configuration;
using System.Text.RegularExpressions;

namespace Com.Yuantel.Sms.MobilesRule
{
    /// <summary>
    /// 短信手机号码验证
    /// </summary>
    public class SmsMobilesRule
    {
        // 手机号码正则表达式
        private static readonly string mobilesRule = ConfigurationManager.AppSettings["MobilesRule"];
        // 正则表达式类
        private static readonly Regex reg = new Regex(mobilesRule);

        /// <summary>
        /// 验证提供的号码是否符合规则
        /// </summary>
        /// <param name="mobiles">号码串</param>
        /// <returns></returns>
        public static bool Match(string mobiles)
        {
            Match m = reg.Match(mobiles);
            return m.Success;
        }
    }
}
