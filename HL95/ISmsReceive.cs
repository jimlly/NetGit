using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Yuantel.HL95
{
    public interface ISmsReceive
    {
        string ReceiveSms(string Mobiles, string Content, string SpNumber);
    }
}
