using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Chinook.Core;
using Chinook.Core.Domain.Album.Queries;
using Chinook.Infrastructure.Queries;
using NHibernate.Linq;

namespace Chinook.Application.Album.QueryHandlers
{
	public class GetNewAlbumQueryHandler : BaseQueryHandler<GetNewAlbumQuery, AlbumModel>
	{
		public override AlbumModel Handle(GetNewAlbumQuery query)
		{
			var model =  new AlbumModel();
			FillAdditionalInformation(model);
			
			return model;
		}

		public void FillAdditionalInformation(AlbumModel model)
		{
			model.Artists = Session.Query<Artist>()
				.OrderBy(x => x.Name)
				.Select(Mapper.Map<Artist, SelectListItem>)
				.ToArray();
		}

	}
}
