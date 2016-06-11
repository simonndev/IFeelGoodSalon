using IFeelGoodSalon.Models.Base;
using System.Collections.Generic;

namespace IFeelGoodSalon.Models
{
    public class Treatment : EntityBase
    {
        public Treatment()
        {
            this.TreatmentDurations = new HashSet<Duration>();
        }

        public string Name { get; set; }
        public string Description { get; set; }

        public int DurationMinute { get; set; }

        #region Navigation Properties

        public virtual ICollection<Duration> TreatmentDurations { get; set; }

        #endregion
    }
}
