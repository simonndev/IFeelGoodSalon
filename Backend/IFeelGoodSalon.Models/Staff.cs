using IFeelGoodSalon.Models.Base;
using IFeelGoodSalon.Models.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
