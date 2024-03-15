using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaChat
{
    public class Message
    {
        public long id;
        public string author;
        public bool edited;
        public string timeStamp;
        public string type;
        public string content;
        public Forward? forward;
        public long[]? files;
        public long[][]? media;
        public Message(long _id, string _author, bool _edited, string _timeStamp, string _type, string _content)
        {
            id = _id;
            author = _author;
            edited = _edited;
            timeStamp = _timeStamp;
            type = _type;
            content = _content;

            //тут создание сообщения
        }
    }
}
