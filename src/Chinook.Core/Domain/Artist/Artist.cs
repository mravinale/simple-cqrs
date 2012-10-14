using Chinook.Core.Domain.Base;

namespace Chinook.Core
{
	public class Artist : Entity
	{
		public virtual int ArtistId { get; set; }
		public virtual string Name { get; set; }
	}

	
}
