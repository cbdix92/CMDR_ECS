using System;
using System.Runtime.InteropServices;

namespace OpenGL
{
    public static unsafe partial class GL
    {
		

		#region A
		
		[BuildInfo("glActiveTexture")]
		private static extern void _activeTexture(int texture);

        
        [BuildInfo("glAttachShader")]
		private static extern void _attachShader(uint program, uint shader);

        #endregion

        #region B
        
        [BuildInfo("glBindBuffer")]
		private static extern void _bindBuffer(int target, uint buffer);
		
		
		[BuildInfo("glBindTexture")]
		private static extern void _bindTexture(int target, uint texture);
		
		
		[BuildInfo("glBindTextures")]
		private static extern void _bindTextures(uint first, int count, void* textures);

        
		[BuildInfo("glBindBuffer")]
		private static extern void _bindVertexArray(uint array);

        
		[BuildInfo("glBufferData")]
		private static extern void _bufferData(int target, int size, void* data, int usage);
        #endregion

        #region C

        
		[BuildInfo("glClear")]
		private static extern void _clear(int mask);

        
		[BuildInfo("glClearColor")]
		private static extern void _clearColor(float red, float green, float blue, float alpha);

        
		[BuildInfo("glCompileShader")]
		private static extern void _compileShader(uint shader);

        
		[BuildInfo("glCreateProgram")]
		private static extern uint _createProgram();

        
		[BuildInfo("glCreateShader")]
		private static extern uint _createShader(int shaderType);



        #endregion
		
		#region D
		
		
		[BuildInfo("glDisableVertexAttribArray")]
		private static extern void _disableVertexAttribArray(uint index);

		
		[BuildInfo("glDrawArrays")]
		private static extern void _drawArrays(int mode, int first, int count);
		
		
		[BuildInfo("glDrawElements")]
		private static extern void _drawElements(int mode, int count, Type type, void* indices);
		
		#endregion
		
		#region E
		
		
		[BuildInfo("glEnableVertexAttribArray")]
		private static extern void _enableVertexAttribArray(uint index);

		#endregion

		#region G

		
		[BuildInfo("glGenBuffers")]
		private static extern void _genBuffers(int n, uint* buffers);

		
		[BuildInfo("glGenerateMipmap")]
		internal static void _generateMipMap(int target) { }

		
		[BuildInfo("glGenTextures")]
		private static extern void _genTextures(int n, uint* textures);
		
        
		[BuildInfo("glGenVertexArrays")]
		internal static void _genVertexArrays(uint n, uint* arrays) { }

		
		[BuildInfo("GetShaderInfoLog")]
		private static extern void _getShaderInfoLog(uint shader, int maxLength, int* length, byte* infoLog);

		
		[BuildInfo("getGetShaderiv")]
		private static extern void _getShaderiv(uint shader, int pname, int* param);

		
		[BuildInfo("glGetUniformfv")]
		private static extern void _getUniformfv(uint program, int location, float* param);
		
		
		[BuildInfo("glGetUniformLocation")]
		private static extern int _getUniformLocation(uint program, byte* name);
		
        #endregion

        #region L
        
		[BuildInfo("glLinkProgram")]
		private static extern void _linkProgram(uint program);

        #endregion

        #region S

        
		[BuildInfo("glShaderSource")]
		private static extern void _shaderSource(uint shader, int count, byte** source, int* length);

        #endregion
		
		#region T
		
		
		[BuildInfo("glTexImage2D")]
		private static extern void _texImage2D(int target, int level, int internalformat, int width, int height, int border, int format, int type, void* data);

		
		[BuildInfo("glTexParameteri")]
		private static extern void _texParameteri(int target, int pname, int param);
		
		
		[BuildInfo("glTexStorage2D")]
		internal static void _texStorage2D(int target, int levels, int internalFormat, int width, int height){ }

		
		[BuildInfo("glTexSubImage2D")]
		private static extern void _texSubImage2D(int target, int level, int xoffset, int yoffset, int width, int height, int format, int type, void* pixels);
		#endregion

		#region U

		
		[BuildInfo("glUniform3f")]
		private static extern void _uniform3f(int location, float v0, float v1, float v2);
		
		
		[BuildInfo("glUniform4f")]
		private static extern void _uniform4f(int location, float v0, float v1, float v2, float v3);

        
		[BuildInfo("glUniformMatrix4fv")]
		private static extern void _uniformMatrix4fv(int location, int count, bool transpose, float* value);
		
		
		[BuildInfo("glUseProgram")]
		private static extern void _useProgram(uint program);

        #endregion

        #region V

        
		[BuildInfo("glVertexAttribPointer")]
		private static extern void _vertexAttribPointer(uint index, int size, Type type, bool normalized, int stride, void* pointer);
		
		#endregion
    }
}
