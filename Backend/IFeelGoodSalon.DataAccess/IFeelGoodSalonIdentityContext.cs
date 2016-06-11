using IFeelGoodSalon.DataPattern.Ef6;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using IFeelGoodSalon.Models.Identity;
using IFeelGoodSalon.DataAccess.Maps.Identity;
using IFeelGoodSalon.DataAccess.Extensions;

namespace IFeelGoodSalon.DataAccess
{
    public class IFeelGoodSalonIdentityContext : ObservableDbContext
    {

        static IFeelGoodSalonIdentityContext()
        {
            // Database.SetInitializer<UrfIdentityDataContext>(null);

            Database.SetInitializer
                (new CreateDatabaseIfNotExists<IFeelGoodSalonIdentityContext>());
        }

        /// <summary>
        /// Ininitialize the Identity DB Context with lazy-loading and proxy-creation both enabled.
        /// </summary>
        public IFeelGoodSalonIdentityContext()
            : base("Name = IdentityConnectionString", true, true)
        {
        }

        #region DataSets
        public DbSet<SalonUser> Users { get; set; }
        public DbSet<SalonRole> Roles { get; set; }
        public DbSet<SalonUserRole> UserRoles { get; set; }
        public DbSet<SalonUserClaim> UserClaims { get; set; }
        public DbSet<SalonUserLogin> UserLogins { get; set; }
        //public DbSet<Staff> ApplicationUserDetails { get; set; }
        #endregion DataSets

        public static IFeelGoodSalonIdentityContext Create()
        {
            return new IFeelGoodSalonIdentityContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new SalonRoleMap());
            modelBuilder.Configurations.Add(new SalonUserMap());
            modelBuilder.Configurations.Add(new SalonUserClaimMap());
            modelBuilder.Configurations.Add(new SalonUserLoginMap());
            modelBuilder.Configurations.Add(new SalonUserRoleMap());
        }

        protected override DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, IDictionary<object, object> items)
        {
            // Base validation for Data Annotations, IValidatableObject
            var result = base.ValidateEntity(entityEntry, items);

            // Only validate entities new/updated entities
            if ((result.Entry.State != EntityState.Added) &&
                (result.Entry.State != EntityState.Modified))
            {
                return result;
            }

            // Validate User Identity
            this.ValidateApplicationUser(result);
            this.ValidateApplicationRole(result);

            return result;
        }
    }
}
