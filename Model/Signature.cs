using System;
using System.Collections.Generic;
using System.Text;

namespace Com.Yuantel.MobileMsg.Model
{
    public class Signature
    {
        private int _id = 0;
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private int _seqNo = 0;
        public int SeqNo
        {
            get { return _seqNo; }
            set { _seqNo = value; }
        }

        private string _title = "";
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        private string _content = "";
        public string Content
        {
            get { return _content; }
            set { _content = value; }
        }

        private int _selected = 0;
        public int Selected
        {
            get { return _selected; }
            set { _selected = value; }
        }
    }
}
