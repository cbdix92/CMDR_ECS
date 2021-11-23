using System;
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
			fixed(uint* id = &texture[0])
			{
				_bindTextures(0, textures.Length, id);
			}
		}
		
		public static void BindVertexArray(uint array) { _bindVertexArray(array); }

        public static void BufferData(BUFFER_BINDING_TARGET target, int size, void* data, USAGE usage) { _bufferData(target, size, data, usage); }
        #endregion

        #region C
        public static void Clear(BUFFER_MASK mask) { _clear(mask); }

        public static void ClearColor(Color color) { _clearColor(color.R, color.G, color.B, color.A); }

        public static void CompileShader(uint shader) { _compileShader(shader); }

        public static uint CreateProgram() { return _createProgram(); }

        public static uint CreateShader(SHADER_TYPE shaderType) { return _createShader(shaderType); }

        #endregion
		
		#region D
		
		public static void DisableVertexAttribArray(uint index) { _disabelVertexAttribArray(index); }
		
		public static void DrawElements(Mode mode, uint count, float[] indices)
		{
			fixed(uint* id = &idices[0])
			{
				_drawElements(mode, count, typeof(indices), id);
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

        public static uint[] GenBuffers(uint n)
        {
            uint[] buffers = new uint[n];
            fixed(uint* ids = &buffers[0])
            {
                _genBuffers(n, ids);
            }
            return buffers;
        }
		
		public static uint GenTexture()
		{
			uint id;
			_genTextures(1, &id);
			return id;
		}

		public static uint[] GenTextures(uint n)
		{
			uint[] buffers = new uint[n];
			fixed(uint* ids = &buffers[0])
			{
				_genTextures(n, ids);
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
			
			fixed(uint* id = &data[0])
			{
				_texImage2D(target, level, internalformat, width, height, 0, format, typeof(float), id);
			}
		}
		
		#endregion
		
		#region V
		
		public static void VertexAttribPointer(uint index, int size, Type type, bool normalized, void* pointer) 
		{
			_vertexAttribPointer(index, size, type, normalized, size * sizeof(float), pointer);
		}
		
		#endregion
    }
}
