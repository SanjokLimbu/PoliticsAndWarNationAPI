using Newtonsoft.Json;
using PoliticsAndWarAPIAccess.API.Implementation;
using PoliticsAndWarAPIAccess.API.Models;
using System;
using System.Configuration.Assemblies;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace PWAPI
{
    public  class GetNationFromAPI
    { 
        public static async Task GetNations()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PWAPI"].ConnectionString;
            try
            {
                string url = "http://politicsandwar.com/api/nations/?key={Value}";
                string response = await GetAPI.GetClient.GetStringAsync(url);
                var obj = JsonConvert.DeserializeObject<RootObjectModel>(response);
                using(SqlConnection con = new SqlConnection(connectionString))
                {
                    Console.WriteLine("Enter Table Name ");
                    Console.Write(">> ");
                    string tblname = Console.ReadLine();
                    string query = string.Format("insert into {0} (Nation_Id, Nation, Alliance_Id, Alliance) Values (@Nation_Id, @Nation, @Alliance_Id, @Alliance)", tblname);
                    foreach (var data in obj.nations)
                    {
                        
                        SqlCommand com = new SqlCommand(query, con);
                        con.Open();
                        com.Parameters.AddWithValue("@Nation_Id", data.nationid);
                        com.Parameters.AddWithValue("@Nation", data.nation);
                        com.Parameters.AddWithValue("@Alliance_Id", data.allianceid);
                        com.Parameters.AddWithValue("@Alliance", data.alliance);
                        com.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }
    }
}
