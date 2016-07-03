using IFeelGoodSalon.DataPattern.Ef6;
using IFeelGoodSalon.DataPattern.Ef6.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IFeelGoodSalon.Models.Base
{
    public abstract class PersonBase : ObservableEntity, ISoftDeleteEntity
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }
        public string SurName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }

        public string FullName
        {
            get
            {
                if (string.IsNullOrEmpty(this.SurName))
                {
                    return string.Format("{0}, {1}", this.FirstName, this.LastName);
                }

                return string.Format("{0}, {1} {2}", this.FirstName, this.SurName, this.LastName);
            }
        }

        
        public string Initial { get; set; }
        public string HomePhone { get; set; }
        public string CellPhone { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        #region Navigation

        public virtual ICollection<Address> Addresses { get; set; }

        #endregion
    }
}
