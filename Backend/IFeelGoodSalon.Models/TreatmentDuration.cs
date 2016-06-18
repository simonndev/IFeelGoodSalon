using IFeelGoodSalon.DataPattern.Ef6;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IFeelGoodSalon.Models
{
    public class TreatmentDuration : ObservableEntity, IValidatableObject
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public float DefaultPrice { get; set; }
        public float MonToThuPrice { get; set; }
        public float FriToSunPrice { get; set; }
        public float HolidayPrice { get; set; }

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
