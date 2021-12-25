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
            List<uint> indiceOut = new List<uint>();

			List<float> bufferOut = new List<float>();
			
			string nameOut;

			byte bufferBit = 0x00;
			byte textureMask = 0x0f;
			byte normalMask = 0xf0;

            uint VAO = 0;
            uint VBO = 0;
            uint EBO = 0;
			
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
							bufferBit |= textureMask;
							ParseVertexData(data, 2, texOut);
							break;
							
						case "vn":
							bufferBit |= normalMask;
							ParseVertexData(data, 3, normalOut);
							break;
						
						case "f":
							ParseFaceData(data, indiceOut);
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

			ConsolidateData(vertOut, texOut, normalOut, bufferOut);

			VAO = GL.GenVertexArray();
			VBO = GL.GenBuffer();
			EBO = GL.GenBuffer();

			GL.BindVertexArray(VAO);

			// Buffer combined data
			GL.BindBuffer(GL.ARRAY_BUFFER, VBO);
			GL.BufferData(GL.ARRAY_BUFFER, bufferOut.Count * sizeof(float), bufferOut.ToArray(), GL.STATIC_DRAW);

			// Setup VertexAttribPointers
			int texOffset = 3 * sizeof(float);
			int normalOffset = (bufferBit & 0x0f) == 0x0f ? 5 * sizeof(float) : 3 * sizeof(float);

			// Vertices (layout 0) 
			GL.VertexAttribPointer(0, 3, GL.FLOAT, false, 0, (void*)0);
			// Texture coords (layout 1)
			GL.VertexAttribPointer(1, 2, GL.FLOAT, false, 0, (void*)texOffset);
			// Normals (layout 2)
			GL.VertexAttribPointer(2, 3, GL.FLOAT, false, 0, (void*)normalOffset);

			// Enable VertexAttribPointers
			GL.EnableVertexAttribArray(0); // Vertices

			if ((bufferBit & 0x0f) == 0x0f) // Textures
				GL.EnableVertexAttribArray(1);
			if ((bufferBit & 0xf0) == 0xf0) // Normals
				GL.EnableVertexAttribArray(2);


			// Buffer indices
			if (indiceOut.Count != 0)
            {
				GL.BindBuffer(GL.ELEMENT_ARRAY_BUFFER, EBO);
				GL.BufferData(GL.ELEMENT_ARRAY_BUFFER, sizeof(int) * indiceOut.Count, indiceOut.ToArray(), GL.STATIC_DRAW);
            }


			Mesh output = new Mesh(VAO, VBO, EBO, vertOut.Count);

			return output;
        }

        private static void ParseVertexData(string[] lines, int count, List<float> output)
        {
			for(int i = 1; i <= count; i++)
			{		
				output.Add(float.Parse(lines[i]));
			}
        }

        private static void ParseFaceData(string[] lines, List<uint> output)
        {
			for (int i = 1; i < lines.Length; i++)
            {
				string[] indice = lines[i].Split(new string[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
				for(int j = 0; j < indice.Length; j++)
					output.Add(uint.Parse(indice[j]) - 1);
            }
        }

		private static void ConsolidateData(List<float> verts, List<float> texCoords, List<float> normals, List<float> output)
        {
			output.AddRange(verts);

			if(texCoords.Count != 0)
				output.AddRange(texCoords);
				
			if(normals.Count != 0)
				output.AddRange(normals);
        }
    }
}
