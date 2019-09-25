using FoodService.Models.Identity;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodService
{
  public class ApplicationDbContext : IdentityDbContext<AppUser, IdentityRole<int>, int>
  {
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<IdentityRole>().HasData(
            new IdentityRole { Name = "Admin" },
            new IdentityRole { Name = "Manager" },
            new IdentityRole { Name = "Customer" });
    }
  }
}