using System.Linq;
using System.Reflection;
using Castle.Windsor;
using Cronos.Core.Domain.Base;
using Cronos.Infrastructure.Queries;

namespace Cronos.Application.Processors
{
	public class QueryProcessor : IProcessQuery
	{
		private readonly IWindsorContainer container;

		public QueryProcessor(IWindsorContainer container)
		{
			this.container = container;
		}
		
		public TResult Execute<TResult>(IQuery<TResult> query)
		{
			dynamic handler = Assembly.GetExecutingAssembly()
									.GetTypes()
									.Where(t =>typeof(IHandleQuery<,>)
									.MakeGenericType(query.GetType(), typeof(TResult)).IsAssignableFrom(t)
										&& !t.IsAbstract
										&& !t.IsInterface)	
									.Select(i => container.Resolve(i)).Single();

			return handler.Handle((dynamic)query);
		}
	}
}
