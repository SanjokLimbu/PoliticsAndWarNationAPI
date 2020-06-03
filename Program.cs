using System;
using PWAPI;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PoliticsAndWarAPIAccess.API.Models;
using System.Configuration;

namespace PoliticsAndWarAPIAccess.API.Implementation
{
        public static async Task Main()
        {
            GetAPI.InitializeClient();
             await GetNations();
            Console.WriteLine("Please press any key to exit.");
        }
    }
}
