using System;
using System.Runtime.InteropServices;

namespace OpenGL
{
    public static unsafe partial class GL
    {
        #region B
        [DllImport(Import.Opengl32, EntryPoint = "glBindBuffer", SetLastError = true)]
        private static extern void _bindBuffer(BUFFER_BINDING_TARGET target, uint buffer);

        [DllImport(Import.Opengl32, EntryPoint = "glBindBuffer", SetLastError = true)]
        private static extern void _bindVertexArray(uint array);

        [DllImport(Import.Opengl32, EntryPoint = "glBufferData", SetLastError = true)]
        private static extern void _bufferData(BUFFER_BINDING_TARGET target, int size, void* data, USAGE usage);
        #endregion

        #region C

        [DllImport(Import.Opengl32, EntryPoint = "glClear", SetLastError = true)]
        private static extern void _clear(BUFFER_MASK mask);

        [DllImport(Import.Opengl32, EntryPoint = "glClearColor", SetLastError = true)]
        private static extern void _clearColor(float red, float green, float blue, float alpha);
        #endregion

        #region G

        [DllImport(Import.Opengl32, EntryPoint = "glGenBuffers", SetLastError = true)]
        private static extern void _genBuffers(int n, uint* buffers);

        [DllImport(Import.Opengl32, EntryPoint = "glGenVertexArrays", SetLastError = true)]
        private static extern void _genVertexArrays(uint n, uint* arrays);
        #endregion

    }
}
