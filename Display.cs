using System;
using CMDR.Systems;
using GLFW;
using OpenGL;

namespace CMDR
{
    public sealed class Display
    {
        internal static Window Window;

        private static float _width;
        private static float _height;
        public static float Width
        {
            get => _width;
            set
            {
                _width = value * 1;
                Projection = CreateOrthographic();
            }
        }
        public static float Height
        {
            get => _height;
            set
            {
                _height = value * 1;
                Projection = CreateOrthographic();
            }
        }

        public static float Left { get => -(Zoom * (Width / 2)); }
        public static float Right { get => (Zoom * (Width / 2)); }
        public static float Top { get => -(Zoom * (Height / 2)); }
        public static float Bottom { get => (Zoom * (Height / 2)); }

        public static readonly float Far = 1f;
        public static readonly float Near = -1;

        public static Matrix4 Projection;

        private static float _zoom = 1f;
        public static float Zoom
        {
            get => _zoom;
            set
            {
                if (value != _zoom & value != 0)
                {
                    _zoom = value;
                    Projection = CreateOrthographic();
                }
            }
        }

        public Display(int width, int height, string title)
        {
            (Width, Height) = (width, height);
            (Camera.Width, Camera.Height) = (width, height);

            Glfw.WindowHint(Hint.ClientApi, ClientApi.OpenGL);
            Glfw.WindowHint(Hint.ContextVersionMajor, 3);
            Glfw.WindowHint(Hint.ContextVersionMinor, 3);
            Glfw.WindowHint(Hint.OpenglProfile, Profile.Core);
            Glfw.WindowHint(Hint.Doublebuffer, true);
            Glfw.WindowHint(Hint.Decorated, true);
            Glfw.WindowHint(Hint.OpenglForwardCompatible, true);

            Window = Glfw.CreateWindow(width, height, title, Monitor.None, Window.None);

            if (Window == null)
                throw new NullReferenceException("Window is Null");

            Glfw.MakeContextCurrent(Window);

            // Center the game window on the monitor
            var screen = Glfw.PrimaryMonitor.WorkArea;
            var x = (screen.Width - width) / 2;
            var y = (screen.Height - height) / 2;
            Glfw.SetWindowPosition(Window, x, y);


            Log.Init();
            GL.Init();
            Render.Init();
            ShaderManager.Init();

        }

        internal static Matrix4 CreateOrthographic()
        {
            return Matrix4.CreateOrthographic(Top, Bottom, Left, Right, Far, Near);
        }

        public static Matrix4 CreatePerspective()
        {
            return Matrix4.CreatePerspective(Top, Bottom, Left, Right, Far, Near);
        }

        public void Start()
        {
            GameLoop.Start();
        }
    }
}
