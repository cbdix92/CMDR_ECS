using System;
using System.Linq;
using CMDR.Components;
using System.Reflection;
using System.Collections;

namespace CMDR
{
    public static class Data
    {
        public static byte Size = Byte.MaxValue;
        internal static void GenerateComponents(out Hashtable output)
        {
            output = new Hashtable();

            var TComponentCollection = typeof(ComponentCollection<>);

            foreach (Type TComponent in Assembly.GetExecutingAssembly().GetTypes().Where(T => T.GetInterfaces().Contains(typeof(IComponent))))
            {
                var TNew = TComponentCollection.MakeGenericType(TComponent);
                output.Add(TComponent, Activator.CreateInstance(TNew));
            }
        }
    }
}
