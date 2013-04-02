using System.Linq;
using System.Reflection;
using Castle.Windsor;
using Cronos.Core.Domain.Base;
using Cronos.Infrastructure.Commands;

namespace Cronos.Application.Processors
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
