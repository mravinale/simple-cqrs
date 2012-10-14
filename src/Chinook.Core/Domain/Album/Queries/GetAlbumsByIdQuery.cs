using Chinook.Core.Domain.Base;

namespace Chinook.Core.Domain.Album.Queries
{
	public class GetAlbumsByIdQuery : IQuery<AlbumModel>
	{
		public int Id { get; set; }
	}
}
