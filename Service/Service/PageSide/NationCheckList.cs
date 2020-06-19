using PoliticsAndWarAPIAccess.API.Models;
using PWAPI.Interface;
using PWAPI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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
                    _obj.Add(new nation() { Nation_Id = rdr.GetInt32(0), Nation = rdr.GetString(1),Alliance_Id = rdr.GetInt32(2), Alliance = rdr.GetString(3), Score = Math.Round(rdr.GetDouble(4), 2), Soldiers = rdr.GetInt32(5), Tanks = rdr.GetInt32(6), Aircraft = rdr.GetInt32(7), Ships = rdr.GetInt32(8) });
                }
            }
            else
            {
                throw new Exception("rdr don't have any rows");
            }
            con.Close();
        }

        public IEnumerable<nation> GetAllianceUnits(int id)
        {
            return _obj.Where(e => e.Alliance_Id == id);

        }

        public List<nation> GetAllNations()
        {
            return _obj;
        }
    }
}
