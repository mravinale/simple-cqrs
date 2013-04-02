using Cronos.Core.Domain.Base;

namespace Cronos.Infrastructure.Commands
{
	public interface IProcessCommand
	{
		void Execute(ICommand command);
	}
}