using System;
using CMDR.Components;
using System.Collections.Generic;

namespace CMDR
{	
	public class ComponentCollection<T>
		where T: IComponent<T>, new()
	{
        private List<T> _data = new List<T>();
		
		public int Count { get => _data.Count; }
		
		public T this[int index]
		{
			get => _data[index];
		}
        public T Generate()
        {
            _data.Add(new T { Handle = _data.Count });
            return _data[_data.Count - 1];
        }
		public void FinalDestroy(T target)
        {
            _data.Remove(target);
        }
		
	}
}
