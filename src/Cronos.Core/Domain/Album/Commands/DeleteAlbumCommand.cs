using Cronos.Core.Domain.Base;

namespace Cronos.Core.Domain.Album.Commands
{
	public class DeleteAlbumCommand : ICommand
	{
		public int Id { get; set; }
	}
}