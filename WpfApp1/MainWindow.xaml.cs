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

namespace GigaChat
{
    public partial class MainWindow : Window
    {
        float animationHideTime = 0.3f;
        HashSet<ResizableUIElement> widgets = new HashSet<ResizableUIElement>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void OnWindowResize(object sender, SizeChangedEventArgs e)
        {
            ControlDataPanel.Height = this.Height- SystemWindowPanel.Height;
            ControlPanel.Height = this.Height - SystemWindowPanel.Height;
            ControlPanel.Width = this.Width *0.2;
            DataPanel.Height = this.Height - SystemWindowPanel.Height;
            DataPanel.Width = this.Width * 0.9;
            DataMesPanel.Height = this.Height - SystemWindowPanel.Height - DataNamePanel.Height;
            SystemTopPanel.Width = this.Width - 175;
            ChannelsPanel.Width = ControlPanel.Width;
            ChannelsPanel.Height = ControlPanel.Height - ChannelsLine.Height - SettingsButton.Height - SystemWindowPanel.Height;
            ChannelPanelScrollViewer.Width = ChannelsPanel.Width;
            ChannelPanelScrollViewer.Height = ChannelsPanel.Height;

            foreach (var widget in widgets)
            {
                if (widget != null)
                    widget.resize(this.Width, this.Height);
            }
        }

        private void ExitButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }
        
