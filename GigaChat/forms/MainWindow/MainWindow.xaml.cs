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

namespace GigaChat {
    public partial class MainWindow : Window
    {
        float animationHideTime = 0.3f;
        HashSet<ResizableUIElement> widgets = new HashSet<ResizableUIElement>();
        uint MessageCreatePanelHeight = 75;
        double HideButtonMarginLeft;

        public MainWindow()
        {
            InitializeComponent();
        }
        private void OnWindowResize(object sender, SizeChangedEventArgs e)
        {
            /*  ВЕРХНИЙ СЛОЙ  */
                // системная панель окна
                SystemWindowPanel.Height = 26;
                SystemWindowPanel.Width = this.Width;
                HideButtonMarginLeft = SystemWindowPanel.Width - (GigaChatLabel.Width + 10) - HideButton.Width - (ModeButton.Width + 10) - (ExitButton.Width + 10) - 20;
                HideButton.Margin = new Thickness(HideButtonMarginLeft,0,0,0);//left, top, right, bottom
                // общая панель каналов и сообщений
                ControlDataPanel.Height = this.Height - SystemWindowPanel.Height;
                ControlDataPanel.Width = this.Width;

                /*  ПАНЕЛИ КАНАЛОВ И СООБЩЕНИЙ  */

                    // общая панель каналов и настроек
                    ControlChannelPanel.Height = this.Height - SystemWindowPanel.Height;
                    ControlChannelPanel.Width = this.Width * 0.2;
                    /*  ПАНЕЛЬ КАНАЛОВ  */
                        // панель каналов
                        ChannelPanelScrollViewer.Height = ControlChannelPanel.Height - SettingsButton.Height - ChannelsLine.Height;
                        ChannelsPanel.Height = ControlChannelPanel.Height - SettingsButton.Height - ChannelsLine.Height;
                        ChannelPanelScrollViewer.Width = ControlChannelPanel.Width;
                        ChannelsPanel.Width = ControlChannelPanel.Width;
                        // виджеты каналов
                        foreach (var widget in widgets)
                        {
                            if (widget != null)
                                widget.resize(this.Width, this.Height);
                        }
                    

                    // общая панель создания и отправки сообщений
                    DataPanel.Height = this.Height - SystemWindowPanel.Height;
                    DataPanel.Width = this.Width * 0.8;
                    /*  ПАНЕЛЬ СООБЩЕНИЙ  */
                        // панель названия каналов
                        DataNamePanel.Height = 43;
                        DataNamePanel.Width = DataPanel.Width;
                        // панель создания сообщений
                        MessageCreatePanel.Height = MessageCreatePanelHeight;
                        MessageCreatePanel.Width = DataPanel.Width;
                        CreateMessageBorder.Height = MessageCreatePanelHeight - 35;
                        CreateMessageBorder.Width = MessageCreatePanel.Width - CreateMessageBorder.Margin.Left - CreateMessageBorder.Margin.Right - CreateMessageBorder.Padding.Left - CreateMessageBorder.Padding.Right;
                        // панель сообщений
                        DataMesPanel.Height = DataPanel.Height - DataNamePanel.Height - MessageCreatePanelHeight - 15;
                        DataMesPanel.Width = DataPanel.Width - 10;
                            //текстовая панель создания сообщения
                            MessageTextCreate.Height = MessageCreatePanel.Height - 9;
                            MessageTextCreate.Width = MessageCreatePanel.Width - 18;
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

        private void SystemWindowPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Normal;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                this.DragMove();
            }
        }

        private void MessageTextCreate_GotFocus(object sender, RoutedEventArgs e)
        {
            if(MessageTextCreate.Text == "Написать.....")
            {
                MessageTextCreate.Text = "";
                MessageTextCreate.Opacity = 1.0;
            }
        }

        private void MessageTextCreate_LostFocus(object sender, RoutedEventArgs e)
        {
            if(MessageTextCreate.Text == "")
            {
                MessageTextCreate.Text = "Написать.....";
                MessageTextCreate.Opacity = 0.4;
            }
        }

        private void CreateMessageBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageTextCreate.Focus();
            MessageTextCreate_GotFocus(sender, e);
        }


        private async void mainWindow_Initialized(object sender, EventArgs e)
        {
            bool isDLBOn = false;
            if (isDLBOn)
            {
                try
                {
                    DLBResponses.Success<Channel>? channelsResponse = await HTTPRequests.GetChannelsAsync();
                    if (channelsResponse == null) return;

                    List<StackChannel> channels = new List<StackChannel>();
                    foreach (var channelE in channelsResponse.data)
                    {
                        StackChannel channel = new StackChannel(
                            channelE.id,
                            channelE.userStatus,
                            channelE.title,
                            channelE.description,
                            channelE.isPublic,
                            channelE.enabled,
                            channelE.icon,
                            channelE.lastMessage
                        );
                        channels.Add(channel);
                        ChannelsPanel.Children.Add(channel);
                        widgets.Add(channel);
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
                List<StackChannel> channels = new List<StackChannel>();
                channels.Add(
                    new StackChannel
                    (
                        1,
                        1,
                        "title",
                        "description",
                        true,
                        true,
                        new CloudFile
                        {
                            id = 1,
                            uri = LocalData.DEFAULT_ICON_PATH
                        },
                        new ChannelLastMessage
                        {
                            id = 1,
                            author = "",
                            edited = false,
                            timestamp = "",
                            type = "system",
                            data = "",
                            files = new long[] { },
                            media = new long[][] { },
                            forward = new Forward
                            {
                                type = "no",
                                path = new long[] { }
                            }
                        }
                    )
                );
                channels.Add(
                    new StackChannel
                    (
                        2,
                        1,
                        "title1",
                        "description1",
                        true,
                        true,
                        new CloudFile
                        {
                            id = 2,
                            uri = LocalData.DEFAULT_ICON_PATH
                        },
                        new ChannelLastMessage
                        {
                            id = 2,
                            author = "",
                            edited = false,
                            timestamp = "",
                            type = "system",
                            data = "",
                            files = new long[] { },
                            media = new long[][] { },
                            forward = new Forward
                            {
                                type = "no",
                                path = new long[] { }
                            }
                        }
                    )
                );
                foreach(StackChannel channel in channels)
                {
                    ChannelsPanel.Children.Add(channel);
                    widgets.Add(channel);
                }
            }
        }
    }
}