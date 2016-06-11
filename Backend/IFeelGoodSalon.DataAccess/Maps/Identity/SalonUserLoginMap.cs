using IFeelGoodSalon.Models.Identity;
using System.Data.Entity.ModelConfiguration;

namespace IFeelGoodSalon.DataAccess.Maps.Identity
{
    public class SalonUserLoginMap : EntityTypeConfiguration<SalonUserLogin>
    {
        public SalonUserLoginMap()
        {
            ToTable("SalonUserLogins");

            // Primary Key
            HasKey(l => new { l.LoginProvider, l.ProviderKey, l.UserId });

            Property(l => l.UserId).IsRequired();
            Property(l => l.LoginProvider)
                .IsRequired()
                .HasMaxLength(128)
                .HasColumnName("LoginProvider");
            Property(l => l.ProviderKey)
                .IsRequired()
                .HasMaxLength(128)
                .HasColumnName("ProviderKey");
        }
    }
}
