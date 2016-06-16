using IFeelGoodSalon.DataPattern.Ef6.Base;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFeelGoodSalon.Models.Identity
{
    public class SalonUser : IdentityUser<Guid, SalonUserLogin, SalonUserRole, SalonUserClaim>, IObservableEntity, IActivableEntity
    {
        public SalonUser()
        {
            Id = Guid.NewGuid();
        }

        public SalonUser(string username)
            : this()
        {
            UserName = username;
        }

        public bool IsActive { get; set; }

        public DateTime? LastLogin { get; set; }

        #region Navigation Properties

        public virtual Staff SalonStaff { get; set; }

        #endregion

        [NotMapped]
        public EntityModelState ModelState { get; set; }
    }
}
