﻿using System;
using System.Linq;
using CMDR.Components;

namespace CMDR
{
    public static class Camera
    {
        public static float X;
        public static float Y;

        public static float Xvel;
        public static float Yvel;
        public static int SizeX { get; internal set; }
        public static int SizeY { get; internal set; }

        public static bool CameraRectCheck(Transform transform)
        {
            bool B1 = transform.X - X < SizeX;
            bool B2 = transform.Y - Y < SizeY;
            return B1 && B2;
        }

        // Returns renderable objects in view of the camera
        internal static SGameObject[] GetRenderable()
        {
            Transform[] transforms = SceneManager.ActiveScene.Components.Get<Transform>();
            return SceneManager.ActiveScene.GameObjects.Get().GroupBy(gameObject => gameObject.Contains<RenderData>() && CameraRectCheck(transforms[gameObject.Get<Transform>()])).Select(x => x.FirstOrDefault()).ToArray();

        }
    }
}
