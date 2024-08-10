using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http;
using System.IO;
using System.Text.Json;
using Newtonsoft.Json;

namespace GigaChat
{
    public class DLBResponses
    {
        [JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
        public record class Success<E>
        {
            public required string status { set; get; }
            public required int count { get; set; }
            public required E[] data { set; get; }
        }
        public record class AccessDenied
        {
            public required string status { set; get; }
            public required string description { set; get; }
        }
    }
}
