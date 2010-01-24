using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using GMSBlog.Service.NHibernate.Mappings;
using NHibernate.Tool.hbm2ddl;

namespace GMSBlog.Web.Tests.Helpers
{
    public static class DatabaseHelpers
    {
        private static string connectionString = "NHibernateBlogService.Test";

        public static void Initialize()
        {
            Initialize(false);
        }
        public static void Initialize(bool empty)
        {
            SessionFactory = CreateSessionFactory(empty);
        }

        static ISessionFactory SessionFactory;


        private static ISessionFactory CreateSessionFactory(bool empty)
        {
            var cfg = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008
                .ConnectionString(c => c.FromConnectionStringWithKey(connectionString)))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<PostMappings>())
                .ExposeConfiguration(c =>
                {
                    if (empty)
                    {
                        new SchemaExport(c).Drop(false,true);
                        new SchemaExport(c).Create(false, true);
                    }
                });

            return cfg.BuildSessionFactory();
        }
    }
}
