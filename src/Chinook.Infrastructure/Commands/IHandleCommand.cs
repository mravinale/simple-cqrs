namespace Chinook.Infrastructure.Commands
{
	public interface IHandleCommand<TCommand>
	{
		void Handle(TCommand command);
	}
}