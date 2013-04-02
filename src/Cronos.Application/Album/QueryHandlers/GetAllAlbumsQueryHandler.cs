using System;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using Cronos.Core;
using Cronos.Core.Configuration;
using Cronos.Core.Domain.Album.Queries;
using Cronos.Infrastructure.Infractructure;
using Cronos.Infrastructure.Queries;
using NHibernate.Linq;

namespace Cronos.Application.Album.QueryHandlers
{
	public class GetAllAlbumsQueryHandler : BaseQueryHandler<GetAllAlbumsQuery, DataTableParamModel>
	{
		public override DataTableParamModel Handle(GetAllAlbumsQuery query)
		{
			Expression<Func<Core.Album, bool>> filter = i => i.Title.Contains(query.sSearch);
			Expression<Func<Core.Album, string>> sorting = c => query.iSortCol_0 == 1 ? c.Title : c.Artist.Name;

			query.iTotalDisplayRecords = Session.Query<Core.Album>().ApplyFilter(query.sSearch, filter).Count();

			query.aaData = Session.Query<Core.Album>()
							.ApplyFilter(query.sSearch, filter)
							.ApplySorting(query.sSortDir_0, sorting)
							.ApplyPaging(query.iDisplayStart, query.iDisplayLength)
							.Select(Mapper.Map<Core.Album, AlbumModel>)
							.Select(c => new[] { Convert.ToString(c.AlbumId), c.Title, c.ArtistName });

			query.iTotalRecords = query.aaData.Count();
			
			return query;
		}
	}
}
