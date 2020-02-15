using System;
using System.Drawing;

namespace CMDR
{
    internal static class BitCollider
    {
        // BitCollider is responsible for generating collison data from images, 
        // and detecting collisions between two pairs of collision data.
		
		public static byte AlphaThreshold = 60;
		
		internal static bool BitColliderCheck(Transform t1, Transform, t2, Collider c1, Collider c2)
		{
			bool result = false;
			
			int p1x = (int)Math.Min(t1.X, t2.X);
			int p1y = (int)Math.Min(t1.Y, t2.Y);
			
			int p2x = (int)Math.Max(t1.X, t2.X);
			int p2y = (int)Math.Max(t1.Y, t2.Y);
			
			int boundSize = (p2x - p1x) * (p2y - p1y);
			
			int width = p2x - p1x;
			
			for (int i = 0; i < boundSize; i++)
			{
				if (c1.Data[(i / width) - p1Y - t1.Y, (i % width) - p1x - t1.X]
				&&
					c2.Data[(i / width) - p1y - t2.Y, (i % width) - p1x - t2.X])
					{
						result = true;
						break;
					}
			}
			return result;
		}
		
        public static void GenerateColData(ref Collider collider, string src)
		{
			try
			{
				GenerateColData(ref collider, Image.FromFile(src));
			}
			catch (FileNotFoundException)
			{
				throw new FileNotFoundException($"'{src}', Collider");
			}
		}
		
        public static bool[,] GenerateColData(ref Collider collider, Image image)
        {
            int width = image.Width;
			int height = image.Height;
			collider.Data = new bool[height,width];
			
			BitMap bitmap = new BitMap(image);
			
			for (int y = 0; y <= height; y++)
				for (int x = 0; x <= width; x++)
				{
					collider.Data[y,x] = bitmap.GetPixel(x,y).A > AlphaThreshold;
				}
        }
    }
}
