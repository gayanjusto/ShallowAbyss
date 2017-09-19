using System;
using System.Linq;

namespace Assets.Scripts.Tools
{
    public static class ClassChildren
    {
        public static T[] GetArrayOfType<T>(params object[] constructorArgs) where T : class
        {
            T[] exporters = typeof(T)
              .Assembly.GetTypes()
              .Where(t => t.IsSubclassOf(typeof(T)) && !t.IsAbstract)
              .Select(t => (T)Activator.CreateInstance(t)).ToArray();

            return exporters;
        }
    }
}
