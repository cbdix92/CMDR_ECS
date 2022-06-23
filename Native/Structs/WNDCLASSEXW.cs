using System;
using System.Runtime.InteropServices;

namespace CMDR.Native
{
	/// <summary>
	/// Contains window class information. It is used with the RegisterClassEx and GetClassInfoEx  functions.
	/// <see href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-wndclassexa"/> Microsoft Docs </see>
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	internal struct WNDCLASSEXW
	{
		public uint cbSize;
		public uint style;
		public WNDPROC lpfnWndProc;
		public int cbClsExtra;
		public int cbWndExtra;
		public IntPtr hInstance;
		public IntPtr hIcon;
		public IntPtr hCursor;
		public IntPtr hbrBackground;
		[MarshalAs(UnmanagedType.LPStr)]
		public string lpszMenuName;
		[MarshalAs(UnmanagedType.LPStr)]
		public string lpszClassName;
		public IntPtr hIconSm;
		
		internal WNDCLASSEXW(uint cbSize, uint style, WNDPROC lpfnWndProc, int cbClsExtra, int cbWndExtra, IntPtr hInstance,
			IntPtr hIcon, IntPtr hCursor, IntPtr hbrBackground, string lpszMenuName, string lpszClassName, IntPtr hIconSm)
		{
			this.cbSize = cbSize;
			this.style = style;
			this.lpfnWndProc = lpfnWndProc;
			this.cbClsExtra = cbClsExtra;
			this.cbWndExtra = cbWndExtra;
			this.hInstance = hInstance;
			this.hIcon = hIcon;
			this.hCursor = hCursor;
			this.hbrBackground = hbrBackground;
			this.lpszMenuName = lpszMenuName;
			this.lpszClassName = lpszClassName;
			this.hIconSm = hIconSm;
		}

		internal static WNDCLASSEXW Create()
		{
            WNDCLASSEXW _ = new WNDCLASSEXW();
			_.cbSize = (uint)Marshal.SizeOf<WNDCLASSEXW>(_);
			return _;
		}
	}
}
