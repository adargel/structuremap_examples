using System.Collections.Generic;
using NUnit.Framework;

namespace UsingStructureMap
{
    public static class StructureMapTestExtensions
    {
        public static void ShouldContainInstanceOfType<T>(this IEnumerable<object> source)
        {
            var found = false;
            foreach (var instance in source)
            {
                if (instance is T)
                    found = true;
            }
            if (!found)
                throw new AssertionException(string.Format("Expected instance of Type {0}, but got {1}",
                                                           typeof (T), source));
        }
    }
}