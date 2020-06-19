using Newtonsoft.Json;
using PoliticsAndWarAPIAccess.API.Models;
using PWAPI.Interface;
using System;
using System.Timers;
using System.Configuration;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace PWAPI
{
    public class GetNationFromAPI 
    {
        public async Task GetNation()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PWAPI"].ConnectionString;
            string url = ConfigurationManager.GetSection("url").ToString();
            string response = await GetAPI.GetClient.GetStringAsync(url);
            var obj = JsonConvert.DeserializeObject<RootObjectModel>(response);
            using SqlConnection con = new SqlConnection(connectionString);
            string deleteQuery = "DELETE FROM PW_Nation";
            SqlCommand com = new SqlCommand(deleteQuery, con);
            con.Open();
            com.ExecuteNonQuery();
            con.Close();
            string nationtblname = ConfigurationManager.GetSection("tblname").ToString();
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
            };
        }

        public static Task GetNations()
        {
            throw new Exception();
        }
    }
}
