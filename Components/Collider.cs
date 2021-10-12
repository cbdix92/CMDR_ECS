using System;
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
				Receive();
				_static = value;
				Send();
			}
		}

        private int _height;
		private int _width;
		public int Height
		{
			get => _height;
			set
			{
				Receive();
				_height = value;
				if(value > SpatialIndexer.CellSize && !_static)
					SpatialIndexer.CellSize = value;

				Send();
			}
		}
		public int Width
		{
			get => _width;
			set
			{
				Receive();
				_width = value;
				if(value > SpatialIndexer.CellSize)
					SpatialIndexer.CellSize = value;

				Send();
			}
		}
		public void SetBounds(RenderData renderData)
        {
			Receive();
			(Width, Height) = (renderData.ImgData.Width, renderData.ImgData.Height);
			Send();
        }
		public void SetBounds(int width, int height)
        {
			Receive();
			(Width, Height) = (width, height);
			Send();
        }
		public void GenerateColData(string src)
        {
			BitCollider.GenerateColData(ref this, src);
        }
		public void Receive()
        {
			this = Scene.Get<Collider>(ID);
		}
		public void Send()
		{
			Scene.Update<Collider>(this);
		}
	}
}
