using IFeelGoodSalon.DataPattern.Ef6;
using IFeelGoodSalon.DataPattern.Ef6.Base;
using System;
using System.Collections.Generic;

namespace IFeelGoodSalon.Models
{
    public class Treatment : ObservableEntity, ISoftDeleteEntity
    {
        public Treatment()
        {
            this.TreatmentDurations = new HashSet<TreatmentDuration>();
        }

        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        #region Foreign Keys
        public int CategoryId { get; set; }
        #endregion

        #region Navigation Properties

        public virtual TreatmentCategory Category { get; set; }
        public virtual ICollection<TreatmentDuration> TreatmentDurations { get; set; }

        #endregion
    }
}
