using System;
using System.Runtime.InteropServices;

namespace CMDR.Native
{
	/// <summary>
	/// Contains information about a low-level keyboard input event.
	/// <see href="https://docs.microsoft.com/en-us/windows/win32/winmsg/hook-structures"/> Microsoft Docs </see>
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	internal struct KBDLLHOOKSTRUCT
	{
		public IntPtr VkCode;
		public IntPtr ScaneCode;
		public IntPtr Flags;
		public IntPtr Time;
		public IntPtr ExtraInfo;
	}
}
