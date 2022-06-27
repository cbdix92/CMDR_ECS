using System;
using System.Runtime.InteropServices;
using HDC=System.IntPtr;
using HGLRC=System.IntPtr;

namespace CMDR.Native
{
    internal static partial class Win
    {
        const string OpenGL = "opengl32.dll";

        /// <summary>
        /// The wglCopyContext function copies selected groups of rendering states from one OpenGL rendering context to another.
        /// </summary>
        /// <param name="hglrc1"> Specifies the source OpenGL rendering context whose state information is to be copied. </param>
        /// <param name="hglrc2"> Specifies the destination OpenGL rendering context to which state information is to be copied. </param>
        /// <param name="hglrcSrc"> Specifies which groups of the hglrcSrc rendering state are to be copied to hglrcDst.
        /// It contains the bitwise-OR of the same symbolic names that are passed to the glPushAttrib function.
        /// You can use GL_ALL_ATTRIB_BITS to copy all the rendering state information. </param>
        /// <returns> If the function succeeds, the return value is TRUE. If the function fails, the return value is FALSE. </returns>
        /// <see href="https://docs.microsoft.com/en-us/windows/win32/api/wingdi/nf-wingdi-wglcopycontext"> MICROSOFT DOCS </see>
        [DllImport(OpenGL, SetLastError = true)]
        internal static extern bool wglCopyContext(HGLRC hglrc1, HGLRC hglrc2, uint hglrcSrc );

        /// <summary>
        /// The wglCreateContext function creates a new OpenGL rendering context, which is suitable for drawing on the device referenced by hdc.
        /// The rendering context has the same pixel format as the device context.
        /// </summary>
        /// <param name="hdc"> Handle to a device context for which the function creates a suitable OpenGL rendering context. </param>
        /// <returns> If the function succeeds, the return value is a valid handle to an OpenGL rendering context. If the function fails, the return value is NULL. </returns>
        /// <see href="https://docs.microsoft.com/en-us/windows/win32/api/wingdi/nf-wingdi-wglcreatecontext"> MICROSOFT DOCS </see>
        [DllImport(OpenGL, SetLastError = true)]
        internal static extern HGLRC wglCreateContext(HDC hdc);

        /// <summary>
        /// The wglCreateLayerContext function creates a new OpenGL rendering context for drawing to a specified layer plane on a device context.
        /// </summary>
        /// <param name="hdc"> Specifies the device context for a new rendering context. </param>
        /// <param name="layerPlane"> Specifies the layer plane to which you want to bind a rendering context. The value 0 identifies the main plane.
        /// Positive values of iLayerPlane identify overlay planes, where 1 is the first overlay plane over the main plane, 2 is the second overlay plane over
        /// the first overlay plane, and so on. Negative values identify underlay planes, where 1 is the first underlay plane under the main plane, 2 is the
        /// second underlay plane under the first underlay plane, and so on. The number of overlay and underlay planes is given in the bReserved member
        /// of the PIXELFORMATDESCRIPTOR structure. </param>
        /// <returns> If the function succeeds, the return value is a handle to an OpenGL rendering context. If the function fails, the return value is NULL. </returns>
        /// <see href="https://docs.microsoft.com/en-us/windows/win32/api/wingdi/nf-wingdi-wglcreatelayercontext"> MICROSOFT DOCS </see>
        [DllImport(OpenGL, SetLastError = true)]
        internal static extern HGLRC wglCreateLayerContext(HDC hdc, int layerPlane);

        /// <summary>
        /// The wglDeleteContext function deletes a specified OpenGL rendering context.
        /// </summary>
        /// <param name="hglrc"> Handle to an OpenGL rendering context that the function will delete. </param>
        /// <returns> If the function succeeds, the return value is TRUE. If the function fails, the return value is FALSE. </returns>
        /// <see href="https://docs.microsoft.com/en-us/windows/win32/api/wingdi/nf-wingdi-wgldeletecontext"> MICROSOFT DOCS </see>
        [DllImport(OpenGL, SetLastError = true)]
        internal static extern bool wglDeleteContext(HGLRC hglrc);

