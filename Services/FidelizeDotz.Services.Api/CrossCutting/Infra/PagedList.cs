using System;
using System.Collections.Generic;
using System.Linq;

namespace FidelizeDotz.Services.Api.CrossCutting.Infra
{
    public class PagedList<T> : IPagedList<T>
    {
        internal PagedList(IEnumerable<T> source, int pageIndex, int pageSize, int indexFrom)
        {
            if (indexFrom > pageIndex)
                throw new ArgumentException(
                    string.Format("indexFrom: {0} > pageIndex: {1}, must indexFrom <= pageIndex", indexFrom,
                        pageIndex));
            if (source is IQueryable<T> source1)
            {
                PageIndex = pageIndex;
                PageSize = pageSize;
                IndexFrom = indexFrom;
                TotalCount = source1.Count();
                TotalPages = (int) Math.Ceiling(TotalCount / (double) PageSize);
                Items = source1.Skip((PageIndex - IndexFrom) * PageSize).Take(PageSize).ToList();
            }
            else
            {
                PageIndex = pageIndex;
                PageSize = pageSize;
                IndexFrom = indexFrom;
                TotalCount = source.Count();
                TotalPages = (int) Math.Ceiling(TotalCount / (double) PageSize);
                Items = source.Skip((PageIndex - IndexFrom) * PageSize).Take(PageSize).ToList();
            }
        }

        internal PagedList()
        {
            Items = new T[0];
        }

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        public int TotalPages { get; set; }

        public int IndexFrom { get; set; }

        public IList<T> Items { get; set; }

        public bool HasPreviousPage => PageIndex - IndexFrom > 0;

        public bool HasNextPage => PageIndex - IndexFrom + 1 < TotalPages;
    }
}