using System;
using System.Runtime.InteropServices;

namespace CMDR.Native
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct RAWINPUTHEADER
    {
        uint dwType;
        uint dwSize;
        IntPtr hDevice;
        IntPtr wParam;
    }
}