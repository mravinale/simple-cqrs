using Chinook.Core.Domain.Base;

namespace Chinook.Infrastructure.Queries
{
	public interface IProcessQuery
	{
		TResult Execute<TResult>(IQuery<TResult> query);
	}
}