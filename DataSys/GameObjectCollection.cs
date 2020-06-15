using System;
using System.Collections;
using System.Collections.Generic;

namespace CMDR
{
    internal class GameObjectCollection
    {
        private SGameObject[] _data = new SGameObject[Data.StorageScale];

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
        public SGameObject Generate(Scene scene)
        {
            if (Count == _data.Length)
                Array.Resize(ref _data, _data.Length + Data.StorageScale);


            _data[Count] = new SGameObject
            {
                ID = Count,
                Scene = scene,
                Components = new KeyValuePair<Type, int>[Data.ComponentTotal]
            };
            return _data[Count++];
            //Count++;
            //return _data[Count - 1].Handle;
        }
        public void FinalDestroy(int gameObject)
        {
            _data[gameObject] = _data[Count--];

        }
        public IEnumerator GetEnumerator()
        {
            return _data.GetEnumerator();
        }

        public void Update(SGameObject gameObject)
        {
            _data[gameObject.ID] = gameObject;
        }
    }
}
