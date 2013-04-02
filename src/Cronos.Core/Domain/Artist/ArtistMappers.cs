using System.Web.Mvc;
using AutoMapper;
using Cronos.Core.Configuration;
using Cronos.Core.Models;

namespace Cronos.Core
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

