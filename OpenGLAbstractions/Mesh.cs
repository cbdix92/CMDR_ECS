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
        public readonly bool UV;
        public readonly bool Normals;


        public Mesh(uint vao, uint vbo, int numVertices, bool uv, bool normals)
        {
            VAO = vao;
            VBO = vbo;
            NumVertices = numVertices;
            UV = uv;
            Normals = normals;
        }
    }
}
