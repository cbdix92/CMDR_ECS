using System;
using System.Runtime.InteropServices;

namespace OpenGL
{
    public static class GL
    {

        public delegate IntPtr GetPtr(string name);

        #region PUBLIC_METHODS

        public static void ClearColor(float r, float g, float b, float a) => _ClearColor(r,g,b,a);
        
        #endregion

        #region DELEGATES

        private delegate void ClearColorDelegate(float r, float g, float b, float a);

        #endregion

        #region PRIVATE_METHODS

        private static ClearColorDelegate _ClearColor;
        
        #endregion


        public static void Import(GetPtr loader)
        {
            _ClearColor = Marshal.GetDelegateForFunctionPointer<ClearColorDelegate>(loader.Invoke("glClearColor"));
        }

    }
}
