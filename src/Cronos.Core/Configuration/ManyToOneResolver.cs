using AutoMapper;
using NHibernate;

namespace Cronos.Core.Configuration
{
	public class ManyToOneResolver<T> : ValueResolver<int, T>
		where T : Entity
	{
		private readonly ISessionFactory sessionFactory;

		public ManyToOneResolver(ISessionFactory sessionFactory)
		{
			this.sessionFactory = sessionFactory;
		}

		protected override T ResolveCore(int id)
		{
			return id.Equals(default(int)) ? null : sessionFactory.GetCurrentSession().Load<T>(id);
		}
	}
}
