using System;
using CMDR.Systems;
using GLFW;
using OpenGL;

namespace CMDR
{
    public sealed class Display
    {
        public static Window Window;

        private static Vector2 _size;
        private static int _fov;
		private static Matrix4 _projection;
		
		public static bool ChangeState { get; private set; }

        #region SIZE
        public static int Width
        {
            get => (int)_size.X;
            set
            {
                _size.X = value * 1;
                ChangeState = true;
            }
        }
        public static int Height
        {
            get => (int)_size.Y;
            set
            {
                _size.Y = value * 1;
                ChangeState = true;
            }
        }

        #endregion

        public static int FOV
        {
            get => _fov;
            set
            {
                _fov = value;
				ChangeState = true;
            }
        }

        //public static float Left { get => 0; }
        //public static float Right { get => Width; }
        //public static float Top { get => 0; }
        //public static float Bottom { get => Height; }

        public static float Left { get => -Width / 2; }
        public static float Right { get => Width / 2; }
        public static float Top { get => -Height / 2; }
        public static float Bottom { get => Height / 2; }

        public static readonly float Near = 1f;
        public static readonly float Far = 1000f;

        public static Matrix4 Projection
		{
			get
			{
				if (ChangeState)
					_projection = CreatePerspectiveFOV();
				return _projection;
			}
		}

        public Display(int width, int height, string title, bool doubleBuffer = true, bool decorated = true)
        {
            ((Width, Camera.Width), (Height, Camera.Height)) = ((width, width), (height, height));

            Glfw.WindowHint(Hint.ClientApi, ClientApi.OpenGL);
            Glfw.WindowHint(Hint.ContextVersionMajor, 3);
            Glfw.WindowHint(Hint.ContextVersionMinor, 3);
            Glfw.WindowHint(Hint.OpenglProfile, Profile.Core);
            Glfw.WindowHint(Hint.Doublebuffer, doubleBuffer);
            Glfw.WindowHint(Hint.Decorated, decorated);
            Glfw.WindowHint(Hint.OpenglForwardCompatible, true);

            Window = Glfw.CreateWindow(width, height, title, Monitor.None, Window.None);

            if (Window == null)
                throw new NullReferenceException("Window is Null");

            Glfw.MakeContextCurrent(Window);

            CenterWindow();
			Glfw.SetWindowSizeCallback(Window, WindowSizeCallback);

            Log.Init();
            GL.Init();
            Render.Init();
            ShaderManager.Init();

        }
		
		internal static void WindowSizeCallback(Window window, int width, int size)
		{
			((Width, Camera.Width), (Height, Camera.Height)) = ((width, width), (height, height));
			Glfw.SetWindowSize(window, width, height);
		}

        internal static void CreateOrthographic()
        {
            Projection = Matrix4.CreateOrthographic(Top, Bottom, Left, Right, Far, Near);
			ChangeState = false;
        }

        public static void CreatePerspective()
        {
            Projection = Matrix4.CreatePerspective(Top, Bottom, Left, Right, Far, Near);
			ChangeState = false;
        }

        public static void CreatePerspectiveFOV()
        {
            if (Height == 0)
                Projection = default(Matrix4);
            Projection = Matrix4.CreatePerspectiveFOV(90, Width / Height, Far, Near);
			ChangeState = false;
        }

        public static void CenterWindow()
        {
            var screen = Glfw.PrimaryMonitor.WorkArea;
            int x = (screen.Width - Width) / 2;
            int y = (screen.Height - Height) / 2;
            Glfw.SetWindowPosition(Window, x, y);
        }

        public void Start()
        {
            GameLoop.Start();
        }
    }
}
