using System;

namespace CMDR.Native
{
    [Flags]
    internal enum PFD : uint
    {
        /// <summary> The buffer can draw to a window or device surface. </summary>
        DRAW_TO_WINDOW = 0x00000004,

        /// <summary> The buffer can draw to a memory bitmap. </summary>
        DRAW_TO_BITMAP = 0x00000008,

        /// <summary> 
        /// The buffer supports GDI drawing. 
        /// This flag and PFD_DOUBLEBUFFER are mutually exclusive in the current generic implementation. 
        /// </summary>
        SUPPORT_GDI = 0x00000010,

        /// <summary> The buffer supports OpenGL drawing. </summary>
        SUPPORT_OPENGL = 0x00000020,

        /// <summary> 
        /// The pixel format is supported by a device driver that accelerates the generic implementation.
        /// If this flag is clear and the PFD_GENERIC_FORMAT flag is set, the pixel format is supported by the generic implementation only.
        /// </summary>
        GENERIC_ACCELERATED = 0x00001000,

        /// <summary> 
        /// The pixel format is supported by the GDI software implementation, which is also known as the generic implementation. 
        /// If this bit is clear, the pixel format is supported by a device driver or hardware.
        /// </summary>
        GENERIC_FORMAT = 0x00000040,

        /// <summary> 
        /// The buffer uses RGBA pixels on a palette-managed device.
        /// A logical palette is required to achieve the best results for this pixel type. 
        /// Colors in the palette should be specified according to the values of the cRedBits, cRedShift, cGreenBits, cGreenShift, cBluebits, and cBlueShift members.
        /// The palette should be created and realized in the device context before calling wglMakeCurrent.
        /// </summary>
        NEED_PALETTE = 0x00000080,

        /// <summary> 
        /// Defined in the pixel format descriptors of hardware that supports one hardware palette in 256-color mode only.
        /// For such systems to use hardware acceleration, the hardware palette must be in a fixed order (for example, 3-3-2) when in RGBA mode or must match the
        /// logical palette when in color-index mode.When this flag is set, you must call SetSystemPaletteUse in your program to force a one-to-one mapping of the
        /// logical palette and the system palette. 
        /// If your OpenGL hardware supports multiple hardware palettes and the device driver can allocate spare hardware palettes for OpenGL, this flag is typically clear.
        /// This flag is not set in the generic pixel formats.
        /// </summary>
        NEED_SYSTEM_PALETTE = 0x00000100,

        /// <summary> The buffer is double-buffered. This flag and PFD_SUPPORT_GDI are mutually exclusive in the current generic implementation. </summary>
        DOUBLEBUFFER = 0x00000001,

        /// <summary> The buffer is stereoscopic. This flag is not supported in the current generic implementation. </summary>
        STEREO = 0x00000002,

        /// <summary>
        /// Indicates whether a device can swap individual layer planes with pixel formats that include double-buffered overlay or underlay planes.
        /// Otherwise all layer planes are swapped together as a group. When this flag is set, wglSwapLayerBuffers is supported.
        /// </summary>
        SWAP_LAYER_BUFFERS = 0x00000800,

        /// ---------------------------------------------------------------------
        /// You can specify the following bit flags when calling <see cref="ChoosePixelFormat">.

        /// <summary>
        /// The requested pixel format can either have or not have a depth buffer.
        /// To select a pixel format without a depth buffer, you must specify this flag.
        /// The requested pixel format can be with or without a depth buffer.
        /// Otherwise, only pixel formats with a depth buffer are considered.
        /// </summary>
        DEPTH_DONTCARE = 0x20000000,

        /// <summary> The requested pixel format can be either single- or double-buffered. </summary>
        DOUBLEBUFFER_DONTCARE = 0x40000000,

        /// <summary> The requested pixel format can be either monoscopic or stereoscopic. </summary>
        STEREO_DONTCARE = 0x80000000,

        /// ---------------------------------------------------------------------
        /// With the <see cref="glAddSwapHintRectWIN"> extension function, two new flags are included for the <see cref="PIXELFORMATDESCRIPTOR"> pixel format structure.

        /// <summary>
        /// Specifies the content of the back buffer in the double-buffered main color plane following a buffer swap.
        /// Swapping the color buffers causes the content of the back buffer to be copied to the front buffer.
        /// The content of the back buffer is not affected by the swap. <see cref="SWAP_COPY"> is a hint only and might not be provided by a driver.
        /// </summary>
        SWAP_COPY = 0x00000400,

        /// <summary> 
        /// Specifies the content of the back buffer in the double-buffered main color plane following a buffer swap.
        /// Swapping the color buffers causes the exchange of the back buffer's content with the front buffer's content.
        /// Following the swap, the back buffer's content contains the front buffer's content before the swap.
        /// <see cref="SWAP_EXCHANGE"> is a hint only and might not be provided by a driver.
        /// </summary>
        SWAP_EXCHANGE = 0x00000200,

        /// ---------------------------------------------------------------------
        /// Specifies the type of pixel data. For <see cref="PIXELFORMATDESCRIPTOR.iPixelData"> the following types are defined.

        /// <summary> RGBA pixels. Each pixel has four components in this order: red, green, blue, and alpha. </summary>
        TYPE_RGBA = 0,

        /// <summary> Color-index pixels. Each pixel uses a color-index value. </summary>
        TYPE_COLORINDEX = 1
    }
}