using System;

namespace CMDR
{
    public class GameObjectCollection
    {
        private GameObject[] _data = new GameObject[5];

        public int Count { get; private set; }

        public GameObject this[int index]
        {
            get => _data[index];
        }
        public GameObject Generate()
        {
            // Find empty space for the new GameObject
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
    }
}