        /// <summary>
        /// The wglDescribeLayerPlane function obtains information about the layer planes of a given pixel format.
        /// </summary>
        /// <param name="hdc"> Specifies the device context of a window whose layer planes are to be described. </param>
        /// <param name="iLayer"> Specifies which layer planes of a pixel format are being described. </param>
        /// <param name="iLayerPlane"> Specifies the overlay or underlay plane. Positive values of iLayerPlane identify overlay planes,
        /// where 1 is the first overlay plane over the main plane, 2 is the second overlay plane over the first overlay plane, and so on.
        /// Negative values identify underlay planes, where 1 is the first underlay plane under the main plane, 2 is the second underlay plane under
        /// the first underlay plane, and so on. The number of overlay and underlay planes is given in the bReserved member of the PIXELFORMATDESCRIPTOR structure. </param>
        /// <param name="nSize"> Specifies the size, in bytes, of the structure pointed to by plpd. The wglDescribeLayerPlane function stores layer plane data in
        /// a LAYERPLANEDESCRIPTOR structure, and stores no more than nBytes of data. Set the value of nBytes to the size of LAYERPLANEDESCRIPTOR. </param>
        /// <param name="lpd"> Points to a LAYERPLANEDESCRIPTOR structure. The wglDescribeLayerPlane function sets the value of the structure's data members.
        /// The function stores the number of bytes of data copied to the structure in the nSize member. </param>
        /// <returns> If the function succeeds, the return value is TRUE. In addition, the wglDescribeLayerPlane function sets the members of the LAYERPLANEDESCRIPTOR
        /// structure pointed to by plpd according to the specified layer plane (iLayerPlane ) of the specified pixel format (iPixelFormat ).
        /// If the function fails, the return value is FALSE. </returns>
        /// <see href="https://docs.microsoft.com/en-us/windows/win32/api/wingdi/nf-wingdi-wgldescribelayerplane"> MICROSOFT DOCS </see>
        [DllImport(OpenGL, SetLastError = true)]
        internal static extern bool wglDescribeLayerPlane(HDC hdc, int iLayer, int iLayerPlane, uint nSize, [MarshalAs(UnmanagedType.LPStruct)]LAYERPLANEDESCRIPTOR lpd);

        /// <summary>
        /// The wglGetCurrentContext function obtains a handle to the current OpenGL rendering context of the calling thread.
        /// </summary>
        /// <returns> If the calling thread has a current OpenGL rendering context, wglGetCurrentContext returns a handle to that rendering context.
        /// Otherwise, the return value is NULL. </returns>
        /// <see href="https://docs.microsoft.com/en-us/windows/win32/api/wingdi/nf-wingdi-wglgetcurrentcontext"> MICROSOFT DOCS </see>
        [DllImport(OpenGL, SetLastError = true)]
        internal static extern HGLRC wglGetCurrentContext();

        /// <summary>
        /// Retrieves the palette entries from a given color-index layer plane for a specified device context.
        /// </summary>
        /// <param name="hdc"> The device context of a window whose layer planes are to be described. </param>
        /// <param name="iLayerPlane"> The overlay or underlay plane. Positive values of iLayerPlane identify overlay planes, where 1 is the
        /// first overlay plane over the main plane, 2 is the second overlay plane over the first overlay plane, and so on. Negative values
        /// identify underlay planes, where 1 is the first underlay plane under the main plane, 2 is the second underlay plane under the first
        /// underlay plane, and so on. The number of overlay and underlay planes is given in the bReserved member of the PIXELFORMATDESCRIPTOR structure. </param>
        /// <param name="index"> The first palette entry to be retrieved. </param>
        /// <param name="nPallette"> The number of palette entries to be retrieved. </param>
        /// <param name="colorRef"> An array of structures that contain palette RGB color values. The array must contain at least as many structures as specified by cEntries. </param>
        /// <returns>  </returns>
        /// <see href="https://docs.microsoft.com/en-us/windows/win32/api/wingdi/nf-wingdi-wglgetlayerpaletteentries"> MICROSOFT DOCS </see>
        [DllImport(OpenGL, SetLastError = true)]
        internal static extern wglGetLayerPaletteEntries(HDC hdc, int iLayerPlane, int index , int nPallette , ref uint colorRef);
        
