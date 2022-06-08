using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Diagnostics;
using GLFW;
using CMDR.Systems;


namespace CMDR.Native
{
	
	internal static partial class Win
	{

		internal static HookProc KeyboardHook = null;
		internal static HookProc MouseHook = null;
		
		internal static Dictionary<string, IntPtr> Libs = new Dictionary<string, IntPtr>();


		internal static void Start()
        {
			KeyboardHook = new HookProc(Input.KeyboardCallback);
			MouseHook = new HookProc(Input.MouseCallback);

			uint thread = (uint)Process.GetCurrentProcess().Threads[0].Id;
			SetWindowsHookExW(WH.KEYBOARD, KeyboardHook, IntPtr.Zero, thread);
			SetWindowsHookExW(WH.MOUSE, MouseHook, IntPtr.Zero, thread);
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
			SetLastError(0);
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
			SetLastError(0);
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