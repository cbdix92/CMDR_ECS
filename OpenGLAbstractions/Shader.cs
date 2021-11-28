using System;
using System.IO;
using OpenGL;


namespace CMDR
{
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
			CheckCompileErrors();

			GL.ShaderSource(FragID, fragRead);
			GL.CompileShader(FragID);
			CheckCompileErrors();

			ID = GL.CreateProgram();
			GL.AttachShader(ID, VertID);
			GL.AttachShader(ID, FragID);
			GL.LinkProgram(ID);
			CheckLinkErrors();


		}

		public static void CheckCompileErrors()
		{
			object compiled;
			GL.GetShaderiv(ID, PNAME.GL_COMPILE_STATUS, out compiled);

			if (!(bool)compiled)
			{
				throw new Exception(GL.GetShaderInfoLog(ID));
			}
		}

		public static void CheckLinkErrors()
		{
			object linked;
			GL.GetShaderiv(ID, PNAME.GL_LINK_STATUS, out linked);
			if (!(bool)linked)
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