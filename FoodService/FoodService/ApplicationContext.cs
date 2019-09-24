using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FoodService.Models;

namespace FoodService
{
  public class ApplicationContext : DbContext
  {
    public DbSet<Meal> Meals { get; set; }
    public DbSet<Restaurant> Restaurants { get; set; }
    public ApplicationContext(DbContextOptions options) : base(options)
    {
    }
  }
}
