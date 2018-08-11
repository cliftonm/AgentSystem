using System;
using System.Collections.Generic;

namespace Clifton.AgentSystem.Lib
{
    public static class ExtensionMethods
    {
        public static void ForEach<T>(this IEnumerable<T> src, Action<T> action)
        {
            foreach (var item in src)
            {
                action(item);
            }
        }
    }
}
