using IFeelGoodSalon.Models.Identity;
using System.Data.Entity.ModelConfiguration;

namespace IFeelGoodSalon.DataAccess.Maps.Identity
{
    public class SalonUserClaimMap: EntityTypeConfiguration<SalonUserClaim>
    {
        public SalonUserClaimMap()
        {
            ToTable("SalonUserClaims");

            // Primary Key
            HasKey(c => new { c.Id, c.UserId });

            Property(c => c.Id).IsRequired().HasColumnName("Id");
            Property(c => c.UserId).IsRequired().HasColumnName("UserId");
            Property(c => c.ClaimType).HasColumnName("ClaimType");
            Property(c => c.ClaimValue).HasColumnName("ClaimValue");
        }
    }
}
