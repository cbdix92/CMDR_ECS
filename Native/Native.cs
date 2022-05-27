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
	internal static class Native
	{
		internal delegate IntPtr HookProc(int code, IntPtr wParam, IntPtr lParam);

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


		internal static void Start()
        {
			SetWindowsHookEx(HookType.WH_KEYBOARD, Input.KeyboardCallback, IntPtr.Zero, (uint)Thread.CurrentThread.ManagedThreadId);
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