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
            
            foreach(var widget in widgets)
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
                WindowState = WindowState.Normal;
            }
            else
            {
                WindowState = WindowState.Maximized;
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
                        channels[i] = new StackChannel(
                            channelsResponse.data[i].id,
                            channelsResponse.data[i].title,
                            channelsResponse.data[i].description,
                            channelsResponse.data[i].enabled,
                            channelsResponse.data[i].lastMessage,
                            channelsResponse.data[i].icon
                        );
                        ChannelsPanel.Children.Add(channels[i]);
                        widgets.Add(channels[i]);
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
                StackChannel channel = new StackChannel(1, "12345678901234567890", "descr", true, "wow", LocalData.CUSTOM_CHANNEL);
                ChannelsPanel.Children.Add(channel);
                widgets.Add(channel);
                StackChannel channel1 = new StackChannel(2, "парапапарам", "zeroDESC", true, "LOL", null);
                ChannelsPanel.Children.Add(channel1);
                widgets.Add(channel1);
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
        public Label title;
        public Label description;
        public bool enabled;
        public string lastMessage;
        public Image icon;
        StackPanel metaPanel;
        StackPanel dataPanel;
        public StackChannel(long _id, string _title, string _description, bool _enabled, string _lastmessage, string _icon)
        {
            id = _id;
            enabled = _enabled;
            lastMessage = _lastmessage;


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

            icon = new Image
            {
                Source = new BitmapImage(new Uri(_icon == null ? LocalData.DEFAULT_ICON_PATH : _icon))
            };
            metaPanel.Children.Add(icon);
            Height = 50;
            Orientation = Orientation.Horizontal;

            title = new Label()
            {
                Content = _title,
                FontSize = 18,
                FontFamily = new FontFamily("Comic Sans MS"),
                Foreground = new SolidColorBrush(Color.FromRgb(255, 255, 255))
            };
            dataPanel.Children.Add(title);

            this.HorizontalAlignment = HorizontalAlignment.Center;
            this.Margin = new Thickness(0, 10, 0, 10); //left, top, right, bottom
            this.Children.Add(metaPanel);
            this.Children.Add(dataPanel);

            Background = new SolidColorBrush(Color.FromRgb(30, 30, 30));
            Opacity = 0.6;
            MouseEnter += (s, e) => { Opacity = 1; };
            MouseLeave += (s, e) => { Opacity = 0.6; };
        }
        
        public void resize(double Width, double Height)
        {
            this.Width = Width * 0.15;
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
