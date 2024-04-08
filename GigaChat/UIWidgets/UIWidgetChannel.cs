using GigaChat;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;
using UIWidgets;
using System.Windows.Documents;

namespace UIWigets
{
    public class StackChannel : StackPanel, ResizableUIElement
    {
        public long id;
        public int userStatus;
        public Label title;
        public Label description;
        public bool isPublic;
        public bool enabled;
        public ChannelCustomImage icon;
        public ChannelLastMessage? lastMessageData;
        public Label lastMessage;

        StackPanel metaPanel;
        StackPanel dataPanel;
        int IventNow = 0;

        public bool isActive = false;
        public StackChannel
        (
            long _id,
            int _userStatus,
            string _title,
            string _description,
            bool _isPublic,
            bool _enabled,
            CloudFile? _icon,
            ChannelLastMessage? _lastMessageData
        )
        {
            id = _id;
            userStatus = _userStatus;
            title = new Label()
            {
                Content = _title,
                FontSize = 14,
                FontFamily = new FontFamily("Comic Sans MS"),
                Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255))
            };
            description = new Label()
            {
                Content = _description,
                FontSize = 12,
                FontFamily = new FontFamily("Comic Sans MS"),
                Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255))
            };
            isPublic = _isPublic;
            enabled = _enabled;
            if (_icon != null) 
                icon = new ChannelCustomImage(_icon.uri);
            else 
                icon = new ChannelCustomImage(LocalData.DEFAULT_ICON_PATH);
            lastMessageData = _lastMessageData;
            lastMessage = new Label()
            {
                Content = _lastMessageData,
                FontSize = 12,
                FontFamily = new FontFamily("Comic Sans MS"),
                Foreground = new SolidColorBrush(Color.FromRgb(200, 255, 255))
            };



            //============- Hierarchy -=====================>
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
                    Width = 100
                };
                Children.Add(metaPanel);
                metaPanel.Children.Add(icon.Icon);
                Children.Add(dataPanel);
                dataPanel.Children.Add(title);
                dataPanel.Children.Add(lastMessage);
            
            /*
            Rectangle horizontalFillRectangle = new Rectangle();
            horizontalFillRectangle.Width = 200;
            horizontalFillRectangle.Height = 100;
            */

            //============- GradientStackPanel -============>
                LinearGradientBrush Gradient = new LinearGradientBrush()
                {
                    StartPoint = new Point(0, 0.5),
                    EndPoint = new Point(1, 0.5)
                };
                Gradient.GradientStops.Add(new GradientStop(Color.FromRgb(0, 30, 30), 0.0));
                Gradient.GradientStops.Add(new GradientStop(Color.FromRgb(0, 200, 255), 1));
            //============- SettingStackPanel -=============>
                HorizontalAlignment = HorizontalAlignment.Center;
                Margin = new Thickness(0, 0, 0, 0); //left, top, right, bottom
                Orientation = Orientation.Horizontal;
                Background = Gradient;
                Height = 50;
                Opacity = 0.6;
                MouseEnter += SP_MouseEnter;
                MouseLeave += SP_MouseLeave;
                MouseLeftButtonDown += (sender, e) => SP_MouseLeftButtonDown(sender, null);
        }

        private void SP_MouseLeftButtonDown(object sender, MouseEventArgs? e)
        {
            ((MainWindow)System.Windows.Application.Current.MainWindow).MessagePanel.Children.Clear();

            foreach(StackPanel channel in ((MainWindow)System.Windows.Application.Current.MainWindow).ChannelsPanel.Children)
            {
                LinearGradientBrush Gradient = new LinearGradientBrush()
                {
                    StartPoint = new Point(0, 0.5),
                    EndPoint = new Point(1, 0.5)
                };
                Gradient.GradientStops.Add(new GradientStop(Color.FromRgb(0, 30, 30), 0.0));
                Gradient.GradientStops.Add(new GradientStop(Color.FromRgb(0, 200, 255), 1));
                channel.Background = Gradient;
            }

            LinearGradientBrush BG = new LinearGradientBrush(){
                StartPoint = new Point(0, 0.5),
                EndPoint = new Point(1, 0.5)
            };
            BG.GradientStops.Add(new GradientStop(Color.FromRgb(0, 30, 30), 0.0));
            BG.GradientStops.Add(new GradientStop(Color.FromRgb(0, 255, 200), 1));
            Background = BG;
            Opacity = 1;

            ((MainWindow)System.Windows.Application.Current.MainWindow).MessagePanel_ChannelAvatar.Source = this.icon.Source;
            ((MainWindow)System.Windows.Application.Current.MainWindow).MessagePanel_ChannelName.Content = this.title.Content;
            //request
            List<StackMessage> messages = new List<StackMessage>();

            messages.Add(
                new StackMessage
                (
                    0,
                    0,
                    false,
                    1708077886279210,
                    MessageType.system,
                    "----------< КАНАЛ СОЗДАН >----------",
                    null,
                    null,
                    null
                )
            );
            messages.Add(
                new StackMessage
                (
                    1,
                    5,
                    false,
                    1708077886279220,
                    MessageType.text,
                    "Первое сообщение для теста",
                    null,
                    null,
                    null
                )
            );
            foreach (StackMessage message in messages)
            {
                ((MainWindow)System.Windows.Application.Current.MainWindow).MessagePanel.Children.Add(message);
            }
        }

        private async void SP_MouseEnter(object sender, MouseEventArgs e)
        {
            if (IventNow == 0)
            {
                IventNow = 1;
                for (float i = 0.7f; i <= 1.0; i += 0.05f)
                {
                    if (IventNow != 1 || isActive) break;
                    Opacity = i;
                    await Task.Delay(20);
                    if ((int)(i * 10) == 1)
                    {
                        IventNow = 0;
                    }
                    Opacity = 1.0;
                }

            }
        }

        private async void SP_MouseLeave(object sender, MouseEventArgs e)
        {
            IventNow = 2;
            for (float i = 1.0f; i >= 0.6; i -= 0.05f)
            {
                if (IventNow != 2 || isActive) break;
                Opacity = i;
                await Task.Delay(20);
                if ((int)(i * 10) == 6)
                {
                    IventNow = 0;
                }
            }
            Opacity = 0.6;
        }

        public void resize(double Width, double Height)
        {
            this.Width = Width * 0.2;
            metaPanel.Width = this.Width * 0.3;
            dataPanel.Width = this.Width * 0.7;

            if (metaPanel.Height < metaPanel.Width)
            {
                icon.Width = metaPanel.Height;
                icon.Height = metaPanel.Height;
            }
            else
            {
                icon.Width = metaPanel.Width;
                icon.Height = metaPanel.Width;
            }
        }
    }
}
