using System.Collections.Generic;
using System.Linq;

namespace FidelizeDotz.Services.Api.CrossCutting.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<T> Add<T>(this IEnumerable<T> source, T itemToAdd)
        {
            if (source != null)
                ((IList<T>) source).Add(itemToAdd);

            return source;
        }

        /// <summary>
        ///     Return a new List with the index of each element on It
        /// </summary>
        /// <typeparam name="T">Type of the element item</typeparam>
        /// <param name="self">List to be used as source</param>
        /// <returns>List items with yours Index</returns>
        public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> self)
        {
            return self.Select((item, index) => (item, index));
        }
    }
}