using System;
using System.Collections;
using System.Collections.Generic;
using CMDR.Components;
using System.Reflection;
using System.Linq;

namespace CMDR
{
    public static class Data
    {

		internal static Hashtable Components = new Hashtable();

        internal static GameObjectCollection GameObjects = new GameObjectCollection();



        private static object _threadLockGameObject = new object();
        private static object _threadLockComponent = new object();
		
        public static GameObject CreateGameObject()
        {
            // Creates a new instance of GameObject
            lock (_threadLockGameObject)
                return GameObjects.Generate();
        }
        public static T CreateComponent<T>()
            where T: IComponent<T>, new()
        {
            lock(_threadLockComponent)
            {
                dynamic tmp = Components[typeof(T)];
                return tmp.Generate<T>();
            }
        }
        public static void Destroy(GameObject target)
        {
            // UnParent all Components coupled to target
            // Parentless Components will automatically be destroyed
            foreach (Type type in target.Components.Keys)
            {
                dynamic targetCollection = Components[type];
                int componetHandle = target.Components[type];
                if (targetCollection[componetHandle].Parents.Remove(target.Handle))
                    targetCollection.FinalDestroy(targetCollection[target.Components[type]]);
            }
            GameObjects.Destroy(target);
        }
        public static void Destroy(IComponent target)
        {
            dynamic targetCollection = Components[target.ID];

            // Update parent gameobjects of the removal
            foreach (int parentIndex in target.Parents)
                GameObjects[parentIndex].Components.Remove(target.ID);

            // Remove the component from the collection
            targetCollection.FinalDestroy(target);
        }
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
