using System;
using System.IO;
using System.Drawing;
using CMDR.Components;
using CMDR.Systems;

namespace CMDR
{
    internal static class BitCollider
    {
        // BitCollider is responsible for generating collison data from images, 
        // and detecting collisions between two pairs of collision data.
		
		public static byte AlphaThreshold = 60;
		
		internal static bool BitColliderCheck(Transform t1, Transform t2, Collider c1, Collider c2)
		{
			Console.WriteLine("Enter But Collider: "+ GameLoop.Time.ElapsedMilliseconds.ToString());
			bool result = false;

			// Prevent int casting during iteration
			(int X, int Y) t1p = ((int)t1.X, (int)t1.Y);
			(int X, int Y) t2p = ((int)t2.X, (int)t2.Y);

			// Find the top left and bottom right points for the insecting area between the two objects.
			(int X, int Y) p1 = ((int)Math.Max(t1.X, t2.X), (int)Math.Max(t1.Y, t2.Y));
			(int X, int Y) p2 = ((int)Math.Min(t1.X + c1.Width, t2.X + c2.Width), (int)Math.Min(t1.Y + c1.Height, t2.Y + c2.Height));
			
			int boundSize = (p2.X - p1.X) * (p2.Y - p1.Y) - 1;
			
			int width = p2.X - p1.X;

			int x1;
			int x2;
			int y1;
			int y2;
			bool chk1;
			bool chk2;
			
			for (int i = 0; i < boundSize; i++)
			{


				x1 = (i % width) + (p1.X - t1p.X);
				x2 = (i % width) + (p1.X - t2p.X);
				y1 = (i / width) + (p1.Y - t1p.Y);
				y2 = (i / width) + (p1.Y - t2p.Y);
				chk1 = c1.ColData[y1, x1];
				chk2 = c2.ColData[y2, x2];
				if (chk1 && chk2)
					{
						result = true;
						break;
					}
			}
			Console.WriteLine("Exit Bit Collider: " + GameLoop.Time.ElapsedMilliseconds.ToString() + " " + result.ToString());
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
			collider.ColData = new bool[height,width];
			
			Bitmap bitmap = new Bitmap(image);
			
			for (int y = 0; y < height; y++)
				for (int x = 0; x < width; x++)
				{
					collider.ColData[y,x] = bitmap.GetPixel(x,y).A > AlphaThreshold;
				}
        }
    }
}
