using FluentNHibernate.Mapping;
using GeoWPFCreateDbTest.Nhibernate.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoWPFCreateDbTest.Nhibernate.Map
{
    public class CarMap : ClassMap<Car>
    {
        public CarMap()
        {
            Id(x => x.Id);
            Map(x => x.Made);
            Map(x => x.Type);
            Map(x => x.Vintage);
            Map(x => x.ProductionTime);
            Map(x => x.Condition);
            Map(x => x.NumberOfOwners);
            References(x => x.Location).Cascade.All().Not.LazyLoad();
        }
    }
}
