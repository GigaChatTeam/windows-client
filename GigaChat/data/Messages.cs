using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaChat
{
    public record class Message
    {
        public required long id { set; get; }
        public required long author { set; get; }
        public required bool edited { set; get; }
        public required string timeStamp { set; get; }
        public required string type { set; get; }
        public required string content { set; get; }
        public required long[]? files { set; get; }
        public required long[][]? media { set; get; }
        public required Forward? forward { set; get; }
    }
}
