using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace NZWalks.API.Data
{
    public class NZWalksAuthDbContext : IdentityDbContext
    {
        public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            var reaerRoleId = "d6e7a6da-a040-4246-83bc-fbc27fd00077";
            var writeRoleId = "13684359-b248-4076-811b-40b58466fa97";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = reaerRoleId,
                    ConcurrencyStamp =reaerRoleId,
                    Name ="Reader",
                    NormalizedName = "Reader".ToUpper()
                },
                 new IdentityRole
                {
                    Id = writeRoleId,
                    ConcurrencyStamp =writeRoleId,
                    Name ="Reader",
                    NormalizedName = "Writer".ToUpper()
                },
            };

            builder.Entity<IdentityRole>().HasData(roles);

            } 
    }
}
