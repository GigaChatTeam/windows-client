global using UIWigets;
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
using System.Text.Json.Serialization;



namespace GigaChat
{
    interface ResizableUIElement
    {
        void resize(double Width, double Height);
    }
    public record class Channel
    {
        public required long id { set; get; }
        public int userStatus = -1;
        public required string title { set; get; }
        public string description = "";
        [JsonPropertyName("public")]
        public required bool isPublic { set; get; }
        public required bool enabled { set; get; }
        public CloudFile? icon { set; get; }
        public ChannelLastMessage? lastMessage { set; get; }
    }
    public record class ChannelLastMessage
    {
        public required long id { get; set; }
        public required string author { get; set; }
        public required bool edited { get; set; }
        public required string timestamp { get; set; }
        public required string type { get; set; }
        public required string data { get; set; }
        public required long[] files { get; set; }
        public required long[][] media { get; set; }
        public required Forward forward { get; set; }
    }
}