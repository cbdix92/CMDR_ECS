using System;

namespace CMDR.Native
{
    internal sealed class Window
    {
        WNDCLASSEXA WNDCLASS;
        internal Window(uint style, IntPtr lpfnWndProc, int cbClsExtra, int cbWndExtra, IntPtr hInstance,
            IntPtr hIcon, IntPtr hCursor, IntPtr hbrBackground, string lpszMenuName, string lpszClassName, IntPtr hIconSm)
        {
            WNDCLASS = WNDCLASSEXA.Create(style, lpfnWndProc, cbClsExtra, cbWndExtra, hInstance,
            hIcon, hCursor, hbrBackground, lpszMenuName, lpszClassName, hIconSm);
        }
    }
}
