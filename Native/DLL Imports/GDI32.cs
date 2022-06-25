using System;
using System.Runtime.InteropServices;
using HDC = System.IntPtr;

namespace CMDR.Native
{
    internal static partial class Win
    {
        const string GDI32 = "gdi32.dll";

        /// <summary>
        /// The ChoosePixelFormat function attempts to match an appropriate pixel format supported by a device context to a given pixel format specification
        /// </summary>
        /// <param name="hdc"> Specifies the device context that the function examines to determine the best match for the pixel format descriptor pointed to by ppfd. </param>
        /// <param name="ppfd"> Pointer to a <see cref="PIXELFORMATDESCRIPTOR"> structure that specifies the requested pixel format. </param>
        /// <returns> If the function succeeds, the return value is a pixel format index (one-based) that is the closest match to the given pixel format descriptor.
        /// If the function fails, the return value is zero. </returns>
        /// <see href="https://docs.microsoft.com/en-us/windows/win32/api/wingdi/nf-wingdi-choosepixelformat"> MICROSOFT DOCS </see>
        [DllImport(GDI32, SetLastError = true)]
        internal static extern int ChoosePixelFormat(HDC hdc, [MarshalAs(UnmanagedType.LPStruct)] [In] PIXELFORMATDESCRIPTOR ppfd);

        /// <summary>
        /// The SetPixelFormat function sets the pixel format of the specified device context to the format specified by the iPixelFormat index.
        /// </summary>
        /// <param name="hdc"> Specifies the device context whose pixel format the function attempts to set. </param>
        /// <param name="format"> Index that identifies the pixel format to set. The various pixel formats supported by a device context are identified by one-based indexes. </param>
        /// <param name="ppfd"> Pointer to a <see cref="PIXELFORMATDESCRIPTOR"> structure that contains the logical pixel format specification.
        /// The system's metafile component uses this structure to record the logical pixel format specification.
        /// The structure has no other effect upon the behavior of the SetPixelFormat function. </param>
        /// <returns> If the function succeeds, the return value is TRUE. If the function fails, the return value is FALSE. </returns>
        /// <see href="https://docs.microsoft.com/en-us/windows/win32/api/wingdi/nf-wingdi-setpixelformat"> MICROSOFT DOCS </see>
        [DllImport(GDI32, SetLastError = true)]
        internal static extern bool SetPixelFormat(HDC hdc, int format, [MarshalAs(UnmanagedType.LPStruct)] PIXELFORMATDESCRIPTOR ppfd);
    }
}