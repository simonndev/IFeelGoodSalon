﻿using System.ComponentModel.DataAnnotations;
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

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int Minute { get; set; }

        public float DefaultPrice { get; set; }
        public float MonToThuPrice { get; set; }
        public float FriToSunPrice { get; set; }
        public float HolidayPrice { get; set; }

        public int TreatmentId { get; set; }

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
