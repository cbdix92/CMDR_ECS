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

		private static Hashtable _comps = new Hashtable();

        public static GameObjectCollection GameObjects = new GameObjectCollection();



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
                dynamic tmp = _comps[typeof(T)];
                return tmp.Generate<T>();
            }
        }
        public static void Destroy(GameObject target)
        {
            // UnParent all Components coupled to target
            // Parentless Components will automatically be destroyed
            foreach(Type type in target.Components.Keys)
            {
                dynamic targetCollection = _comps[type];
                int componetHandle = target.Components[type];
                if (targetCollection[componetHandle].Parents.Remove(target.Handle))
                    Destroy<type>(targetCollection[componetHandle]);
            }
            GameObjects.Destroy(target);
        }
        public static void Destroy<T>(T target)
            where T: IComponent<T>
        {
            dynamic targetCollection = _comps[target.ID];

            // Update parent gameobjects of the removal
            foreach (int parentIndex in target.Parents)
                GameObjects[parentIndex].Components.Remove(target.ID);

            // Remove the component from the collection
            targetCollection.FinalDestroy(target);
        }
		internal static void Init()
		{
			foreach (Type type in Assembly.GetExecutingAssembly().GetTypes().Where(x => x.GetInterfaces().Contains(typeof(IComponent))))
			{
				_comps.Add(type, new ComponentCollection<typeof(type)>());
			}
			
		}
    }
}
