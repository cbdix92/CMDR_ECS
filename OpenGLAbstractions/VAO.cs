using System;
using OpenGL;
public struct VAO
{
    uint ID;

    public VAO(float[] data, uint VBO)
    {
        ID = GL.GenVertexArray();
        GL.BindBuffer(BUFFER_BINDING_TARGET.ARRAY_BUFFER, VBO);

        GL.BindVertexArray(ID);
    }

    public void Draw(ShaderProgram shader)
    {
        
    }
}