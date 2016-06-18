using IFeelGoodSalon.DataPattern.Ef6;
using IFeelGoodSalon.DataPattern.Ef6.Base;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace IFeelGoodSalon.Models.Base
{
    public abstract class ContactBase : ObservableEntity, ISoftDeleteEntity
    {
        public int Id { get; set; }

        [MaxLength(50)]
        [DisplayName("Provider")]
        public string ProviderName { get; set; }

        public bool IsPrimary { get; set; }
        public bool IsDeleted { get; set; }
    }
}
