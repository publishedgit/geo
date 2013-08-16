using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeoWPFCreateDbTest.BL;
using BL.Repository;

namespace GeoWPFCreateDbTest.BL.Provider
{
    public class Provider : IDisposable
    {
        public LocationRepo LocationRepository { get; set; }
        public CarRepo CarRepository { get; set; }

        public Provider()
        {
            LocationRepository = new LocationRepo();
            CarRepository = new CarRepo();
        }

        public void Dispose() { }
    }
}
