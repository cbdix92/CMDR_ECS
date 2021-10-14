using System;
using System.IO;
using OpenTK.Graphics.OpenGL4;

public struct ShaderProgram
{
    int ID;
    int VertID;
    int FragID;

    public ShaderProgram(string pathVert, string pathFrag)
    {

        // Shader IDs
        VertID = GL.CreateShader(ShaderType.VertexShader);
        FragID = Gl.CreateShader(ShaderType.FragmentShader);

        // Read shader source code
        var vertRead = File.ReadAllText(pathVert);
        var fragRead = File.ReadAllText(pathFrag);

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
}