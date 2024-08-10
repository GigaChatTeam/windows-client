using GigaChat;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Media.Imaging;

namespace UIWidgets
{
    public class StackMessage : StackPanel
    {
        public long id;
        public Label author;
        public bool isEdited;
        public long timeStamp;
        /*public CloudFile icon = new CloudFile(){
            1,
            LocalData.DEFAULT_AUTHOR_ICON_PATH
        };*/
        public MessageType type;
        public Label content;
        public long[]? files;
        public long[,]? media;
        public Forward? forward;

        StackPanel? metaPanel;
        StackPanel? dataPanel;
        public StackMessage
        (
            long _id,
            long _author,
            bool _isEdited,
            long _timestamp,
            MessageType _type,
            string _content,
            long[]? _files,
            long[,]? _media,
            Forward? _forward
        )
        {
            id = _id;
            author = new Label()
            {
                Content = "ZODIAC",
                Margin = new Thickness(6,3,0,0),
                Padding = new Thickness(0),
                FontSize = 14,
                FontWeight = FontWeights.Bold,
                FontFamily = new FontFamily("Comic Sans MS"),
                Foreground = new SolidColorBrush(Color.FromRgb(255, 0, 0))
            };
            isEdited = _isEdited;
            timeStamp = _timestamp;
            type = _type;
            content = new Label()
            {
                Content = _content,
                Margin = new Thickness(6,0,0,0),
                Padding = new Thickness(0),
                FontSize = 14,
                FontFamily = new FontFamily("Comic Sans MS"),
                Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255))
            };
            files = _files;
            forward = _forward;

            if(type == MessageType.text)
            {
                Orientation = Orientation.Horizontal;
                metaPanel = new StackPanel()
                {
                    Orientation = Orientation.Vertical,
                    Height = 50,
                    Width = 50
                };
                dataPanel = new StackPanel()
                {
                    Orientation = Orientation.Vertical,
                    Height = 50,
                    Width = ((MainWindow)Application.Current.MainWindow).MessagePanel.Width - metaPanel.Width - 40
                };



                Children.Add( metaPanel );
                metaPanel.Children.Add(new Image
                {
                    Source = new BitmapImage(new Uri(LocalData.DEFAULT_AUTHOR_ICON_PATH))
                });
                Children.Add( dataPanel );
                dataPanel.Children.Add(author);
                dataPanel.Children.Add(content);

                this.Background = new SolidColorBrush(Color.FromRgb(30, 30, 30));
                this.Height = 50;
            }
            else if(type == MessageType.system)
            {
                HorizontalAlignment = HorizontalAlignment.Center;
                this.Background = new SolidColorBrush(Color.FromRgb(43, 43, 43));
                this.Height = 50;
                this.Children.Add(new Label()
                {
                    Content = _content,
                    FontSize = 20,
                    FontFamily = new FontFamily("Comic Sans MS"),
                    Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255))
                });
            }
        }
    }
}
