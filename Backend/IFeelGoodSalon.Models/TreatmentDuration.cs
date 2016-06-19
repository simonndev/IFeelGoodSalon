using IFeelGoodSalon.DataPattern.Ef6;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IFeelGoodSalon.Models
{
    public class TreatmentDuration : ObservableEntity, IValidatableObject
    {
        public int Id { get; set; }

        public decimal DefaultPrice { get; set; }
        public decimal MonToThuPrice { get; set; }
        public decimal FriToSunPrice { get; set; }
        public decimal HolidayPrice { get; set; }

        #region Foreign Keys
        public int TreatmentId { get; set; }
        public int DurationId { get; set; }
        #endregion

        #region Navigation
        public virtual Treatment Treatment { get; set; }
        public virtual Duration Duration { get; set; }
        #endregion

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.DefaultPrice <= 0 || this.MonToThuPrice <= 0 || this.FriToSunPrice <= 0 || this.HolidayPrice <= 0)
            {
                yield return new ValidationResult(
                    "Price cannot be less than zero",
                    new[] { "DefaultPrice", "MonToThuPrice", "FriToSunPrice", "HolidayPrice" });
            }
        }
    }
}
