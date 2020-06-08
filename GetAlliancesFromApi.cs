using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PoliticsAndWarAPIAccess.API.Implementation;
using PoliticsAndWarAPIAccess.API.Models;
using PWAPI.MyPWDatabaseTableAdapters;

namespace PWAPI
{
    public class GetAlliancesFromApi
    {
        public static async Task GetAlliance()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PWAPI"].ConnectionString;
            try
            {
                using(SqlConnection con = new SqlConnection(connectionString))
                {
                    var data_Add = new SqlCommand("sp_add_data", con)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    con.Open();
                    data_Add.ExecuteNonQuery();
                    con.Close();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            

        }
    }
}
