using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace PWAPI.Model
{
    public class nation
    {
        [Key]
        public int Nation_Id { get; set; }
        [Required]
        public string Nation { get; set; }
        [Required]
        public int Alliance_Id { get; set; }
        [Required]
        public string Alliance { get; set; }
        [Required]
        public double Score { get; set; }
        [Required]
        public int Cities { get; set; }
        [Required]
        public int Soldiers { get; set; }
        [Required]
        public int Tanks { get; set; }
        [Required]
        public int Aircraft { get; set; }
        [Required]
        public int Ships { get; set; }
    }
}
