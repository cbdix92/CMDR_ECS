using System;
using CMDR.Systems;
using GLFW;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL4;

namespace CMDR
{
    public sealed class Display
    {
        internal static Window Window;
        public Display(int width, int height, string title)
        {
            (Camera.Width, Camera.Height) = (width, height);
            

            if(!Glfw.Init())
            {
                GLFW.Exception.GetErrorMessage(Glfw.GetError(out string error));
                throw new System.Exception(error);
            }

            Window = Glfw.CreateWindow(width, height, title, Monitor.None, Window.None);

            Glfw.MakeContextCurrent(Window);

        }

        public void Start()
        {
            GL.ClearColor(Color4.Black);

            GameLoop.Start();
        }
    }
}
