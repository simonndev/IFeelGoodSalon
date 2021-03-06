﻿using IFeelGoodSalon.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace IFeelGoodSalon.DataAccess.Maps
{
    public class UserMap: EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            this.HasKey(u => u.Id);

            this.Property(u => u.Username)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("IDX_User_Username") { IsUnique = true }));

            this.Property(u => u.Email)
                .HasMaxLength(255)
                .HasColumnAnnotation(
                    IndexAnnotation.AnnotationName,
                    new IndexAnnotation(new IndexAttribute("IDX_User_Email") { IsUnique = true }));

            this.Property(u => u.PasswordHashed).IsRequired();
        }
    }
}
