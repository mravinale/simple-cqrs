using Cronos.Core;
using Cronos.Core.Domain.Base;
using NHibernate.Cfg.MappingSchema;
using NHibernate.Mapping.ByCode;

namespace Cronos.Infrastructure.NHibernate
{
	public static class EntitiesMapper
	{
		public static HbmMapping CreateMappingConfiguration()
		{
			var mapper = new ConventionModelMapper();

			var baseEntityType = typeof (Entity);
			mapper.IsEntity((t, declared) => baseEntityType.IsAssignableFrom(t) && baseEntityType != t && !t.IsInterface);
			mapper.IsRootEntity((t, declared) => baseEntityType.Equals(t.BaseType));

			mapper.BeforeMapManyToOne += (insp, prop, map) => map.Column(prop.LocalMember.GetPropertyOrFieldType().Name + "Id");
			mapper.BeforeMapBag += (insp, prop, map) => map.Key(km => km.Column(prop.GetContainerEntity(insp).Name + "Id"));
			mapper.BeforeMapBag += (insp, prop, map) => map.Cascade(Cascade.All);

			mapper.Class<Album>(map => map.Id(x => x.AlbumId, m=> m.Generator(Generators.Identity)));
			mapper.Class<Artist>(map => map.Id(x => x.ArtistId, m => m.Generator(Generators.Identity)));

			var mapping = mapper.CompileMappingFor(baseEntityType.Assembly.GetExportedTypes());

			return mapping;
		}
	}
}
