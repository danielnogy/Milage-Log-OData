using ArhiParcurs.Data;
using ArhiParcurs.Shared.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ArhiParcurs;

public static class ModelBuilderExtensions
{
    public static void SeedData(this ModelBuilder modelBuilder)
    {

        //seed the ApplicationRole table
        //modelBuilder.Entity<ApplicationRole>().HasData(
        //                          new ApplicationRole { Id = "b6fe17ae-029f-4b6c-bd83-11ef9dd9d18b", Name = "Admin", NormalizedName = "ADMIN" },
        //                                                           new ApplicationRole { Id = "1627678d-0770-42eb-98a8-50d66fe8b981", Name = "User", NormalizedName = "USER" }
        //           
        var hasher = new PasswordHasher<ApplicationUser>();
        modelBuilder.Entity<ApplicationUser>().HasData(
            new ApplicationUser
            {
                Id = "d865aca4-f1d8-4a5f-957d-89509b1de4bb",
                UserName = "admin@admin.com",
                NormalizedUserName = "ADMIN@ADMIN.COM",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "Admin12!") // Set your password here
            }
        );
       // modelBuilder.Entity<IdentityUserRole<string>>().HasData(
       //    new IdentityUserRole<string> { RoleId = "b6fe17ae-029f-4b6c-bd83-11ef9dd9d18b", UserId = "d865aca4-f1d8-4a5f-957d-89509b1de4bb" }
       //);
    }
}
