using IFeelGoodSalon.DataPattern.Ef6.Base;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace IFeelGoodSalon.Models.Identity
{
    public class SalonUserClaim : IdentityUserClaim<Guid>, IObservableEntity
    {
        [NotMapped]
        public EntityModelState ModelState { get; set; }
    }
}
