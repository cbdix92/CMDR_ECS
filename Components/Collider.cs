using System;

namespace CMDR.Components
{
    public struct Collider : IComponent
    {
        #region IComponent
        public int Handle { get; set; }
        public Type ID { get; set; }
        public int Parent { get; set; }
        #endregion

        public bool[,] ColData { get; set; }
    }
}
