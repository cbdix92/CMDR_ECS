using System;
using System.Drawing;
using System.Collections.Generic;
using CMDR.Systems;

namespace CMDR.Components
{
    public struct Collider : IComponent<Collider>
    {
		#region IComponent
		public int ID { get; set; }
		public int Parent { get; set; }
		public Type Type { get; set; }
		public Scene Scene { get; set; }
		#endregion

		public List<(int X, int Y)> GridKeys;

		public bool[,] ColData;

		private bool _static;
		public bool Static
		{
			get => _static;
			set
			{
				_static = value;
				Update();
			}
		}

        private int _height;
		private int _width;
		public int Height
		{
			get => _height;
			set
			{
				_height = value;
				if(value > SpatialIndexer.CellSize)
					SpatialIndexer.CellSize = value;

				Update();
			}
		}
		public int Width
		{
			get => _width;
			set
			{
				_width = value;
				if(value > SpatialIndexer.CellSize)
					SpatialIndexer.CellSize = value;

				Update();
			}
		}

		public void Update()
		{
			Scene.Update<Collider>(this);
		}
	}
}
