using System;
using System.Collections.Generic;
using OpenGL;
using System.IO;

namespace CMDR
{
	public static class ShaderManager
	{

		public static Dictionary<int, Shader> Shaders = new Dictionary<int, Shader>();

		private static readonly Dictionary<int, uint> _vertIDs = new Dictionary<int, uint>();
		private static readonly Dictionary<int, uint> _fragIDs = new Dictionary<int, uint>();

		private static readonly string _dir = @"Shaders\";
		private static readonly string _defaultVertName = @"VertDefault.vert";
		private static readonly string _defaultFragName = @"FragDefault.frag";

		private static int _defaultShaderKey;
		public static void LoadDefaults()
        {
			string vertPath = Path.Combine(_dir, _defaultVertName);
			
			string fragPath = Path.Combine(_dir, _defaultFragName);

			if (Directory.Exists(_dir) == false || File.Exists(vertPath) == false || File.Exists(fragPath) == false)
            {
				throw new FileNotFoundException($"Could not find correct default shaders at either {vertPath} or {fragPath}.");
            }
			
			_defaultShaderKey = string.Concat(vertPath, fragPath).GetHashCode();

			Load(vertPath, fragPath);
        }

		public static Shader DefaultShader()
        {
			return Shaders[_defaultShaderKey];
        }

		public static Shader Load(string vertPath, string fragPath)
		{
			int programKey = string.Concat(vertPath, fragPath).GetHashCode();

			if (Shaders.ContainsKey(programKey))
			{
				return Shaders[programKey];
			}

			int vertKey = vertPath.GetHashCode();
			int fragKey = fragPath.GetHashCode();

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

			switch (shaderType)
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

			Shader shader = new Shader(id, vertID, fragID, key);
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
			GL.GetProgramiv(id, GL.LINK_STATUS, out int linked);
			if (linked == 0)
			{
				throw new Exception(GL.GetProgramInfoLog(id));
			}
		}
	}
}
