using System;
using System.Runtime.InteropServices;
using OpenGL;

namespace CMDR
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Mesh
    {
        public uint VAO { get; private set; }
        public uint VBO { get; private set; }

        public readonly int NumVertices;


        public Mesh(uint vao, uint vbo, int numVertices)
        {
            VAO = vao;
            VBO = vbo;
            NumVertices = numVertices;
        }
    }
}
