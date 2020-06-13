using PoliticsAndWarAPIAccess.API.Models;
using PWAPI.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PWAPI.Interface
{
    public interface INation
    {
        List<nation> GetAllNations();
    }
}
