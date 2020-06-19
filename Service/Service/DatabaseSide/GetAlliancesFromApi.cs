using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace PWAPI
{
    public class GetAlliancesFromApi
    {
        public static void GetAlliance()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PWAPI"].ConnectionString;
            try
            {
                using SqlConnection con = new SqlConnection(connectionString);
                string deleteQuery = "DELETE FROM PW_Alliance";
                SqlCommand com = new SqlCommand(deleteQuery, con);
                con.Open();
                com.ExecuteNonQuery();
                con.Close();
                var data_Add = new SqlCommand("sp_add_data", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                con.Open();
                data_Add.ExecuteNonQuery();
                con.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
            

        }
    }
}
