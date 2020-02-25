using System;
using System.Collections;
using System.Collections.Generic;

namespace CMDR
{
    internal class GameObjectCollection
    {
        private SGameObject[] _data = new SGameObject[Data.Size];

        public int Count { get; private set; }

        public SGameObject this[int index]
        {
            get => _data[index];
        }
        public SGameObject[] Get()
        {
            SGameObject[] result = new SGameObject[Count];
            Array.Copy(_data, result, Count);
            return result;
        }
        public GameObject Generate(Scene scene)
        {
            if (Count == _data.Length)
                Array.Resize(ref _data, _data.Length + Data.Size);


            _data[Count] = new SGameObject
            {
                Handle = new GameObject { ID = Count, Scene = scene },
                ID = Count,
                Scene = scene,
                Components = new KeyValuePair<Type, int>[Data.ComponentTotal]
        };
            return _data[Count - 1].Handle;
        }
        public void FinalDestroy(int gameObject)
        {
            _data[gameObject] = _data[Count--];

        }
        public IEnumerator GetEnumerator()
        {
            return _data.GetEnumerator();
        }
    }
}
