using System;
using System.Linq;
using System.Linq.Expressions;

namespace Chinook.Infrastructure.Infractructure
{
	public static class IssueServicesExtensions
	{
		public static IQueryable<T> ApplyFilter<T>(this IQueryable<T> query, string sSearch, Expression<Func<T, bool>> filter)
		{
			//Text Filter
			if (filter != null && !string.IsNullOrEmpty(sSearch))
				query = query.Where(filter);

			return query;
		}

		public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> data, int itemsDisplayed, int pageSize)
		{
			if (pageSize > 0 && itemsDisplayed > 0)
				data = data.Skip(itemsDisplayed);
			
			data = data.Take(pageSize);

			return data;
		}

		public static IQueryable<T> ApplySorting<T>(this IQueryable<T> data, string sortDirection, Expression<Func<T, string>> orderingFunction)
		{
			return sortDirection == "asc" ? data.OrderBy(orderingFunction) : data.OrderByDescending(orderingFunction);
		}
	}
}
