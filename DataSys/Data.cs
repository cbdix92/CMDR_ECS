using System;
using System.Linq;
using CMDR.Components;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

namespace CMDR
{
    public static class Data
    {
        public static byte Size = Byte.MaxValue;
        public static int ComponentCount { get; private set; }

        public static KeyValuePair<Type, int>[] KeyValues;
        internal static void GenerateComponents(out Hashtable output)
        {

            IEnumerable Types = Assembly.GetExecutingAssembly().GetTypes().Where(T => T.GetInterfaces().Contains(typeof(IComponent)));

            output = new Hashtable();

            var TComponentCollection = typeof(ComponentCollection<>);

            foreach (Type TComponent in Types)
            {
                if (TComponent.Name == typeof(IComponent<>).Name)
                    continue;
                var TNew = TComponentCollection.MakeGenericType(TComponent);
                output.Add(TComponent, Activator.CreateInstance(TNew));

                ComponentCount++;
            }

            KeyValues = new KeyValuePair<Type, int>[ComponentCount];
            int i = 0;
            foreach (Type type in Types)
            {
                KeyValues[i] = new KeyValuePair<Type, int>(type, -1);
                i++;
            }
        }
        internal static KeyValuePair<Type, int>[] GenerateKeyValues()
        {
            KeyValuePair<Type, int>[] result = new KeyValuePair<Type, int>[ComponentCount];
            Array.Copy(KeyValues, result, ComponentCount);
            return result;
        }
    }
}
