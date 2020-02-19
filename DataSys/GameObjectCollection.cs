using System;
using System.Collections;
using System.Collections.Generic;

namespace CMDR
{
    public class GameObjectCollection
    {
        private List<GameObject> _data = new List<GameObject>();

        public int Count { get; private set; }

        public GameObject this[int index]
        {
            get => _data[index];
        }
        public List<GameObject> Get()
        {
            return _data;
        }
        public GameObject Generate()
        {
            _data.Add(new GameObject(_data.Count));
            return _data[_data.Count - 1];
        }
        public void Destroy(GameObject gameObject)
        {
            _data.Remove(gameObject);;

        }
        public IEnumerator GetEnumerator()
        {
            return _data.GetEnumerator();
        }
    }
}
