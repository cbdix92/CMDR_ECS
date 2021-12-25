using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OpenGL;

namespace CMDR
{
    public static unsafe class MeshManager
    {
		
        public static Mesh Load(string path)
        {
			
            List<float> vertOut = new List<float>();
            List<float> normalOut = new List<float>();
			List<float> texOut = new List<float>();

            List<int> vertexIndice = new List<int>();
            List<int> texIndice = new List<int>();
            List<int> normalIndice = new List<int>();

			List<float> bufferOut = new List<float>();

			bool texture = false;
			bool normal = false;
			
			string nameOut;

            uint VAO;
            uint VBO;
			
			using(StreamReader sr = new StreamReader(path))
			{
				string line;
				while((line = sr.ReadLine()) != null)
				{
					string[] data = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
					if (data.Length == 0)
						continue;
					switch(data[0])
					{
						case "#":
							break;
							
						case "v":
							ParseVertexData(data, 3, vertOut);
							break;
							
						case "vt":
							texture = true;
							ParseVertexData(data, 2, texOut);
							break;
							
						case "vn":
							normal = true;
							ParseVertexData(data, 3, normalOut);
							break;
						
						case "f":
							for (int i = 1; i < data.Length; i++)
							{
								string[] indice = data[i].Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
								vertexIndice.Add(int.Parse(indice[0]) - 1);

								if (texture)
									texIndice.Add(int.Parse(indice[1]) - 1);
								if (normal)
									normalIndice.Add(int.Parse(indice[texture?2:1]) - 1);
							}
							break;
						
						case "mtllib":
							break;
						
						case "o":
							nameOut = data[1];
							break;
						
						case "g":
							break;

						case "usemtl":
							break;
						
						case "s":
							break;
					}
				}
			}

			if (vertOut.Count == 0)
				throw new Exception($"OBJ at '{path}' did not contain vertices!");

			// Consolidate Data
			bufferOut.AddRange(Index(vertOut, vertexIndice, 3));
			int numVertex = bufferOut.Count;

			if (texture)
				bufferOut.AddRange(Index(texOut, texIndice, 2));

			if (normal)
				bufferOut.AddRange(Index(normalOut, normalIndice, 3));



			VAO = GL.GenVertexArray();
			VBO = GL.GenBuffer();

			GL.BindVertexArray(VAO);

			// Buffer combined data
			GL.BindBuffer(GL.ARRAY_BUFFER, VBO);
			GL.BufferData(GL.ARRAY_BUFFER, bufferOut.Count * sizeof(float), bufferOut.ToArray(), GL.STATIC_DRAW);

			// Setup VertexAttribPointers
			int texOffset = 3 * sizeof(float);
			int normalOffset = texture ? 5 * sizeof(float) : 3 * sizeof(float);

			// Vertices (layout 0) 
			GL.VertexAttribPointer(0, 3, GL.FLOAT, false, 0, (void*)0);
			// Texture coords (layout 1)
			GL.VertexAttribPointer(1, 2, GL.FLOAT, false, 0, (void*)texOffset);
			// Normals (layout 2)
			GL.VertexAttribPointer(2, 3, GL.FLOAT, false, 0, (void*)normalOffset);

			// Enable VertexAttribPointers
			GL.EnableVertexAttribArray(0);

			if (texture)
				GL.EnableVertexAttribArray(1);
			if (normal)
				GL.EnableVertexAttribArray(2);


			Mesh output = new Mesh(VAO, VBO, numVertex);

			return output;
        }

        private static void ParseVertexData(string[] lines, int count, List<float> output)
        {
			for(int i = 1; i <= count; i++)
			{		
				output.Add(float.Parse(lines[i]));
			}
        }

		private static List<float> Index(List<float> data, List<int> indices, int stride)
        {
			List<float> output = new List<float>();
			
			foreach(int i in indices)
            {
				output.AddRange(data.GetRange(i*stride, stride));

            }
			return output;
        }
    }
}
