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
        public static int Width { get; internal set; }
        public static int Height { get; internal set; }


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
        internal static IEnumerable<SGameObject> GetRenderable(Transform[] transforms)
        {
            // TODO ... LINQ is very slow. Do something better.
            IEnumerable<SGameObject> _ = SceneManager.ActiveScene.GameObjects.Get().AsQueryable().Where(gameObject => gameObject.Contains<RenderData>()
            &&
            CameraRectCheck(transforms[gameObject.Get<Transform>()]));

            return _;

        }
    }
}