        /// <summary>
        /// The wglGetProcAddress function returns the address of an OpenGL extension function for use with the current OpenGL rendering context.
        /// </summary>
        /// <param name="name"> Points to a null-terminated string that is the name of the extension function.
        ///  The name of the extension function must be identical to a corresponding function implemented by OpenGL. </param>
        /// <returns> When the function succeeds, the return value is the address of the extension function. 
        /// When no current rendering context exists or the function fails, the return value is NULL. </returns>
        /// <see href="https://docs.microsoft.com/en-us/windows/win32/api/wingdi/nf-wingdi-wglgetprocaddress"> MICROSOFT DOCS </see>
        [DllImport(OpenGL, SetLastError = true)]
        internal static extern IntPtr wglGetProcAddress(string name);

        /// <summary>
        /// The wglMakeCurrent function makes a specified OpenGL rendering context the calling thread's current rendering context.
        /// All subsequent OpenGL calls made by the thread are drawn on the device identified by hdc.
        /// You can also use wglMakeCurrent to change the calling thread's current rendering context so it's no longer current.
        /// </summary>
        /// <param name="hdc">  Handle to a device context. Subsequent OpenGL calls made by the calling thread are drawn on the device identified by hdc. </param>
        /// <param name="hglrc"> Handle to an OpenGL rendering context that the function sets as the calling thread's rendering context.
        /// If hglrc is NULL, the function makes the calling thread's current rendering context no longer current, and releases the device context that is used by the rendering context.
        /// In this case, hdc is ignored. </param>
        /// <returns> When the wglMakeCurrent function succeeds, the return value is TRUE; otherwise the return value is FALSE. </returns>
        /// <see href="https://docs.microsoft.com/en-us/windows/win32/api/wingdi/nf-wingdi-wglmakecurrent"> MICROSOFT DOCS </see>
        [DllImport(OpenGL, SetLastError = true)]
        internal static extern bool wglMakeCurrent(HDC hdc, HGLRC hglrc);

        /// <summary>
        /// The wglRealizeLayerPalette function maps palette entries from a given color-index layer plane into the physical palette or initializes the palette of an RGBA layer plane.
        /// </summary>
        /// <param name="hdc"> Specifies the device context of a window whose layer plane palette is to be realized into the physical palette. </param>
        /// <param name="iLayerPlane"> Specifies the overlay or underlay plane. Positive values of iLayerPlane identify overlay planes,
        /// where 1 is the first overlay plane over the main plane, 2 is the second overlay plane over the first overlay plane, and so on.
        /// Negative values identify underlay planes, where 1 is the first underlay plane under the main plane, 2 is the second underlay plane under
        /// the first underlay plane, and so on. The number of overlay and underlay planes is given in the bReserved member of the PIXELFORMATDESCRIPTOR structure. </param>
        /// <param name="bRealize"> Indicates whether the palette is to be realized into the physical palette.
        /// When bRealize is TRUE, the palette entries are mapped into the physical palette where available.
        /// When bRealize is FALSE, the palette entries for the layer plane of the window are no longer needed and might be released
        /// for use by another foreground window. </param>
        /// <returns> If the function succeeds, the return value is TRUE, even if bRealize is TRUE and the physical palette is not available.
        /// If the function fails or when no pixel format is selected, the return value is FALSE.  </returns>
        /// <see href="https://docs.microsoft.com/en-us/windows/win32/api/wingdi/nf-wingdi-wglrealizelayerpalette"> MICROSOFT DOCS </see>
        [DllImport(OpenGL, SetLastError = true)]
        internal static extern bool wglRealizeLayerPalette(HDC hdc, int iLayerPlane, bool bRealize);

