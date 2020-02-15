using System;
using System.Collections.Generic;
using CMDR.Components;
using System.Reflection;

namespace CMDR
{
    public static class Data
    {
        public static int SizeStep = 10;

        public static GameObjectCollection GameObjects = new GameObjectCollection();
        public static Dictionary<Type, ComponentCollection> Components = new Dictionary<Type, ComponentCollection>();

        private static object _threadLockGameObject = new object();
        private static object _threadLockComponent = new object();
        public static GameObject CreateGameObject()
        {
            // Creates a new instance of GameObject
            lock (_threadLockGameObject)
                return GameObjects.Generate();
        }
        public static T CreateComponent<T>(Type type, IProfiler profiler)
            where T: IComponent, new()
        {
            lock(_threadLockComponent)
                return Components[type].Generate<T>(profiler);
        }
        public static void Destroy(GameObject target)
        {
            // UnParent all Components coupled to target
            // Parentless Components will automatically be destroyed
            foreach(Type componentType in target.Components.Keys)
            {
                target.UnParent(componentType, target.Components[componentType]);
            }
            GameObjects.Destroy(target);
        }
        public static void Destroy(IComponent target)
        {

            // Update parent object of the removal
            foreach (int parent in Components[target.ID][target.Handle].Parents.Get)
                GameObjects[parent].Components.Remove(target.ID);

            // Remove the component from the collection
            Components[target.ID].FinalDestroy(target);
        }
        internal static void UnParent(this GameObject parent, Type type, int handle)
        {
            // When a component no longer has parents it is removed
            if (Components[type][handle].Parents.Remove(parent.Handle))
                Components[type].FinalDestroy(Components[type][handle]);
        }
		internal static void Init()
		{
			foreach (Type type in Assembly.GetExecutingAssembly().GetTypes().Where(x => x.GetInterfaces().Contains(typeof(IComponent))))
			{
				Components.Add(type, new ComponentCollection(type));
			}
			
		}
    }
}
