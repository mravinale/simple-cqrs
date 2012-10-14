using AutoMapper;
using Chinook.Application.Hubs;
using Chinook.Core;
using Chinook.Core.Domain.Album.Commands;
using Chinook.Infrastructure.Commands;

namespace Chinook.Application.Album.CommandHandlers
{
	public class EditAlbumCommandHandler : BaseCommandHandler<EditAlbumCommand> 
	{
		public override void Handle(EditAlbumCommand command)
		{
			var entity = Session.Get<Core.Album>(command.AlbumId);

			Session.Update(Mapper.Map(command, entity));

			//client side method invocation
		    ConnectionManager.GetClients<AlbumsHub>().Redraw();
		}
	}
}
