using Cronos.Core.Domain.Base;

namespace Cronos.Core.Domain.Album.Queries
{
	public class GetAlbumsByIdQuery : IQuery<AlbumModel>
	{
		public int Id { get; set; }
	}
}
