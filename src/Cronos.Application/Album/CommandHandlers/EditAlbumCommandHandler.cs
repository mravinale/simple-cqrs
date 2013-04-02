using AutoMapper;
using Cronos.Application.Hubs;
using Cronos.Core;
using Cronos.Core.Domain.Album.Commands;
using Cronos.Infrastructure.Commands;

namespace Cronos.Application.Album.CommandHandlers
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
