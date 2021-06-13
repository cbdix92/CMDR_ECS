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
        public static SGameObject GameObject2;

        public static Collider Collider;
        public static Transform Transform;
        public static RenderData RenderData;

        public static Collider Collider2;
        public static Transform Transform2;
        public static RenderData RenderData2;

        private static float _speed = 1.0F;

        public static string[] paths = new string[]
        {
            "assets/character/ninja_male_assets/Run__000.png",
            "assets/character/ninja_male_assets/Run__001.png",
            "assets/character/ninja_male_assets/Run__002.png",
            "assets/character/ninja_male_assets/Run__003.png",
            "assets/character/ninja_male_assets/Run__004.png",
            "assets/character/ninja_male_assets/Run__005.png",
            "assets/character/ninja_male_assets/Run__006.png",
            "assets/character/ninja_male_assets/Run__007.png",
            "assets/character/ninja_male_assets/Run__008.png",
            "assets/character/ninja_male_assets/Run__009.png"
        };

        [STAThread]
        static void Main(string[] args)
        {
            Debugger.EnableDebugger = true;

            TestScene = new Scene();
            GameObject2 = TestScene.GenerateGameObject();
            GameObject1 = TestScene.GenerateGameObject();

            Collider = TestScene.Generate<Collider>();
            Transform = TestScene.Generate<Transform>();
            RenderData = TestScene.Generate<RenderData>();
            RenderData.CreateAnimation("Test", paths, 60);
            RenderData.FromFile("assets/character/ninja_male_assets/Run__000.png");
            Collider.SetBounds(RenderData);
            Collider.GenerateColData("assets/character/ninja_male_assets/Run__000.png");

            Collider2 = TestScene.Generate<Collider>();
            Transform2 = TestScene.Generate<Transform>();
            RenderData2 = TestScene.Generate<RenderData>();
            RenderData2.FromFile("Test.png");
            RenderData2.Static = true;
            Collider2.SetBounds(RenderData2);
            Collider2.GenerateColData("Test.png");


            IComponent[] comps = new IComponent[] { Collider, Transform, RenderData };
            GameObject1.Use(comps);
            Transform.Teleport(100, 100);
            Transform.Static = false;

            IComponent[] comps2 = new IComponent[] { Collider2, Transform2, RenderData2 };
            GameObject2.Use(comps2);
            Transform2.Teleport(180, 256);
            Transform2.Static = false;

            Display = new Display(1000, 1000);

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
