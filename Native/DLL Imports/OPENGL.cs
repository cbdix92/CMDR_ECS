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
        /// The wglCreateContext function creates a new OpenGL rendering context, which is suitable for drawing on the device referenced by hdc.
        /// The rendering context has the same pixel format as the device context.
        /// </summary>
        /// <param name="hdc"> Handle to a device context for which the function creates a suitable OpenGL rendering context. </param>
        /// <returns> If the function succeeds, the return value is a valid handle to an OpenGL rendering context. If the function fails, the return value is NULL. </returns>
        /// <see href="https://docs.microsoft.com/en-us/windows/win32/api/wingdi/nf-wingdi-wglcreatecontext"> MICROSOFT DOCS </see>
        [DllImport(OpenGL, SetLastError = true)]
        internal static extern HGLRC wglCreateContext(HDC hdc);

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

    }
}