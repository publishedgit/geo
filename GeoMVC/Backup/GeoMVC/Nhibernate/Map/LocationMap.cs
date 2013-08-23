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
            Map(x => x.ZipCode);
            Map(x => x.Address);
            Map(x => x.ParkingLot);
        }
        
    }
}
