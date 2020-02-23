using System;
using CMDR.Components;
using System.Collections.Generic;

namespace CMDR
{	
    /*
	public class ComponentCollection<T>
		where T: struct, IComponent<T>
	{
        private List<T> _data = new List<T>();
		
		public int Count { get => _data.Count; }
        public List<T> Get()
        {
            return _data;
        }
        public void Add(T component)
        {
            _data.Add(component);
        }
		public void Remove(int component)
        {
            _data.Remove(_data[component]);
        }
	}
    */
    public class ComponentCollection<T>
        where T: struct, IComponent<T>
    {
        private T[] _data = new T[Data.Size];

        public int Count { get; private set; }

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
            _data[component] = _data[Count--];
        }
    }
}
