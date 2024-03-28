using GigaChat;
using System;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;
using System.Windows.Shapes;
using System.Configuration;
using System.Net;

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
        int IventNow = 0; StackPanel dataPanel;

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
                FontSize = 10,
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
                metaPanel.Children.Add(icon);
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
                Gradient.GradientStops.Add(new GradientStop(Color.FromRgb(30, 30, 30), 0.0));
                Gradient.GradientStops.Add(new GradientStop(Color.FromRgb(0, 255, 243), 1));
            //============- SettingStackPanel -=============>
                HorizontalAlignment = HorizontalAlignment.Center;
                Margin = new Thickness(0, 0, 0, 0); //left, top, right, bottom
                Orientation = Orientation.Horizontal;
                Background = Gradient;
                Height = 50;
                Opacity = 0.6;
                MouseEnter += SP_MouseEnter;
                MouseLeave += SP_MouseLeave;
                MouseLeftButtonDown += SP_MouseLeftButtonDown;
        }

        private void SP_MouseLeftButtonDown(object sender, MouseEventArgs e)
        {
            //ChannelsPanel.Children.Clear();
            //deselect all channels
            /*
            ChannelPanel.Background = new LinearGradientBrush()
            {
                StartPoint = new Point(0, 0.5),
                    EndPoint = new Point(1, 0.5)
            };
            Gradient.GradientStops.Add(new GradientStop(Color.FromRgb(150, 150, 150), 0.0));
            Gradient.GradientStops.Add(new GradientStop(Color.FromRgb(0, 255, 243), 1));
            */
            //DataNamePanel = Channel.icon;
            //MessagePanel_ChannelName.Text = Channel.Title;
            //request
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
