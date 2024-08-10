using System.Windows.Media.Imaging;
using System.Windows.Controls;

namespace GigaChat
{
    public class ChannelCustomImage : Image
    {
        public Image? Icon;
        public ChannelCustomImage(string? _source)
        {
            Icon = new Image
            {
                Source = new BitmapImage(new Uri(_source == null ? LocalData.DEFAULT_ICON_PATH : _source))
            };
        }
        public ChannelCustomImage(Image? _icon)
        {
            Icon = _icon;
        }
        public ChannelCustomImage(CloudFile _icon)
        {
            Icon = new Image
            {
                Source = new BitmapImage(new Uri(_icon.uri == null ? LocalData.DEFAULT_ICON_PATH : _icon.uri))
            };
        }
    }
    public record class CloudFile
    {
        public required long id { get; set; }
        public string? uri { get; set; }
    }
    public record class Forward
    {
        public required string type { set; get; }
        public required long[] path { set; get; }
    }

    public enum MessageType
    {
        text,
        system,
        voice,
        audio,
        video
    }
}
