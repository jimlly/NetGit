/**
 * 文件：Enums.cs
 * 作者：朱孟峰
 * 日期：2009-03-16
 * 描述：枚举类型
*/

using System;

namespace Com.Yuantel.MobileMsg.Model
{
    /// <summary>
    /// 信息优先级
    /// </summary>
    public enum MsgPriority : byte
    {
        Lower = 0,
        Normal = 1,
        High = 2,
    }

    /// <summary>
    /// 批次状态
    /// </summary>
    public enum BatchState : byte
    {
        Default = 0,        // 提交的缺省状态，等待审核
        Checking = 1,       // 正在审核
        Denied = 2,         // 审核未通过
        Paying = 3,         // 正在计费
        LessBalance = 4,    // 余额不足
        Sending = 5,        // 正在发送
        Completed = 9       // 完成
    }

    /// <summary>
    /// 短信状态
    /// </summary>
    public enum MsgState : byte
    {
        Default = 0,        // 缺省状态
        Sending = 1,        // 正在发送
        Success = 2,        // 发送成功
        Failed = 9          // 发送失败
    }

}
