using System;
using CMDR;
using CMDR.Components;

namespace Test
{
    class Test
    {
        public static Display Display;

        public static Scene TestScene;

        public static SGameObject GameObject1;

        public static Collider Collider;
        public static Transform Transform;
        public static RenderData RenderData;

        static void Main(string[] args)
        {
            TestScene = new Scene();
            GameObject1 = TestScene.GenerateGameObject();

            Collider = TestScene.Generate<Collider>();
            Transform = TestScene.Generate<Transform>();
            RenderData = TestScene.Generate<RenderData>();
            RenderData.FromFile("Test.png");

            //Transform.Xvel = 5;
            test(Transform);

            IComponent[] comps = new IComponent[] { Collider, Transform, RenderData };
            GameObject1.Use(comps);
            Display = new Display(800, 600);
            Console.Write(Transform.Xvel);
            Display.Start();
        }
        public static void test(Transform t)
        {
            t.Xvel = 5;
        }
    }
}
