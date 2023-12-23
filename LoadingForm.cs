using cypher;
using Microsoft.AspNet.SignalR.Client;
using ServiceStack.AsyncEx;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GigaChat
{
    public partial class LoadingForm : Form
    {
        int part;
        string token;
        long id;
        string path;
        string user;
        public LoadingForm(int _part, string _token, long _id)
        {
            InitializeComponent();
            part = _part;
            token = _token;
            id = _id;
        }
        private void LoadingForm_Load(object sender, EventArgs e)
        {
            switch (part)
            {
                case 1:
                    loadingBar.Value = 0;
                    //CYPHER(token, getPath());
                    addToBar(50);
                    RTCDconnection();
                    addToBar(40);
                    BaseForm baseForm = new BaseForm();
                    baseForm.Show();
                    this.Close();
                    break;
                default:
                    MessageBox.Show("backend error!");
                    break;
            }
        }
        public void RTCDconnection()
        {
            RtcConnection rtcConnection = new RtcConnection();
            Task.Run(async () => await rtcConnection.Connect($"ws://192.168.196.60:8080/?token={token}&id={id}")).WaitAndUnwrapException();
        }
        public string getPath()
        {
            path = Environment.GetEnvironmentVariable("APPDATA");

            if (string.IsNullOrEmpty(path))
            {
                MessageBox.Show("error to getting path");
            }
            return path;
        }
        /*public void CYPHER(string token, string path)
        {
            Cypher cypherizing = new Cypher(token, path);
            cypherizing.Set();
        }*/
        public void addToBar(int value)
        {
            if (value >= 0)
            {
                for (int i = 0; i < value; i++)
                {
                    Thread.Sleep(10);
                    loadingBar.Value += 10;
                }
            }
            else
            {
                for (int i = 0; i > value; i--)
                {
                    Thread.Sleep(10);
                    loadingBar.Value -= 10;
                }
            }
        }
    }

    public class RtcConnection
    {
        private HubConnection _connection;
        private IHubProxy _proxy;

        public async Task Connect(string serverUrl)
        {
            _connection = new HubConnection(serverUrl);
            _proxy = _connection.CreateHubProxy("RtcHub");

            // Определите методы, которые будут вызываться с сервера
            _proxy.On<string, string>("ReceiveMessage", (user, message) => MessageBox.Show($"{user}: {message}"));

            // Начать подключение
            await _connection.Start();
        }

        public async Task SendMessage(string user, string message)
        {
            // Вызов метода на сервере
            await _proxy.Invoke("SendMessage", user, message);
        }
    }
}
