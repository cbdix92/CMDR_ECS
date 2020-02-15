using System;

namespace CMDR.Components
{
    public class Collider : IComponent
    {
        #region IComponent
        public int Handle { get; set; }
        public Type ID { get; set; }
        public ParentList Parents { get; set; }
        #endregion

        public bool[,] ColData;
    }
}
