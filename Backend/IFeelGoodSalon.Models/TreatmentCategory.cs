using IFeelGoodSalon.DataPattern.Ef6;
using IFeelGoodSalon.DataPattern.Ef6.Base;
using System;
using System.Collections.Generic;

namespace IFeelGoodSalon.Models
{
    public class TreatmentCategory : ObservableEntity, ISoftDeleteEntity
    {
        public TreatmentCategory()
        {
            this.Treatments = new HashSet<Treatment>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        #region Navigation Properties

        public virtual ICollection<Treatment> Treatments { get; set; }

        #endregion
    }
}
