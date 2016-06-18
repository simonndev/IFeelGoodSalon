using IFeelGoodSalon.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace IFeelGoodSalon.DataAccess.Maps
{
    public class StaffMap : EntityTypeConfiguration<Staff>
    {
        public StaffMap()
        {
            this.HasKey(s => s.Id);

            this.Property(s => s.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);

            this.Property(s => s.FirstName)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("IDX_Staff_Name", 1)));

            this.Property(s => s.LastName)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("IDX_Staff_Name", 2)));

            this.Property(s => s.SurName).HasMaxLength(60);
            this.Property(s => s.SurName).HasMaxLength(25);
            this.Property(s => s.HomePhone).HasMaxLength(15);
            this.Property(s => s.CellPhone).HasMaxLength(60);
        }
    }
}