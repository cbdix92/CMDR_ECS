using System;
using System.IO;
using OpenGL;
using System.Collections.Generic;


namespace CMDR
{

	internal static class ShaderManager
    {
		
		internal static Dictionary<int, Shader> Shaders = new Dictionary<int, Shader>();

		/// <summary>
		/// Determines whether shader has already been loaded previously.
		/// </summary>
		/// <param name="vert"> Path to vertex shader. </param>
		/// <param name="frag"> Path to Fragment shader. </param>
		/// <param name="shader"> Current shader instance that is attempting to load. </param>
		/// <returns></returns>
		internal static int Exist(string vert, string frag, ref Shader shader)
        {
			int hashKey = String.Concat(vert,frag).GetHashCode();
			if(Shaders.ContainsKey(hashKey))
            {
				shader = Shaders[hashKey];
				return 0;
            }
			return hashKey;
        }
    }
	
	public struct Shader
	{
		public uint ID { get; private set; }
		public uint VertID { get; private set; }
		public uint FragID { get; private set; }

		public Shader(string pathVert, string pathFrag)
		{
			(ID, VertID, FragID) = (0, 0, 0);

			// Check if this shader program has already been loaded
			int hashKey = ShaderManager.Exist(pathVert, pathFrag, ref this);
			if (hashKey == 0)
				return;

			// Shader IDs generation
			VertID = GL.CreateShader(GL.VERTEX_SHADER);
			FragID = GL.CreateShader(GL.FRAGMENT_SHADER);

			// Read shaders source codes
			var vertRead = File.ReadAllText(pathVert);
			var fragRead = File.ReadAllText(pathFrag);

			// Vertex Shader
			GL.ShaderSource(VertID, vertRead);
			GL.CompileShader(VertID);
			CheckCompileErrors();

			// Frag Shader
			GL.ShaderSource(FragID, fragRead);
			GL.CompileShader(FragID);
			CheckCompileErrors();

			
			ID = GL.CreateProgram();
			GL.AttachShader(ID, VertID);
			GL.AttachShader(ID, FragID);
			GL.LinkProgram(ID);
			CheckLinkErrors();

			ShaderManager.Shaders.Add(hashKey, this);
		}

		public void CheckCompileErrors()
		{
			GL.GetShaderiv(ID, GL.COMPILE_STATUS, out int compiled);

			if (compiled == 0)
			{
				throw new Exception(GL.GetShaderInfoLog(ID));
			}
		}

		public void CheckLinkErrors()
		{
			GL.GetShaderiv(ID, GL.LINK_STATUS, out int linked);
			if (linked == 0)
			{
				throw new Exception(GL.GetShaderInfoLog(ID));
			}
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

		public void Use()
		{
			GL.UseProgram(ID);
		}
	}
}