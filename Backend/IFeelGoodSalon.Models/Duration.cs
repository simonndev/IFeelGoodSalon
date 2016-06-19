using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using IFeelGoodSalon.DataPattern.Ef6;
using System.ComponentModel.DataAnnotations.Schema;

namespace IFeelGoodSalon.Models
{
    public class Duration : ObservableEntity, IValidatableObject
    {
        public Duration()
        {
            this.TreatmentDurations = new HashSet<TreatmentDuration>();
        }

        public int Id { get; set; }

        public int Minute { get; set; }

        #region Navigation Properties

        public virtual ICollection<TreatmentDuration> TreatmentDurations { get; set; }

        #endregion

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Minute <= 0)
            {
                yield return new ValidationResult(
                    "Treatment duration cannot be less than zero",
                    new[] { "Minute" });
            }
        }
    }
}
