using System;
using System.Collections.Generic;
using System.Web;
using NHibernate;
using NHibernate.Context;
using NHibernate.Engine;

namespace Cronos.Infrastructure.NHibernate
{
	public class LazySessionContext : ICurrentSessionContext
	{
		private readonly ISessionFactoryImplementor factory;
		private const string CurrentSessionContextKey = "NHibernateCurrentSession";

		public LazySessionContext(ISessionFactoryImplementor factory)
		{
			this.factory = factory;
		}

		/// <summary>
		/// Retrieve the current session for the session factory.
		/// </summary>
		/// <returns></returns>
		public ISession CurrentSession()
		{
			Lazy<ISession> initializer;
			var currentSessionFactoryMap = GetCurrentFactoryMap();
			if (currentSessionFactoryMap == null ||
			    !currentSessionFactoryMap.TryGetValue(factory, out initializer))
			{
				return null;
			}
			return initializer.Value;
		}

		/// <summary>
		/// Bind a new sessionInitializer to the context of the sessionFactory.
		/// </summary>
		/// <param name="sessionInitializer"></param>
		/// <param name="sessionFactory"></param>
		public static void Bind(Lazy<ISession> sessionInitializer, ISessionFactory sessionFactory)
		{
			var map = GetCurrentFactoryMap();
			map[sessionFactory] = sessionInitializer;
		}

		public static void Bind(ISessionFactory sessionFactory)
		{
			var map = GetCurrentFactoryMap();
			map[sessionFactory] = new Lazy<ISession>(() => BeginSession(sessionFactory));
		}

		private static ISession BeginSession(ISessionFactory sf)
		{
			var session = sf.OpenSession();
			session.BeginTransaction();
			return session;
		}

		/// <summary>
		/// Unbind the current session of the session factory.
		/// </summary>
		/// <param name="sessionFactory"></param>
		/// <returns></returns>
		public static ISession UnBind(ISessionFactory sessionFactory)
		{
			var map = GetCurrentFactoryMap();
			var sessionInitializer = map[sessionFactory];
			map[sessionFactory] = null;
			if (sessionInitializer == null || !sessionInitializer.IsValueCreated) return null;
			return sessionInitializer.Value;
		}

		/// <summary>
		/// Provides the CurrentMap of SessionFactories.
		/// If there is no map create/store and return a new one.
		/// </summary>
		/// <returns></returns>
		private static IDictionary<ISessionFactory, Lazy<ISession>> GetCurrentFactoryMap()
		{
			var currentFactoryMap = (IDictionary<ISessionFactory, Lazy<ISession>>)
			                        HttpContext.Current.Items[CurrentSessionContextKey];
			if (currentFactoryMap == null)
			{
				currentFactoryMap = new Dictionary<ISessionFactory, Lazy<ISession>>();
				HttpContext.Current.Items[CurrentSessionContextKey] = currentFactoryMap;
			}
			return currentFactoryMap;
		}
	}
}
