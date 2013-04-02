using AutoMapper;
using Cronos.Application.Hubs;
using Cronos.Core;
using Cronos.Core.Domain.Album.Commands;
using Cronos.Infrastructure.Commands;

namespace Cronos.Application.Album.CommandHandlers
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
