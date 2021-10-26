using System;
using System.Runtime.InteropServices;

namespace OpenGL
{
    public static class Import
    {
        internal const string User32 = "user32.dll";
        internal const string Kernel32 = "kernel32.dll";
        internal const string Opengl32 = "opengl32.dll";



        private static IntPtr _lib = LoadLibrary(Opengl32);
        
        
        internal static IntPtr GetProcAddress(string name)
        {
            return GetProcAddress(_lib, name);
        }

        [DllImport(Kernel32, SetLastError = true)]
        internal extern static IntPtr GetProcAddress(IntPtr lib, string funcName);

        [DllImport(Kernel32, SetLastError = true)]
        internal static extern IntPtr LoadLibrary(string lpFileName);
		
		[DLLImport(Opengl32, SetLastError = true;)]
		internal static extern IntPtr wglGetProcAddress(string name);

    }
}
