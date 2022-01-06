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

			bool uv = false;
			bool normal = false;
			
			string nameOut;
			bool smooth = false;
			
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
							uv = true;
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

								if (uv)
									texIndice.Add(int.Parse(indice[1]) - 1);
								if (normal)
									normalIndice.Add(int.Parse(indice[uv?2:1]) - 1);
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
							if (data[1] == "1")
								smooth = true;
							break;
					}
				}
			}

			if (vertOut.Count == 0)
				throw new Exception($"OBJ at '{path}' did not contain vertices!");

			// Consolidate Data
			bufferOut.AddRange(Index(vertOut, vertexIndice, 3));
			int numVertices = bufferOut.Count;
			bufferOut.AddRange(GenerateNormals(bufferOut, smooth));

			if (uv)
				bufferOut.AddRange(Index(texOut, texIndice, 2));

			//if (normal)
				//bufferOut.AddRange(Index(normalOut, normalIndice, 3));


			return BufferData(bufferOut, numVertices, uv, normal);
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

		private static List<float> GenerateNormals(List<float> vertices, bool smooth)
        {
			List<float> normalOut = new List<float>();

			for (int i = 0; i < vertices.Count;)
			{
				Vector3 v1 = new Vector3(vertices[i++], vertices[i++], vertices[i++]);
				Vector3 v2 = new Vector3(vertices[i++], vertices[i++], vertices[i++]);
				Vector3 v3 = new Vector3(vertices[i++], vertices[i++], vertices[i++]);
				Vector3 cross = Vector3.Cross((v2 - v1), (v3 - v1));

				if (smooth)
				{
					Vector3 vn1 = Vector3.Normalize(v1 + cross);
					Vector3 vn2 = Vector3.Normalize(v1 + cross);
					Vector3 vn3 = Vector3.Normalize(v1 + cross);

					normalOut.AddRange(vn1.ToArray());
					normalOut.AddRange(vn2.ToArray());
					normalOut.AddRange(vn3.ToArray());
					continue;
				}

				float[] fn = Vector3.Normalize(cross).ToArray();

				normalOut.AddRange(fn);
				normalOut.AddRange(fn);
				normalOut.AddRange(fn);

			}


			return normalOut;
        } 

		private static Mesh BufferData(List<float> data, int numVertices, bool uv, bool normal)
        {
			uint VAO;
			uint VBO;

			VAO = GL.GenVertexArray();
			VBO = GL.GenBuffer();

			GL.BindVertexArray(VAO);

			// Buffer combined data
			GL.BindBuffer(GL.ARRAY_BUFFER, VBO);
			GL.BufferData(GL.ARRAY_BUFFER, data.Count * sizeof(float), data.ToArray(), GL.STATIC_DRAW);

			// Setup VertexAttribPointers
			int uvOffset = 3 * sizeof(float);
			int normalOffset = uv ? 5 * sizeof(float) : 3 * sizeof(float);

			// Vertices (layout 0) 
			GL.VertexAttribPointer(0, 3, GL.FLOAT, false, 0, (void*)0);
			// Texture coords (layout 1)
			GL.VertexAttribPointer(1, 2, GL.FLOAT, false, 0, (void*)uvOffset);
			// Normals (layout 2)
			GL.VertexAttribPointer(2, 3, GL.FLOAT, false, 0, (void*)normalOffset);

			// Enable VertexAttribPointers
			GL.EnableVertexAttribArray(0);

			if (uv)
				GL.EnableVertexAttribArray(1);
			if (normal)
				GL.EnableVertexAttribArray(2);


			return new Mesh(VAO, VBO, numVertices, uv, normal);
		}
    }
}
