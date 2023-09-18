using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Web.UI.WebControls;

namespace GigaChat
{
    public partial class winRegister : Form
    {
        public winRegister()
        {
            InitializeComponent();
        }

        private void exitReg_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void exitReg_MouseHover(object sender, EventArgs e)
        {
            exitReg.BackColor = Color.FromArgb(63,72,204);
        }

        private void exitReg_MouseLeave(object sender, EventArgs e)
        {
            exitReg.BackColor = Color.FromArgb(0, 162, 232); 
        }

        private void registerReg_Click(object sender, EventArgs e)
        {
            Process.Start("https://gigachat.fun/");
        }

        private void registerReg_MouseHover(object sender, EventArgs e)
        {
            registerReg.Font = new Font("Comic Sans MS",8,FontStyle.Underline);
        }

        private void registerReg_MouseLeave(object sender, EventArgs e)
        {
            registerReg.Font = new Font("Comic Sans MS",8);
        }

        private void passwordVisCheckBox_MouseHover(object sender, EventArgs e)
        {
            passwordVisCheckBox.Font = new Font("Comic Sans MS", 8, FontStyle.Underline);
        }

        private void passwordVisCheckBox_MouseLeave(object sender, EventArgs e)
        {
            passwordVisCheckBox.Font = new Font("Comic Sans MS", 8);
        }

        private void passwordVisCheckBox_Click(object sender, EventArgs e)
        {
            passwordBoxReg.UseSystemPasswordChar = (!passwordBoxReg.UseSystemPasswordChar) ? true : false;
        }

        Point lastPoint;
        private void winRegister_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.Left += e.X - lastPoint.X;
                this.Top += e.Y - lastPoint.Y;
            }
        }
        private void winRegister_MouseDown(object sender, MouseEventArgs e)
        {
            lastPoint = new Point(e.X,e.Y);
        }
        //мозготрах тут:
        private void LOGINbuttonReg_Click(object sender, EventArgs e)
        {
            string LOGIN = loginBoxReg.Text;
            string PASSWORD = passwordBoxReg.Text;
            string token = GetToken(LOGIN, PASSWORD);
            if (token != null)
            {
                MessageBox.Show("Token: " + token);
                BaseForm baseForm = new BaseForm();
                this.Hide();
                baseForm.Show();
            }
        }

        public string GetToken(string login, string password)
        {
            // Формирование URL-адреса запроса
            string url = $"https://example.com?login={login}&password={password}";

            try
            {
                // Создание объекта WebClient для выполнения HTTP-запроса
                WebClient client = new WebClient();

                // Установка заголовка User-Agent для обхода блокировок
                client.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.3");

                // Выполнение HTTP-запроса и получение ответа в виде строки
                string response = client.DownloadString(url);

                // Обработка полученного ответа и извлечение токена
                string token = response.Split(':')[1].Replace("\"", "").Trim();

                return token;
            }
            catch (Exception ex)
            {
                // Обработка ошибок при выполнении запроса
                Console.WriteLine(ex.Message);
                MessageBox.Show("Error occurred while getting token");
                return null;
            }
        }
    }
}
