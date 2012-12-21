using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Yuantel.MobileMsg.Model
{
    public class Phrase
    {
        private int _id;

        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }
        private int _seqNo;

        public int SeqNo
        {
            get { return _seqNo; }
            set { _seqNo = value; }
        }
        private string _phrase;

        public string Phrase1
        {
            get { return _phrase; }
            set { _phrase = value; }
        }
        private DateTime _crDate;

        public DateTime CrDate
        {
            get { return _crDate; }
            set { _crDate = value; }
        }
    }
}
