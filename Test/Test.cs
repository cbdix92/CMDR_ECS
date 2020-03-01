using System;
using CMDR;
using CMDR.Components;

namespace Test
{
    class Test
    {
        public static Scene TestScene;

        public static TestGameObject TestGameObject;
        public static GameObject GameObject1;
        static void Main(string[] args)
        {
            TestScene = new Scene();
            TestGameObject = new TestGameObject(TestScene);
            GameObject1 = TestScene.GenerateGameObject();
        }
    }

    public class TestGameObject
    {
        public Component Trans;
        public GameObject GameObj;

        public TestGameObject(Scene scene)
        {
            Trans = scene.Generate<Transform>();
            GameObj = scene.GenerateGameObject();
            GameObj.Use(Trans);
        }
    }
}
