using System;
using System.IO;
using OpenGL;
using System.Collections.Generic;


namespace CMDR
{

	public static class ShaderLoader
    {

		public static Dictionary<int, Shader> Shaders = new Dictionary<int, Shader>();
		private static readonly Dictionary<int, uint> _vertIDs = new Dictionary<int, uint>();
		private static readonly Dictionary<int, uint> _fragIDs = new Dictionary<int, uint>();
		
		public static Shader Load(string vertPath, string fragPath)
        {
			int vertKey = vertPath.GetHashCode();
			int fragKey = fragPath.GetHashCode();
			int programKey = string.Concat(vertPath,fragPath).GetHashCode();
			
			if(Shaders.ContainsKey(programKey))
            {
				return Shaders[programKey];
            }
			
			uint vertID = _vertIDs.ContainsKey(vertKey) ? _vertIDs[vertKey] : ShaderCache(GL.VERTEX_SHADER, vertKey, vertPath);
			uint fragID = _fragIDs.ContainsKey(fragKey) ? _fragIDs[fragKey] : ShaderCache(GL.FRAGMENT_SHADER, fragKey, fragPath);
			
			return ProgramCache(programKey, vertID, fragID);
        }
		
		private static uint ShaderCache(int shaderType, int key, string path)
		{
			uint id = GL.CreateShader(shaderType);
			
			var read = File.ReadAllText(path);
			
			GL.ShaderSource(id, read);
			GL.CompileShader(id);
			CheckCompileErrors(id);
			
			switch(shaderType)
			{
				case GL.VERTEX_SHADER:
					_vertIDs.Add(key, id);
					break;
				
				case GL.FRAGMENT_SHADER:
					_fragIDs.Add(key, id);
					break;
			}
			return id;
		}
		
		private static Shader ProgramCache(int key, uint vertID, uint fragID)
		{
			uint id = GL.CreateProgram();
			GL.AttachShader(id, vertID);
			GL.AttachShader(id, fragID);
			GL.LinkProgram(id);
			CheckLinkErrors(id);
			
			Shader shader = new Shader(id, vertID, fragID);
			Shaders.Add(key, shader);
			return shader;
		}
		
		private static void CheckCompileErrors(uint id)
		{
			GL.GetShaderiv(id, GL.COMPILE_STATUS, out int compiled);

			if (compiled == 0)
			{
				throw new Exception(GL.GetShaderInfoLog(id));
			}
		}
		
		private static void CheckLinkErrors(uint id)
		{
			GL.GetShaderiv(id, GL.LINK_STATUS, out int linked);
			if (linked == 0)
			{
				throw new Exception(GL.GetShaderInfoLog(id));
			}
		}
    }
	
	public struct Shader
	{
		public readonly uint ID;
		public readonly uint VertID;
		public readonly uint FragID;


		internal Shader(uint id, uint vertID, uint fragID)
		{
			(ID, VertID, FragID) = (id, vertID, fragID);
		}

		public void SetUniformMatrix4(string name, Matrix4 matrix)
		{
			GL.UniformMatrix4fv(GL.GetUniformLocation(ID, name), 16, false, matrix);
		}

		public void SetUniformVec3(string name, Vector3 vec)
		{
			GL.Uniform3f(GL.GetUniformLocation(ID, name), vec);
		}

		public void SetUniformVec4(string name, Vector4 vec)
		{
			GL.Uniform4f(GL.GetUniformLocation(ID, name), vec);
		}
		public void SetUniformVec4(string name, Color color)
        {
			GL.Uniform4f(GL.GetUniformLocation(ID, name), color);
        }

		public void Use()
		{
			GL.UseProgram(ID);
		}
	}
}