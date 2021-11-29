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
			VertID = GL.CreateShader(GLenum.VERTEX_SHADER);
			FragID = GL.CreateShader(GLenum.FRAGMENT_SHADER);

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

		public void CheckCompileErrors()
		{
			GL.GetShaderiv(ID, GLenum.COMPILE_STATUS, out int compiled);

			if (compiled == 0)
			{
				throw new Exception(GL.GetShaderInfoLog(ID));
			}
		}

		public void CheckLinkErrors()
		{
			GL.GetShaderiv(ID, GLenum.LINK_STATUS, out int linked);
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