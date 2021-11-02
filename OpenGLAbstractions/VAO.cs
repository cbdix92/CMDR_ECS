using System;
using OpenGL;
public struct VAO
{
    uint ID;

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