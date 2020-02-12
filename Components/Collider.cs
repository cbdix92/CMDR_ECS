using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMDR.Components
{
    public struct Collider : IComponent
    {
        #region IComponent
        public int Parent { get; set; }
        public int Handle { get; set; }
        public Type ID { get; set; }
        #endregion

        public bool[,] Data;

        public Collider(int parent, int handle)
        {
            Parent = parent;
            Handle = handle;
            ID = typeof(Collider);

            Data = BitCollider.GenerateColData(Parent);

        }
    }
}
