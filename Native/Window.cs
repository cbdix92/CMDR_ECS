using System;

namespace CMDR.Native
{
    public sealed class Window
    {
		private static Window _instance = null;
		private static readonly object _lock = new object();

        private WNDCLASSEXW WNDCLASS;
		
        private Window()
		{
		}
		
		public static Window GetWindow
		{
			get
			{
				lock(_lock)
				{
					if(_instance == null)
						_instance = new Window();
					return _instance;
				}
			}
		}
		
		public static Window CreateWindow()
		{
            Window window = GetWindow;

            window.WNDCLASS = WNDCLASSEXW.Create();
			window.WNDCLASS.style = (uint)CS.OWNDC;
            window.WNDCLASS.lpfnWndProc = IntPtr.Zero;
            window.WNDCLASS.cbClsExtra = 0;
            window.WNDCLASS.cbWndExtra = 0;
            window.WNDCLASS.hInstance = IntPtr.Zero;
            window.WNDCLASS.hIcon = IntPtr.Zero;
            window.WNDCLASS.hCursor = IntPtr.Zero;
            window.WNDCLASS.hbrBackground = IntPtr.Zero;
            window.WNDCLASS.lpszMenuName = String.Empty;
            window.WNDCLASS.lpszClassName = String.Empty;
            window.WNDCLASS.hIconSm = IntPtr.Zero;


            return window;
		}
		
		
		
		
    }
}
