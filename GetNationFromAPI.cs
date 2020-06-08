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
    public class GetNationFromAPI
    { 
        public static async Task GetNations()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PWAPI"].ConnectionString;
            try
            {
                string url = "https://politicsandwar.com/api/v2/nations/f2a61c3f288e6e/&format=1";
                string response = await GetAPI.GetClient.GetStringAsync(url);
                var obj = JsonConvert.DeserializeObject<RootObjectModel>(response);
                using(SqlConnection con = new SqlConnection(connectionString))
                {
                    string deleteQuery = "DELETE FROM PW_Nation";
                    SqlCommand com = new SqlCommand(deleteQuery, con);
                    con.Open();
                    com.ExecuteNonQuery();
                    con.Close();
                    Console.WriteLine("Enter Nation Table Name ");
                    Console.Write(">> ");
                    string nationtblname = Console.ReadLine();
                    string nationquery = string.Format("insert into {0} (Nation_Id, Nation, Alliance_Id, Alliance, Score, VacMode, Alliance_Position, soldiers, tanks, aircraft, ships) " +
                        "Values (@Nation_Id, @Nation, @Alliance_Id, @Alliance, @Score, @VacMode, @Alliance_Position, @soldiers, @tanks, @aircraft, @ships)", nationtblname);
                    foreach (var nations in obj.data)
                    {
                        SqlCommand comm = new SqlCommand(nationquery, con);
                        con.Open();
                        comm.Parameters.AddWithValue("@Nation_Id", nations.nation_id);
                        comm.Parameters.AddWithValue("@Nation", nations.nation);
                        comm.Parameters.AddWithValue("@Alliance_Id", nations.alliance_id);
                        comm.Parameters.AddWithValue("@Alliance", nations.alliance);
                        comm.Parameters.AddWithValue("@Score", nations.score);
                        comm.Parameters.AddWithValue("@VacMode", nations.v_mode);
                        comm.Parameters.AddWithValue("@Alliance_Position", nations.alliance_position);
                        comm.Parameters.AddWithValue("@soldiers", nations.soldiers);
                        comm.Parameters.AddWithValue("@tanks", nations.tanks);
                        comm.Parameters.AddWithValue("@aircraft", nations.aircraft);
                        comm.Parameters.AddWithValue("@ships", nations.ships);
                        comm.ExecuteNonQuery();
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
