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
            Window = new Window(500, 500, 0, 0, "TestWindow");
            
            Window.Create();

            GameLoop.Start();
        }
    }
}
