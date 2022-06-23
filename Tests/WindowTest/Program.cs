using System;
using CMDR;
using CMDR.Systems;

namespace WindowTest
{
    class Program
    {
        static Window Window;
        static void Main(string[] args)
        {
            Window = new Window(800, 600, 0, 0, "TestWindow");
            
            Window.Create();

            GameLoop.Start();
        }
    }
}
