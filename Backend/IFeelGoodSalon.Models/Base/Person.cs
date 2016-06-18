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
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(20, ErrorMessage = "First name cannot be longer than 50 characters.")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [MaxLength(60)]
        public string SurName { get; set; }

        [Required]
        [MaxLength(20)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Full Name")]
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

        [Display(Name = "Nick Name")]
        public string NickName { get; set; }
        public string Initial { get; set; }

        [MaxLength(15)]
        public string HomePhone { get; set; }

        [MaxLength(15)]
        public string CellPhone { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        #region Navigation

        public virtual ICollection<Address> Addresses { get; set; }

        #endregion
    }
}