        /// <summary>
        /// Sets the palette entries in a given color-index layer plane for a specified device context.
        /// </summary>
        /// <param name="hdc"> The device context of a window whose layer palette is to be set. </param>
        /// <param name="iLayerPlane"> An overlay or underlay plane. Positive values of iLayerPlane identify overlay planes,
        /// where 1 is the first overlay plane over the main plane, 2 is the second overlay plane over the first overlay plane, and so on.
        /// Negative values identify underlay planes, where 1 is the first underlay plane under the main plane, 2 is the second underlay plane
        /// under the first underlay plane, and so on. The number of overlay and underlay planes is given in the bReserved member
        /// of the PIXELFORMATDESCRIPTOR structure. </param>
        /// <param name="iStart"> The first palette entry to be set. </param>
        /// <param name="nSize"> The number of palette entries to be set. </param>
        /// <param name="colorRef"> A pointer to the first member of an array of cEntries structures that contain RGB color information. </param>
        /// <returns> If the function succeeds, the return value is the number of entries that were set in the palette in the specified layer plane of the window.
        /// If the function fails or no pixel format is selected, the return value is zero. </returns>
        /// <see href="https://docs.microsoft.com/en-us/windows/win32/api/wingdi/nf-wingdi-wglsetlayerpaletteentries"> MICROSOFT DOCS </see>
        [DllImport(OpenGL, SetLastError = true)]
        internal static extern int wglSetLayerPaletteEntries(HDC hdc, int iLayerPlane, int iStart, int nSize, ref uint colorRef);

        /// <summary>
        /// The wglShareLists function enables multiple OpenGL rendering contexts to share a single display-list space.
        /// </summary>
        /// <param name="hglrc1"> Specifies the OpenGL rendering context with which to share display lists. </param>
        /// <param name="hglrc2"> Specifies the OpenGL rendering context to share display lists with hglrc1.
        /// The hglrc2 parameter should not contain any existing display lists when wglShareLists is called. </param>
        /// <returns> When the function succeeds, the return value is TRUE. When the function fails, the return value is FALSE and the display lists are not shared. </returns>
        /// <see href="https://docs.microsoft.com/en-us/windows/win32/api/wingdi/nf-wingdi-wglsharelists"> MICROSOFT DOCS </see>
        [DllImport(OpenGL, SetLastError = true)]
        internal static extern bool wglShareLists(HGLRC hglrc1, HGLRC hglrc2);

        /// <summary>
        /// The wglSwapLayerBuffers function swaps the front and back buffers in the overlay, underlay, and main planes of the window referenced by a specified device context.
        /// </summary>
        /// <param name="hdc"> Specifies the device context of a window whose layer plane palette is to be realized into the physical palette. </param>
        /// <param name="fuPlanes"> Specifies the overlay, underlay, and main planes whose front and back buffers are to be swapped.
        /// The bReserved member of the PIXELFORMATDESCRIPTOR structure specifies the number of overlay and underlay planes.
        /// The fuPlanes parameter is a bitwise combination of the following values. </param>
        /// <returns> If the function succeeds, the return value is TRUE. If the function fails, the return value is FALSE. </returns>
        /// <see href="https://docs.microsoft.com/en-us/windows/win32/api/wingdi/nf-wingdi-wglswaplayerbuffers"> MICROSOFT DOCS </see>
        [DllImport(OpenGL, SetLastError = true)]
        internal static extern bool wglSwapLayerBuffers(HDC hdc, uint fuPlanes);

