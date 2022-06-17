using System;
using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.InteropServices;


namespace CMDR.Native
{
	
	internal static partial class Win
	{
		internal static Window CurrentWindow = null;

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
				DestroyWindow(CurrentWindow);
				throw new Win32Exception("Window already existed. Cannot create more than one window.");
			}


			WNDCLASSEXW wndClass = new WNDCLASSEXW();
			wndClass.cbSize = (uint)Marshal.SizeOf(typeof(WNDCLASSEXW));
			wndClass.style = window.ClassStyle | (uint)(CS.OWNDC | CS.VREDRAW | CS.HREDRAW);
			wndClass.lpfnWndProc = new WNDPROC(WindowProcedure);
            wndClass.cbClsExtra = 0;
            wndClass.cbWndExtra = 0;
            wndClass.hInstance = Process.GetCurrentProcess().Handle;
            //wndClass.hIcon = 
            //wndClass.hCursor = 
            //wndClass.hbrBackground = 
            //wndClass.lpszMenuName = 
            wndClass.lpszClassName = "CMDR_WINDOW_CLASS";
			//wndClass.hIconSm =   

			if(RegisterClassExW(ref wndClass) == 0)
			{
				CheckError("RegisterWindow", true);
			}

			window.HWND = CreateWindowExW(WS_EX.OVERLAPPEDWINDOW, "CMDR_WINDOW_CLASS", window.Title, (WS)window.ClassStyle, window.StartingPosX, window.StartingPosY, window.Width, window.Height, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero, IntPtr.Zero);
			
			if (window.HWND == IntPtr.Zero)
				CheckError("CreateWindow", true);
			
			return true;
		}

		internal static IntPtr WindowProcedure(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam)
		{
			switch((WM)uMsg)
            {
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
				CheckError("DestroyWindow", true);
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
					throw new Win32Exception(error);
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