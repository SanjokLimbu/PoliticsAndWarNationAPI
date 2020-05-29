using System;
using PWAPI;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PoliticsAndWarAPIAccess.API.Models;
using System.Configuration;

namespace PoliticsAndWarAPIAccess.API.Implementation
{
    public class Program
    {
        public static async Task GetNations()
        {
            try
            {
                string url = "https://politicsandwar.com/api/v2/nations/{key}/&format=1";
                string? response = await GetAPI.GetClient.GetStringAsync(url);
                var obj = JsonConvert.DeserializeObject<RootObjectModel>(response);
                using MyPWDatabase myPW = new MyPWDatabase();
                foreach (var nations in obj.data)
                {
                    myPW.PW_Nation.Nation_IdColumn.DefaultValue = nations.nation_id;
                    myPW.PW_Nation.NationColumn.DefaultValue = nations.nation;
                    myPW.PW_Nation.Alliance_IdColumn.DefaultValue = nations.alliance_id;
                    myPW.PW_Nation.AllianceColumn.DefaultValue = nations.alliance;
                    myPW.PW_Nation.SoldiersColumn.DefaultValue = nations.soldiers;
                    myPW.PW_Nation.TanksColumn.DefaultValue = nations.tanks;
                    myPW.PW_Nation.AirplanesColumn.DefaultValue = nations.aircraft;
                    myPW.PW_Nation.ShipsColumn.DefaultValue = nations.ships;
                    myPW.AcceptChanges();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
            
        }
        public static async Task Main()
        {
            GetAPI.InitializeClient();
             await GetNations();
            Console.WriteLine("Please press any key to exit.");
        }
    }
}
