using Cronos.Core.Domain.Base;

namespace Cronos.Infrastructure.Queries
{
	public interface IHandleQuery<TQuery, TResult> where TQuery : IQuery<TResult>
	{
		TResult Handle(TQuery query);
	}
}