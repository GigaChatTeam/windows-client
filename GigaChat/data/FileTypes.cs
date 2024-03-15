using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaChat
{
    public record class CloudFile
    {
        public required long id { get; set; }
        public string uri { get; set; }
    }
    public record class Forward
    {
        public required string type { set; get; }
        public required long[] path { set; get; }
    }
}
