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
    public class AlliancePageModel : PageModel
    {
        private readonly IAlliance MyAlliance;
        public List<alliance> alliances;
        public AlliancePageModel(IAlliance alliance)
        {
            this.MyAlliance = alliance;
        }
        public void OnGet()
        {
            alliances = MyAlliance.GetAlliances();
        }
    }
}