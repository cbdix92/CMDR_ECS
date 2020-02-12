using System;
using System.Collections;
using System.Collections.Generic;
using CMDR.Components;

namespace CMDR
{
    internal static class Data
    {
        public static GameObjectCollection GameObjects = new GameObjectCollection();
        public static Dictionary<Type, List<IComponent>> Components = new Dictionary<Type, List<IComponent>>();

        private static object _threadLock = new object();
        internal static void CreateGameObject()
        {
            // Creates a new instance of GameObject
            lock(_threadLock)
            {
                GameObjects.Add(new GameObject(GameObjects.Count));
            }
        }
        internal static void Destroy(Type target, int handle)
        {
            if(target == typeof(GameObject))
            {
                // Destroy GameObjects and all its components
                foreach (Type component in GameObjects[handle].Components.Keys)
                {
                    int c_index = GameObjects[handle].Components[component];
                    Components[component].RemoveAt(c_index);
                }
                return;
            }
            if (Components.ContainsKey(target))
            {
                Components[target].RemoveAt(handle);
            }
        }
    }
    public class GameObjectCollection
    {
        private List<GameObject> _data = new List<GameObject>();

        private object _threadLockDestroy = new object();
        private object _threadLockGenerate = new object();
        public GameObjectCollection() { }
        public int Generate()
        {
            lock(_threadLockGenerate)
            {
                _data.Add(new GameObject(_data.Count));
                return _data.Count - 1;
            }
        }
        public void Destroy(GameObject target)
        {
            lock(_threadLockDestroy)
            {
                _data.RemoveAt(target.Handle);
                _data.Insert(target.Handle, _data[_data.Count - 1]);
            }
        }
    }
}
