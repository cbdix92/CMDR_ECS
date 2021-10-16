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

			Glfw.WindowHint(Hint.ClientApi, ClientApi.OpenGL);
			Glfw.WindowHint(Hint.OpenglProfile, Profile.Core);
            Glfw.WindowHint(Hint.Doublebuffer, 1);
			

            Window = Glfw.CreateWindow(width, height, title, Glfw.GetPrimaryMonitor(), null);

            if(Window == null)
                throw new Exception("Window returned Null")

            Glfw.MakeContextCurrent(Window);

        }

        public void Start()
        {
            GL.ClearColor(Color4.Black);

            GameLoop.Start();
        }
    }
}
