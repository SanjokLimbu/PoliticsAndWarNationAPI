using System;
using System.Collections.Generic;
using System.Text;

namespace PWAPI.Model
{
    public class nation
    {
        public int Nation_Id { get; set; }
        public string Nation { get; set; }
        public string Alliance { get; set; }
        public double Score { get; set; }
        public int Soldiers { get; set; }
        public int Tanks { get; set; }
        public int Aircraft { get; set; }
        public int Ships { get; set; }
    }
}
