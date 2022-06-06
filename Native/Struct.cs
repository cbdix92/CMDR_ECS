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
	internal unsafe struct MouseHookStruct
	{
		public Point Pos;
		public int* HWND;
		public uint HitTestCode;
		public int* ExtraInfo;
	}

	/// <summary>
	/// Contains information about a low-level mouse input event.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	internal unsafe struct MSLLHOOK
    {
		public Point Pos;
		public int* MouseData;
		public int* Flags;
		public int* Time;
		public int* ExtraInfo;
    }

	/// <summary>
	/// Contains information about a low-level keyboard input event.
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	internal unsafe struct KBDLLHOOK
    {
		public int* VkCode;
		public int* ScaneCode;
		public int* Flags;
		public int* Time;
		public int* ExtraInfo;
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

		public static unsafe WNDCLASSEXA Create(uint style, int* lpfnWndProc, int cbClsExtra, int cbWndExtra, int* hInstance, int* hIcon, int* hCursor, int* hbrBackground, string lpszMenuName, string lpszClassName, int* hIconSm)
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
