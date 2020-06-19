using PWAPI.Interface;
using PWAPI.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PWAPI.Service
{
    public class AllianceCheckList : IAlliance
    {
        private List<alliance> _Alliance;
        public AllianceCheckList()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PWAPI"].ConnectionString;
            SqlConnection con = new SqlConnection(connectionString);
            SqlCommand com = new SqlCommand("sp_get_alliance", con)
            {
                CommandType = CommandType.StoredProcedure
            };
            _Alliance = new List<alliance>();
            con.Open();
            SqlDataReader rdr = com.ExecuteReader();
            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    _Alliance.Add(new alliance() { Allianceid = rdr.GetInt32(0), Alliance = rdr.GetString(1), Score = Math.Round(rdr.GetDouble(2), 2), Soldiers = rdr.GetInt32(3), Tanks = rdr.GetInt32(4), Aircraft = rdr.GetInt32(5), Ships = rdr.GetInt32(6) });
                }
            }
            else
            {
                throw new Exception("Rows not found.");
            }
            con.Close();
        }
        List<alliance> IAlliance.GetAlliances()
        {
            return _Alliance;
        }
    }
}
