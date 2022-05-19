using System.Runtime.InteropServices;
using GLFW;

namespace OpenGL
{
    public static partial class GL
    {
        internal static void Build()
        {
            #region A
            _activeTexture = Marshal.GetDelegateForFunctionPointer<_activeTextureDelegate>(Glfw.GetProcAddress("glActiveTexture"));
            _attachShader = Marshal.GetDelegateForFunctionPointer<_attachShaderDelegate>(Glfw.GetProcAddress("glAttachShader"));
            #endregion

            #region B
            _bindBuffer = Marshal.GetDelegateForFunctionPointer<_bindBufferDelegate>(Glfw.GetProcAddress("glBindBuffer"));
            _bindTexture = Marshal.GetDelegateForFunctionPointer<_bindTextureDelegate>(Glfw.GetProcAddress("glBindTexture"));
            _bindTextures = Marshal.GetDelegateForFunctionPointer<_bindTexturesDelegate>(Glfw.GetProcAddress("glBindTextures"));
            _bindVertexArray = Marshal.GetDelegateForFunctionPointer<_bindVertexArrayDelegate>(Glfw.GetProcAddress("glBindVertexArray"));
            _bufferData = Marshal.GetDelegateForFunctionPointer<_bufferDataDelegate>(Glfw.GetProcAddress("glBufferData"));
            #endregion

            #region C
            _clear = Marshal.GetDelegateForFunctionPointer<_clearDelegate>(Glfw.GetProcAddress("glClear"));
            _clearColor = Marshal.GetDelegateForFunctionPointer<_clearColorDelegate>(Glfw.GetProcAddress("glClearColor"));
            _compileShader = Marshal.GetDelegateForFunctionPointer<_compileShaderDelegate>(Glfw.GetProcAddress("glCompileShader"));
            _createProgram = Marshal.GetDelegateForFunctionPointer<_createProgramDelegate>(Glfw.GetProcAddress("glCreateProgram"));
            _createShader = Marshal.GetDelegateForFunctionPointer<_createShaderDelegate>(Glfw.GetProcAddress("glCreateShader"));
            #endregion

            #region D
            _disableVertexAttribArray = Marshal.GetDelegateForFunctionPointer<_disableVertexAttribArrayDelegate>(Glfw.GetProcAddress("glDisableVertexAttribArray"));
            _drawArrays = Marshal.GetDelegateForFunctionPointer<_drawArraysDelegate>(Glfw.GetProcAddress("glDrawArrays"));
            _drawElements = Marshal.GetDelegateForFunctionPointer<_drawElementsDelegate>(Glfw.GetProcAddress("glDrawElements"));
            #endregion

            #region E

            _enable = Marshal.GetDelegateForFunctionPointer<_enableDelegate>(Glfw.GetProcAddress("glEnable"));
            _enableVertexAttribArray = Marshal.GetDelegateForFunctionPointer<_enableVertexAttribArrayDelegate>(Glfw.GetProcAddress("glEnableVertexAttribArray"));
            
            #endregion

            #region G
            _genBuffers = Marshal.GetDelegateForFunctionPointer<_genBuffersDelegate>(Glfw.GetProcAddress("glGenBuffers"));
            _generateMipMap = Marshal.GetDelegateForFunctionPointer<_generateMipMapDelegate>(Glfw.GetProcAddress("glGenerateMipmap"));
            _genTextures = Marshal.GetDelegateForFunctionPointer<_genTexturesDelegate>(Glfw.GetProcAddress("glGenTextures"));
            _genVertexArrays = Marshal.GetDelegateForFunctionPointer<_genVertexArraysDelegate>(Glfw.GetProcAddress("glGenVertexArrays"));
            _getActiveUniformName = Marshal.GetDelegateForFunctionPointer<_getActiveUniformNameDelegate>(Glfw.GetProcAddress("glGetActiveUniformName"));
            _getProgramInfoLog = Marshal.GetDelegateForFunctionPointer<_getProgramInfoLogDelegate>(Glfw.GetProcAddress("glGetProgramInfoLog"));
            _getProgramiv = Marshal.GetDelegateForFunctionPointer<_getProgramivDelegate>(Glfw.GetProcAddress("glGetProgramiv"));
            _getShaderInfoLog = Marshal.GetDelegateForFunctionPointer<_getShaderInfoLogDelegate>(Glfw.GetProcAddress("glGetShaderInfoLog"));
            _getShaderiv = Marshal.GetDelegateForFunctionPointer<_getShaderivDelegate>(Glfw.GetProcAddress("glGetShaderiv"));
            _getUniformfv = Marshal.GetDelegateForFunctionPointer<_getUniformfvDelegate>(Glfw.GetProcAddress("glGetUniformfv"));
            _getUniformLocation = Marshal.GetDelegateForFunctionPointer<_getUniformLocationDelegate>(Glfw.GetProcAddress("glGetUniformLocation"));
            #endregion

            #region L
            _linkProgram = Marshal.GetDelegateForFunctionPointer<_linkProgramDelegate>(Glfw.GetProcAddress("glLinkProgram"));
            #endregion

            #region S
            _shaderSource = Marshal.GetDelegateForFunctionPointer<_shaderSourceDelegate>(Glfw.GetProcAddress("glShaderSource"));
            #endregion

            #region T
            _texImage2D = Marshal.GetDelegateForFunctionPointer<_texImage2DDelegate>(Glfw.GetProcAddress("glTexImage2D"));
            _texParameteri = Marshal.GetDelegateForFunctionPointer<_texParameteriDelegate>(Glfw.GetProcAddress("glTexParameteri"));
            _texStorage2D = Marshal.GetDelegateForFunctionPointer<_texStorage2DDelegate>(Glfw.GetProcAddress("glTexStorage2D"));
            _texSubImage2D = Marshal.GetDelegateForFunctionPointer<_texSubImage2DDelegate>(Glfw.GetProcAddress("glTexSubImage2D"));
            #endregion

            #region U
            _uniform3f = Marshal.GetDelegateForFunctionPointer<_uniform3fDelegate>(Glfw.GetProcAddress("glUniform3f"));
            _uniform4f = Marshal.GetDelegateForFunctionPointer<_uniform4fDelegate>(Glfw.GetProcAddress("glUniform4f"));
            _uniformMatrix4fv = Marshal.GetDelegateForFunctionPointer<_uniformMatrix4fvDelegate>(Glfw.GetProcAddress("glUniformMatrix4fv"));
            _useProgram = Marshal.GetDelegateForFunctionPointer<_useProgramDelegate>(Glfw.GetProcAddress("glUseProgram"));
            #endregion

            #region V
            _vertexAttribPointer = Marshal.GetDelegateForFunctionPointer<_vertexAttribPointerDelegate>(Glfw.GetProcAddress("glVertexAttribPointer"));
            #endregion


        }
    }
}
