using System;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using System.Collections.Generic;

namespace OpenGL
{
	internal static class Builder
	{
		
		private const string User32 = "user32.dll";
        private const string Kernel32 = "kernel32.dll";
        private const string Opengl32 = "opengl32.dll";
		
		internal static Dictionary<string, IntPtr> Libs = new Dictionary<string, IntPtr>();
		
		[DllImport(Opengl32, CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        internal static extern IntPtr wglGetProcAddress([MarshalAs(UnmanagedType.LPStr)]string name);

        [DllImport(Kernel32, CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
        internal static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

		[DllImport(Kernel32, SetLastError = true)]
        internal static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPStr)]string lpFileName);
		
		[DllImport(Kernel32, SetLastError = true)]
		internal static extern bool FreeLibrary(IntPtr hModule);
		
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
				
				// 32bit process
				if(IntPtr.Size == 4)
				{
					int* ptr = (int*)GetProcFromBuildInfo(buildInfo) + 2;
					int* target = (int*)method.MethodHandle.GetFunctionPointer() + 2;
					*target = *ptr;
				}
				// 64bit process
				else
				{
					ulong* ptr = (ulong*)GetProcFromBuildInfo(buildInfo) + 1;
					ulong* target = (ulong*)method.MethodHandle.GetFunctionPointer() + 1;
					*target = *ptr;
				}
				
			}

			//if(FreeLibs())
			//throw new Exception("Libs failed to unload!");
			return true;
		}
		
		internal static void LoadLibs()
		{
			Log.SetLastError(0);
			Libs.Add(Opengl32, LoadLibrary(Opengl32));
			CheckError(Opengl32);
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
		
		private static IntPtr GetProcFromBuildInfo(BuildInfo info)
		{
			IntPtr result;
			int error;
			if (info.Lib == Opengl32)
			{
				result = wglGetProcAddress(info.Name);
				error = Marshal.GetLastWin32Error();
				if (error == 0)
                {
					var pointer = (Int64)result;
					if(pointer != 0 || pointer != 1 || pointer != 2 || pointer != 3 || pointer != -1)
						return result;

                }

				Log.LogWin32Error(error, info.Name, true);

				result = GetProcAddress(Libs[Opengl32], info.Name);
				error = Marshal.GetLastWin32Error();
				if (error == 0)
					return result;

				Log.LogWin32Error(error, info.Name);

			}
			else
			{
				result = GetProcAddress(Libs[Opengl32], info.Name);
				error = Marshal.GetLastWin32Error();
				if (error == 0)
					return result;

				Log.LogWin32Error(error, info.Name);
			}
			throw new TypeLoadException("Builder Failure! See Log!");



		}
	}
	
	
	
}