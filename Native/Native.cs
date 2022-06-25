using System;
using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.InteropServices;
using OpenGL;


namespace CMDR.Native
{
	
	internal static partial class Win
	{
		internal static Window CurrentWindow = null;

		internal static WNDCLASSEXW wndClass = default;

		internal static WNDPROC WndProc = new WNDPROC(WindowProcedure);

		private static MSG _message;

		internal static bool CreateWindow(Window window)
		{
			if(CurrentWindow == null)
			{
				CurrentWindow = window;
			}
			else
			{
				CheckError("Window already existed. Cannot create more than one window.", true);
			}


			//wndClass = new WNDCLASSEXW();
			wndClass.cbSize = (uint)Marshal.SizeOf(typeof(WNDCLASSEXW));
			wndClass.style = window.ClassStyle | (uint)CS.OWNDC;
			wndClass.lpfnWndProc = new WNDPROC(WindowProcedure);
            wndClass.cbClsExtra = 0;
            wndClass.cbWndExtra = 0;
            wndClass.hInstance = Process.GetCurrentProcess().Handle;
			wndClass.hIcon = LoadIconW(IntPtr.Zero, (ushort)IDI.APPLICATION);
			wndClass.hCursor = LoadCursorW(IntPtr.Zero, (ushort)IDC.ARROW);
			wndClass.hbrBackground = IntPtr.Zero; // TODO ... Make Black
			wndClass.lpszMenuName = "MENU_NAME";
			wndClass.lpszClassName = "CMDRWCLASS";
			wndClass.hIconSm = IntPtr.Zero;

			if (RegisterClassExW(ref wndClass) == 0)
			{
				CheckError("RegisterWindow", true);
			}

			window.HWND = CreateWindowExW(
				window.WindowStyleEX,
				"CMDRWCLASS",
				window.Title,
				window.WindowStyle,
				window.StartingPosX,
				window.StartingPosY,
				window.Width,
				window.Height,
				IntPtr.Zero,
				IntPtr.Zero,
				wndClass.hInstance,
				IntPtr.Zero
				);
			
			if (window.HWND == IntPtr.Zero)
				CheckError("CreateWindow returned null", true);
			
			return true;
		}

		internal static void PrepareContext(Window window)
        {
			PIXELFORMATDESCRIPTOR pfd = new PIXELFORMATDESCRIPTOR()
			{
				nSize = (ushort)Marshal.SizeOf(typeof(PIXELFORMATDESCRIPTOR)),
				nVersion = 1,
				swFlags = (uint)(PFD.DRAW_TO_WINDOW | PFD.SUPPORT_OPENGL | PFD.DOUBLEBUFFER),
				iPixelType = (byte)PFD.TYPE_RGBA,
				cColorBits = 32,
				cRedBits = 0,
				cRedShift = 0,
				cGreenBits = 0,
				cGreenShift = 0,
				cBluebits = 0,
				cBlueShift = 0,
				cAlphaBits = 0,
				cAlphaShift = 0,
				cAccumBits = 0,
				cAccumRedBits = 0,
				cAccumGreenBits = 0,
				cAccumBlueBits = 0,
				cAccumAlphaBits = 0,
				cDepthBits = 24,
				cStencilBits = 8,
				cAuxBuffers = 0,
				iLayerType = 0,
				bReserved = 0,
				dwLayerMask = 0,
				dwVisibleMask = 0,
				dwDamageMask = 0
			};

			window.DC = GetDC(window.HWND);

			if (window.DC == IntPtr.Zero)
			{
				CheckError("Device Context is null", true);
			}

			window.PixelFormatNumber = ChoosePixelFormat(window.DC, pfd);

			if (window.PixelFormatNumber == 0)
				CheckError("PixelFormaNumber is zero", true);

			if (SetPixelFormat(window.DC, window.PixelFormatNumber, pfd) == false)
				CheckError("Set Pixel Format", true);

			window.HGLRC = wglCreateContext(window.DC);

			wglMakeCurrent(window.DC, window.HGLRC);

			GL.Build();
		}

		internal static IntPtr WindowProcedure(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam)
		{
			switch((WM)uMsg)
            {
				case WM.CREATE:
					PrepareContext(CurrentWindow);
					ShowWindow(CurrentWindow.HWND, (int)SW.SHOW);
					return IntPtr.Zero;
				case WM.QUIT:
					return IntPtr.Zero;
				case WM.CLOSE:
					CurrentWindow.OnClose();
					return IntPtr.Zero;
				default:
					return DefWindowProcW(hWnd, uMsg, wParam, lParam);
            }
		}

		internal static void DestroyWindow(Window window)
		{
			PostQuitMessage(0);
			if (DestroyWindow(window.HWND))
            {
				//throw new Win32Exception(Marshal.GetLastWin32Error());
				CheckError("DestroyWindow", true);

            }
		}

