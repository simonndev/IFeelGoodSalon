using IFeelGoodSalon.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace IFeelGoodSalon.DataAccess.Maps
{
    public class TreatmentCategoryMap : EntityTypeConfiguration<TreatmentCategory>
    {
        public TreatmentCategoryMap()
        {
            this.HasKey(tc => tc.Id);
            this.Property(tc => tc.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            this.Property(tc => tc.Name)
                .IsRequired()
                .HasMaxLength(255)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("IDX_TreatmentCatergory_Name")));
        }
    }
}
