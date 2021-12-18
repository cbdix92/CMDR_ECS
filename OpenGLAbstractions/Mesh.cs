using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenGL;

namespace CMDR
{
    public struct Mesh
    {
        public uint VAO { get; private set; }
        public uint VBO { get; private set; }
        public uint EBO { get; private set; }

        private float[] _vertices;
        private float[] _normals;
        private int[] _indices;


        public Mesh(uint vao, uint vbo, uint ebo, float[] verts, float[] normals, int[] indices)
        {
            VAO = vao;
            VBO = vbo;
            EBO = ebo;

            _vertices = verts;
            _normals = normals;
            _indices = indices;

            VBO = GL.GenBuffer();
            EBO = GL.GenBuffer();

            GL.BindVertexArray(VAO);

            GL.BindBuffer(GL.ARRAY_BUFFER, VBO);

            GL.BufferData(GL.ARRAY_BUFFER, sizeof(float) * _vertices.Length, _vertices, GL.STATIC_DRAW);

            GL.BindBuffer(GL.ELEMENT_ARRAY_BUFFER, EBO);
            GL.BufferData(GL.ELEMENT_ARRAY_BUFFER, sizeof(int) * _indices.Length, _indices, GL.STATIC_DRAW);


        }
    }
}
