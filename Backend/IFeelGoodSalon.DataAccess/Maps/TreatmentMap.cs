using IFeelGoodSalon.DataAccess.Maps.Base;
using IFeelGoodSalon.Models;

namespace IFeelGoodSalon.DataAccess.Maps
{
    public class TreatmentMap : IdentityEntityMapBase<Treatment>
    {
        public TreatmentMap()
            : base()
        {
            this.Property(treatment => treatment.Name).IsRequired().HasMaxLength(100);
            this.Property(treatment => treatment.DurationMinute).IsRequired();
        }
    }
}
