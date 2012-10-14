using Chinook.Core.Domain.Base;

namespace Chinook.Infrastructure.Queries
{
	public interface IHandleQuery<TQuery, TResult> where TQuery : IQuery<TResult>
	{
		TResult Handle(TQuery query);
	}
}