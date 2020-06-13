using PoliticsAndWarAPIAccess.API.Models;
using PWAPI.Interface;
using PWAPI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace PWAPI.Service
{
    public class NationCheckList : INation
    {
        private List<nation> _obj;
        public NationCheckList()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PWAPI"].ConnectionString;
            using SqlConnection con = new SqlConnection(connectionString);
            SqlCommand com = new SqlCommand("sp_get_nation", con)
            {
                CommandType = CommandType.StoredProcedure
            };
            _obj = new List<nation>();
            con.Open();
            SqlDataReader rdr = com.ExecuteReader();
            if (rdr.HasRows)
            {
                while (rdr.Read())
                {
                    _obj.Add(new nation() { Nation_Id = rdr.GetInt32(0), Nation = rdr.GetString(1), Alliance = rdr.GetString(2), Score = Math.Round(rdr.GetDouble(3), 2), Soldiers = rdr.GetInt32(4), Tanks = rdr.GetInt32(5), Aircraft = rdr.GetInt32(6), Ships = rdr.GetInt32(7) });
                }
            }
            else
            {
                throw new Exception("rdr don't have any rows");
            }
            con.Close();
        }
        public List<nation> GetAllNations()
        {
            return _obj;
        }
    }
}
