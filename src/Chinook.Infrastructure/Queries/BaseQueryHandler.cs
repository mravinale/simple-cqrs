using Chinook.Core.Domain.Base;
using NHibernate;

namespace Chinook.Infrastructure.Queries
{
	public abstract class BaseQueryHandler<TQuery, TResult> : IHandleQuery<TQuery, TResult> where TQuery : IQuery<TResult>
	{
		public ISessionFactory SessionFactory { get; set; }

		protected ISession Session
		{
			get { return SessionFactory.GetCurrentSession(); }
		}

		#region IQueryHandler<TQuery,TResult> Members

		public abstract TResult Handle(TQuery query);

		#endregion
	}
}
