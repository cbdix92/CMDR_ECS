using System;
using System.Runtime.InteropServices;

namespace CMDR.Native
{
    internal struct PIXELFORMATDESCRIPTOR
    {
        ushort nSize;
        ushort nVersion;
        uint swFlags;
        byte iPixelType;
        byte cColorBits;
        byte cRedBits;
        byte cRedShift;
        byte cGreenBits;
        byte cGreenShift;
        byte cBluebits;
        byte cBlueShift;
        byte cAlphaBits;
        byte cAlphaShift;
        byte cAccumBits;
        byte cAccumRedBits;
        byte cAccumGreenBits;
        byte cAccumBlueBits;
        byte cAccumAlphaBits;
        byte cDepthBits;
        byte cStencilBits;
        byte cAuxBuffers;
        byte iLayerType;
        byte bReserved;
        uint dwLayerMask;
        uint dwVisibleMask;
        uint dwDamageMask;
    }
}