using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Cronos.Core;
using Cronos.Core.Domain.Album.Queries;
using Cronos.Infrastructure.Queries;
using NHibernate.Linq;

namespace Cronos.Application.Album.QueryHandlers
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
