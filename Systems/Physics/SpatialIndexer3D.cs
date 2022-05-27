using System;
using System.Collections.Generic;
using CMDR.Components;


namespace CMDR.Systems
{
	internal static class SpatialIndexer
	{
		public static int StorageThreshold = 500;
		public static int StorageStep = 50;
		
		public static Dictionary<(int, int, int), List<int>> GridCells = new Dictionary<(int, int, int), List<int>>();
		
		private static int _cellSize = 30;
		public static int CellSize
		{
			get => _cellSize;
			set
			{
				if(value != _cellSize)
				{
					Transform[] transforms = SceneManager.ActiveScene.Components.Get<Transform>();
					Collider[] colliders = SceneManager.ActiveScene.Components.Get<Collider>();
					
					foreach(SGameObject gameObject in SceneManager.ActiveScene.GameObjects)
					{
						if(gameObject.Contains<Collider>() && gameObject.Contains<Transform>() && !colliders[gameObject.Get<Collider>()].Static)
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
					hash.Add(id);
					
			// Remove colliders parent object to prevent self collision
			hash.Remove(collider.Parent);

            result = new int[hash.Count];
            hash.CopyTo(result);
            return result;
        }
		
		internal static void CalcGridPos(ref Collider collider, Transform transform)
		{
			if (collider.GridKeys == null)
				collider.GridKeys = new List<(int X, int Y, int Z)>();

			// Remove the Colliders parent from all grid cells
			foreach ((int, int, int) keys in collider.GridKeys)
				GridCells[keys].Remove(collider.Parent);
			
			// Top left corner converted to grid coordinates
			(int X, int Y, int Z) p1 = ((int)Math.Floor(transform.X / CellSize), (int)Math.Floor(transform.Y / CellSize), (int)Math.Floor(transform.Z / CellSize));
			
			// Bottom right corner converted to grid coordinates
			(int X, int Y, int Z) p2 = ((int)Math.Floor((transform.X + collider.ScaleX) / CellSize), (int)Math.Floor((transform.Y + collider.ScaleY) / CellSize), (int)Math.Floor((transform.Z + collider.ScaleZ) / CellSize));

			// Place gameObject in all it's occupied cells
			collider.GridKeys.Clear();
			for(int z = p1.Z; z <= p2.Z; z++)
				for (int y = p1.Y; y <= p2.Y; y++)
					for(int x = p1.X; x <= p2.X; x++)
					{
						// Create new cells that don't exist
						if(!GridCells.ContainsKey((y, x, z)))
							GridCells[(y, x, z)] = new List<int>();
						
						GridCells[(y, x, z)].Add(collider.Parent);
						collider.GridKeys.Add((y, x, z));
					}
				
			// Remove empty grid cells
			if(GridCells.Count >= StorageThreshold)
			{
				int DelCount = 0;
				var _temp = GridCells.Keys;
				foreach ((int, int, int) key in _temp)// GridCells.Keys)
				{
					if(GridCells[key].Count == 0)
					{
						DelCount++;
						GridCells.Remove(key);
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