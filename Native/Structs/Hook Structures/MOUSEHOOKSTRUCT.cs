using System;
using System.Runtime.InteropServices;

namespace CMDR.Native
{
	/// <summary>
	/// Contains information about a mouse event passed to a WH_MOUSE hook procedure, MouseProc.
	/// <see href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-mousehookstruct"/> Microsoft Docs </see>
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
	/// This is an extension of the MOUSEHOOKSTRUCT structure that includes information 
	/// about wheel movement or the use of the X button.
	/// <see href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-mousehookstructex"/> Microsoft Docs </see>
	/// </summary>
	[StructLayout(LayoutKind.Sequential)]
	internal struct MouseHookStructEX
	{
		public IntPtr dWord;
		public Point Pos;
		public IntPtr HWND;
		public uint HitTestCode;
		public IntPtr ExtraInfo;
	}
}
