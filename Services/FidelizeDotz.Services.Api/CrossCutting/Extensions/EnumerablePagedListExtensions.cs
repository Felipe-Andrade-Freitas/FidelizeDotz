using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FidelizeDotz.Services.Api.CrossCutting.Infra;
using Microsoft.EntityFrameworkCore;

namespace MetLife.Sinistro.Api.CrossCutting.Extensions
{
    public static class EnumerablePagedListExtensions
    {
        public static IPagedList<T> ToPagedList<T>(
            this IEnumerable<T> source,
            int pageIndex,
            int pageSize,
            int indexFrom = 0)
        {
            return new PagedList<T>(source, pageIndex, pageSize, indexFrom);
        }

        public static async Task<IPagedList<T>> ToPagedListAsync<T>(
            this IQueryable<T> source,
            int pageIndex,
            int pageSize,
            int indexFrom = 0,
            CancellationToken cancellationToken = default)
        {
            if (indexFrom > pageIndex)
                throw new ArgumentException(
                    string.Format("indexFrom: {0} > pageIndex: {1}, must indexFrom <= pageIndex", indexFrom,
                        pageIndex));
            var count = await source.CountAsync(cancellationToken).ConfigureAwait(false);
            var items = await source.Skip((pageIndex - indexFrom) * pageSize).Take(pageSize)
                .ToListAsync(cancellationToken).ConfigureAwait(false);
            var pagedList = new PagedList<T>
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                IndexFrom = indexFrom,
                TotalCount = count,
                Items = items,
                TotalPages = (int) Math.Ceiling(count / (double) pageSize)
            };
            IPagedList<T> pagedList1 = pagedList;
            items = null;
            pagedList = null;
            return pagedList1;
        }
    }
}