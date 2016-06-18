using IFeelGoodSalon.DataPattern.Ef6;
using System;

namespace IFeelGoodSalon.Models
{
    public class UserRole : ObservableEntity
    {
        public int Id { get; set; }

        public DateTime AssignedDate { get; set; }

        #region Foreign Keys
        public Guid UserId { get; set; }
        public int RoleId { get; set; }
        #endregion

        #region Navigation
        public virtual User User { get; set; }
        public virtual Role Role { get; set; }
        #endregion
    }
}
