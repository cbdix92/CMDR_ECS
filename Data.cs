using System;
using System.Collections.Generic;
using CMDR.Components;

namespace CMDR
{
    public static class Data
    {
        public static GameObjectCollection GameObjects = new GameObjectCollection();
        public static Dictionary<Type, List<IComponent>> Components = new Dictionary<Type, List<IComponent>>();

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
            // To Do ... 

            // Update parent object of the removal

            // remove the component from the collection
        }
    }
    public class GameObjectCollection
    {
        private GameObject[] _data = new GameObject[5];

        private int _count;
        public int Count { get; private set; }

        public GameObject Generate()
        {
            for (int i = 0; i < _data.Length; i++)
            {
                if (_data[i] == null)
                {
                    _data[i] = new GameObject(i);
                    Count++;
                    return _data[i];
                }
            }
            // No empty space encountered. Array needs to be resized.
            GameObject[] newArray = new GameObject[_data.Length + 5];
            _data.CopyTo(newArray, 0);
            _data = newArray;
            return Generate();
        }
        public void Destroy(GameObject target)
        {
            int targetIndex = target.Handle;
            Count--;
            if (Count == 0)
            {
                _data[targetIndex] = null;
                return;
            }
            // Place last object in the array in the targets index, and adjust the objects handle
            _data.SetValue(_data[Count], targetIndex);
            _data[targetIndex].Handle = targetIndex;

            _data[Count] = null;

        }
        public class ComponentData<T>
            where T : IComponent, new()
        {
            private T[] _data;

            private int _count;
            public int Count { get; private set; }
            public ComponentData(int initialSize)
            {
                _data = new T[initialSize];
            }
            public T Generate(ComponentProfiler profiler = null)
            {
                for (int i = 0; i < _data.Length; i++)
                {
                    if (_data[i] == null)
                    {
                        _data[i] = new T();
                        if (profiler != null)
                            profiler.Profile(_data[i]);
                        Count++;
                        return _data[i];
                    }
                }
                // No empty space encountered. Array needs to be resized.
                T[] newArray = new T[_data.Length + 10];
                _data.CopyTo(newArray, 0);
                _data = newArray;
                return Generate();
            }
            public void Destroy(T target)
            {

            }
        }
    }
}
