using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;

namespace Clifton.AgentSystem.Agents
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
