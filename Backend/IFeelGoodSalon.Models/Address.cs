using IFeelGoodSalon.DataPattern.Ef6;
using IFeelGoodSalon.DataPattern.Ef6.Base;
using IFeelGoodSalon.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFeelGoodSalon.Models
{
    public class Address: ObservableEntity, ISoftDeleteEntity
    {
        public int Id { get; set; }

        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }

        public bool IsPrimary { get; set; }
        public bool IsDeleted { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }

        #region Navigation

        public virtual PersonBase Person { get; set; }

        #endregion
    }
}
