using System;
using System.Runtime.InteropServices;

namespace CMDR.Native
{
	[StructLayout(LayoutKind.Sequential)]
	internal struct Point
	{
		public int X;
		public int Y;
	}

	/// <summary>
	/// Contains information about a mouse event passed to a WH_MOUSE hook procedure, MouseProc.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	internal struct MouseHookStruct
	{
		public Point Pos;
		public IntPtr HWND;
		public uint HitTestCode;
		public IntPtr ExtraInfo;
	}

	/// <summary>
	/// Contains information about a low-level mouse input event.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	internal struct MSLLHOOK
    {
		public Point Pos;
		public IntPtr MouseData;
		public IntPtr Flags;
		public IntPtr Time;
		public IntPtr ExtraInfo;
    }

	/// <summary>
	/// Contains information about a low-level keyboard input event.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	internal struct KBDLLHOOK
    {
		public IntPtr VkCode;
		public IntPtr ScaneCode;
		public IntPtr Flags;
		public IntPtr Time;
		public IntPtr ExtraInfo;
    }

	/// <summary>
	/// Contains window class information. It is used with the RegisterClassEx and GetClassInfoEx  functions.
	/// see https://docs.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-wndclassexa
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	internal unsafe struct WNDCLASSEXA
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
