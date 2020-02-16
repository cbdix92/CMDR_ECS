using System;
using System.IO;
using System.Drawing;
using CMDR.Components;

namespace CMDR
{
    internal static class BitCollider
    {
        // BitCollider is responsible for generating collison data from images, 
        // and detecting collisions between two pairs of collision data.
		
		public static byte AlphaThreshold = 60;
		
		internal static bool BitColliderCheck(Transform t1, Transform t2, Collider c1, Collider c2)
		{
			bool result = false;

			// Prevent int casting during iteration
			(int X, int Y) t1p = ((int)t1.X, (int)t1.Y);
			(int X, int Y) t2p = ((int)t2.X, (int)t2.Y);

			// Find the top left and bottom right points for the bounding box of the two colliding objects
			(int X, int Y) p1 = ((int)Math.Min(t1.X, t2.X), (int)Math.Min(t1.Y, t2.Y));
			(int X, int Y) p2 = ((int)Math.Max(t1.X, t2.X), (int)Math.Min(t1.Y, t2.Y));
			
			int boundSize = (p2.X - p1.X) * (p2.Y - p1.Y);
			
			int width = p2.X - p1.X;
			
			for (int i = 0; i < boundSize; i++)
			{
				if (c1.Data[(i / width) - p1.Y - t1p.Y, (i % width) - p1.X - t1p.X]
				&&
					c2.Data[(i / width) - p1.Y - t2p.Y, (i % width) - p1.X - t2p.X])
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
		
        public static void GenerateColData(ref Collider collider, Image image)
        {
            int width = image.Width;
			int height = image.Height;
			collider.Data = new bool[height,width];
			
			Bitmap bitmap = new Bitmap(image);
			
			for (int y = 0; y <= height; y++)
				for (int x = 0; x <= width; x++)
				{
					collider.Data[y,x] = bitmap.GetPixel(x,y).A > AlphaThreshold;
				}
        }
    }
}
