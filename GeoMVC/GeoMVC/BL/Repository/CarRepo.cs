using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeoWPFCreateDbTest.Nhibernate.Map;
using GeoWPFCreateDbTest.Nhibernate.Model;
using GeoWPFCreateDbTest.Nhibernate;
using NHibernate;
using GeoWPFCreateDbTest.BL.Provider;

namespace BL.Repository
{
    public class CarRepo
    {
        public void Add(Car newCar)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using( ITransaction transaction = session.BeginTransaction())
                {                    
                    session.Save(newCar);
                    transaction.Commit();
                }
            }
        }

        public Car GetCarById(int CarId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var result = session.QueryOver<Car>().Where(x => x.Id == CarId).SingleOrDefault();
                    return result ?? new Car();
                }
            }
        }

        public List<Car> GetCarsByLocationId(int LocationId)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var result = session.QueryOver<Car>().Where(x => LocationId == x.Location.Id).List().ToList<Car>();
                    return result;
                }
            }
        }

        public List<Car> GetAllCar()
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    var result = session.QueryOver<Car>().List().ToList<Car>();
                    return result;
                }
            }
        }

        public void Update(Car newCar)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    session.Update(newCar);
                    transaction.Commit();
                }
            }
        }

        public void Delete(Car newCar)
        {
            using (ISession session = NHibernateHelper.OpenSession())
            {
                using (ITransaction transaction = session.BeginTransaction())
                {
                    try
                    {
                        session.Delete(newCar);
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Commit();
                    }

                }
            }
        }

        public void DeleteCarById(int carId)
        {
            Delete(GetCarById(carId));
        }
    }
}
