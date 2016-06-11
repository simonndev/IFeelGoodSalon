﻿using IFeelGoodSalon.Models.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace IFeelGoodSalon.DataAccess.Maps.Identity
{
    public class SalonRoleMap : EntityTypeConfiguration<SalonRole>
    {
        public SalonRoleMap()
        {
            ToTable("SalonRoles");

            // Primary Key
            HasKey(r => r.Id);

            Property(r => r.Id).IsRequired().HasColumnName("Id");

            Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnName("Name")
                .HasColumnAnnotation("Index",
                    new IndexAnnotation(new IndexAttribute("RoleNameIndex")
                    {
                        IsUnique = true
                    }));

            // Navigation
            HasMany(r => r.Users)
                .WithRequired()
                .HasForeignKey(ur => ur.RoleId)
                .WillCascadeOnDelete();
        }
    }
}
