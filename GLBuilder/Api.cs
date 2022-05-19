using System;
using System.Runtime.InteropServices;

namespace OpenGL
{
    public static unsafe partial class GL
    {


		#region A
		private delegate void _activeTextureDelegate(int texture);
		private static _activeTextureDelegate _activeTexture;


		private delegate void _attachShaderDelegate(uint program, uint shader);
		private static _attachShaderDelegate _attachShader;

		#endregion

		#region B
		private delegate void _bindBufferDelegate(int target, uint buffer);
		private static _bindBufferDelegate _bindBuffer;


		private delegate void _bindTextureDelegate(int target, uint texture);
		private static _bindTextureDelegate _bindTexture;


		private delegate void _bindTexturesDelegate(uint first, int count, void* textures);
		private static _bindTexturesDelegate _bindTextures;


		private delegate void _bindVertexArrayDelegate(uint array);
		private static _bindVertexArrayDelegate _bindVertexArray;


		private delegate void _bufferDataDelegate(int target, int size, void* data, int usage);
		private static _bufferDataDelegate _bufferData;
		#endregion

		#region C

		private delegate void _clearDelegate(int mask);
		private static _clearDelegate _clear;


		private delegate void _clearColorDelegate(float red, float green, float blue, float alpha);
		private static _clearColorDelegate _clearColor;


		private delegate void _compileShaderDelegate(uint shader);
		private static _compileShaderDelegate _compileShader;


		private delegate uint _createProgramDelegate();
		private static _createProgramDelegate _createProgram;


		private delegate uint _createShaderDelegate(int shaderType);
		private static _createShaderDelegate _createShader;



		#endregion

		#region D

		private delegate void _disableVertexAttribArrayDelegate(uint index);
		private static _disableVertexAttribArrayDelegate _disableVertexAttribArray;


		private delegate void _drawArraysDelegate(int mode, int first, int count);
		private static _drawArraysDelegate _drawArrays;


		private delegate void _drawElementsDelegate(int mode, int count, int type, void* indices);
		private static _drawElementsDelegate _drawElements;

		#endregion

		#region E

		private delegate void _enableDelegate(int cap);
		private static _enableDelegate _enable;

		private delegate void _enableVertexAttribArrayDelegate(uint index);
		private static _enableVertexAttribArrayDelegate _enableVertexAttribArray;

		#endregion

		#region G

		private delegate void _genBuffersDelegate(int n, uint* buffers);
		private static _genBuffersDelegate _genBuffers;


		private delegate void _generateMipMapDelegate(int target);
		private static _generateMipMapDelegate _generateMipMap;


		private delegate void _genTexturesDelegate(int n, uint* textures);
		private static _genTexturesDelegate _genTextures;


		private delegate void _genVertexArraysDelegate(int n, uint* arrays);
		private static _genVertexArraysDelegate _genVertexArrays;

		private delegate void _getActiveUniformNameDelegate(uint program, uint index, int bufSize, int* length, byte* unifromName);
		private static _getActiveUniformNameDelegate _getActiveUniformName;

		private delegate void _getProgramInfoLogDelegate(uint program, int maxLength, int* length, byte* infoLog);
		private static _getProgramInfoLogDelegate _getProgramInfoLog;


		private delegate void _getProgramivDelegate(uint program, int pname, int* param);
		private static _getProgramivDelegate _getProgramiv;


		private delegate void _getShaderInfoLogDelegate(uint shader, int maxLength, int* length, byte* infoLog);
		private static _getShaderInfoLogDelegate _getShaderInfoLog;


		private delegate void _getShaderivDelegate(uint shader, int pname, int* param);
		private static _getShaderivDelegate _getShaderiv;


		private delegate void _getUniformfvDelegate(uint program, int location, float* param);
		private static _getUniformfvDelegate _getUniformfv;

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate int _getUniformLocationDelegate(uint program, byte* name);
		private static _getUniformLocationDelegate _getUniformLocation;

		#endregion

		#region L

		private delegate void _linkProgramDelegate(uint program);
		private static _linkProgramDelegate _linkProgram;

		#endregion

		#region S

		private delegate void _shaderSourceDelegate(uint shader, int count, byte** source, int* length);
		private static _shaderSourceDelegate _shaderSource;

		#endregion

		#region T


		private delegate void _texImage2DDelegate(int target, int level, int internalformat, int width, int height, int border, int format, int type, void* data);
		private static _texImage2DDelegate _texImage2D;


		private delegate void _texParameteriDelegate(int target, int pname, int param);
		private static _texParameteriDelegate _texParameteri;


		private delegate void _texStorage2DDelegate(int target, int levels, int internalFormat, int width, int height);
		private static _texStorage2DDelegate _texStorage2D;


		private delegate void _texSubImage2DDelegate(int target, int level, int xoffset, int yoffset, int width, int height, int format, int type, void* pixels);
		private static _texSubImage2DDelegate _texSubImage2D;
		#endregion

		#region U
		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate void _uniform3fDelegate(int location, float v0, float v1, float v2);
		private static _uniform3fDelegate _uniform3f;

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate void _uniform4fDelegate(int location, float v0, float v1, float v2, float v3);
		private static _uniform4fDelegate _uniform4f;

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate void _uniformMatrix4fvDelegate(int location, int count, bool transpose, float* value);
		private static _uniformMatrix4fvDelegate _uniformMatrix4fv;

		[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
		private delegate void _useProgramDelegate(uint program);
		private static _useProgramDelegate _useProgram;

		#endregion

		#region V

		private delegate void _vertexAttribPointerDelegate(uint index, int size, int type, bool normalized, int stride, void* pointer);
		private static _vertexAttribPointerDelegate _vertexAttribPointer;
		
		#endregion
    }
}
