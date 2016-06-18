using IFeelGoodSalon.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace IFeelGoodSalon.DataAccess.Maps
{
    public class UserRoleMap : EntityTypeConfiguration<UserRole>
    {
        public UserRoleMap()
        {
            this.HasKey(ur => ur.Id);

            this.Property(ur => ur.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Computed);
        }
    }
}
