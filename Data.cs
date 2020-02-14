using System;
using System.Collections.Generic;
using CMDR.Components;

namespace CMDR
{
    public static class Data
    {
        public static int SizeStep = 10;

        public static GameObjectCollection GameObjects = new GameObjectCollection();
        public static Dictionary<Type, ComponentCollection> Components = new Dictionary<Type, ComponentCollection>();

        private static object _threadLock = new object();
        public static GameObject CreateGameObject()
        {
            // Creates a new instance of GameObject
            lock(_threadLock)
            {
                return GameObjects.Generate();
            }
        }
        public static void Destroy(GameObject target)
        {
            // Destroy all IComponents attached to GameObject
            foreach(IComponent component in target.Components.Keys)
            {
                Destroy(Data.Components[component.ID][component.Handle]);
            }
            GameObjects.Destroy(target);
        }
        public static void Destroy(IComponent target)
        {
            // Update parent object of the removal
            int parent = Components[target.ID][target.Handle].Parent;
            GameObjects[parent].Components.Remove(target.ID);

            // remove the component from the collection
            Components[target.ID].Destroy(target);
        }
    }
}
