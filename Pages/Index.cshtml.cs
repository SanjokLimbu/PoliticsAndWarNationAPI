using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using PW.Model.PageModel;

namespace PWWebApplication.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        string connectionString = ConfigurationManager.ConnectionStrings["PWAPI"].ConnectionString;
        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }
        public IActionResult OnGetGenerateCharts()
        {
            return GetData();
        }
        public JsonResult GetData()
        {
            DataSet ds = GetViews();
            List<Alliances> listAllianceforCharts = new List<Alliances>();
            foreach(DataRow dr in ds.Tables[0].Rows)
            {
                listAllianceforCharts.Add(new Alliances
                {
                    Alliance = Convert.ToString(dr["Alliance"]),
                    Cities = Convert.ToInt32(dr["Cities"]),
                    Soldiers = Convert.ToInt32(dr["Soldiers"]),
                    Tanks = Convert.ToInt32(dr["Tanks"]),
                    Aircraft = Convert.ToInt32(dr["Aircraft"]),
                    Ships = Convert.ToInt32(dr["Ships"])
                });
            }
            return new JsonResult(listAllianceforCharts);
        }
        private DataSet GetViews()
        {
            DataSet dt = new DataSet();
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand com = new SqlCommand("sp_get_alliancedetailsfor_charts", con)
                {
                    CommandType = CommandType.StoredProcedure
                };
                SqlDataAdapter adapter = new SqlDataAdapter(com);
                adapter.Fill(dt);
                return dt;
            }
        }
    }
}
