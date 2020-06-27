using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PW.Model.RegisterModel;

namespace PW
{
    public class DContext : IdentityDbContext
    {
        public DContext(DbContextOptions<DContext> options)
            :base(options)
        {    
        }
        public DbSet<Registration> Registrations { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
