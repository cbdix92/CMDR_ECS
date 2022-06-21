using System;
using System.Collections;
using System.Collection.Generic;

namespace CMDR
{
    internal protected class GameObjectCollection
    {
        #region PUBLIC_MEMBERS

        public int Count { get; private set; }

        public GameObject this[int index]
        {

            get
            {

                if(index >= Count || index < 0)
                    throw new IndexOutOfRangeException($"{index} is out of range for GameObjectCollection");
                
                return _data[index];

            }
        }

        #endregion

        #region PRIVATE_MEMBERS

        private GameObject[] _data;

        #endregion

        #region CONSTRUCTOR

        internal GameObjectCollection()
        {
            _data = new GameObject[Data.StorageScale];
        }

        #endregion

        #region PUBLIC_METHODS

        public GameObject Add(GameObject gameObject)
        {

            if(Count == _data.Length)
                Array.Resize(ref _data, _data.Length + Data.StorageScale);
            
            _data[Count] = gameObject;

            return _data[Count++];

        }

        public void Remove(int id)
        {
            // TODO ...
            // Remove all GameObjects components

            // TODO ... 
            // Tell DataSystem to remove GameObject from Query

            if(id == --Count)
            {
                _data[id] = GameObject.Default;
                
                return;
            }

            // Place GameObject at the end of the array in the deleted GameObjects place.
            _data[id] = _data[Count];

            GameObjectMoved(ref _data[id], id);

        }

        public GameObject[] ToArray()
        {
            
            ArraySegment<GameObject> _ = new ArraySegment<GameObject>(_data, 0, Count);
            
            return _.ToArray();

        }

        #endregion

        #region PRIVATE_METHODS

        private void GameObjectMoved(ref GameObject gameObject, int newID)
        {
            gameObject.ID = newID;
            
            // TODO ... 
            // Tell Components of GameObject the new Parent ID
            
        }

        #endregion
    }
}