using Cronos.Core.Domain.Base;

namespace Cronos.Core
{
	public class Artist : Entity
	{
		public virtual int ArtistId { get; set; }
		public virtual string Name { get; set; }
	}

	
}
