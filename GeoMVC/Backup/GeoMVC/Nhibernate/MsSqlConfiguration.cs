using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NHibernate.Dialect;
using NHibernate.Driver;
using FluentNHibernate.Cfg.Db;

namespace GeoWPFCreateDbTest.Nhibernate
{
    public class MsSqlConfiguration : PersistenceConfiguration<MsSqlConfiguration, MsSqlConnectionStringBuilder>
    {
        protected MsSqlConfiguration()
        {
            Driver<SqlClientDriver>();
        }

        public static MsSqlConfiguration MsSql7
        {
            get { return new MsSqlConfiguration().Dialect<MsSql7Dialect>(); }
        }

        public static MsSqlConfiguration MsSql2000
        {
            get { return new MsSqlConfiguration().Dialect<MsSql2000Dialect>(); }
        }

        public static MsSqlConfiguration MsSql2005
        {
            get { return new MsSqlConfiguration().Dialect<MsSql2005Dialect>(); }
        }

        public static MsSqlConfiguration MsSql2008
        {
            get { return new MsSqlConfiguration().Dialect<MsSql2008Dialect>(); }
        }

        public static MsSqlConfiguration MsSql2012
        {
            get { return new MsSqlConfiguration().Dialect<MsSql2012Dialect>(); }
        }
    }
}
