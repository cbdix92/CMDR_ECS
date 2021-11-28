﻿using System;
using System.Text;
using CMDR;

namespace OpenGL
{
    public static unsafe partial class GL
    {
        public static bool Init()
        {
            Log.Init();
            return Builder.Start();
        }

        #region A

        public static void AttachShader(uint program, uint shader) { _attachShader(program, shader); }

        #endregion

        #region B
        public static void BindBuffer(BUFFER_BINDING_TARGET target, uint buffer) { _bindBuffer(target, buffer); }

        public static void BindTexture(BUFFER_BINDING_TARGET target, uint texture){ _bindTexture(target, texture); }
		
        public static void BindTextures(uint[] textures)
		{
			fixed(uint* id = &textures[0])
			{
				_bindTextures(0, textures.Length, id);
			}
		}
		
		public static void BindVertexArray(uint array) { _bindVertexArray(array); }

        public static void BufferData(BUFFER_BINDING_TARGET target, int size, float[] data, USAGE usage) 
		{
			fixed(float* ptr = &data[0])
            {
				_bufferData(target, size, ptr, usage);
            }
		
		}
        #endregion

        #region C
        public static void Clear(BUFFER_MASK mask) { _clear(mask); }

        public static void ClearColor(Color color) { _clearColor(color.R, color.G, color.B, color.A); }

        public static void CompileShader(uint shader) { _compileShader(shader); }

        public static uint CreateProgram() { return _createProgram(); }

        public static uint CreateShader(SHADER_TYPE shaderType) { return _createShader(shaderType); }

        #endregion
		
		#region D
		
		public static void DisableVertexAttribArray(uint index) { _disableVertexAttribArray(index); }


		public static void DrawArrays(MODE mode, int first, int count) { _drawArrays(mode, first, count); }
		
		public static void DrawElements(MODE mode, int count, float[] indices)
		{
			fixed(float* id = &indices[0])
			{
				_drawElements(mode, count, typeof(float), id);
			}
		}
		
		#endregion
		
		#region E
		
		public static void EnableVertexAttribArray(uint index) { _enableVertexAttribArray(index); }
		
		#endregion

        #region G
        public static uint GenBuffer()
        {
            uint id;
            _genBuffers(1, &id);
            return id;
        }

        public static uint[] GenBuffers(int n)
        {
            uint[] buffers = new uint[n];
            fixed(uint* ptr = &buffers[0])
            {
                _genBuffers(n, ptr);
            }
            return buffers;
        }
		
		public static uint GenTexture()
		{
			uint id;
			_genTextures(1, &id);
			return id;
		}

		public static uint[] GenTextures(int n)
		{
			uint[] buffers = new uint[n];
			fixed(uint* ptr = &buffers[0])
			{
				_genTextures(n, ptr);
			}
			return buffers;
		}
        public static uint GenVertexArray()
        {
            uint id;
            _genVertexArrays(1, &id);
            return id;
        }
        public static uint[] GenVertexArrays(uint n)
        {
            uint[] buffers = new uint[n];
            fixed(uint* id = &buffers[0])
            {
                _genVertexArrays(n, id);
            }
            return buffers;
        }
		
		public static void GetShaderiv(uint shader, PNAME pname, out object obj)
		{
			_getShaderiv(shader, pname, &obj);
		}
		
		public static string GetShaderInfoLog(uint shader)
		{
			byte[] buffer = new byte[1024];
			fixed(byte* ptr = &buffer[0])
			{
				_getShaderInfoLog(shader, buffer.Length, null, ptr);
			}
			return Encoding.UTF8.GetString(buffer, 0, buffer.Length);
		}
		
		public static void GetUniformfv(uint program, int location, out float param)
		{
			fixed(float* result = &param)
			{
				_getUniformfv(program, location, result);
			}
		}
		
		public static int GetUniformLocation(uint program, string name)
		{
			byte[] bytes = Encoding.ASCII.GetBytes(name);
			fixed(byte* ptr = &bytes[0])
			{
				return _getUniformLocation(program, ptr);
			}
		}
        #endregion

        #region L

        public static void LinkProgram(uint program) { _linkProgram(program); }

        #endregion

        #region S

        public static void ShaderSource(uint shader, string source)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(source);
            fixed (byte* p = &buffer[0])
            {
                byte*[] sources = new byte*[] { p };
                fixed (byte** s = &sources[0])
                {
                    int length = buffer.Length;
                    _shaderSource(shader, 1, s, &length);
                }
            }
        }

        #endregion
		
		#region T
		
		public static void TexImage2D(TEXTURE_TARGET target, uint level, uint internalformat, uint width, uint height, PIXEL_FORMAT format, float[] data)
		{
			if (target == TEXTURE_TARGET.GL_TEXTURE_RECTANGLE || target == TEXTURE_TARGET.GL_PROXY_TEXTURE_RECTANGLE)
				level = 0;
			
			fixed(float* ptr = &data[0])
			{
				_texImage2D(target, level, internalformat, width, height, 0, format, typeof(float), ptr);
			}
		}

        #endregion

        #region U

		public static void Uniform3f(int location, Vector3 vec)
		{
			_uniform3f(location, vec[0], vec[1], vec[2]);
		}
		
		public static void Uniform4f(int location, Vector4 vec)
		{
			_uniform4f(location, vec[0], vec[1], vec[2], vec[3]);
		}
		
        public static void UniformMatrix4fv(int location, int count, bool transpose, Matrix4 matrix)
        {
            fixed(float* ptr = &matrix.ToArray()[0])
            {
                _uniformMatrix4fv(location, count, transpose, ptr);
            }
        }
		
		public static void UseProgram(uint program) { _useProgram(program); }
        
		#endregion

        #region V

        public static void VertexAttribPointer(uint index, int size, Type type, bool normalized, void* pointer) 
		{
			_vertexAttribPointer(index, size, type, normalized, size * sizeof(float), pointer);
		}
		
		#endregion
    }
}
