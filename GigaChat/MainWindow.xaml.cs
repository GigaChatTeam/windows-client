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
using UIWigets;

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
            ControlDataPanel.Height = this.Height - SystemWindowPanel.Height;
            ControlPanel.Height = this.Height - SystemWindowPanel.Height;
            ControlPanel.Width = this.Width * 0.2;
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
            for (int i = 9; i >= 0; i--)
            {
                Opacity = (float)i / 10.0;
                await Task.Delay((int)(animationHideTime * 100));
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
                    DLBResponses.Success<Channel>? channelsResponse = await HTTPRequests.GetChannelsAsync();
                    if (channelsResponse == null)
                        return;
                    //StackChannel[] channels = new StackChannel[channelsResponse.data.Length];
                    //for (int i = 0; i < channelsResponse.count; i++)
                    foreach (var channel in channelsResponse.data)
                    {
                        StackChannel chan = new StackChannel(
                            channel.id,
                            channel.title,
                            channel.description,
                            channel.enabled,
                            channel.icon,
                            channel.lastMessage
                        );
                        ChannelsPanel.Children.Add(chan);
                        widgets.Add(chan);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    Application.Current.Shutdown();
                }
            }
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
        public Forward? forward;
        public long[]? files;
        public long[][]? media;
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

    public class Forward
    {
        public string type { set; get; }
        public long[] path { set; get; }

        public Forward(string _type, long[] _path)
        {
            this.type = _type;
            this.path = _path;
        }
    }
}