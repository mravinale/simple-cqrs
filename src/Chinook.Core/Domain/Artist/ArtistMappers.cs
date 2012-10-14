using System.Web.Mvc;
using AutoMapper;
using Chinook.Core.Configuration;
using Chinook.Core.Models;

namespace Chinook.Core
{
	public class ArtistMappers : IObjectMapperConfigurator
	{
		public void Apply()
		{
			Mapper.CreateMap<Artist, ArtistModel>();

			Mapper.CreateMap<ArtistModel, Artist>();
		}
	}
}

