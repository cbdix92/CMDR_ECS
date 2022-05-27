using OpenGL;


namespace CMDR
{
	
	public struct Shader
	{
		public readonly uint ID;
		public readonly uint VertID;
		public readonly uint FragID;
		public readonly int ShaderKey;


		internal Shader(uint id, uint vertID, uint fragID, int shaderKey)
		{
			(ID, VertID, FragID, ShaderKey) = (id, vertID, fragID, shaderKey);
		}

		public void SetUniformMatrix4(string name, bool transpose, Matrix4 matrix)
		{
			GL.UniformMatrix4fv(GL.GetUniformLocation(ID, name), transpose, matrix);
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