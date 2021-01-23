using System;
using CMDR;
using CMDR.Components;
using CMDR.Systems;
using System.Windows.Input;

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

        private static float _speed = 0.3F;

        [STAThread]
        static void Main(string[] args)
        {
            Debugger.EnableDebugger = true;

            TestScene = new Scene();
            GameObject1 = TestScene.GenerateGameObject();

            Collider = TestScene.Generate<Collider>();
            Transform = TestScene.Generate<Transform>();
            RenderData = TestScene.Generate<RenderData>();
            RenderData.FromFile("Test.png");


            IComponent[] comps = new IComponent[] { Collider, Transform, RenderData };
            GameObject1.Use(comps);
            Display = new Display(800, 600);
            //Transform.Xvel = _speed;
            Transform.Teleport(100, 100);
            Transform.Static = false;

            Input.AddKeyBind(Key.W, () => { Transform.Yvel += -_speed; }, () => { Transform.Yvel -= -_speed; });
            Input.AddKeyBind(Key.A, () => { Transform.Xvel += -_speed; }, () => { Transform.Xvel -= -_speed; });
            Input.AddKeyBind(Key.S, () => { Transform.Yvel += _speed; }, () => { Transform.Yvel -= _speed; });
            Input.AddKeyBind(Key.D, () => { Transform.Xvel += _speed; }, () => { Transform.Xvel -= _speed; });
            Input.AddKeyBind(Key.Q, () => { Transform.Xvel = 2; });
            Input.AddKeyBind(Key.E, () => { Transform.Xvel = 0; });
            Input.AddKeyBind(Key.H, () => { Console.WriteLine("Hello"); });

            Display.Start();
        }
    }
}
