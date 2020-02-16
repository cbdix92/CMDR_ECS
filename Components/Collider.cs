using System;
using System.Drawing;
using System.Collections.Generic;

namespace CMDR.Components
{
    public struct Collider : IComponent
    {
        #region IComponent
        public int Handle { get; set; }
        public Type ID { get; set; }
        public ParentList Parents { get; set; }
        #endregion

		public List<(int X, int Y)> OccupiedGridKeys;
		
		public int Height;
		public int Width;
		
        public bool[,] Data;
    }
}
