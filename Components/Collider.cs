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

		public List<(int X, int Y, int Z)> GridKeys;

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
		
		
		private Vector3 _scale;
		public float ScaleX
		{
			get => _scale.X;
			set
			{
				Receive();
				_scale.X = value;
				if(value > SpatialIndexer.CellSize && !_static)
					SpatialIndexer.CellSize = (int)value;

				Send();
			}
		}
		
		public float ScaleY
		{
			get => _scale.Y;
			set
			{
				Receive();
				_scale.Y = value;
				if(value > SpatialIndexer.CellSize)
					SpatialIndexer.CellSize = (int)value;

				Send();
			}
		}
		
		public float ScaleZ
		{
			get => _scale.Z;
			set
			{
				Receive();
				_scale.Z = value;
				if(value > SpatialIndexer.CellSize)
					SpatialIndexer.CellSize = (int)value;
				
				Send();
			}
		}
		
		public void Init()
        {

        }
		public void SetBounds(Vector3 scale)
        {
			Receive();
			(ScaleX, ScaleY, ScaleZ) = (scale.X, scale.Y, scale.Z);
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
