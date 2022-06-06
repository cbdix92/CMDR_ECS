using System;
using System.Runtime.InteropServices;

namespace CMDR.Native
{
    internal static partial class Win
    {

		private const string User32 = "user32.dll";
		private const string Kernel32 = "kernel32.dll";

		[DllImport(Kernel32, CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		internal static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

		[DllImport(Kernel32, SetLastError = true)]
		internal static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPStr)] string lpFileName);

		[DllImport(Kernel32, SetLastError = true)]
		internal static extern bool FreeLibrary(IntPtr hModule);

		[DllImport(Kernel32, SetLastError = true)]
		internal static extern uint GetCurrentThreadID();

		[DllImport(User32, SetLastError = true)]
		internal static extern unsafe IntPtr SetWindowsHookEx(WH hookType, Proc lpfn, IntPtr hmod, uint dwThreadID);

		[DllImport(User32, SetLastError = true)]
		internal static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

		[DllImport(User32, SetLastError = true)]
		internal static unsafe extern ushort RegisterClassExA([MarshalAs(UnmanagedType.LPStruct)][In] ref WNDCLASSEXA wnd);

	}
}
