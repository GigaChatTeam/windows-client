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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net.Http;
using System.IO;
using System.Text.Json;
using UIWigets;

namespace GigaChat
{
    public partial class LoaderWindow : Window
    {
        [STAThread]
        public static void Main()
        {
            var app = new Application();
            app.Run(new LoaderWindow());
        }
        public LoaderWindow()
        {
            InitializeComponent();
        }
        public async void loadProgress_complete(object sender, RoutedEventArgs e)
        {
            loaderBar.Value = 0;
            for (int i = 1; i < 101; i++)
            {
                if(i == 60)
                {
                    HTTPRequests.UpdateAuthorizationData(LocalData.CLIENT_ID, LocalData.CLIENT_SECRET, LocalData.CLIENT_KEY);
                }
                loaderBar.Value = i;
                await Task.Delay(10);
            }
            MainWindow MainForm = new MainWindow();
            MainForm.Show();
            this.Visibility = Visibility.Hidden;
        }
    }
}
