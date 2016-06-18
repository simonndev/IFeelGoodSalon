using IFeelGoodSalon.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace IFeelGoodSalon.DataAccess.Maps
{
    public class TreatmentDurationMap : EntityTypeConfiguration<TreatmentDuration>
    {
        public TreatmentDurationMap()
        {
            this.HasKey(trd => trd.Id);
            this.Property(trd => trd.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
