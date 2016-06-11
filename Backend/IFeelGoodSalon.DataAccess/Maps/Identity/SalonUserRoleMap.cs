using IFeelGoodSalon.Models.Identity;
using System.Data.Entity.ModelConfiguration;

namespace IFeelGoodSalon.DataAccess.Maps.Identity
{
    public class SalonUserRoleMap : EntityTypeConfiguration<SalonUserRole>
    {
        public SalonUserRoleMap()
        {
            ToTable("SalonUserRoles");

            // Primary Key
            HasKey(t => new { t.UserId, t.RoleId });

            Property(t => t.UserId).IsRequired().HasColumnName("UserId");
            Property(t => t.RoleId).IsRequired().HasColumnName("RoleId");
        }
    }
}
