using IFeelGoodSalon.DataAccess.Maps;
using IFeelGoodSalon.DataPattern.Ef6;
using IFeelGoodSalon.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace IFeelGoodSalon.DataAccess
{
    public class IFeelGoodSalonContext : ObservableDbContext
    {
        public IFeelGoodSalonContext()
            : base("Name = ConnectionString")
        {
        }

        public DbSet<Staff> Staffs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        
        public DbSet<Treatment> Treatments { get; set; }
        public DbSet<TreatmentDuration> TreatmentDurations { get; set; }
        public DbSet<Duration> Durations { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Configurations.Add(new StaffMap());
            modelBuilder.Configurations.Add(new UserMap());
            modelBuilder.Configurations.Add(new UserRoleMap());
            modelBuilder.Configurations.Add(new RoleMap());

            modelBuilder.Configurations.Add(new TreatmentMap());
            modelBuilder.Configurations.Add(new TreatmentDurationMap());
            modelBuilder.Configurations.Add(new DurationMap());

            // A Staff might not need an user-account.
            // Staff.IsDeleted = true and User.IsActive = false
            modelBuilder.Entity<User>()
                .HasRequired(u => u.Staff)
                .WithOptional(s => s.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserRole>()
                .HasRequired(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<UserRole>()
                .HasRequired(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TreatmentDuration>()
                .HasRequired(trd => trd.Treatment)
                .WithMany(tr => tr.TreatmentDurations)
                .HasForeignKey(trd => trd.TreatmentId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<TreatmentDuration>()
                .HasRequired(trd => trd.Duration)
                .WithMany(d => d.TreatmentDurations)
                .HasForeignKey(trd => trd.DurationId)
                .WillCascadeOnDelete(false);
        }
    }
}