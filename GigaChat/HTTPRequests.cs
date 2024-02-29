using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Net.Http;
using System.Text.Json;

namespace GigaChat
{
    public class HTTPRequests
    {
        public static async Task<DLBResponses.Success<Channel>?> GetChannelsAsync()
        {

            // Создание экземпляра HttpClient
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(LocalData.HTTP_URL);
                if (response.IsSuccessStatusCode)
                {
                    string? json = await response.Content.ReadAsStringAsync();
                    if (json == null)
                    {
                        MessageBox.Show($"Error: {response.StatusCode}");
                        return null;
                    }
                    return JsonSerializer.Deserialize<DLBResponses.Success<Channel>>(json);
                }
                else
                {
                    MessageBox.Show($"Error: {response.StatusCode}");
                    return null;
                }
            }
        }
    }
}
