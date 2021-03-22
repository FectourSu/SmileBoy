using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmileBoy.Client.Core.Helpers
{
    public static class PaginationHelper
    {
        public static IQueryable<TModel> Pagination<TModel>(this IQueryable<TModel> queryable, int index, int pageSize)
        {
            return queryable
                .Skip((index - 1) * pageSize)
                .Take(pageSize);

        }

        public static IEnumerable<TModel> Pagination<TModel>(this IEnumerable<TModel> enumerable, int index, int pageSize)
        {
            return enumerable
                .Skip((index - 1) * pageSize)
                .Take(pageSize);
        }

        public static async Task<IEnumerable<TResult>> SelectAsync<TSource, TResult>(this IEnumerable<TSource> sources, Func<TSource, Task<TResult>> method)
        {
            return await Task.WhenAll(sources.Select(async s => await method(s)));
        }

    }
}
