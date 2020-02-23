using System;
using CMDR.Components;
using System.Collections;
using System.Collections.Generic;

namespace CMDR
{	
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
}
