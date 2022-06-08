using System;
using System.Runtime.InteropServices;

namespace CMDR.Native
{
	/// <summary>
	/// Contains information about a low-level mouse input event.
	/// <see href="https://docs.microsoft.com/en-us/windows/win32/winmsg/hook-structures"/> Microsoft Docs </see>
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	internal struct MSLLHOOKSTRUCT
	{
		public Point Pos;
		public IntPtr MouseData;
		public IntPtr Flags;
		public IntPtr Time;
		public IntPtr ExtraInfo;
	}
}
