using System;
using System.Linq;
using System.Collections.Generic;
using CMDR.Components;

namespace CMDR
{
    public static class Camera
    {
        public static float X;
        public static float Y;
        public static float Z;

        public static float Xvel;
        public static float Yvel;
        public static float Zvel;
        public static float Width { get; internal set; }
        public static float Height { get; internal set; }
		
		public static float Left { get => -Width / 2;}
		public static float Right { get => Width / 2; }
		public static float Top { get => -Height / 2; }
		public static float Bottom { get => Height / 2; }
		public static float Far { get; set; }
		public static float Near { get; set; }


		internal static Matrix4 CreateOrthographic(Matrix4 result)
		{

            result.M00 = 2 / (Right - Left);
			result.M11 = 2 / (Top - Bottom);
			result.M22 = 2 / (Far - Near);
			result.M30 = -(Right + Left) / (Right - Left);
			result.M31 = -(Top + Bottom) / (Top - Bottom);
			result.M32 = -(Far + Near) / (Far - Near);
            return result;
		}
		
        /// <summary>
        /// Check if a Transform is within view of the camera by performing bounding box collision test.
        /// </summary>
        /// <param name="transform"></param>
        /// <returns></returns>
        public static bool CameraRectCheck(Transform transform)
        {
            return transform.X - X < Width && transform.Y - Y < Height;
        }

        /// <summary>
        /// Returns renderable SGameObjects in view of the camera.
        /// </summary>
        /// <param name="transforms"> Array of transforms that are checked.</param>
        /// <returns></returns>
        internal static IEnumerable<SGameObject> GetRenderable(SGameObject[] gameObjects, Transform[] transforms)
        {
            // TODO ... LINQ is very slow. Do something better.
            //IEnumerable<SGameObject> _ = SceneManager.ActiveScene.GameObjects.Get().AsQueryable().Where(gameObject => gameObject.Contains<RenderData>()
            //&&
            //CameraRectCheck(transforms[gameObject.Get<Transform>()]));
			
			SGameObject[] result = new SGameObjects[gameObjects.Length];
			int count;
			for(int i = 0; i < gameObjects.Length; i++)
			{
				SGameObject g = gameObjects[i];
				if(g.Contains<RenderData>() && CameraRectCheck(transforms[g.Get<Transform>()]))
				{
					result[count++] = gameObjects[i];
				}	
			}
			Array.Resize<SGameObject>(result, count);

            return result;

        }
    }
}
