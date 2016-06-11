using IFeelGoodSalon.Models.Base;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace IFeelGoodSalon.Models
{
    public class Duration : EntityBase, IValidatableObject
    {
        public int Minute { get; set; }

        public float DefaultPrice { get; set; }
        public float MonToThuPrice { get; set; }
        public float FriToSunPrice { get; set; }
        public float HolidayPrice { get; set; }

        public int TreatmentId { get; set; }

        #region Navigation Properties

        public virtual Treatment SalonTreatment { get; set; }

        #endregion

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Minute <= 0)
            {
                yield return new ValidationResult(
                    "Treatment duration cannot be less than zero",
                    new[] { "Minute" });
            }

            if (this.DefaultPrice <= 0 || this.MonToThuPrice <= 0 || this.FriToSunPrice <= 0 || this.HolidayPrice <= 0)
            {
                yield return new ValidationResult(
                    "Price cannot be less than zero",
                    new[] { "DefaultPrice", "MonToThuPrice", "FriToSunPrice", "HolidayPrice" });
            }
        }
    }
}
