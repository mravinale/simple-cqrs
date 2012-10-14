using Chinook.Core.Domain.Base;

namespace Chinook.Infrastructure.Commands
{
	public interface IProcessCommand
	{
		void Execute(ICommand command);
	}
}