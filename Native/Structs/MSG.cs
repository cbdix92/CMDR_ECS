using System;
using System.Runtime.InteropServices;

namespace CMDR.Native
{
    /// <summary>
    /// Contains message information from a thread's message queue.
    /// </summary>
    /// <see href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/ns-winuser-msg"/> MICROSOFT DOCS </see>
    [StructLayout(LayoutKind.Sequential)]
    internal struct MSG
    {
        public IntPtr hwnd;
        public WM message;
        public IntPtr wParam;
        public IntPtr lParam;
        public uint time;
        public Point pt;
        public uint lPrivate;
    }
}
