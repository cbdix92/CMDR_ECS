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

		public List<(int X, int Y)> GridKeys;
		
		private int _height;
		private int _width;
		
		public int Height
		{
			get => _height;
			set
			{
				_height = value;
				if(value > SpatialIndexer.CellSize)
					CellSize = value;
			}
		}
		public int Width;
		{
			get => _width;
			set
			{
				_width = value;
				if(value > SpatialIndexer.CellSize)
					CellSize = value;
			}
		}
		
        public bool[,] Data;
    }
}
