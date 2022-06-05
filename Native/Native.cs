using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using GLFW;
using CMDR.Systems;
using System.Threading;


namespace CMDR.Native
{
	[StructLayout(LayoutKind.Sequential)]
	internal struct Point
	{
		public int X;
		public int Y;
	}
	
	[StructLayout(LayoutKind.Sequential)]
	internal unsafe struct MouseHookStruct
	{
		public Point Pos;
		public int* HWND;
		public uint HitTestCode;
		public int* ExtraInfo;
	}
	
	[StructLayout(LayoutKind.Sequential)]
	internal unsafe struct WNDCLASS
	{
		uint cbSize;
		uint style;
		int* lpfnWndProc;
		int cbClsExtra;
		int cbWndExtra;
		int* hInstance;
		int* hIcon;
		int* hCursor;
		int* hbrBackground;
		[MarshalAs(UnmanagedType.LPTStr)]
		string lpszMenuName;
		[MarshalAs(UnmanagedType.LPTStr)]
		string lpszClassName;
		int* hIconSm;

		public static unsafe WNDCLASS Create(uint style, int* lpfnWndProc, int cbClsExtra, int cbWndExtra, int* hInstance, int* hIcon, int* hCursor, int* hbrBackground, string lpszMenuName, string lpszClassName, int* hIconSm)
		{
			var _ = new WNDCLASS()
			{
				style = style,
				lpfnWndProc = lpfnWndProc,
				cbClsExtra = cbClsExtra,
				cbWndExtra = cbWndExtra,
				hInstance = hInstance,
				hIcon = hIcon,
				hCursor = hCursor,
				hbrBackground = hbrBackground,
				lpszMenuName = lpszMenuName,
				lpszClassName = lpszClassName,
				hIconSm = hIconSm,
            };
			_.cbSize = (uint)Marshal.SizeOf<WNDCLASS>(_);
			return _;
		}
	}
	
	internal static class Win
	{
		internal delegate IntPtr HookProc(int code, IntPtr wParam, IntPtr lParam);

		internal static HookProc KeyboardHook = null;
		internal static HookProc MouseHook = null;

		private const string User32 = "user32.dll";
        private const string Kernel32 = "kernel32.dll";
		
		internal static Dictionary<string, IntPtr> Libs = new Dictionary<string, IntPtr>();

        [DllImport(Kernel32, CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        internal static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

		[DllImport(Kernel32, SetLastError = true)]
        internal static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPStr)]string lpFileName);
		
		[DllImport(Kernel32, SetLastError = true)]
		internal static extern bool FreeLibrary(IntPtr hModule);

		[DllImport(User32, SetLastError = true)]
		internal static extern unsafe IntPtr SetWindowsHookEx(HookType hookType, HookProc lpfn, IntPtr hmod, uint dwThreadID);

		[DllImport(User32, SetLastError = true)]
		internal static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);
		
		[DllImport(User32, SetLastError = true)]
		internal static unsafe extern ushort RegisterClassExA([MarshalAs(UnmanagedType.LPStruct)] [In] ref WNDCLASS wnd);



		internal static void Start()
        {
			KeyboardHook = new HookProc(Input.KeyboardCallback);
			MouseHook = new HookProc(Input.MouseCallback);
			SetWindowsHookEx(HookType.WH_KEYBOARD, KeyboardHook, IntPtr.Zero, (uint)AppDomain.GetCurrentThreadId());
			SetWindowsHookEx(HookType.WH_MOUSE, MouseHook, IntPtr.Zero, (uint)AppDomain.GetCurrentThreadId());
        }

        #region OLDBUILDER_CODE
        /*
		internal unsafe static bool Start()
		{
			Assembly assembly = Assembly.GetExecutingAssembly();
			MethodInfo[] methods = assembly.GetTypes().SelectMany(
				x => x.GetMethods(BindingFlags.Static | BindingFlags.NonPublic)).Where(
				y => y.GetCustomAttributes(typeof(BuildInfo), false).Length > 0).ToArray();

			LoadLibs();
			
			foreach(MethodInfo method in methods)
			{
				
				RuntimeHelpers.PrepareMethod(method.MethodHandle);

				BuildInfo buildInfo = method.GetCustomAttribute<BuildInfo>();

				UInt64* location = (UInt64*)(method.MethodHandle.Value.ToPointer());
				int index = (int)(((*location) >> 32) & 0xff);
				
				// 64 bit process
				if(IntPtr.Size == 8)
				{
					ulong* source = (ulong*)GetProcFromBuildInfo(buildInfo) + 1;

					ulong* classstart = (ulong*)method.DeclaringType.TypeHandle.Value.ToPointer();
					ulong* target = (ulong*)classstart + index + 10;
					//ulong* target = (ulong*)method.MethodHandle.GetFunctionPointer() + 1;
					*target = *source;
				}
				// 32 bit process
				else
				{
					uint* source = (uint*)GetProcFromBuildInfo(buildInfo) + 2;
					uint* classstart = (uint*)method.DeclaringType.TypeHandle.Value.ToPointer();
					uint* target = (uint*)classstart + index + 10;
					//int* target = (int*)method.MethodHandle.GetFunctionPointer() + 2;
					*target = *source;
				}
				
			}
			return true;
		}
		*/
        #endregion

        internal static void LoadLibs()
		{
			Log.SetLastError(0);
			Libs.Add(Kernel32, LoadLibrary(Kernel32));
			CheckError(Kernel32);
			Libs.Add(User32, LoadLibrary(User32));
			CheckError(User32);
		}
		
		private static void CheckError(string name)
        {
			int error = Marshal.GetLastWin32Error();
			if (error != 0)
				Log.LogWin32Error(error, name);
			Log.SetLastError(0);
        }
		
		internal static bool FreeLibs()
		{
			bool result = false;
			foreach(IntPtr hModule in Libs.Values)
				result |= FreeLibrary(hModule);
			return result;
		}
		
		internal static IntPtr GetProcFromBuildInfo(BuildInfo info)
		{
			IntPtr result = Glfw.GetProcAddress(info.Name);
			int error = Marshal.GetLastWin32Error();

			if (error == 0)
            {
				var pointer = (Int64)result;
				if(pointer != 0 || pointer != 1 || pointer != 2 || pointer != 3 || pointer != -1)
					return result;

            }

			Log.LogWin32Error(error, info.Name, true);

			//result = GetProcAddress(Libs[Opengl32], info.Name);
			error = Marshal.GetLastWin32Error();
			if (error == 0)
				return result;

			Log.LogWin32Error(error, info.Name);

			throw new TypeLoadException("Builder Failure! See Log!");



		}
	}
	
	
	
}