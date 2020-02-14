using System;
using CMDR.Components;

namespace CMDR
{
    public class ComponentCollection
    {
        private IComponent[] _data = new IComponent[Data.SizeStep];
        
        readonly Type _type;
        
        public int Count { get; private set; }

        public IComponent this[int index]
        {
            get => _data[index];
        }

        public ComponentCollection(Type type)
        {
            _type = type;
        }
        public T Generate<T>(IProfiler profiler = null)
            where T : IComponent, new()
        {
            for (int i = Count; i <= _data.Length; i++)
            {
                if (_data[i] == null)
                {
                    _data[i] = new T
                    {
                        Handle = i,
                        ID = typeof(T),
                        Parent = -1;
                    };

                    if (profiler != null)
                        profiler.Profile(ref _data[i]);
                    
                    Count++;
                    return (T)_data[i];
                }
            }
            // No empty space encountered. Array needs to be resized.
            Array.Resize(ref _data, Data.SizeStep);
            return Generate<T>();
        }
        public void Destroy(IComponent target)
        {
            if (target.ID != _type)
                throw new ArgumentException($"{_type} collection does not contain {target.ID}");

            _data[target.Handle] = null;
        }
    }

    /*
    public class ComponentCollection
    {
        private Dictionary<Type, IComponent[]> _data = new Dictionary<Type, IComponent[]>();
        public int Count { get; private set; }

        public int SizeStep = 10;

        public T Generate<T>(ComponentProfiler profiler = null)
            where T : IComponent, new()
        {
            Type t = typeof(T);
            for (int i = 0; i < _data[t].Length; i++)
            {
                if (_data[t][i] == null)
                {
                    _data[t][i] = new T();

                    if (profiler != null)
                        profiler.Profile(_data[t][i]);

                    Count++;
                    return (T)_data[t][i];
                }
            }
            // No empty space encountered. Array needs to be resized
            IComponent[] newArray = new IComponent[_data[t].Length + SizeStep];
            _data[t].CopyTo(newArray, 0);
            _data[t] = newArray;
            return Generate<T>(profiler);
        }

        public void Destroy(IComponent target)
        {
            Type t = target.ID;
            int targetIndex = target.Handle;
        }
    }
    */
}
