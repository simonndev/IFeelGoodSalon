using IFeelGoodSalon.Models.Base;
using System;

namespace IFeelGoodSalon.Models
{
    public class Customer : PersonBase
    {
        public DateTime RegisteredDate { get; set; }
    }
}
