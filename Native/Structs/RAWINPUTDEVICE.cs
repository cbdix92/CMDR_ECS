using System;
using System.Runtime.InteropServices;

namespace CMDR.Native
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct RAWINPUTDEVICE
    {
        ushort usUsagePage;
        ushort usUsage;
        uint dwFlags;
        IntPtr hwndTarget;
    }
}