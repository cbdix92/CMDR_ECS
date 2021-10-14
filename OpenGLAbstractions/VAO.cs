using System;
using OpenTK.Graphics.OpenGL4;

public struct VAO
{
    int ID;

    public VAO(float[] data, int VBO)
    {
        ID = GL.GenVertexArray();
        GL.BindArray(BufferTarget.ArrayBuffer, VBO);

        GL.BindVertexArray(ID);
    }

    public void Draw(ShaderProgram shader)
    {
        
    }
}