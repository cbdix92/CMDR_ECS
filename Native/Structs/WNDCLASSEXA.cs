using System;
using System.Runtime.InteropServices;

namespace CMDR.Native
{
	/// <summary>
	/// Contains window class information. It is used with the RegisterClassEx and GetClassInfoEx  functions.
	/// <see href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-wndclassexa"/> Microsoft Docs </see>
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	internal struct WNDCLASSEXA
	{
		uint cbSize;
		uint style;
		IntPtr lpfnWndProc;
		int cbClsExtra;
		int cbWndExtra;
		IntPtr hInstance;
		IntPtr hIcon;
		IntPtr hCursor;
		IntPtr hbrBackground;
		[MarshalAs(UnmanagedType.LPTStr)]
		string lpszMenuName;
		[MarshalAs(UnmanagedType.LPTStr)]
		string lpszClassName;
		IntPtr hIconSm;

		internal static WNDCLASSEXA Create(uint style, IntPtr lpfnWndProc, int cbClsExtra, int cbWndExtra, IntPtr hInstance,
			IntPtr hIcon, IntPtr hCursor, IntPtr hbrBackground, string lpszMenuName, string lpszClassName, IntPtr hIconSm)
		{
			var _ = new WNDCLASSEXA()
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
			_.cbSize = (uint)Marshal.SizeOf<WNDCLASSEXA>(_);
			return _;
		}
	}
}
