using System;

namespace CMDR.Native
{
    /// <see href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-loadimagew"> WINDOWS DOCS </see>
    internal enum LR : uint
    {
        CREATEDIBSECTION = 0x00002000,
        DEFAULTCOLOR = 0x00000000,
        DEFAULTSIZE = 0x00000040,
        LOADFROMFILE = 0x00000010,
        LOADMAP3DCOLORS = 0x00001000,
        LOADTRANSPARENT = 0x00000020,
        MONOCHROME = 0x00000001,
        SHARED = 0x00008000,
        VGACOLOR = 0x00000080
    }
}