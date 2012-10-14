
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Chinook.Core
{
	public class AlbumModel
	{
		[Display(Name = "Id")]
		public int AlbumId { get; set; }

		[Required]
		public string Title { get; set; }

		[HiddenInput(DisplayValue = false)]
		[Display(Name = "Artists")]
		public int ArtistId { get; set; }

		public string ArtistName { get; set; }
		
		public IEnumerable<SelectListItem> Artists { get; set; }
	}
	
}
