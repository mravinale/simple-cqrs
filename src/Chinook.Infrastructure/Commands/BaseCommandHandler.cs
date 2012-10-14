using Chinook.Core.Domain.Base;
using NHibernate;
using SignalR;

namespace Chinook.Infrastructure.Commands
{
	public abstract class BaseCommandHandler<TCommand> : IHandleCommand<TCommand> where TCommand : ICommand
	{
		public ISessionFactory SessionFactory { get; set; }
		public IConnectionManager ConnectionManager { get; set; }

		protected ISession Session
		{
			get { return SessionFactory.GetCurrentSession(); }
		}

		#region Implementation of IHandleCommand<TCommand>

		public abstract void Handle(TCommand command);

		#endregion
	}
}
