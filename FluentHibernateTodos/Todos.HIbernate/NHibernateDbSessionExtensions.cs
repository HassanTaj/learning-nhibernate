using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;

using Microsoft.Extensions.DependencyInjection;

using NHibernate.Cfg;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Dialect;
using NHibernate.Mapping.ByCode;
using NHibernate.Tool.hbm2ddl;

using System.Linq;
using System.Reflection;

namespace Todos.HIbernate {
    public static class NHibernateDbSessionExtensions {
        public static IServiceCollection AddNHibernate(this IServiceCollection services, string connectionString) {
            /** Note:
             *   Note as of the writing of this comment
             *   nhibernate doesn't support creating 
             *   Neither NHibernate nor FNH create the database itself.
             *   You have to provide either code or a precreated database yourself.But it can create the tables for you.
             *   The class doing this is call SchemaExport.In Fluent, it looks like this:
             */

            #region Method 1 To create Session Factory
            //var mapper = new ModelMapper();
            //mapper.AddMappings(typeof(NHibernateDbSessionExtensions).Assembly.ExportedTypes);
            //HbmMapping domainMapping = mapper.CompileMappingForAllExplicitlyAddedEntities();
            //var configuration = new Configuration();
            //configuration.DataBaseIntegration(c => {
            //    c.Dialect<MsSql2012Dialect>();
            //    c.ConnectionString = connectionString;
            //    c.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
            //    c.SchemaAction = SchemaAutoAction.Validate;
            //    c.LogFormattedSql = true;
            //    c.LogSqlInConsole = true;
            //});
            //configuration.AddMapping(domainMapping);
            //var sessionFactory = configuration.BuildSessionFactory();
            #endregion

            #region Method 2 To create Session Factory
            //IPersistenceConfigurer configDB = (MsSqlConfiguration.MsSql2012.ConnectionString(connectionString).ShowSql());
            //var sessionFactory = Fluently.Configure().Database(configDB)
            //    .Mappings(M => M.FluentMappings.AddFromAssembly(Assembly.GetExecutingAssembly()))
            //    .ExposeConfiguration(cfg => new SchemaExport(cfg).Create(true, true))
            //    .BuildSessionFactory();
            #endregion

            #region Method 3 To Create Session Factory - One Liner
            var sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012
                .ConnectionString("DefaultConnection")
                .ShowSql())
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<NHibernateDbSession>())
                .ExposeConfiguration(cfg => new SchemaExport(cfg).Create(true, true))
                //.ExposeConfiguration(cfg => new SchemaExport(cfg).Execute(true, true, false))
                .BuildSessionFactory();
            #endregion

            services.AddSingleton(sessionFactory);
            services.AddScoped(factory => sessionFactory.OpenSession());
            services.AddScoped<INHibernateDbSession, NHibernateDbSession>();

            return services;
        }
    }
}
