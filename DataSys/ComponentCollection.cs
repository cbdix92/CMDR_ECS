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
        public IComponent[] Get()
        {
            return _data;
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
                    };
                    
                    _data[i].Parents.Add(i);

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
        public void FinalDestroy(IComponent target)
        {
            if (target.ID != _type)
                throw new ArgumentException($"{_type} collection does not contain {target.ID}");

            _data[target.Handle] = null;
        }
    }

    public class ComponentCollections<T>
        where T: IComponent, new()
    {
        private T[] _data = new T[Data.SizeStep];

        readonly Type _type = typeof(T);

        public int Count { get; private set; }

        public T this[int index]
        {
            get => _data[index];
        }
        public T[] Get()
        {
            return _data;
        }
        public T Generate<U>(IProfiler profiler = null)
            where U: T, IComponent, new()
        {
            for (int i = Count; i <= _data.Length; i++)
            {
                if (_data[i] == null)
                {
                    _data[i] = new U
                    {
                        Handle = i,
                        ID = typeof(U),
                    };

                    _data[i].Parents.Add(i);

                    if (profiler != null)
                        profiler.Profile(ref _data[i]);

                    Count++;
                    return _data[i];
                }
            }
            // No empty space encountered. Array needs to be resized.
            Array.Resize(ref _data, Data.SizeStep);
            return Generate<U>();
        }
        public void FinalDestroy(IComponent target)
        {
            if (target.ID != _type)
                throw new ArgumentException($"{_type} collection does not contain {target.ID}");

            _data[target.Handle] = default;
        }
    }
}
