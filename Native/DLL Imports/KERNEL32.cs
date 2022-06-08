using System;
using System.Runtime.InteropServices;

namespace CMDR.Native
{
    internal static partial class Win
    {
        const string Kernel32 = "kernel32.dll";

		[DllImport(Kernel32, CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		internal static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

		[DllImport(Kernel32, SetLastError = true)]
		internal static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPStr)] string lpFileName);

		[DllImport(Kernel32, SetLastError = true)]
		internal static extern bool FreeLibrary(IntPtr hModule);

		[DllImport(Kernel32, SetLastError = true)]
		internal static extern uint GetCurrentThreadID();
	}
}
