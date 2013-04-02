using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Cronos.Infrastructure.NHibernate;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Connection;
using NHibernate.Dialect;
using NHibernate.Driver;

namespace Cronos.Infrastructure.Configuration
{
	public class NHibernateInstaller : IWindsorInstaller
	{
		public void Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Register(
				Component.For<global::NHibernate.Cfg.Configuration>().Instance(CreateConfiguration()),
				Component.For<ISessionFactory>().Instance(CreateConfiguration().BuildSessionFactory()),
				Component.For<IWindsorContainer>().Instance(container)
				);
		}

		private global::NHibernate.Cfg.Configuration CreateConfiguration()
		{
			var configuration = new global::NHibernate.Cfg.Configuration()
				.CurrentSessionContext<LazySessionContext>();

			configuration.DataBaseIntegration(db =>
			{
				db.ConnectionProvider<DriverConnectionProvider>();
				db.Dialect<MsSql2005Dialect>();
				db.Driver<SqlClientDriver>();
				db.ConnectionStringName = "default";
				db.BatchSize = 30;
				db.KeywordsAutoImport = Hbm2DDLKeyWords.AutoQuote;
				db.Timeout = 10;
				db.LogFormattedSql = true;
				db.LogSqlInConsole = false;
				db.HqlToSqlSubstitutions = "true 1, false 0, yes 'Y', no 'N'";
			});

			configuration.AddDeserializedMapping(EntitiesMapper.CreateMappingConfiguration(), "Cronos"); ;

			return configuration;
		}
	}
}
