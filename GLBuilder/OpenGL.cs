using System;
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
            fixed(uint* ids = &buffers[0])
            {
                _genBuffers(n, ids);
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
            _shaderSource(shader, source.Length, source, 0);
        }

        #endregion
    }
}
