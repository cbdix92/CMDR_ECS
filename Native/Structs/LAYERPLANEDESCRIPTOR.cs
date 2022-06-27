using System;

namespace CMDR.Native
{
    /// <summary>
    /// The LAYERPLANEDESCRIPTOR structure describes the pixel format of a drawing surface.
    /// </summary>
    /// <see href="https://docs.microsoft.com/en-us/windows/win32/api/wingdi/ns-wingdi-layerplanedescriptor"> MICROSOFT DOCS </see>
    [StructLayout(LayoutKind.Sequential)]
    internal struct LAYERPLANEDESCRIPTOR
    {
        public ushort nSize;
        public ushort nVersion;
        public uint dwFlags;
        public byte iPixelType;
        public byte cColorBits;
        public byte cRedBits;
        public byte cRedShift;
        public byte cGreenBits;
        public byte cGreenShift;
        public byte cBlueBits;
        public byte cBlueShift;
        public byte cAlphaBits;
        public byte cAlphaShift;
        public byte cAccumBits;
        public byte cAccumRedBits;
        public byte cAccumGreenBits;
        public byte cAccumBlueBits;
        public byte cAccumAlphaBits;
        public byte cDepthBits;
        public byte cStencilBits;
        public byte cAuxBuffers;
        public byte iLayerPlane;
        public byte bReserved;
        public IntPtr crTransparent;
    }
}