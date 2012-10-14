using Chinook.Core.Domain.Base;

namespace Chinook.Core.Domain.Album.Commands
{
	public class DeleteAlbumCommand : ICommand
	{
		public int Id { get; set; }
	}
}