using System;
using System.Text.Json.Serialization;

namespace API_Tests
{


    public class Responses
    {
        [JsonPropertyName("msg")]
        public string msg { get; set; }

        [JsonPropertyName("url")]
        public string url { get; set; }
    }
    public class URLS
        {
            [JsonPropertyName("url")]
            public string url { get; set; }

            [JsonPropertyName("shortCode")]
            public string shortCode { get; set; }

            [JsonPropertyName("shortUrl")]
            public string shortUrl { get; set; }

            [JsonPropertyName("dateCreated")]
            public string dateCreated { get; set; }

            [JsonPropertyName("visits")]
            public int visits { get; set; }

        }

}