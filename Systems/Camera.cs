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

        private static float _width;
        private static float _height;
        public static float Width
        {
            get => _width;
            set
            {
                _width = value * 1;
                Projection = CreateOrthographic();
            }
        }
        public static float Height
        {
            get => _height;
            set
            {
                _height = value * 1;
                Projection = CreateOrthographic();
            }
        }
		
		public static float Left { get => -(Zoom * Width) / 2;}
		public static float Right { get => (Zoom * Width) / 2; }
		public static float Top { get => -(Zoom * Height) / 2; }
		public static float Bottom { get => (Zoom * Height) / 2; }
        
        public static readonly float Far = 2;
        public static readonly float Near = 1;

        public static Matrix4 Projection;

        private static float _zoom = 1;
        public static float Zoom
        {
            get => _zoom;
            set
            {
                if(value != _zoom & value != 0)
                {
                    _zoom = value;
                    Projection = CreateOrthographic();
                }
            }
        }

		internal static Matrix4 CreateOrthographic()
		{
            Matrix4 result = Matrix4.Identity;

            float w_inv = 1.0f / (Right - Left);
            float h_inv = 1.0f / (Bottom - Top);
            float d_inv = 1.0f / (Far - Near);
            result.M00 = 2.0f * w_inv;
            result.M11 = 2.0f * h_inv;
            result.M22 = d_inv;
            result.M03 = -(Right + Left) * w_inv;
            result.M13 = -(Bottom + Top) * h_inv;
            result.M23 = -Near * d_inv;
            //Console.WriteLine(result.ToString());
            /*
            result.M00 = 2 / (Right - Left);
			result.M11 = 2 / (Top - Bottom);
			result.M22 = -2 / (Far - Near);
			result.M30 = -(Right + Left) / (Right - Left);
			result.M31 = -(Top + Bottom) / (Top - Bottom);
			result.M32 = -(Far + Near) / (Far - Near);
            */
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
        internal static SGameObject[] GetRenderable(SGameObject[] gameObjects, Transform[] transforms)
        {
            // TODO ... LINQ is very slow. Do something better.
            //IEnumerable<SGameObject> _ = SceneManager.ActiveScene.GameObjects.Get().AsQueryable().Where(gameObject => gameObject.Contains<RenderData>()
            //&&
            //CameraRectCheck(transforms[gameObject.Get<Transform>()]));
			
			SGameObject[] result = new SGameObject[gameObjects.Length];
			int count = 0;
			for(int i = 0; i < gameObjects.Length; i++)
			{
				SGameObject g = gameObjects[i];
				if(g.Contains<RenderData>() && CameraRectCheck(transforms[g.Get<Transform>()]))
				{
					result[count++] = gameObjects[i];
				}	
			}
			Array.Resize<SGameObject>(ref result, count);

            return result;

        }
    }
}
