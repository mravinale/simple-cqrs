using System;
using System.Web;
using Castle.Windsor;
using NHibernate;

namespace Chinook.Infrastructure.NHibernate
{
	public class NHibernateModule : IHttpModule
	{
		private HttpApplication application;

		private IContainerAccessor containerAccessor;

		public void Init(HttpApplication context)
		{
			application = context;
			containerAccessor = (IContainerAccessor) context;
			application.BeginRequest += ApplicationBeginRequest;
			application.EndRequest += ApplicationEndRequest;
		}

		private IWindsorContainer Container
		{
			get { return containerAccessor.Container; }
		}

		private void ApplicationBeginRequest(object sender, EventArgs e)
		{
			if (Container == null)
				return;

			var sessionFactory = Container.Resolve<ISessionFactory>();
			LazySessionContext.Bind(sessionFactory);
		}

		private void ApplicationEndRequest(object sender, EventArgs e)
		{
			if (Container == null)
				return;

			var sessionFactory = Container.Resolve<ISessionFactory>();
			var session = LazySessionContext.UnBind(sessionFactory);

			if (session == null)
				// session was not accessed during this request
				return;

			var tx = session.Transaction;

			if (tx != null)
			{
				if (tx.IsActive)
				{
					try
					{
						tx.Commit();
					}
					catch (Exception)
					{
						tx.Rollback();
						throw;
					}
				}
			}
			else
			{
				session.Flush();
			}
			session.Close();
		}

		public void Dispose()
		{
			application.BeginRequest -= ApplicationBeginRequest;
			application.EndRequest -= ApplicationEndRequest;
		}
	}
}
