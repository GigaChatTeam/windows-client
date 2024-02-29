using GigaChat;
using System;
using System.Windows.Media.Imaging;
using System.Windows.Controls;

namespace UIWigets
{
    public class ChannelCustomImage : Image
    {
        Image? icon;
        private ChannelCustomImage(Image _icon)
        {
            icon = _icon;
        }
        public ChannelCustomImage(string _source) : this(
           new Image
           {
               Source = new BitmapImage(new Uri(_source == null ? LocalData.DEFAULT_ICON_PATH : _source))
           })
        { }
        public ChannelCustomImage(CloudFile _icon) : this( _icon.uri)
        {   }
    }
}