        private async void HideButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            for(int i = 9; i>=0; i--)
            {
                Opacity = (float)i/10.0;
                await Task.Delay((int)(animationHideTime*100));
            }
            Opacity = 1;
            WindowState = WindowState.Minimized;
        }

        private void ModeButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                this.WindowStyle = WindowStyle.ThreeDBorderWindow;
                this.WindowState = WindowState.Normal;
                this.WindowStyle = WindowStyle.None;
            }
            else
            {
                this.WindowStyle = WindowStyle.ThreeDBorderWindow;
                this.WindowState = WindowState.Maximized;
                this.WindowStyle = WindowStyle.None;
            }
        }

        private async void mainWindow_Initialized(object sender, EventArgs e)
        {
            bool isDLBOn = false;
            if (isDLBOn)
            {
                try
                {
                    DLBResponses.Success<Channel> channelsResponse = await HTTP.GetChannelsAsync();
                    StackChannel[] channels = new StackChannel[channelsResponse.data.Length];
                    for (int i = 0; i < channels.Length; i++)
                    {
                        /*channels[i] = new StackChannel(
                            channelsResponse.data[i].id,
                            channelsResponse.data[i].title,
                            channelsResponse.data[i].description,
                            channelsResponse.data[i].enabled,
                            channelsResponse.data[i].lastMessage,
                            channelsResponse.data[i].icon
                        );
                        ChannelsPanel.Children.Add(channels[i]);
                        widgets.Add(channels[i]);*/
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Application.Current.Shutdown();
                }
            }
            else
            {
                /*StackChannel[] channels = new StackChannel[15]
                {
                    new StackChannel(1, "12345678901234567890", "descr", true, "wow", LocalData.CUSTOM_CHANNEL),
                    new StackChannel(1, "www", "descr", true, "wow", LocalData.CUSTOM_CHANNEL),
                    new StackChannel(1, "ardndz", "descr", true, "wow", LocalData.CUSTOM_CHANNEL),
                    new StackChannel(1, "AAABBBB", "descr", true, "wow", LocalData.CUSTOM_CHANNEL),
                    new StackChannel(1, "WWWLENINGRAD", "descr", true, "wow", LocalData.CUSTOM_CHANNEL),
                    new StackChannel(1, "WWWDOTRU", "descr", true, "wow", LocalData.CUSTOM_CHANNEL),
                    new StackChannel(1, "DADAS", "descr", true, "wow", LocalData.CUSTOM_CHANNEL),
                    new StackChannel(1, "DDOS", "descr", true, "wow", LocalData.CUSTOM_CHANNEL),
                    new StackChannel(1, "UKHAAN", "descr", true, "wow", LocalData.CUSTOM_CHANNEL),
                    new StackChannel(2, "парапапарам", "zeroDESC", true, "LOL", null),
                    new StackChannel(1, "UKHAAN", "descr", true, "wow", LocalData.CUSTOM_CHANNEL),
                    new StackChannel(1, "UKHAAN", "descr", true, "wow", LocalData.CUSTOM_CHANNEL),
                    new StackChannel(1, "UKHAAN", "descr", true, "wow", LocalData.CUSTOM_CHANNEL),
                    new StackChannel(1, "UKHAAN", "descr", true, "wow", LocalData.CUSTOM_CHANNEL),
                    new StackChannel(1, "UKHAAN", "descr", true, "wow", LocalData.CUSTOM_CHANNEL),
                };
                foreach(StackChannel channel in channels)
                {
                    ChannelsPanel.Children.Add(channel);
                    widgets.Add(channel);
                }*/
            }
        }
    } 

    interface ResizableUIElement
    {
        void resize(double Width, double Height);
    }

    public class HTTP
    {
        public static async Task<DLBResponses.Success<Channel>> GetChannelsAsync()
        {

            // Создание экземпляра HttpClient
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(LocalData.HTTP_URL);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    DLBResponses.Success<Channel> channelsData = JsonSerializer.Deserialize<DLBResponses.Success<Channel>>(json);

                    /*foreach (var channel in channelsData.data)
                    {
                        MessageBox.Show($"ID: {channel.id}\nTitle: {channel.title}\nDescription: {channel.description}\nEnabled: {channel.enabled}\nIcon: {channel.icon}");
                    }*/
                    return channelsData;
                }
                else
                {
                    MessageBox.Show($"Error: {response.StatusCode}");
                    return null;
                }
            }
        }
    }


    public class DLBResponses
    {
        public class Success<T>
        {
            public string status { set; get; }
            public int count { get; set; }
            public T[] data { set; get; }
        }
        public class AccessDenied
        {
            public string status { set; get; }
            public string description { set; get; }
        }
    }

    
    public class Channel
    {
        public long id { set; get; }
        public string title { set; get; }
        public string description { set; get; }
        public string lastMessage { set; get; }
        public bool enabled { set; get; }
        public string icon { set; get; }
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
        public StackChannel(long _id, string _title, string _description, bool _enabled, string _iconSource, long _iconId, string _lastMessageContent, long _lastMessageId, string _lastMessageAuthor, bool _lastMessageEdited, string _lastMessageTimeStamp, string _lastMessageType)
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

            icon = new ChannelCustomImage( _iconId, _iconSource );
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

            lastMessage = new ChannelLastMessage(_lastMessageId, _lastMessageAuthor, _lastMessageEdited, _lastMessageTimeStamp, _lastMessageType, _lastMessageContent);
            dataPanel.Children.Add(lastMessage);

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
                if ((int)(i*10) == 6)
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
    public class ChannelCustomImage : Image
    {
        public long id;
        Image icon;
        public ChannelCustomImage(long _id, string _source)
        {
            id = _id;
            icon = new Image
            {
                Source = new BitmapImage(new Uri(_source == null ? LocalData.DEFAULT_ICON_PATH : _source))
            };
        }
    }
    public class ChannelLastMessage : Label
    {
        public long id;
        public string author;
        public bool edited;
        public string timeStamp;
        public string type;
        public string content;
        //public File files;
        //public MediaElement[] media;
        //public XXtype forward;
        Label lastMessage;

        public ChannelLastMessage(long _id, string _author, bool _edited, string _timeStamp, string _type, string _content)
        {
            id = _id;
            author = _author;
            edited = _edited;
            timeStamp = _timeStamp;
            type = _type;
            content = _content;

            lastMessage = new Label()
            {
                Content = _content,
                FontSize = 10,
                FontFamily = new FontFamily("Comic Sans MS"),
                Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255))
            };
        }
    }
    public class Message
    {
        public long id;
        public string author;
        public bool edited;
        public string timeStamp;
        public string type;
        public string content;
        //public File files;
        //public MediaElement[] media;
        //public XXtype forward;
        public Message(long _id, string _author, bool _edited, string _timeStamp, string _type, string _content)
        {
            id = _id;
            author = _author;
            edited = _edited;
            timeStamp = _timeStamp;
            type = _type;
            content = _content;

            //тут создание сообщения
        }
        
    }
}
