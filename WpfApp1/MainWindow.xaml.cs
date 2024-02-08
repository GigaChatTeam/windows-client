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
//using Newtonsoft.Json;
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
            ControlPanel.Width = this.Width *0.1;
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
                /*Channel[] channelsResponse = */await HTTP.GetChannelsAsync();
                /*StackChannel[] channels = new StackChannel[channelsResponse.Length];
                for(int i = 0; i < channelsResponse.Length; i++)
                {
                    channels[i].id = channelsResponse[i].id;
                    channels[i].title = channelsResponse[i].title;
                    channels[i].description = channelsResponse[i].description;
                    channels[i].created = channelsResponse[i].created;
                    channels[i].enabled = channelsResponse[i].enabled;
                    channels[i].lastmessage = channelsResponse[i].lastmessage;
                    channels[i].icon = channelsResponse[i].icon;
                }
                foreach(StackChannel channel in channels)
                {
                    ChannelsPanel.Children.Add(channel);
                }*/
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                Application.Current.Shutdown();
            }
        }
    } 

    public class HTTP
    {
        public static async Task<ChannelsData> GetChannelsAsync()
        {
            var url = "http://10.242.223.170:8084/user/@me/channels?client=5&token=Et9pMkeTo9AYVCeDmzEiLmaHxS5kxtvkqQAoXiGNnfR7nzX9&sort=id&order=asc&meta=true"; // Заменить на свой URL

            // Создание экземпляра HttpClient
            using (var client = new HttpClient())
            {
                // Отправка GET-запроса и получение ответа
                var response = await client.GetAsync(url);

                // Проверка статуса ответа
                if (response.IsSuccessStatusCode)
                {
                    // Преобразование ответа в строку JSON
                    var json = await response.Content.ReadAsStringAsync();

                    // Десериализация JSON-данных в объекты классов
                    var channelsData = JsonSerializer.Deserialize<ChannelsData>(json);

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

    public class ChannelsData
    {
        public string status { set; get; }
        public int count { get; set; }
        public Channel[] data { set; get; }
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

    // Класс для объекта с заголовком и текстом
    public class StackChannel : Grid
    {
        public int id;
        public string title;
        public string description;
        public bool enabled;
        public string lastMessage;
        public string icon;
        public StackChannel(int _id,string _title,string _description,bool _enabled,string _lastmessage, string _icon)
        {
            id = _id;
            title = _title;
            description = _description;
            enabled = _enabled;
            lastMessage = _lastmessage;
            icon = _icon;

            // Создание элементов для заголовка и текста
            Label label = new Label()
            {
                Content = _title,
                FontSize = 18,
                FontFamily = new System.Windows.Media.FontFamily("Comic Sans MS"),
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(10)
            };
            StackPanel panel = new StackPanel();

            // Добавление элементов в сетку
            Children.Add(panel);
            Children.Add(label);
            // Установка свойств для панельки
            Background = new SolidColorBrush(Color.FromRgb(25, 25, 25)); ;
            Opacity = 0.5;

            // Подписка на события наведения курсора
            MouseEnter += (s, e) => { Opacity = 1; };
            MouseLeave += (s, e) => { Opacity = 0.6; };
        }
    }
}
