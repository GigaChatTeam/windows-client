using System.Net.Http;
using System.Windows;

namespace GigaChat
{
    class ReusableTokenRequest
    {
        private static readonly HttpClient client = new HttpClient();
        public static async void GetReusableToken()
        {
            if(Properties.Settings.Default.ReusableToken == "" )
            {
                Dictionary<string, string> values = new Dictionary<string, string>
                {
                    { "LOGIN", "hello" },
                    { "PASSWORD", "world" }
                };

                FormUrlEncodedContent content = new FormUrlEncodedContent(values);

                HttpResponseMessage response = await client.PostAsync("https://postman-echo.com/post", content);

                string TOKEN = await response.Content.ReadAsStringAsync();
                MessageBox.Show(TOKEN);
                Properties.Settings.Default.ReusableToken = TOKEN;
                MessageBox.Show(Properties.Settings.Default.ReusableToken); 
                Properties.Settings.Default.Save();
            }
            
        }
    }
}
