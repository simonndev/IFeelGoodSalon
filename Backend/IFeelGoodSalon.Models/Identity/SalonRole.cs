using IFeelGoodSalon.DataPattern.Ef6.Base;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace IFeelGoodSalon.Models.Identity
{
    public class SalonRole: IdentityRole<Guid, SalonUserRole>, IObservableEntity
    {
        public SalonRole()
        {
            Id = Guid.NewGuid();
        }

        public SalonRole(string name)
            : this()
        {
            Name = name;
        }

        [NotMapped]
        public EntityModelState ModelState { get; set; }
    }
}
