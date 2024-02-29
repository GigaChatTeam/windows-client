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
        public required string title { set; get; }
        public required string description { set; get; }
        public required ChannelLastMessage lastMessage { set; get; }
        public required bool enabled { set; get; }
        public required CloudFile icon { set; get; }
    }
    public class StackChannel : StackPanel, ResizableUIElement
    {        
        public long id;
        public int user_status;
        public Label title;
        public Label description;
        public bool isPublic;
        public bool enabled;
        public ChannelCustomImage icon;
        public ChannelLastMessage lastMessage;

        StackPanel metaPanel;
        StackPanel dataPanel;
        int IventNow = 0;
        public bool isActive = false;
        public StackChannel(long _id, string _title, string _description, bool _enabled, CloudFile _icon, ChannelLastMessage _lastMessage)
        {
            id = _id;
            enabled = _enabled;

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

            icon = new ChannelCustomImage( _icon.uri );
            metaPanel.Children.Add(icon);
            Height = 50;
            Orientation = Orientation.Horizontal;

            title = new Label()
            {
                Content = _title,
                FontSize = 14,
                FontFamily = new FontFamily("Comic Sans MS"),
                Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255))
            };
            dataPanel.Children.Add(title);

            description = new Label()
            {
                Content = _description,
                FontSize = 10,
                FontFamily = new FontFamily("Comic Sans MS"),
                Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255))
            };
            lastMessage = _lastMessage;

            //lastMessage = new ChannelLastMessage();
            //dataPanel.Children.Add(lastMessage);

            this.HorizontalAlignment = HorizontalAlignment.Center;
            this.Margin = new Thickness(0, 0, 0, 0); //left, top, right, bottom
            this.Children.Add(metaPanel);
            this.Children.Add(dataPanel);

            Rectangle horizontalFillRectangle = new Rectangle();
            horizontalFillRectangle.Width = 200;
            horizontalFillRectangle.Height = 100;

            LinearGradientBrush Gradient = new LinearGradientBrush()
            {
                StartPoint = new Point(0, 0.5),
                EndPoint = new Point(1, 0.5)
            };

            Gradient.GradientStops.Add(new GradientStop(Color.FromRgb(30, 30, 30), 0.0));
            Gradient.GradientStops.Add(new GradientStop(Color.FromRgb(0, 255, 243), 1));


            Background = Gradient;
            Opacity = 0.6;
            MouseEnter += SP_MouseEnter;
            MouseLeave += SP_MouseLeave;
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
                icon. Height = metaPanel.Height;
            }
            else
            {
                icon.Width = metaPanel.Width;
                icon.Height = metaPanel.Width;
            }
        }
    }
    
    public record class CloudFile
    {
        public required long id { get; set; }
        public required string uri { get; set; }
    }
    public record class ChannelLastMessage
    {
        public required long id { get; set; }
        public required string author { get; set; }
        public required bool edited { get; set; }
        public required string timeStamp { get; set; }
        public required string type { get; set; }
        public required string content { get; set; }
        public required Forward forward { get; set; }
        public required long[] files { get; set; }
        public required long[][] media { get; set; }


        public record class Forward
        {
            public required string type { set; get; }
            public required long[] path { set; get; }
        }
    }
}