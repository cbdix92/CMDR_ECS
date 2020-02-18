using System;
using System.Collections;
using System.Collections.Generic;

namespace CMDR.Components
{
    public class ParentList
    {
        private List<int> _parents = new List<int>();
        public int this[int index]
        {
            get => _parents[index];
            set => _parents[index] = value;
        }
        public List<int> Get
        {
            get => _parents;
        }
        public void Add(int parent)
        {
            _parents.Add(parent);
        }
        public bool Remove(int parent)
        {
            _parents.Remove(parent);

            // Signals the Component to Destroy() when it no longer has parents.
            return _parents.Count == 0;
        }
        public IEnumerator GetEnumerator()
        {
            return _parents.GetEnumerator();
        }
    }
}
