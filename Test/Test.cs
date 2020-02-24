using System;
using CMDR;

namespace Test
{
    class Test
    {
        public static Scene TestScene;

        public static GameObject GameObject1;
        static void Main(string[] args)
        {
            TestScene = new Scene();
            GameObject1 = TestScene.GenerateGameObject();

        }
    }
}
