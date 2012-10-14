using AutoMapper;
using Chinook.Application.Hubs;
using Chinook.Core;
using Chinook.Core.Domain.Album.Commands;
using Chinook.Infrastructure.Commands;

namespace Chinook.Application.Album.CommandHandlers
{
	public class InsertAlbumCommandHandler : BaseCommandHandler<InsertAlbumCommand> 
	{
		public override void Handle(InsertAlbumCommand command)
		{
			Session.Save(Mapper.Map<AlbumModel,Core.Album>(command));

			//client side method invocation
			ConnectionManager.GetClients<AlbumsHub>().Redraw();
		}
	}
}
