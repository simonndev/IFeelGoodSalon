using IFeelGoodSalon.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFeelGoodSalon.Models
{
    public class NumberContact : ContactBase
    {
        public string HomePhone { get; set; }
        public string CellPhone { get; set; }
    }
}
