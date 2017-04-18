using System;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace NStore.Tests
{
    public static class AsyncExtensions
    {
        public static Task ForEachAsync<T>(
          this IEnumerable<T> source, int dop, Func<T, Task> body)
        {
            return Task.WhenAll(
                from partition in Partitioner.Create(source).GetPartitions(dop)
                select Task.Run(async delegate
                {
                    using (partition)
                        while (partition.MoveNext())
                            await body(partition.Current).ContinueWith(t =>
                                  {
                                      //observe exceptions
                                  });

                }));
        }
    }
}
