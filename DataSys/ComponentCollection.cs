using System;
using CMDR.Components;
using System.Collections.Generic;

namespace CMDR
{	
    public class ComponentCollection<T>
        where T: struct, IComponent
    {
        private T[] _data = new T[Data.Size];

        public int Count { get; private set; }

        public T this[int index]
        {
            get => _data[index];
        }
        public T[] Get()
        {
            T[] result = new T[Count];
            Array.Copy(_data, result, Count);
            return result;
        }
        public int Add(T component)
        {
            if (Count == _data.Length)
                Array.Resize(ref _data, _data.Length + Data.Size);
            
            _data[Count] = component;
            
            Count++;
            return Count - 1;
        }
        public void Remove(int component)
        {
            // The last component in the array will overwrite the one that we want to remove.
            _data[component] = _data[Count--];

            // Tell the moved components parent of it's new position
            SceneManager.ActiveScene.GameObjects[_data[component].Parent].ComponentMoved(typeof(T), Count);
        }
    }
}
