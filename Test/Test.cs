﻿using System;
using CMDR;
using CMDR.Components;

namespace Test
{
    class Test
    {
        public static Display Display;

        public static Scene TestScene;

        public static GameObject GameObject1;

        public static Component Collider;
        public static Component Transform;
        public static Component RenderData;

        static void Main(string[] args)
        {
            TestScene = new Scene();
            GameObject1 = TestScene.GenerateGameObject();

            Collider = TestScene.Generate<Collider>();
            Transform = TestScene.Generate<Transform>();
            RenderData = TestScene.Generate<RenderData>();
            RenderData.Get<RenderData>().FromFile("Test.png");

            Component[] comps = new Component[] { Collider, Transform, RenderData };
            GameObject1.Use(comps);
            Display = new Display(800, 600);
            Display.Start();
        }
    }
}
