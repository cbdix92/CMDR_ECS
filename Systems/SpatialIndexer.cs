using System;
using System.Collections.Generic;
using System.Linq;
using CMDR.Components;

namespace CMDR.Systems
{
    internal struct Cell
    {
        public (int X, int Y) GridKey;
        public List<int> Cache;


        public Cell((int X, int Y)gridKey)
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
    internal static class SpatialIndexer
    {
        public static Dictionary<(int, int), Cell> GridCells = new Dictionary<(int, int), Cell>();

        private static int _cellSize = 30;
        public static int CellSize
        {
            get => _cellSize;
            set
            {
                if (value != _cellSize)
                {
                    _cellSize = value;
                    // CalcGridPos for new cellsize here all colliders here
                    // ............................................
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
    }
}
