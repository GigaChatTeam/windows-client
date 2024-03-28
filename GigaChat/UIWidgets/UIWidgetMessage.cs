using GigaChat;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;

namespace UIWidgets
{
    public class StackMessage : StackPanel
    {
        public long id;
        public Label author;
        public bool isEdited;
        public long timeStamp;
        public string type;
        public Label content;
        public long[] files;
        public long[,] media;
        public Forward forward;
        public StackMessage
        (
            long _id,
            long _author,
            bool _isEdited,
            long _timestamp,
            string _type,
            string _content,
            long[] _files,
            long[,] _media,
            Forward _forward
        )
        {
            id = _id;
            author = new Label()
            {
                Content = _author,
                FontSize = 14,
                FontFamily = new FontFamily("Comic Sans MS"),
                Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255))
            };
            isEdited = _isEdited;
            timeStamp = _timestamp;
            type = _type;
            content = new Label()
            {
                Content = _content,
                FontSize = 10,
                FontFamily = new FontFamily("Comic Sans MS"),
                Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255))
            };
            files = _files;
            forward = _forward;

        }
    }
}
