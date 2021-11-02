using System;
using System.Runtime.InteropServices;

namespace OpenGL
{
    public static unsafe partial class GL
    {
		
		private const string User32 = "user32.dll";
        private const string Kernel32 = "kernel32.dll";
        private const string Opengl32 = "opengl32.dll";
        #region B
        [DllImport(Opengl32, EntryPoint = "glBindBuffer", SetLastError = true)]
        private static extern void _bindBuffer(BUFFER_BINDING_TARGET target, uint buffer);

        [DllImport(Opengl32, EntryPoint = "glBindBuffer", SetLastError = true)]
        private static extern void _bindVertexArray(uint array);

        [DllImport(Opengl32, EntryPoint = "glBufferData", SetLastError = true)]
        private static extern void _bufferData(BUFFER_BINDING_TARGET target, int size, void* data, USAGE usage);
        #endregion

        #region C

        [DllImport(Opengl32, EntryPoint = "glClear", SetLastError = true)]
        private static extern void _clear(BUFFER_MASK mask);

        [DllImport(Opengl32, EntryPoint = "glClearColor", SetLastError = true)]
        private static extern void _clearColor(float red, float green, float blue, float alpha);
        #endregion

        #region G

        //[DllImport(Opengl32, EntryPoint = "glGenBuffers", SetLastError = true)]
        [BuildInfo(Opengl32, "glGenBuffers")]
        internal static void _genBuffers(int n, uint* buffers) { }

        //[DllImport(Opengl32, EntryPoint = "glGenVertexArrays", SetLastError = true)]
        [BuildInfo(Opengl32, "glGenVertexArrays")]
        internal static void _genVertexArrays(uint n, uint* arrays) { }
        #endregion

    }
}
