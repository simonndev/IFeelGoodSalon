using IFeelGoodSalon.Models.Identity;
using System;
using System.Data.Entity.Validation;
using System.Linq;

namespace IFeelGoodSalon.DataAccess.Extensions
{
    public static class ValidationExtensions
    {
        public static void ValidateApplicationRole(
            this IFeelGoodSalonIdentityContext dbContext,
            DbEntityValidationResult result)
        {
            var entity = result.Entry.Entity as SalonRole;
            if (entity == null)
            {
                return;
            }

            SalonRole temp = dbContext
                .Roles.FirstOrDefault(x => x.Name == entity.Name);

            if ((temp != null) && (temp.Id != entity.Id))
            {
                result.ValidationErrors.Add(
                    new DbValidationError("Name", String.Format("A role with name {0} is already registered.", entity.Name)));
            }
        }

        public static void ValidateApplicationUser(
            this IFeelGoodSalonIdentityContext dbContext,
            DbEntityValidationResult result)
        {
            var entity = result.Entry.Entity as SalonUser;
            if (entity == null)
            {
                return;
            }

            SalonUser temp = dbContext
                .Users.FirstOrDefault(x => x.UserName == entity.UserName);

            if ((temp != null) && (temp.Id != entity.Id))
            {
                result.ValidationErrors.Add(
                    new DbValidationError(
                        // A {0} with the {1} of '{2}' is already registered ({3})
                        "UserName",
                        String.Format("An user with user-name {0} is already registered.", entity.UserName)));
            }
        }
    }
}
