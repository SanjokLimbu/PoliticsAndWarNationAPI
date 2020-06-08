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
/* This is my Stored Procedure
CREATE PROCEDURE sp_add_data
AS
INSERT INTO PW_Alliance(Alliance_ID, Alliance, Score, Soldiers, Tanks, Aircraft, Ships)
SELECT DISTINCT N.Alliance_id, N.Alliance, SUM(N.Score), SUM(N.Soldiers), SUM(N.Tanks), SUM(N.Aircraft), SUM(N.Ships)
FROM PW_Nation AS N
WHERE N.Alliance_Position != 0 AND N.VacMode = 0 AND N.Alliance_Id != 0
GROUP BY N.Alliance_Id, N.Alliance
Go
*/
