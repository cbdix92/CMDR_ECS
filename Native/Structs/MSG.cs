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
        IntPtr hwnd;
        uint message;
        IntPtr wParam;
        IntPtr lParam;
        uint time;
        Point pt;
        uint lPrivate;
    }
}
