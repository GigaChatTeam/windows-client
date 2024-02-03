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
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Логика взаимодействия для LoaderWindow.xaml
    /// </summary>
    public partial class LoaderWindow : Window
    {
        public LoaderWindow()
        {
            InitializeComponent();
        }

        public async void loadProgress_complete(object sender, RoutedEventArgs e)
        {
            loaderBar.Value = 0;
            for (int i = 1; i < 101; i++)
            {
                loaderBar.Value = i;
                await Task.Delay(10);
            }
            MainWindow MainForm = new MainWindow();
            MainForm.Show();
            this.Visibility = Visibility.Hidden;
        }
    }
}
