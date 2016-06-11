using IFeelGoodSalon.DataPattern.Ef6.Base;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace IFeelGoodSalon.Models.Identity
{
    public class SalonUserRole : IdentityUserRole<Guid>, IObservableEntity
    {
        [NotMapped]
        public EntityModelState ModelState { get; set; }
    }
}
