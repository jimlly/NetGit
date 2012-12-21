using System;

namespace Com.Yuantel.Msg.DAL
{
    public interface IMobileMsg
    {
        string Send(int seqNo, string mobiles, string msg);

        string GetMsgId();

        string GetStateDesc(string msgId);
    }
}
