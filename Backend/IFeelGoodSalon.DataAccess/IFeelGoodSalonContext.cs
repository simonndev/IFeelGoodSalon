using IFeelGoodSalon.DataPattern.Ef6;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using IFeelGoodSalon.Models;
using IFeelGoodSalon.DataAccess.Maps;

namespace IFeelGoodSalon.DataAccess
{
    public class IFeelGoodSalonContext : ObservableDbContext
    {
        public IFeelGoodSalonContext()
            : base("Name = ConnectionString")
        {
        }

        public DbSet<Staff> Staff { get; set; }
        public DbSet<Treatment> Treatments { get; set; }
        public DbSet<Duration> Durations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new TreatmentMap());
            modelBuilder.Configurations.Add(new DurationMap());
        }
    }
}
