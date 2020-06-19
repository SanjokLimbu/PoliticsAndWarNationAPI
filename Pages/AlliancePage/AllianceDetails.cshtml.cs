using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PWAPI.Interface;
using PWAPI.Model;

namespace MyWeb.Pages.AlliancePage
{
    public class AllianceDetailsModel : PageModel
    {
        private readonly INation nation;

        public AllianceDetailsModel(INation nation)
        {
            this.nation = nation;
        }
        public IEnumerable<nation> MyNation { get; set; }
        public void OnGet(int id)
        {
            MyNation = nation.GetAllianceUnits(id);
        }
    }
}