using System;
using System.IO;
using OpenGL;
using System.Collections.Generic;


namespace CMDR
{

	internal static class ShaderLoader
    {
		
		private static Dictionary<int, Shader> _shaders = new Dictionary<int, Shader>();
		private static Dictionary<int, uint> _vertIDs = new Dictionary<int, uint>();
		private static Dictionary<int, uint> _fragIDs = new Dictionary<int, uint>();

		/// <summary>
		/// Determines whether shader has already been loaded previously.
		/// </summary>
		/// <param name="vert"> Path to vertex shader. </param>
		/// <param name="frag"> Path to Fragment shader. </param>
		/// <param name="shader"> Current shader instance that is attempting to load. </param>
		/// <returns></returns>
		internal static Shader Load(string vertPath, string fragPath)
        {
			int vertKey = vert.GetHashCode();
			int fragKey = frag.GetHashCode();
			int programKey = String.Concat(vertPath,fragPath).GetHashCode();
			
			if(_shaders.ContainsKey(programKey))
            {
				return _shaders[programKey];
            }
			
			uint vertID = _vertIDs.Contains(vertKey) ? _vertIDs[vertKey] : ShadeCache(GL.VERTEX_SHADER, vertKey, vertPath);
			uint fragID = _fragIDs.Contains(fragKey) ? _fragIDs[fragKey] : ShadeCache(GL.FRAGMENT_SHADER, fragKey, fragPath);
			
			return ProgramCache(programKey, vertID, fragID);
        }
		
		private static uint ShaderCache(int shaderType, int key, string path)
		{
			uint id = GL.CreateShader(shaderType);
			
			var read = File.ReadAllText(path);
			
			GL.ShaderSource(shaderType, read);
			GL.CompileShader(shaderType);
			CheckCompileErrors();
			
			switch(shaderType)
			{
				case GL.VERTEX_SHADER:
					_vertIDs.Add(key, id);
				
				case GL.FRAGMENT_SHADER:
					_fragIDs.Add(key, id);
			}
			return id;
		}
		
		private static Shader ProgramCache(int key, uint vertID, uint fragID)
		{
			uint id = GL.CreateProgram();
			GL.AttachShader(id, vertID);
			GL.AttachShader(id, fragID);
			GL.LinkProgram(id);
			CheckLinkErrors();
			
			Shader shader = new Shader(id, vertID, fragID);
			_shaders.Add(key, shader);
			return shader;
		}
		
		private void CheckCompileErrors()
		{
			GL.GetShaderiv(ID, GL.COMPILE_STATUS, out int compiled);

			if (compiled == 0)
			{
				throw new Exception(GL.GetShaderInfoLog(ID));
			}
		}
		
		private void CheckLinkErrors()
		{
			GL.GetShaderiv(ID, GL.LINK_STATUS, out int linked);
			if (linked == 0)
			{
				throw new Exception(GL.GetShaderInfoLog(ID));
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