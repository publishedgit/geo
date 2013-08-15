using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Cfg;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GeoWPFCreateDbTest.Nhibernate.Model;
using NHibernate.Tool.hbm2ddl;
using System.Data.SqlClient;
using System.Configuration;
using GeoWPFCreateDbTest.Nhibernate;
using GeoWPFCreateDbTest.BL.Provider;
using GeoWPFCreateDbTest.Nhibernate.Model;

namespace GeoWPFCreateDbTest.Nhibernate
{
    public class NHibernateHelper
    {
        private static ISessionFactory _sessionFactory;
        private static bool isNotExistingTables = true;
        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                { InitializeSessionFactory(); }

                return _sessionFactory;
            }
        }

        private static void InitializeSessionFactory()
        {
            if(isNotExistingTables == true)
            {
                isNotExistingTables = DbIsNotExist();
            }

            try
            {
                _sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008.ConnectionString(c => c.FromConnectionStringWithKey("GeoDbConnectionString")))
                .Mappings(m =>
                    m.FluentMappings.AddFromAssemblyOf<Car>())
                        .ExposeConfiguration(cfg => new SchemaExport(cfg)
                                                        .Create(false, isNotExistingTables))
                .BuildSessionFactory();
            }
            catch(FluentConfigurationException)
            {
                CreateNewDB();
                InitializeSessionFactory();
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.InnerException.ToString());
            }

            
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }

        private static bool DbIsNotExist()
        {
            SqlConnection con;
            string cmdCheckTheTables = "SELECT * FROM Location";
            SqlCommand cmd;

            bool result = false;

            con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["GeoDbConnectionString"].ConnectionString;

            try
            {
                con.Open();

                cmd = new SqlCommand(cmdCheckTheTables, con);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                System.Data.DataTable dt = new System.Data.DataTable("Location");
                sda.Fill(dt);

                var a = dt.DefaultView.Count;
               // MessageBox.Show("dt.DefaultView.Count = " + a);
                

                if (a <= 0)
                {
                    result = true;
                 //   MessageBox.Show("New Table architecture will be create.");
                }
            }
            catch (Exception)
            {
                result = true;
            }
            finally
            {
                con.Close();
            }

            return result;
        }

        private static void CreateNewDB()
        {
            SqlConnection con;
            string cmdCreateDatabase = "CREATE DATABASE GeoView";
            SqlCommand cmd;

            con = new SqlConnection();
            con.ConnectionString = ConfigurationManager.ConnectionStrings["MasterConnectionString"].ConnectionString;

            try
            {
                con.Open();

                cmd = new SqlCommand(cmdCreateDatabase, con);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {

               // MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
        
    }
}
