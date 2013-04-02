using Cronos.Core;
using NHibernate.Cfg;
using NHibernate.Mapping.ByCode;

namespace Cronos.Data
{
	public class EntitiesMapper
	{
		public void Configure(Configuration configuration)
		{
			var mapper = new ConventionModelMapper();

			var baseEntityType = typeof (Entity);
			mapper.IsEntity((t, declared) => baseEntityType.IsAssignableFrom(t) && baseEntityType != t && !t.IsInterface);
			mapper.IsRootEntity((t, declared) => baseEntityType.Equals(t.BaseType));

			mapper.BeforeMapManyToOne += (insp, prop, map) => map.Column(prop.LocalMember.GetPropertyOrFieldType().Name + "Id");
			mapper.BeforeMapBag += (insp, prop, map) => map.Key(km => km.Column(prop.GetContainerEntity(insp).Name + "Id"));
			mapper.BeforeMapBag += (insp, prop, map) => map.Cascade(Cascade.All);

			mapper.Class<Entity>(map =>
			{
				map.Id(x => x.Id, m => m.Generator(Generators.GuidComb));
				map.Version(x => x.Version, m => m.Generated(VersionGeneration.Never));
			});

			var mapping = mapper.CompileMappingFor(baseEntityType.Assembly.GetExportedTypes());

			configuration.AddDeserializedMapping(mapping, "Cronos");
		}
	}
}
