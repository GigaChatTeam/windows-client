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
            ChannelsPanel.Width = this.Width - 99;
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
            try
            {
                /*DLBResponses.Success<Channel> channelsResponse = await HTTP.GetChannelsAsync();
                StackChannel[] channels = new StackChannel[channelsResponse.data.Length];
                for(int i = 0; i < channels.Length; i++)
                {
                    channels[i] = new StackChannel(
                        channelsResponse.data[i].id,
                        channelsResponse.data[i].title,
                        channelsResponse.data[i].description,
                        channelsResponse.data[i].enabled,
                        channelsResponse.data[i].lastMessage,
                        channelsResponse.data[i].icon
                    );
                    if (channels[i].enabled)
                    {
                        ChannelsPanel.Children.Add(channels[i]);
                    }
                }*/
                StackChannel channel = new StackChannel(1, "Chan", "descr",true,"wow",null);
                if (channel.enabled)
                {
                    channel.Setting();
                    ChannelsPanel.Children.Add(channel);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                Application.Current.Shutdown();
            }
        }
    } 

    public class HTTP
    {
        public static async Task<DLBResponses.Success<Channel>> GetChannelsAsync()
        {
            var url = "http://10.242.223.170:8084/user/@me/channels?client=5&token=Et9pMkeTo9AYVCeDmzEiLmaHxS5kxtvkqQAoXiGNnfR7nzX9&sort=id&order=asc&meta=true"; // Заменить на свой URL

            // Создание экземпляра HttpClient
            using (var client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                if (response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    DLBResponses.Success<Channel> channelsData = JsonSerializer.Deserialize<DLBResponses.Success<Channel>>(json);

                    foreach (var channel in channelsData.data)
                    {
                        MessageBox.Show($"ID: {channel.id}\nTitle: {channel.title}\nDescription: {channel.description}\nEnabled: {channel.enabled}\nIcon: {channel.icon}");
                    }
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

    public class StackChannel : Grid
    {
        public long id;
        public string title;
        public string description;
        public bool enabled;
        public string lastMessage;
        public string icon;
        public StackChannel(long _id,string _title,string _description,bool _enabled,string _lastmessage, string _icon)
        {
            id = _id;
            title = _title;
            description = _description;
            enabled = _enabled;
            lastMessage = _lastmessage;
            icon = _icon;
        }

        public void Setting()
        {
            Border border = new Border()
            {
                CornerRadius = new CornerRadius(50)
            };
            StackPanel VJPanel = new StackPanel();
            VJPanel.Orientation = Orientation.Horizontal;
            StackPanel SubPanel = new StackPanel();
            VJPanel.Orientation = Orientation.Vertical;
            StackPanel SubPanel1 = new StackPanel();
            VJPanel.Orientation = Orientation.Vertical;

            Image ico = new Image();
            ico.Source = new BitmapImage(new Uri(icon == null ? @"D:\Vadim\disk_X\GIT\windows-client\resourses\zeroImage.png" : icon));
            ico.Width = 50;
            ico.Height = 50;

            Label Title = new Label()
            {
                Content = title,
                FontSize = 18,
                FontFamily = new System.Windows.Media.FontFamily("Comic Sans MS"),
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(10)
            };
            Label Description = new Label()
            {
                Content = description,
                FontSize = 10,
                FontFamily = new System.Windows.Media.FontFamily("Comic Sans MS"),
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(5),
                Opacity = 0.7
            };

            this.Margin = new Thickness(0, 10, 0, 0);
            Children.Add(border);
            border.Child = VJPanel;
            VJPanel.Children.Add(SubPanel);
            SubPanel.Children.Add(ico);
            VJPanel.Children.Add(SubPanel1);
            SubPanel1.Children.Add(Title);
            SubPanel1.Children.Add(Description);
            /*
            |--------------------|
            | image | title      |
            |       | description|
            |_______|____________|
             */

            Background = new SolidColorBrush(Color.FromRgb(20, 20, 20));
            Opacity = 0.5;
            MouseEnter += (s, e) => { Opacity = 1; };
            MouseLeave += (s, e) => { Opacity = 0.6; };
        }
    }
}
