using System.Text;
using CMDR;

namespace OpenGL
{
    public static unsafe partial class GL
    {
		public static void Build()
        {
			BuildMethods();
			ShaderManager.LoadDefaults();
			ClearColor(Color.BabyBlue);
        }

        #region A

		public static void ActiveTexture(int texture) { _activeTexture(texture); }
        public static void AttachShader(uint program, uint shader) { _attachShader(program, shader); }

        #endregion

        #region B
        public static void BindBuffer(int target, uint buffer) { _bindBuffer(target, buffer); }

        public static void BindTexture(int target, uint texture){ _bindTexture(target, texture); }
		
        public static void BindTextures(uint[] textures)
		{
			fixed(uint* id = &textures[0])
			{
				_bindTextures(0, textures.Length, id);
			}
		}
		
		public static void BindVertexArray(uint array) { _bindVertexArray(array); }

        public static void BufferData(int target, int size, float[] data, int usage) 
		{
			fixed(float* ptr = &data[0])
            {
				_bufferData(target, size, ptr, usage);
            }
		
		}

		public static void BufferData(int target, int size, int[] data, int usage)
        {
			fixed (int* ptr = &data[0])
			{
				_bufferData(target, size, ptr, usage);
			}
		}

		public static void BufferData(int target, int size, uint[] data, int usage)
		{
			fixed (uint* ptr = &data[0])
			{
				_bufferData(target, size, ptr, usage);
			}
		}

		public static void BufferData(int target, int size, byte[] data, int usage)
        {
			fixed (byte* ptr = &data[0])
            {
				_bufferData(target, size, ptr, usage);
            }
        }
        #endregion

        #region C
        public static void Clear(int mask) { _clear(mask); }

        public static void ClearColor(Color color) { _clearColor(color.R, color.G, color.B, color.A); }

        public static void CompileShader(uint shader) { _compileShader(shader); }

        public static uint CreateProgram() { return _createProgram(); }

        public static uint CreateShader(int shaderType) { return _createShader(shaderType); }

        #endregion
		
		#region D
		
		public static void DisableVertexAttribArray(uint index) { _disableVertexAttribArray(index); }


		public static void DrawArrays(int mode, int first, int count) { _drawArrays(mode, first, count); }
		
		public static void DrawElements(int mode, int count, void* indices)
		{
			_drawElements(mode, count, GL.UNSIGNED_INT, indices);
		}

		#endregion

		#region E

		public static void Enable(int cap) { _enable(cap); }
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
		
		public static void GenerateMipMap(int target) { _generateMipMap(target); }
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
        public static uint[] GenVertexArrays(int n)
        {
            uint[] buffers = new uint[n];
            fixed(uint* id = &buffers[0])
            {
                _genVertexArrays(n, id);
            }
            return buffers;
        }
		
		public static string GetActiveUniformName(uint program, uint index, int bufSize, int length)
        {
			byte[] buffer = new byte[bufSize];
			fixed(byte* ptr = &buffer[0])
            {
				_getActiveUniformName(program, index, bufSize, &length, ptr);
            }
			return Encoding.UTF8.GetString(buffer, 0, buffer.Length);
        }
		public static string GetProgramInfoLog(uint program)
        {
			byte[] buffer = new byte[1024];
			fixed(byte* ptr = &buffer[0])
            {
				_getProgramInfoLog(program, buffer.Length, null, ptr);
            }
			return Encoding.UTF8.GetString(buffer, 0, buffer.Length);
        }

		public static void GetProgramiv(uint program, int pname, out int output)
        {
			fixed(int* ptr = &output)
            {
				_getProgramiv(program, pname, ptr);
            }
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
		public static void GetShaderiv(uint shader, int pname, out int output)
		{
			fixed (int* ptr = &output)
			{
				_getShaderiv(shader, pname, ptr);
			}
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
		
		public static void TexImage2D(int target, int level, int internalformat, int width, int height, int format, float[] data)
		{
			if (target == TEXTURE_RECTANGLE || target == PROXY_TEXTURE_RECTANGLE)
				level = 0;
			
			fixed(float* ptr = &data[0])
			{
				_texImage2D(target, level, internalformat, width, height, 0, format, GL.FLOAT, ptr);
			}
		}

		public static void TexParameteri(int target, int pname, int param) { _texParameteri(target, pname, param); }

		public static void TexStorage2D(int target, int levels, int internalFormat, int width, int height) { _texStorage2D(target, levels, internalFormat, width, height);}
        
		public static void TexSubImage2D(int target, int level, int xoffset, int yoffset, int width, int height, int format, int type, float[] pixels)
		{
			fixed(float* ptr = &pixels[0])
			{
				_texSubImage2D(target, level, xoffset, yoffset, width, height, format, type, ptr);
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
			_uniform4f(location, vec.X, vec.Y, vec.Z, vec.W);
		}

		public static void Uniform4f(int location, Color color)
        {
			_uniform4f(location, color.R, color.G, color.B, color.A);
        }
		
        public static void UniformMatrix4fv(int location, bool transpose, Matrix4 matrix)
        {
			float[] buffer = matrix.ToArrayColumnMajor();

            fixed(float* ptr = &buffer[0])
            {
				_uniformMatrix4fv(location, 1, transpose, ptr);
            }
        }
		
		public static void UseProgram(uint program) { _useProgram(program); }
        
		#endregion

        #region V

        public static void VertexAttribPointer(uint index, int size, int type, bool normalized, int stride, void* pointer) 
		{
			_vertexAttribPointer(index, size, type, normalized, stride, pointer);
		}
		
		#endregion
    }
}
