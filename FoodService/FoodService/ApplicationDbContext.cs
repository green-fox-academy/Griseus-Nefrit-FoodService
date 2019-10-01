using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodService.Models;

namespace FoodService
{
  public class ApplicationDbContext : DbContext
  {
    public DbSet<Meal> Meals { get; set; }
    public DbSet<Restaurant> Restaurants { get; set; }
    
    public DbSet<Price> Prices { get; set; }
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
    }
  }
}
