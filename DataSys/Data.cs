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
        public static byte StorageScale = Byte.MaxValue;
		
        public static int ComponentTotal { get; private set; }

        public static KeyValuePair<Type, int>[] KeyValues;

        readonly static IEnumerable _types = Assembly.GetExecutingAssembly().GetTypes().Where(T => T.GetInterfaces().Contains(typeof(IComponent)));
        
        public static HashSet<Type> Types { get; private set; }

        internal static void GenerateComponents(out Hashtable output)
        {
            output = new Hashtable();

            Types = new HashSet<Type>();

            var TComponentCollection = typeof(ComponentCollection<>);

            foreach (Type TComponent in _types)
            {
                Types.Add(TComponent);

                if (TComponent.Name == typeof(IComponent<>).Name)
                    continue;

                var TNew = TComponentCollection.MakeGenericType(TComponent);
                
                output.Add(TComponent, Activator.CreateInstance(TNew));

                ComponentTotal++;
            }
        }

        internal static void Update(SGameObject gameObject)
        {
            gameObject.Scene.GameObjects.Update(gameObject);
        }
    }
}
