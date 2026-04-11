using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DistributionWaterApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace DistributionWaterApp.Database
{
   public class AppDbContext : DbContext
    {
         public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<ZonaEntity> Zonas { get; set; }
        public DbSet<TurnosAguaEntity> TurnosAgua { get; set; }
    }
}