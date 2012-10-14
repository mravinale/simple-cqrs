using Chinook.Application.Hubs;
using Chinook.Core.Domain.Album.Commands;
using Chinook.Infrastructure.Commands;

namespace Chinook.Application.Album.CommandHandlers
{
	public class DeleteAlbumCommandHandler : BaseCommandHandler<DeleteAlbumCommand> 
	{
		public override void Handle(DeleteAlbumCommand command)
		{
			var entity = Session.Get<Core.Album>(command.Id);

			Session.Delete(entity);

			//client side method invocation
			ConnectionManager.GetClients<AlbumsHub>().Redraw();
		}
	}
}
