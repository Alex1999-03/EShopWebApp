using EShopWebApp.Utils.Constants;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EShopWebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedBuyerUser(builder);
            SeedAdminUser(builder);
        }

        private void SeedBuyerUser(ModelBuilder builder)
        {
            var email = "buyer@gmail.com";
            var password = "Buyer&1234";
            var roleId = Guid.NewGuid().ToString();
            var userId = Guid.NewGuid().ToString();
            var hasher = new PasswordHasher<IdentityUser>();

            var user = new IdentityUser
            {
                Id = userId,
                UserName = email,
                Email = email,
                EmailConfirmed = true,
                NormalizedUserName = email.ToUpper(),
                NormalizedEmail = email.ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString()
            };
            user.PasswordHash = hasher.HashPassword(user, password);

            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = roleId,
                Name = ApplicationRoles.BUYER_ROLE,
                NormalizedName = ApplicationRoles.BUYER_ROLE.ToUpper()
            });
            builder.Entity<IdentityUser>().HasData(user);
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string> 
            { 
                RoleId = roleId, 
                UserId = userId 
            });
        }

        private void SeedAdminUser(ModelBuilder builder)
        {
            var email = "admin@gmail.com";
            var password = "Admin&1234";
            var roleId = Guid.NewGuid().ToString();
            var userId = Guid.NewGuid().ToString();
            var hasher = new PasswordHasher<IdentityUser>();

            var user = new IdentityUser
            {
                Id = userId,
                UserName = email,
                Email = email,
                EmailConfirmed = true,
                NormalizedUserName = email.ToUpper(),
                NormalizedEmail = email.ToUpper(),
                SecurityStamp = Guid.NewGuid().ToString()
            };
            user.PasswordHash = hasher.HashPassword(user, password);

            builder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = roleId,
                Name = ApplicationRoles.ADMIN_ROLE,
                NormalizedName = ApplicationRoles.ADMIN_ROLE.ToUpper()
            });
            builder.Entity<IdentityUser>().HasData(user);
            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = roleId,
                UserId = userId
            });
        }

    }
}