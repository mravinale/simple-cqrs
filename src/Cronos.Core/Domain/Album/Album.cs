
using System.Collections.Generic;

namespace Cronos.Core
{
	public class Album : Entity
	{
		public virtual int AlbumId { get; set; }
		public virtual string Title { get; set; }
		public virtual Artist Artist { get; set; }
	}
}
