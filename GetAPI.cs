using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Net.Http.Headers;

namespace PoliticsAndWarAPIAccess.API.Implementation
{
    public static class GetAPI
    {
        public static HttpClient GetClient { get; set; }
        public static void InitializeClient()
        {
            GetClient = new HttpClient();
            GetClient.DefaultRequestHeaders.Accept.Clear();
            GetClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
