using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GeoWPFCreateDbTest.Nhibernate.Model
{
    public class Location
    {
        public virtual int Id { get; set; }
        public virtual int ZipCode { get; set; }
        public virtual string Address  { get; set; }
        public virtual int ParkingLot { get; set; }
    }

    public class Car
    {
        public virtual int Id { get; set; }
        public virtual string Made { get; set; }
        public virtual string Type { get; set; }
        public virtual DateTime ProductionTime { get; set; }
        public virtual string Condition { get; set; }
        public virtual int NumberOfOwners { get; set; }
        public virtual Location Location { get; set; }

    }


}
