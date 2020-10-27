using System;
using System.Collections.Generic;
using System.Linq;
using CMDR;
using CMDR.Components;

namespace CMDR.Systems
{
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
					Transform[] transforms = SceneManager.ActiveScene.Components.Get<Transform>();
					Collider[] colliders = SceneManager.ActiveScene.Components.Get<Collider>();

                    foreach(SGameObject gameObject in SceneManager.ActiveScene.GameObjects)
					{
						if(gameObject.Contains<Collider>() && gameObject.Contains<Transform>())
						CalcGridPos(ref colliders[gameObject.Get<Collider>()], transforms[gameObject.Get<Transform>()]);
					}
                }
            }
        }
		/// <summary>
		/// Returns a list of GameObject ID's near the provided collider.
		/// </summary>
        internal static int[] GetNearbyColliders(Collider collider)
        {
			// HashSet used to prevent GameObject ID duplication
            HashSet<int> hash = new HashSet<int>();
            int[] result;

            for (int i = 0; i < collider.GridKeys.Count; i++)
				foreach(int id in GridCells[collider.GridKeys[i]])
					if (id != collider.Parent)
						hash.Add(id);

            result = new int[hash.Count];
            hash.CopyTo(result);
            return result;
        }
		
		internal static void CalcGridPos(ref Collider collider, Transform transform)
		{
			if (collider.GridKeys == null)
				collider.GridKeys = new List<(int X, int Y)>();

			// Remove the Colliders parent from all grid cells
			foreach ((int, int) keys in collider.GridKeys)
				GridCells[keys].Remove(collider.Parent);
			
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
					
					GridCells[(y, x)].Add(collider.Parent);
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
			collider.Update();
		}
    }
}
