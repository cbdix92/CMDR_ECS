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

        public static bool CameraRectCheck(Transform transform)
        {
            return transform.X - X < Width && transform.Y - Y < Height;
        }

        // Returns renderable objects in view of the camera
        internal static IEnumerable<SGameObject> GetRenderable(Transform[] transforms)
        {
            IEnumerable<SGameObject> _ = SceneManager.ActiveScene.GameObjects.Get().AsQueryable().Where(gameObject => gameObject.Contains<RenderData>()
            &&
            CameraRectCheck(transforms[gameObject.Get<Transform>()]));

            return _;

        }
    }
}
