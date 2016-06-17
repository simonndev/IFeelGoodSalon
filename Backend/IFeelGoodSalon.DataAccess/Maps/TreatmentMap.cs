using IFeelGoodSalon.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace IFeelGoodSalon.DataAccess.Maps
{
    public class TreatmentMap : EntityTypeConfiguration<Treatment>
    {
        public TreatmentMap()
        {
            this.HasKey(treatment => treatment.Id);

            this.Property(treatment => treatment.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(treatment => treatment.Name).IsRequired().HasMaxLength(100);
            this.Property(treatment => treatment.DurationMinute).IsRequired();
        }
    }
}