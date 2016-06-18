using IFeelGoodSalon.DataPattern.Ef6;
using IFeelGoodSalon.DataPattern.Ef6.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        #region Navigation Properties

        public virtual ICollection<TreatmentDuration> TreatmentDurations { get; set; }

        #endregion
    }
}
