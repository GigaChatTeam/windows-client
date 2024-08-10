
using System.Security.Policy;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Threading;
using UIWidgets;
using Websocket.Client;

namespace GigaChat
{
    class WebSockets
    {
        private static ManualResetEvent exitEvent = new ManualResetEvent(false);
        private static Uri url = new Uri("wss://demo.piesocket.com/v3/channel_123?api_key=VCXCEuvhGcBDP7XhiJJUDvR1e1D3eiVjgZ9VRiaV");
        private static WebsocketClient client = new WebsocketClient(url);


        public static void Connect()
        {
            initClient();

            Task.Run(() => client.Send("REQUEST"));

            exitEvent.WaitOne();
        }
        private static void initClient()
        {
            client.ReconnectTimeout = TimeSpan.FromSeconds(30);
            client.ReconnectionHappened.Subscribe(info =>
                Console.WriteLine($"Reconnection happened, type: {info.Type}"));

            client.MessageReceived.Subscribe(msg =>
            {
                Console.WriteLine(msg);
                Application.Current.Dispatcher.Invoke(() => ((MainWindow)System.Windows.Application.Current.MainWindow).MessagePanel.Children.Add(new StackMessage
                (
                    0,
                    0,
                    false,
                    1708077886279210,
                    MessageType.text,
                    Convert.ToString(msg) ?? "error!",
                    null,
                    null,
                    null
                )));
            });
            client.Start();
        }
    }
}
