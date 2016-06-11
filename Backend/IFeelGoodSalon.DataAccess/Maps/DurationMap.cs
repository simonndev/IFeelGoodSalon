using IFeelGoodSalon.DataAccess.Maps.Base;
using IFeelGoodSalon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IFeelGoodSalon.DataAccess.Maps
{
    public class DurationMap : IdentityEntityMapBase<Duration>
    {
        public DurationMap()
            : base()
        {
            this.Property(duration => duration.Minute).IsRequired();
            this.Property(duration => duration.DefaultPrice).IsRequired();
        }
    }
}
