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
    public class FilterNationAsAlliance : IFilteredNation
    {
        private List<nation> _FilteredNation;
        public FilterNationAsAlliance()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PWAPI"].ConnectionString;
            using SqlConnection con = new SqlConnection(connectionString);
            con.Open();
            SqlCommand com = new SqlCommand("sp_get_nation_of_alliance", con)
            {
                CommandType = CommandType.StoredProcedure
            };
            _FilteredNation = new List<nation>();
            SqlDataReader rdr = com.ExecuteReader();
            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    _FilteredNation.Add(new nation() { Nation_Id = rdr.GetInt32(0), Nation = rdr.GetString(1), Score = Math.Round(rdr.GetDouble(2), 2), Soldiers = rdr.GetInt32(3), Tanks = rdr.GetInt32(4), Aircraft = rdr.GetInt32(5), Ships = rdr.GetInt32(6) });
                }
            }
            else
            {
                throw new Exception("rdr don't have any rows");
            }
            con.Close();
        }
        public List<nation> getFilteredNation(int id)
        {
            return _FilteredNation;
        }
    }
}
