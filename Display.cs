using System;
using System.Drawing;
using System.Reflection;
using System.Threading.Tasks;
using CMDR.Systems;
using GLFW;

namespace CMDR
{
    public sealed class Display
    {
        internal static Window Window;
        public Display(int width, int height, string title)
        {
            (Camera.Width, Camera.Height) = (width, height);
            
            try
            {
                Glfw.Init();
            }
            catch
            {
                GLFW.Exception.GetErrorMessage(Glfw.GetError(out string error));
                throw new System.Exception(error);
            }

            Window = Glfw.CreateWindow(width, height, title, Monitor.None, Window.None);
            Glfw.MakeContextCurrent(Window);

        }

        public void Start()
        {
            GameLoop.Start();
        }
    }
}
