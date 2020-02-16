using System;
using System.Collections.Generic;
using System.Linq;
using CMDR.Components;

namespace CMDR.Systems
{
	/*
    internal struct Cell
    {
        public (int Y, int X) GridKey;
        public List<int> Cache;


        public Cell((int Y, int X)gridKey)
        {
            GridKey = gridKey;
            Cache = new List<int>();
        }
        public void Add(int handle)
        {
            Cache.Add(handle);
        }
        public void Remove(int handle)
        {
            Cache.Remove(handle);
        }
    }
	*/
    internal static class SpatialIndexer
    {
        public static Dictionary<(int, int), List<int>> GridCells = new Dictionary<(int, int), List<int>>();

        private static int _cellSize = 30;
        public static int CellSize
        {
            get => _cellSize;
            set
            {
                if (value != _cellSize)
                {
                    _cellSize = value;
                    foreach(GameObject gameObject in Data.GameObjects)
						CalcGridPos(gameObject);
                }
            }
        }
        internal static int[] GetNearbyColliders(Collider collider)
        {
            HashSet<int> hash = new HashSet<int>();
            int[] result;

            for (int i = 0; i < collider.OverlappedCells.Count; i++)
                for (int c = 0; c < collider.OverLappedCells[i].Cache.Count; c++)
                    hash.Add(collider.OverLappedCells[i].Cache[c]);

            result = new int[hash.Count];
            hash.CopyTo(result);
            return result;
        }
		
		internal static void CalcGridPos(GameObject gameObject)
		{
			Type t = typeof(Transform);
			Type c = typeof(Collider);
			
			Transform transform = Data.Components[t][gameObject[t]];
			Colldier collider = Data.Components[c][gameObject[c]];
			
			// Top left corner converted to grid cordinates
			(int X, int Y) p1 = ((int)Math.Floor((double)transform.X / CelLSize), (int)Math.Floor((double)transform.Y / CellSize);
			
			// Bottom right corner converted to grid cordinates
			(int X, int Y) p2 = ((int)Math.Floor((double)(transform.X + collider.Width) / CelLSize), (int)Math.Floor((double)(transform.Y + collider.Width) / CellSize);
			
			// Place gameObject in all it's occupied cells
			for (int y = p1.Y; y <= p2.Y; y++)
				for(int x = p1.X; x <= p2.X; x++)
				{
					// Create new cells that don't exist
					if(!GridCells.ContainsKey((y, x))
						GridCells[(y, x)] = new List<int>();
					
					GridCells[(y, x)].Add(gameObject.Handle);
					collider.OccupiedGridCells.Add((y, x));
				}
			
		}
    }
}
