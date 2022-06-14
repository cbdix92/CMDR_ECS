using System;
using System.Runtime.InteropServices;

namespace CMDR.Native
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct RAWINPUTDEVICELIST
    {
        IntPtr hDevice;
        uint dwType;
    }
}