        /// <summary>
        /// The wglUseFontBitmaps function creates a set of bitmap display lists for use in the current OpenGL rendering context.
        /// The set of bitmap display lists is based on the glyphs in the currently selected font in the device context.
        /// You can then use bitmaps to draw characters in an OpenGL image.
        /// The wglUseFontBitmaps function creates count display lists, one for each of a run of count glyphs that begins with the
        /// first glyph in the hdc parameter's selected fonts.
        /// </summary>
        /// <param name="hdc"> Specifies the device context whose currently selected font will be used to form the glyph bitmap display lists in the current OpenGL rendering context. </param>
        /// <param name="firstGlyph"> Specifies the first glyph in the run of glyphs that will be used to form glyph bitmap display lists. </param>
        /// <param name="nGlyphs"> Specifies the number of glyphs in the run of glyphs that will be used to form glyph bitmap display lists.
        /// The function creates count display lists, one for each glyph in the run. </param>
        /// <param name="start"> Specifies a starting display list. </param>
        /// <returns> If the function succeeds, the return value is TRUE. If the function fails, the return value is FALSE. </returns>
        /// <see href="https://docs.microsoft.com/en-us/windows/win32/api/wingdi/nf-wingdi-wglusefontbitmapsw"> MICROSOFT DOCS </see>
        [DllImport(OpenGL, SetLastError = true)]
        internal static extern bool wglUseFontBitmapsW(HDC hdc, uint firstGlyph, uint nGlyphs, uint start);

        /// <summary>
        /// The wglUseFontOutlines function creates a set of display lists, one for each glyph of the currently selected outline font of a device context,
        /// for use with the current rendering context. The display lists are used to draw 3-D characters of TrueType fonts.
        /// Each display list describes a glyph outline in floating-point coordinates.
        /// The run of glyphs begins with thefirstglyph of the font of the specified device context.
        /// The em square size of the font, the notional grid size of the original font outline from which the font is fitted,
        /// is mapped to 1.0 in the x- and y-coordinates in the display lists. The extrusion parameter sets how much depth the font has in the z direction.
        /// Thelpgmfparameter returns a GLYPHMETRICSFLOAT structure that contains information about the placement and orientation of each glyph in a character cell.
        /// </summary>
        /// <param name="hdc"> Specifies the device context with the desired outline font. The outline font of hdc is used to create the display lists in the current rendering context. </param>
        /// <param name="first"> Specifies the first of the set of glyphs that form the font outline display lists. </param>
        /// <param name="count"> Specifies the number of glyphs in the set of glyphs used to form the font outline display lists.
        /// The wglUseFontOutlines function creates count display lists, one display list for each glyph in a set of glyphs. </param>
        /// <param name="listBase"> Specifies a starting display list. </param>
        /// <param name="deviation"> Specifies the maximum chordal deviation from the original outlines. When deviation is zero, the chordal deviation is equivalent to one design unit of the original font.
        /// The value of deviation must be equal to or greater than 0. </param>
        /// <param name="extrusion"> Specifies how much a font is extruded in the negative z direction. The value must be equal to or greater than 0. When extrusion is 0, the display lists are not extruded. </param>
        /// <param name="format"> Specifies the format, either WGL_FONT_LINES or WGL_FONT_POLYGONS, to use in the display lists. When format is WGL_FONT_LINES, the wglUseFontOutlines function creates fonts with line segments.
        /// When format is WGL_FONT_POLYGONS, wglUseFontOutlines creates fonts with polygons. </param>
        /// <param name="gmf"> Points to an array of countGLYPHMETRICSFLOAT structures that is to receive the metrics of the glyphs. When lpgmf is NULL, no glyph metrics are returned. </param>
        /// <returns>  </returns>
        /// <see href="https://docs.microsoft.com/en-us/windows/win32/api/wingdi/nf-wingdi-wglusefontoutlinesw"> MICROSOFT DOCS </see>
        [DllImport(OpenGL, SetLastError = true)]
        internal static extern wglUseFontOutlinesW(HDC hdc, uint first, uint count, uint listBase, float deviation, float extrusion, int format, [MarshalAs(UnmanagedType.LPStruct)] ref GLYPHMETRICSFLOAT gmf);

    }
}