using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PWAPI.Interface;
using PWAPI.Model;

namespace MyWeb.Pages
{
    public class NationCheckListModel : PageModel
    {
        private readonly INation Mynation;
        public List<nation> nation { get; set; }
        public NationCheckListModel(INation nation)
        {
            this.Mynation = nation;
        }
        public void OnGet()
        {
            nation = Mynation.GetAllNations();
        }
    }
}