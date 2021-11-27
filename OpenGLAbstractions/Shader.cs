using System;
using System.IO;
using OpenGL;

public struct Shader
{
    uint ID;
    uint VertID;
    uint FragID;

    public Shader(string pathVert, string pathFrag)
    {

        // Shader IDs
        VertID = GL.CreateShader(SHADER_TYPE.GL_VERTEX_SHADER);
        FragID = GL.CreateShader(SHADER_TYPE.GL_FRAGMENT_SHADER);

        // Read shader source code
        var vertRead = File.ReadAllText(pathVert);
        var fragRead = File.ReadAllText(pathFrag);

        //temp debug
        ID = 0;
        VertID = 0;
        FragID = 0;

        GL.ShaderSource(VertID, vertRead);
        GL.CompileShader(VertID);

        GL.ShaderSource(FragID, fragRead);
        GL.CompileShader(FragID);

        ID = GL.CreateProgram();
        GL.AttachShader(ID, VertID);
        GL.AttachShader(ID, FragID);
        GL.LinkProgram(ID);

        //TODO Check for GLSL compile errors here
        // ...


    }

    public void Use()
    {
        GL.UseProgram(ID);
    }
}