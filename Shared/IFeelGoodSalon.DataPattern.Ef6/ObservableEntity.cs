using IFeelGoodSalon.DataPattern.Ef6.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace IFeelGoodSalon.DataPattern.Ef6
{
    public abstract class ObservableEntity : IObservableEntity
    {
        [NotMapped]
        public EntityModelState ModelState { get; set; }
    }
}
