using System;
using CMDR;
using CMDR.Systems;
using OpenGL;

namespace CMDR
{
    internal static class Initializer
    {
        /// <summary>
        /// Initialize all CMDR systems.
        /// </summary>
        internal static void Initialize()
        {
            Native.Win.Start();
            GL.Build();
        }
    }
}
