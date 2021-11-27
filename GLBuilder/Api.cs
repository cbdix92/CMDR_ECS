using System;
using System.Runtime.InteropServices;

namespace OpenGL
{
    public static unsafe partial class GL
    {
		
		private const string User32 = "user32.dll";
        private const string Kernel32 = "kernel32.dll";
        private const string Opengl32 = "opengl32.dll";

        #region A

        [DllImport(Opengl32, EntryPoint = "glAttachShader", SetLastError = true)]
        private static extern void _attachShader(uint program, uint shader);

        #endregion

        #region B
        [DllImport(Opengl32, EntryPoint = "glBindBuffer", SetLastError = true)]
        private static extern void _bindBuffer(BUFFER_BINDING_TARGET target, uint buffer);
		
		[DllImport(Opengl32, EntryPoint = "glBindTexture", SetLastError = true)]
		private static extern void _bindTexture(BUFFER_BINDING_TARGET target, uint texture);
		
		[DllImport(Opengl32, EntryPoint = "glBindTextures", SetLastError = true)]
		private static extern void _bindTextures(uint first, uint count, void* textures);

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

        [DllImport(Opengl32, EntryPoint = "glCompileShader", SetLastError = true)]
        private static extern void _compileShader(uint shader);

        [DllImport(Opengl32, EntryPoint = "glCreateProgram", SetLastError = true)]
        private static extern uint _createProgram();

        [DllImport(Opengl32, EntryPoint = "glCreateShader", SetLastError = true)]
        private static extern uint _createShader(SHADER_TYPE shaderType);



        #endregion
		
		#region D
		
		[DllImport(Opengl32, EntryPoint = "glDisableVertexAttribArray". SetLastError = true)]
		private static extern void _disableVertexAttribArray(uint index);
		
		[DllImport(Opengl32, EntryPoint = "glDrawElements", SetLastError = true)]
		private static extern void _drawElements(MODE mode, uint count, Type type, void* indices);
		
		#endregion
		
		#region E
		
		[DllImport(Opengl32, EntryPoint = "glEnableVertexAttribArray", SetLastError = true)]
		private static extern void _enableVertexAttribArray(uint index);
		
		#endregion
		
        #region G

        [BuildInfo(Opengl32, "glGenBuffers")]
        internal static void _genBuffers(int n, uint* buffers) { }

		[DllImport(Opengl32, EntryPoint = "glGenTextures", SetLastError = true)]
		private static extern void _genTextures(uint n, uint* textures);
		
        [BuildInfo(Opengl32, "glGenVertexArrays")]
        internal static void _genVertexArrays(uint n, uint* arrays) { }
		
        #endregion

        #region L
        [DllImport(Opengl32, EntryPoint = "glLinkProgram", SetLastError = true)]
        private static extern void _linkProgram(uint program);

        #endregion

        #region S

        [DllImport(Opengl32, EntryPoint ="glShaderSource", SetLastError = true)]
        private static extern void _shaderSource(uint shader, int count, byte** source, int* length);

        #endregion
		
		#region T
		
		[DllImport(Opengl32, EntryPoint = "glTexImage2D", SetLastError = true)]
		private static extern void _texImage2D(TEXTURE_TARGET target, uint level, uint internalformat, uint width, uint height, uint border, PIXEL_FORMAT format, Type type, void* data);

        #endregion

        #region U

        [DllImport(Opengl32, EntryPoint = "glUseProgram", SetLastError = true)]
        private static extern void _useProgram(uint program);

        [DllImport(Opengl32, EntryPoint = "glUniformMatrix4fv", SetLastError = true)]
        private static extern void _uniformMatrix4fv(int location, int count, bool transpose, float* value);

        #endregion

        #region V

        [DllImport(Opengl32, EntryPoint = "glVertexAttribPointer", SetLastError = true)]
		private static extern void _vertexAttribPointer(uint index, int size, Type type, bool normalized, int stride, void* pointer);
		
		#endregion
    }
}
