using System.Web.Mvc;
using AutoMapper;
using Chinook.Core.Configuration;
using Chinook.Core.Domain.Album.Commands;

namespace Chinook.Core
{
	public class AlbumMappers : IObjectMapperConfigurator
	{
		public void Apply()
		{
			Mapper.CreateMap<Album, AlbumModel>()
				.ForMember(m => m.AlbumId, cfg => cfg.MapFrom(x => x.AlbumId))
				.ForMember(m => m.ArtistId, cfg => cfg.MapFrom(x => x.Artist.ArtistId))
				.ForMember(m => m.ArtistName, cfg => cfg.MapFrom(x => x.Artist.Name))
				.ForMember(m => m.Title, cfg => cfg.MapFrom(x => x.Title))
				.ForMember(m => m.Artists, cfg => cfg.Ignore());

			Mapper.CreateMap<AlbumModel, Album>()
				.ForMember(x => x.Artist,
						   cfg => cfg.ResolveUsing<ManyToOneResolver<Artist>>()
									.FromMember(x => x.ArtistId));
			
			Mapper.CreateMap<Artist, SelectListItem>()
				.ForMember(x => x.Value, cfg => cfg.MapFrom(x => x.ArtistId))
				.ForMember(x => x.Text, cfg => cfg.MapFrom(x => x.Name))
				.ForMember(x => x.Selected, cfg => cfg.Ignore());
		}
	}
}

