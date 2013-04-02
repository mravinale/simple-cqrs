namespace Cronos.Infrastructure.Commands
{
	public interface IHandleCommand<TCommand>
	{
		void Handle(TCommand command);
	}
}