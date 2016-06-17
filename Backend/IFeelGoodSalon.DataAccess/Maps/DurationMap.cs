using IFeelGoodSalon.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace IFeelGoodSalon.DataAccess.Maps
{
    public class DurationMap : EntityTypeConfiguration<Duration>
    {
        public DurationMap()
        {
            this.HasKey(treatment => treatment.Id);

            this.Property(treatment => treatment.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(duration => duration.Minute).IsRequired();
            this.Property(duration => duration.DefaultPrice).IsRequired();
        }
    }
}