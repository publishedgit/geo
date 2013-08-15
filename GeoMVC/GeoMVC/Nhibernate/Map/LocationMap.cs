using FluentNHibernate.Mapping;
using GeoWPFCreateDbTest.Nhibernate.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoWPFCreateDbTest.Nhibernate.Map
{
    public class LocationMap : ClassMap<Location>
    {
        public LocationMap()
        {
            Id(x => x.Id);
            Map(x => x.ZipCode).Not.Nullable();
            Map(x => x.Address).Not.Nullable();
            Map(x => x.ParkingLot).Not.Nullable();
        }
        
    }
}
