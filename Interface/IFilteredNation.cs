﻿using PWAPI.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace PWAPI.Interface
{
    public interface IFilteredNation
    {
        List<nation> getFilteredNation(int id);
    }
}
