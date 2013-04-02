using Cronos.Application.Hubs;
using Cronos.Core.Domain.Album.Commands;
using Cronos.Infrastructure.Commands;

namespace Cronos.Application.Album.CommandHandlers
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
