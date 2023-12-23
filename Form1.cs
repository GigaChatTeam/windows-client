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
using Newtonsoft.Json;
using System.Web.Services.Description;
using ServiceStack;
using Fizzler;
using ServiceStack.Script;
using System.Drawing.Drawing2D;

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
            exitReg.BackColor = Color.FromArgb(63, 72, 204);
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
            registerReg.Font = new Font("Comic Sans MS", 8, FontStyle.Underline);
        }

        private void registerReg_MouseLeave(object sender, EventArgs e)
        {
            registerReg.Font = new Font("Comic Sans MS", 8);
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
            lastPoint = new Point(e.X, e.Y);
        }
        //мозготрах тут:
        private async void LOGINbuttonReg_Click(object sender, EventArgs e)
        {
            try
            {
                string LOGIN = loginBoxReg.Text;
                string PASSWORD = passwordBoxReg.Text;
                if (validateLP(LOGIN, PASSWORD))
                {
                    string response = await (await GetToken(LOGIN, PASSWORD)).Content.ReadAsStringAsync();
                    MessageBox.Show(response);
                    AuthPacket data = JsonConvert.DeserializeObject<AuthPacket>(response);

                    LoadingForm loadingForm = new LoadingForm(1, data.data.token, data.data.id);
                    this.Hide();
                    loadingForm.Show();
                }
                else
                { 
                    MessageBox.Show("недопустимый логин или пароль, \nвозможно одно из полей не заполнено\nтакже возможно ваш пароль содержит не только символы кириллицы или латиницы,а также символы !@#$%^&*");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"при входе возникла непредвиденная ошибка:\n" + ex.Message);
            }
        }
        public bool validateLP(string login, string password)
        {
            // Проверяем, что обе строки не пустые
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(password))
            {
                return false;
            }

            // Проверяем, что в обеих строках есть символы
            if (login.Length == 0 || password.Length == 0)
            {
                return false;
            }

            // Проверяем, что пароль содержит только допустимые символы
            foreach (char c in password)
            {
                if (!char.IsLetterOrDigit(c) && c != '!' && c != '@' && c != '#' && c != '$' && c != '%' && c != '^' && c != '&' && c != '*')
                {
                    return false;
                }
            }

            return true;
        }
        public async Task<HttpResponseMessage> GetToken(string login, string password)
        {
            // Формирование URL-адреса запроса
            string url = "http://10.242.223.170:8084/auth" + $"?username={login}";

            try
            {
                // Создание объекта WebClient для выполнения HTTP-запроса
                HttpClient client = new HttpClient();
                Dictionary<string, string> contentData = new Dictionary<string, string>() {
                    { "username", login },
                    { "password", password }
                };
                FormUrlEncodedContent content = new FormUrlEncodedContent(contentData);
                client.DefaultRequestHeaders.Add("user-agent", "Windows Desktop Client v0.1");

                // Выполнение HTTP-запроса и получение ответа в виде строки
                return await client.PostAsync(url, content);
            }
            catch (Exception ex)
            {
                // Обработка ошибок при выполнении запроса
                MessageBox.Show($"ошибка получения токена:\n{ex}");
                return null;
            }
        }
    }
    public class AuthPacket
    {
        public string status;
        public authDataPacket data;
    }
    public class authDataPacket
    {
        public long id;
        public string token;
        public string username;
    }
}
