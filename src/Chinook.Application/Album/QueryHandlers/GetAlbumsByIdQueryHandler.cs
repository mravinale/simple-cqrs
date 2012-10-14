using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Chinook.Core;
using Chinook.Core.Domain.Album.Queries;
using Chinook.Infrastructure.Queries;
using NHibernate.Linq;

namespace Chinook.Application.Album.QueryHandlers
{
	public class GetAlbumsByIdQueryHandler : BaseQueryHandler<GetAlbumsByIdQuery, AlbumModel>
	{
		public override AlbumModel Handle(GetAlbumsByIdQuery query)
		{
			var entity = Session.Get<Core.Album>(query.Id);
			var model =  Mapper.Map<Core.Album, AlbumModel>(entity);
			FillAdditionalInformation(model);
			
			return model;
		}

		private void FillAdditionalInformation(AlbumModel model)
		{
			model.Artists = Session.Query<Artist>()
				.OrderBy(x => x.Name)
				.Select(Mapper.Map<Artist, SelectListItem>)
				.ToArray();
		}
	}
}
