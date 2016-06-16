using IFeelGoodSalon.DataPattern.Ef6.Base;
using IFeelGoodSalon.Models.Base;
using IFeelGoodSalon.Models.Identity;
using System;

namespace IFeelGoodSalon.Models
{

    public class Staff : EntityBase<Guid>, ISoftDeleteEntity
    {
        public bool IsAvailable { get; set; }

        public bool IsDeleted { get; set; }

        #region Navigation Properties

        public virtual SalonUser ApplicationUser { get; set; }

        #endregion

    }
}
