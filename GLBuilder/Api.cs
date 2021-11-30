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
		[DllImport(Opengl32, EntryPoint = "glActiveTexture", SetLastError = true)]
		private static extern void _activeTexture(int texture);

        [DllImport(Opengl32, EntryPoint = "glAttachShader", SetLastError = true)]
        private static extern void _attachShader(uint program, uint shader);

        #endregion

        #region B
        [DllImport(Opengl32, EntryPoint = "glBindBuffer", SetLastError = true)]
        private static extern void _bindBuffer(int target, uint buffer);
		
		[DllImport(Opengl32, EntryPoint = "glBindTexture", SetLastError = true)]
		private static extern void _bindTexture(int target, uint texture);
		
		[DllImport(Opengl32, EntryPoint = "glBindTextures", SetLastError = true)]
		private static extern void _bindTextures(uint first, int count, void* textures);

        [DllImport(Opengl32, EntryPoint = "glBindBuffer", SetLastError = true)]
        private static extern void _bindVertexArray(uint array);

        [DllImport(Opengl32, EntryPoint = "glBufferData", SetLastError = true)]
        private static extern void _bufferData(int target, int size, void* data, int usage);
        #endregion

        #region C

        [DllImport(Opengl32, EntryPoint = "glClear", SetLastError = true)]
        private static extern void _clear(int mask);

        [DllImport(Opengl32, EntryPoint = "glClearColor", SetLastError = true)]
        private static extern void _clearColor(float red, float green, float blue, float alpha);

        [DllImport(Opengl32, EntryPoint = "glCompileShader", SetLastError = true)]
        private static extern void _compileShader(uint shader);

        [DllImport(Opengl32, EntryPoint = "glCreateProgram", SetLastError = true)]
        private static extern uint _createProgram();

        [DllImport(Opengl32, EntryPoint = "glCreateShader", SetLastError = true)]
        private static extern uint _createShader(int shaderType);



        #endregion
		
		#region D
		
		[DllImport(Opengl32, EntryPoint = "glDisableVertexAttribArray", SetLastError = true)]
		private static extern void _disableVertexAttribArray(uint index);

		[DllImport(Opengl32, EntryPoint = "glDrawArrays", SetLastError = true)]
		private static extern void _drawArrays(int mode, int first, int count);
		
		[DllImport(Opengl32, EntryPoint = "glDrawElements", SetLastError = true)]
		private static extern void _drawElements(int mode, int count, Type type, void* indices);
		
		#endregion
		
		#region E
		
		[DllImport(Opengl32, EntryPoint = "glEnableVertexAttribArray", SetLastError = true)]
		private static extern void _enableVertexAttribArray(uint index);

		#endregion

		#region G

		[DllImport(Opengl32, EntryPoint = "glGenBuffers", SetLastError = true)]
		private static extern void _genBuffers(int n, uint* buffers);

		[BuildInfo(Opengl32, "glGenerateMipmap")]
		internal static void _generateMipMap(int target) { }

		[DllImport(Opengl32, EntryPoint = "glGenTextures", SetLastError = true)]
		private static extern void _genTextures(int n, uint* textures);
		
        [BuildInfo(Opengl32, "glGenVertexArrays")]
        internal static void _genVertexArrays(uint n, uint* arrays) { }

		[DllImport(Opengl32, EntryPoint = "GetShaderInfoLog", SetLastError = true)]
		private static extern void _getShaderInfoLog(uint shader, int maxLength, int* length, byte* infoLog);

		[DllImport(Opengl32, EntryPoint = "getGetShaderiv", SetLastError = true)]
		private static extern void _getShaderiv(uint shader, int pname, int* param);

		[DllImport(Opengl32, EntryPoint = "glGetUniformfv", SetLastError = true)]
		private static extern void _getUniformfv(uint program, int location, float* param);
		
		[DllImport(Opengl32, EntryPoint = "glGetUniformLocation", SetLastError = true)]	
		private static extern int _getUniformLocation(uint program, byte* name);
		
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
		private static extern void _texImage2D(int target, int level, int internalformat, int width, int height, int border, int format, int type, void* data);

        #endregion

        #region U

		[DllImport(Opengl32, EntryPoint = "glUniform3f", SetLastError = true)]
		private static extern void _uniform3f(int location, float v0, float v1, float v2);
		
		[DllImport(Opengl32, EntryPoint = "glUniform4f", SetLastError = true)]
        private static extern void _uniform4f(int location, float v0, float v1, float v2, float v3);

        [DllImport(Opengl32, EntryPoint = "glUniformMatrix4fv", SetLastError = true)]
        private static extern void _uniformMatrix4fv(int location, int count, bool transpose, float* value);
		
		[DllImport(Opengl32, EntryPoint = "glUseProgram", SetLastError = true)]
        private static extern void _useProgram(uint program);

        #endregion

        #region V

        [DllImport(Opengl32, EntryPoint = "glVertexAttribPointer", SetLastError = true)]
		private static extern void _vertexAttribPointer(uint index, int size, Type type, bool normalized, int stride, void* pointer);
		
		#endregion
    }
}
