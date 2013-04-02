using Cronos.Core.Domain.Base;

namespace Cronos.Infrastructure.Queries
{
	public interface IProcessQuery
	{
		TResult Execute<TResult>(IQuery<TResult> query);
	}
}