		/// <summary>
		/// Handle Windows message queue.
		/// </summary>
		/// <returns> Returns true if the GameLoop should continue, otherwise returns false. </returns>
		internal static bool HandleMessages()
        {
			bool continueGameLoop = true;

			while(PeekMessageW(ref _message, CurrentWindow.HWND, 0,0, PM.REMOVE))
            {
				TranslateMessage(ref _message);
				
				DispatchMessage(ref _message);
				
				// If WM.CLOSE message is received, the return value will stop the GameLoop.
				if (_message.message == WM.CLOSE)
					continueGameLoop = false;
            }

			return continueGameLoop;
        }

		/// <summary>
		/// Check for Windows errors. If an error is detected it will be logged.
		/// </summary>
		/// <param name="name"> Name provided for the log. Typically something to idicate the body of code CheckError was called from.
		/// This makes finding the point where the error was generated easier to find. </param>
		/// <param name="exit"> Specify true for hard throw if error is detected, otherwise error will simply be logged. </param>
		/// <returns> Returns Win32 error code. </returns>
		internal static int CheckError(string name, bool exit)
        {
			int error = Marshal.GetLastWin32Error();
			
			if (error != 0)
            {
				Log.LogWin32Error(error, name);
				
				if (exit)
                {
					if(CurrentWindow != null)
                    {
						if(DestroyWindow(CurrentWindow.HWND) == false)
                        {
							Log.LogWin32Error(Marshal.GetLastWin32Error(), "Window Destroy Failed!");
                        }
                    }
					throw new Win32Exception(error);
                }
            }
			
			SetLastError(0);
			
			return error;
        }

        #region OLDBUILDER_CODE
		//internal static void Start()
  //      {
		//	KeyboardHook = new HookProc(Input.KeyboardCallback);
		//	MouseHook = new HookProc(Input.MouseCallback);

		//	uint thread = (uint)Process.GetCurrentProcess().Threads[0].Id;
		//	SetWindowsHookExW(WH.KEYBOARD, KeyboardHook, IntPtr.Zero, thread);
		//	SetWindowsHookExW(WH.MOUSE, MouseHook, IntPtr.Zero, thread);
  //      }
		
        
		//internal unsafe static bool Start()
		//{
		//	Assembly assembly = Assembly.GetExecutingAssembly();
		//	MethodInfo[] methods = assembly.GetTypes().SelectMany(
		//		x => x.GetMethods(BindingFlags.Static | BindingFlags.NonPublic)).Where(
		//		y => y.GetCustomAttributes(typeof(BuildInfo), false).Length > 0).ToArray();

		//	LoadLibs();
			
		//	foreach(MethodInfo method in methods)
		//	{
				
		//		RuntimeHelpers.PrepareMethod(method.MethodHandle);

		//		BuildInfo buildInfo = method.GetCustomAttribute<BuildInfo>();

		//		UInt64* location = (UInt64*)(method.MethodHandle.Value.ToPointer());
		//		int index = (int)(((*location) >> 32) & 0xff);
				
		//		// 64 bit process
		//		if(IntPtr.Size == 8)
		//		{
		//			ulong* source = (ulong*)GetProcFromBuildInfo(buildInfo) + 1;

		//			ulong* classstart = (ulong*)method.DeclaringType.TypeHandle.Value.ToPointer();
		//			ulong* target = (ulong*)classstart + index + 10;
		//			//ulong* target = (ulong*)method.MethodHandle.GetFunctionPointer() + 1;
		//			*target = *source;
		//		}
		//		// 32 bit process
		//		else
		//		{
		//			uint* source = (uint*)GetProcFromBuildInfo(buildInfo) + 2;
		//			uint* classstart = (uint*)method.DeclaringType.TypeHandle.Value.ToPointer();
		//			uint* target = (uint*)classstart + index + 10;
		//			//int* target = (int*)method.MethodHandle.GetFunctionPointer() + 2;
		//			*target = *source;
		//		}
				
		//	}
		//	return true;
		//}
		
  //      internal static void LoadLibs()
		//{
		//	SetLastError(0);
		//	Libs.Add(Kernel32, LoadLibrary(Kernel32));
		//	CheckError(Kernel32);
		//	Libs.Add(User32, LoadLibrary(User32));
		//	CheckError(User32);
		//}
		//internal static bool FreeLibs()
		//{
		//	bool result = false;
		//	foreach(IntPtr hModule in Libs.Values)
		//		result |= FreeLibrary(hModule);
		//	return result;
		//}
		
		//internal static IntPtr GetProcFromBuildInfo(BuildInfo info)
		//{
		//	IntPtr result = Glfw.GetProcAddress(info.Name);
		//	int error = Marshal.GetLastWin32Error();

		//	if (error == 0)
  //          {
		//		var pointer = (Int64)result;
		//		if(pointer != 0 || pointer != 1 || pointer != 2 || pointer != 3 || pointer != -1)
		//			return result;

  //          }

		//	Log.LogWin32Error(error, info.Name, true);

		//	//result = GetProcAddress(Libs[Opengl32], info.Name);
		//	error = Marshal.GetLastWin32Error();
		//	if (error == 0)
		//		return result;

		//	Log.LogWin32Error(error, info.Name);

		//	throw new TypeLoadException("Builder Failure! See Log!");
		//}
        #endregion
	}
	
	
	
}