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
using System.Net;
using System.IO;

namespace GigaChat
{
    public partial class MainWindow : Window
    {
        float animationHideTime = 0.3f;

        public MainWindow()
        {
            InitializeComponent();
            string channels = HTTPRequests.Get("http://10.242.223.170:8084/user/@me/channels?client=5&token=Et9pMkeTo9AYVCeDmzEiLmaHxS5kxtvkqQAoXiGNnfR7nzX9");
            if(channels != "exception")
            {

            }
            else
            {
                MessageBox.Show("произошла неизвестная ошибка, попробуйте позже");
                Application.Current.Shutdown();
            }
        }


        private void OnWindowResize(object sender, SizeChangedEventArgs e)
        {
            ControlDataPanel.Height = this.Height- SystemWindowPanel.Height;
            ControlPanel.Height = this.Height - SystemWindowPanel.Height;
            ControlPanel.Width = this.Width *0.1;
            DataPanel.Height = this.Height - SystemWindowPanel.Height;
            DataPanel.Width = this.Width * 0.9;
            DataMesPanel.Height = this.Height - SystemWindowPanel.Height - DataNamePanel.Height;
            SystemTopPanel.Width = this.Width - 175;//- GigaChatLabel.Width - HideButton.Width - 10 - ModeButton.Width - 10 - ExitButton.Width
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
    }
    public static class HTTPRequests
    {
        public static string Get(string url)
        {
            string response;
            try
            {
                using (var client = new WebClient())
                {
                    response = client.DownloadString(url);
                }

                return response;
            }
            catch (WebException ex)
            {
                
                MessageBox.Show(ex.Message);
                return "exception";
            }
        }
    }
    // Класс для объекта с заголовком и текстом
    /*public class CustomObject : Grid
    {
        public CustomObject(string header, string text)
        {
            // Создание элементов для заголовка и текста
            var label = new Label()
            {
                Content = header,
                FontSize = 18,
                FontWeight = FontWeights.Bold,
                Margin = new Thickness(10)
            };

            var textBox = new TextBox()
            {
                Text = text,
                IsReadOnly = true,
                Margin = new Thickness(10)
            };

            // Добавление элементов в сетку
            Children.Add(label);
            Children.Add(textBox);

            // Установка свойств для панельки
            Background = Brushes.LightGray;
            Opacity = 0.5;

            // Подписка на события наведения курсора
            MouseEnter += (s, e) => { Opacity = 1; };
            MouseLeave += (s, e) => { Opacity = 0.5; };
        }
    }

    public class ChannelsCheck
    {
        private void AddObjectToLeft_Click(object sender, RoutedEventArgs e)
        {
            // Создание объекта
            var customObject = new CustomObject("Заголовок", "Текст");

            // Установка выравнивания для панельки
            customObject.HorizontalAlignment = HorizontalAlignment.Left;

            // Добавление объекта в StackPanel
            stackPanel.Children.Add(customObject);
        }

        private void AddObjectToRight_Click(object sender, RoutedEventArgs e)
        {
            // Создание объекта
            var customObject = new CustomObject("Заголовок", "Текст");

            // Установка выравнивания для панельки
            customObject.HorizontalAlignment = HorizontalAlignment.Right;

            // Добавление объекта в StackPanel
            stackPanel.Children.Add(customObject);
        }
    }*/
}
