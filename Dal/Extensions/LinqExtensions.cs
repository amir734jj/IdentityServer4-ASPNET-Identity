using System;
using System.Collections.Generic;

namespace Dal.Extensions
{
    public static class LinqExtensions
    {
        /// <summary>
        /// ForEach for IEnumerable
        /// </summary>
        /// <param name="enumerable"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var element in enumerable)
            {
                action(element);
            }
        }
    }
}