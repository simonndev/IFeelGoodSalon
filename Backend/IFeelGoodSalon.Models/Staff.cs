using IFeelGoodSalon.DataPattern.Ef6.Base;
using IFeelGoodSalon.Models.Base;
using IFeelGoodSalon.Models.Identity;
using System;

namespace IFeelGoodSalon.Models
{
    public class Staff : PersonBase
    {
        public DateTime HiredDate { get; set; }

        #region Navigation Properties

        public virtual User User { get; set; }

        #endregion
    }
}
