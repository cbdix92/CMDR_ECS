using System;
using System.Collections.Generic;
using System.Linq;
using CMDR;
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
		public static int StorageThreshold = 100;
		public static int StorageStep = 50;
		
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

            for (int i = 0; i < collider.GridKeys.Count; i++)
				foreach(int handle in GridCells[collider.GridKeys[i]])
					hash.Add(handle);

            result = new int[hash.Count];
            hash.CopyTo(result);
            return result;
        }
		
		internal static void CalcGridPos(GameObject gameObject)
		{
			Type t = typeof(Transform);
			Type c = typeof(Collider);
			
			Transform transform = (Transform)Data.Components[t][gameObject[t]];
			Collider collider = (Collider)Data.Components[c][gameObject[c]];

			// Remove the gameObject from all grid cells
			foreach ((int, int) keys in collider.GridKeys)
				GridCells[keys].Remove(gameObject.Handle);
			
			// Top left corner converted to grid cordinates
			(int X, int Y) p1 = ((int)Math.Floor(transform.X / CellSize), (int)Math.Floor(transform.Y / CellSize));
			
			// Bottom right corner converted to grid cordinates
			(int X, int Y) p2 = ((int)Math.Floor((transform.X + collider.Width) / CellSize), (int)Math.Floor((transform.Y + collider.Width) / CellSize));
			
			// Place gameObject in all it's occupied cells
			for (int y = p1.Y; y <= p2.Y; y++)
				for(int x = p1.X; x <= p2.X; x++)
				{
					// Create new cells that don't exist
					if(!GridCells.ContainsKey((y, x)))
						GridCells[(y, x)] = new List<int>();
					
					GridCells[(y, x)].Add(gameObject.Handle);
					collider.GridKeys.Add((y, x));
				}
				
			// Remove empty grid cells
			if(GridCells.Count > StorageThreshold)
			{
				int DelCount = 0;
				foreach((int, int) keys in GridCells.Keys)
				{
					if(GridCells[keys].Count == 0)
					{
						DelCount++;
						GridCells.Remove(keys);
					}
				}
				// More filled cells exist than the StorageThreshold
				// Raise the Threshold to prevent it from being checked every update
				if (DelCount == 0)
					StorageThreshold += StorageStep;
			}
			
		}
    }
}
