using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeoWPFCreateDbTest.Nhibernate.Map;
using GeoWPFCreateDbTest.Nhibernate.Model;
using GeoWPFCreateDbTest.Nhibernate;
using NHibernate;

namespace BL.Repository
{
    public class LocationRepo
    {
        public void Add(Location newLocation)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Save(newLocation);
                    transaction.Commit();
                }
            }
        }

        public Location GetLocationById(int LocationId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var result = session.QueryOver<Location>().Where(x => x.Id == LocationId).SingleOrDefault();
                    return result ?? new Location();
                }
            }
        }

        public List<Location> GetAllLocation()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var result = session.QueryOver<Location>().List().ToList<Location>();

                    
                    return result;
                }
            }
        }

        public void Update(Location newLocation)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Update(newLocation);
                    transaction.Commit();
                }
            }
        }

        public void Delete(Location newLocation)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    using(var data = new GeoWPFCreateDbTest.BL.Provider.Provider())
                    {
                        var carsForDelete = data.CarRepository.GetCarsByLocationId(newLocation.Id);
                        data.CarRepository.DeleteCarByCollection(carsForDelete);
                    }
                    
                    session.Delete(newLocation);
                    transaction.Commit();
                }
            }
        }
    }
}
