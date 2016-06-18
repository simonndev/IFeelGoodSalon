using IFeelGoodSalon.DataPattern.Ef6;
using IFeelGoodSalon.DataPattern.Ef6.Base;
using System;
using System.Collections.Generic;

namespace IFeelGoodSalon.Models
{
    public class User : ObservableEntity, IActivableEntity
    {
        public User()
        {
            this.UserRoles = new HashSet<UserRole>();
        }

        public Guid Id { get; set; }

        public string Username { get; set; }
        public string PasswordHashed { get; set; }
        public string Email { get; set; }

        public bool IsActive { get; set; }

        public string SecurityStamps { get; set; }
        public DateTime LastLogin { get; set; }

        #region Navigation
        public virtual Staff Staff { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
        #endregion
    }
}
