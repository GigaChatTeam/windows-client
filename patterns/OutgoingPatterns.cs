using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace GigaChat.patterns
{
    internal class OutgoingPatterns
    {
        class System 
        {
            class Channels
            {
                class Listen
                {
                    class Add
                    {
                        [JsonIgnore]
                        public static string[] intention { get; set; } = new string[0];

                        public Int64 client;
                        public Int64 channel;

                        public string serealize()
                        {
                            string packet = JsonSerializer.Serialize(this);
                            return $"{string.Join("-", intention)}%{Helper.SHA512(packet)}%{packet}";
                        }
                    }

                    class Remove
                    {
                        public Int64 client;
                        public Int64 channel;
                    }
                }
            }
        }
        class Channels
        {
            class Messages
            {

            }

            class Users
            {

            }
        }
    }
}
