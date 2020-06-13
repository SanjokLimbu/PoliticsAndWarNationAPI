using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PWAPI.Interface;
using PWAPI.Model;

namespace MyWeb.Pages.FilteredNationPage
{
   public class FilteredCheckListModel : PageModel
    {
        private readonly IFilteredNation MyNation;
        public List<nation> nation { get; set; }
        public FilteredCheckListModel(IFilteredNation nation)
        {
            this.MyNation = nation;
        }
        public void OnGet()
        {
            nation = MyNation.getFilteredNation();
        }
    }
}