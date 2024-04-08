using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Net.Http;
using System.Text.Json;
using System.Net;
using System.Net.Http.Headers;


namespace GigaChat
{
    public class HTTPChannelRequests
    {
        private static HttpClient client = new HttpClient();
        
        private static long clientID = 0;
        private static string clientSecret = "";
        private static string clientKey = "";

        public static void UpdateAuthorizationData(long ID, string secret, string key)
        {
            clientID = ID;
            clientSecret = secret;  
            clientKey = key;

            client.DefaultRequestHeaders.Add("Authorization", $"{clientID}-{clientKey}");
        }

        public static async Task<DLBResponses.Success<Channel>?> GetChannelsAsync()
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

        public static async Task<DLBResponses.Success<Message>?> GetMessagesAsync()
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
                return JsonSerializer.Deserialize<DLBResponses.Success<Message>>(json);
            }
            else
            {
                MessageBox.Show($"Error: {response.StatusCode}");
                return null;
            }
        }
    }
}
