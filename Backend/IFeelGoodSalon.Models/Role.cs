using IFeelGoodSalon.DataPattern.Ef6;
using IFeelGoodSalon.DataPattern.Ef6.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFeelGoodSalon.Models
{
    public class Role : ObservableEntity
    {
        public Role()
        {
            this.UserRoles = new HashSet<UserRole>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        #region Navigation
        public virtual ICollection<UserRole> UserRoles { get; set; }
        #endregion
    }
}
