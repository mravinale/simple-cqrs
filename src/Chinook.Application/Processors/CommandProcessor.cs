using System.Linq;
using System.Reflection;
using Castle.Windsor;
using Chinook.Core.Domain.Base;
using Chinook.Infrastructure.Commands;

namespace Chinook.Application.Processors
{
	public class CommandProcessor : IProcessCommand
	{
		private readonly IWindsorContainer container;

		public CommandProcessor(IWindsorContainer container)
		{
			this.container = container;
		}
		
		public void Execute(ICommand command)
		{
			dynamic handler = Assembly.GetExecutingAssembly()
									.GetTypes()
									.Where(t =>typeof(IHandleCommand<>)
									.MakeGenericType(command.GetType()).IsAssignableFrom(t)
										&& !t.IsAbstract
										&& !t.IsInterface)	
									.Select(i => container.Resolve(i)).Single();

			handler.Handle((dynamic)command);
		}
	}
}
