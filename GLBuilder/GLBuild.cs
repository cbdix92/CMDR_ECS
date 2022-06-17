using System.Runtime.InteropServices;
using CMDR.Native;

namespace OpenGL
{
    public static partial class GL
    {
        private static void BuildMethods()
        {
            #region A
            _activeTexture = Marshal.GetDelegateForFunctionPointer<_activeTextureDelegate>(Win.wglGetProcAddress("glActiveTexture"));
            _attachShader = Marshal.GetDelegateForFunctionPointer<_attachShaderDelegate>(Win.wglGetProcAddress("glAttachShader"));
            #endregion

            #region B
            _bindBuffer = Marshal.GetDelegateForFunctionPointer<_bindBufferDelegate>(Win.wglGetProcAddress("glBindBuffer"));
            _bindTexture = Marshal.GetDelegateForFunctionPointer<_bindTextureDelegate>(Win.wglGetProcAddress("glBindTexture"));
            _bindTextures = Marshal.GetDelegateForFunctionPointer<_bindTexturesDelegate>(Win.wglGetProcAddress("glBindTextures"));
            _bindVertexArray = Marshal.GetDelegateForFunctionPointer<_bindVertexArrayDelegate>(Win.wglGetProcAddress("glBindVertexArray"));
            _bufferData = Marshal.GetDelegateForFunctionPointer<_bufferDataDelegate>(Win.wglGetProcAddress("glBufferData"));
            #endregion

            #region C
            _clear = Marshal.GetDelegateForFunctionPointer<_clearDelegate>(Win.wglGetProcAddress("glClear"));
            _clearColor = Marshal.GetDelegateForFunctionPointer<_clearColorDelegate>(Win.wglGetProcAddress("glClearColor"));
            _compileShader = Marshal.GetDelegateForFunctionPointer<_compileShaderDelegate>(Win.wglGetProcAddress("glCompileShader"));
            _createProgram = Marshal.GetDelegateForFunctionPointer<_createProgramDelegate>(Win.wglGetProcAddress("glCreateProgram"));
            _createShader = Marshal.GetDelegateForFunctionPointer<_createShaderDelegate>(Win.wglGetProcAddress("glCreateShader"));
            #endregion

            #region D
            _disableVertexAttribArray = Marshal.GetDelegateForFunctionPointer<_disableVertexAttribArrayDelegate>(Win.wglGetProcAddress("glDisableVertexAttribArray"));
            _drawArrays = Marshal.GetDelegateForFunctionPointer<_drawArraysDelegate>(Win.wglGetProcAddress("glDrawArrays"));
            _drawElements = Marshal.GetDelegateForFunctionPointer<_drawElementsDelegate>(Win.wglGetProcAddress("glDrawElements"));
            #endregion

            #region E

            _enable = Marshal.GetDelegateForFunctionPointer<_enableDelegate>(Win.wglGetProcAddress("glEnable"));
            _enableVertexAttribArray = Marshal.GetDelegateForFunctionPointer<_enableVertexAttribArrayDelegate>(Win.wglGetProcAddress("glEnableVertexAttribArray"));
            
            #endregion

            #region G
            _genBuffers = Marshal.GetDelegateForFunctionPointer<_genBuffersDelegate>(Win.wglGetProcAddress("glGenBuffers"));
            _generateMipMap = Marshal.GetDelegateForFunctionPointer<_generateMipMapDelegate>(Win.wglGetProcAddress("glGenerateMipmap"));
            _genTextures = Marshal.GetDelegateForFunctionPointer<_genTexturesDelegate>(Win.wglGetProcAddress("glGenTextures"));
            _genVertexArrays = Marshal.GetDelegateForFunctionPointer<_genVertexArraysDelegate>(Win.wglGetProcAddress("glGenVertexArrays"));
            _getActiveUniformName = Marshal.GetDelegateForFunctionPointer<_getActiveUniformNameDelegate>(Win.wglGetProcAddress("glGetActiveUniformName"));
            _getProgramInfoLog = Marshal.GetDelegateForFunctionPointer<_getProgramInfoLogDelegate>(Win.wglGetProcAddress("glGetProgramInfoLog"));
            _getProgramiv = Marshal.GetDelegateForFunctionPointer<_getProgramivDelegate>(Win.wglGetProcAddress("glGetProgramiv"));
            _getShaderInfoLog = Marshal.GetDelegateForFunctionPointer<_getShaderInfoLogDelegate>(Win.wglGetProcAddress("glGetShaderInfoLog"));
            _getShaderiv = Marshal.GetDelegateForFunctionPointer<_getShaderivDelegate>(Win.wglGetProcAddress("glGetShaderiv"));
            _getUniformfv = Marshal.GetDelegateForFunctionPointer<_getUniformfvDelegate>(Win.wglGetProcAddress("glGetUniformfv"));
            _getUniformLocation = Marshal.GetDelegateForFunctionPointer<_getUniformLocationDelegate>(Win.wglGetProcAddress("glGetUniformLocation"));
            #endregion

            #region L
            _linkProgram = Marshal.GetDelegateForFunctionPointer<_linkProgramDelegate>(Win.wglGetProcAddress("glLinkProgram"));
            #endregion

            #region S
            _shaderSource = Marshal.GetDelegateForFunctionPointer<_shaderSourceDelegate>(Win.wglGetProcAddress("glShaderSource"));
            #endregion

            #region T
            _texImage2D = Marshal.GetDelegateForFunctionPointer<_texImage2DDelegate>(Win.wglGetProcAddress("glTexImage2D"));
            _texParameteri = Marshal.GetDelegateForFunctionPointer<_texParameteriDelegate>(Win.wglGetProcAddress("glTexParameteri"));
            _texStorage2D = Marshal.GetDelegateForFunctionPointer<_texStorage2DDelegate>(Win.wglGetProcAddress("glTexStorage2D"));
            _texSubImage2D = Marshal.GetDelegateForFunctionPointer<_texSubImage2DDelegate>(Win.wglGetProcAddress("glTexSubImage2D"));
            #endregion

            #region U
            _uniform3f = Marshal.GetDelegateForFunctionPointer<_uniform3fDelegate>(Win.wglGetProcAddress("glUniform3f"));
            _uniform4f = Marshal.GetDelegateForFunctionPointer<_uniform4fDelegate>(Win.wglGetProcAddress("glUniform4f"));
            _uniformMatrix4fv = Marshal.GetDelegateForFunctionPointer<_uniformMatrix4fvDelegate>(Win.wglGetProcAddress("glUniformMatrix4fv"));
            _useProgram = Marshal.GetDelegateForFunctionPointer<_useProgramDelegate>(Win.wglGetProcAddress("glUseProgram"));
            #endregion

            #region V
            _vertexAttribPointer = Marshal.GetDelegateForFunctionPointer<_vertexAttribPointerDelegate>(Win.wglGetProcAddress("glVertexAttribPointer"));
            #endregion


        }
    }
}
