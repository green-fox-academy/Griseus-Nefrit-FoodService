using FoodService.Models;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodService
{
  public class ApplicationContext : DbContext
  {
    public DbSet<Restaurant> Restaurants  {get;set;}
    public ApplicationContext(DbContextOptions options) : base(options)
    {
    }
  }
}
