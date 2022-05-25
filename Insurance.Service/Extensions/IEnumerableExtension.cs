using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Insurance.Service.Extensions
{
    public static class IEnumerableExtension
    {
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            if (source != null && source.Any())
            {
                foreach (T item in source)
                {
                    action(item);
                }
            }
        }
    }
}
