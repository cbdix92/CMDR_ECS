using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OpenGL;

namespace CMDR
{
    public static class MeshManager
    {
		private static int _comment = 0x23;     // # ->comment line
		private static int _vertex = 0x76;      // v ->vertex data
		private static int _normal = 0x766e; // vn ->vertex normal
		private static int _face = 0x66;    // f ->face
		private static int _name = 0x6f;    // o ->objects name
		private static int _group = 0x67;   // g ->group name
		private static int _smooth = 0x73;  // s ->smooth shading
		private static long _mtl = 0x6d746c6c6962; // mtllib ->materials file
		
        public static Mesh Load(string path)
        {

            List<float> vertOut = new List<float>();
            List<float> normalOut = new List<float>();
			List<float> texOut = new List<float>();
            List<int> indiceOut = new List<int>();
			string nameOut;

            uint VAO = 0;
            uint VBO = 0;
            uint EBO = 0;
			
			using(StreamReader sr = new StreamReader(path))
			{
				string line;
				while((line = sr.ReadLine()) != null)
				{
					string[] data = line.Split(' ');
					
					switch(data[0])
					{
						case "#":
							break;
							
						case "v":
							ParseVertexData(data, 3, vertOut);
							break;
							
						case "vt":
							ParseVertexData(data, 2, texOut);
							break;
							
						case "vn":
							ParseVertexData(data, 3, normalOut);
							break;
						
						case "f":
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

			VAO = GL.GenVertexArray();
			VBO = GL.GenBuffer();
			EBO = GL.GenBuffer();

			GL.BindVertexArray(VAO);

			GL.BindBuffer(GL.ARRAY_BUFFER, VBO);

			GL.BufferData(GL.ARRAY_BUFFER, sizeof(float) * vertOut.Count, vertOut.ToArray(), GL.STATIC_DRAW);

			GL.BindBuffer(GL.ELEMENT_ARRAY_BUFFER, EBO);
			GL.BufferData(GL.ELEMENT_ARRAY_BUFFER, sizeof(int) * indiceOut.Count, indiceOut.ToArray(), GL.STATIC_DRAW);



			Mesh output = new Mesh(VAO, VBO, EBO, vertOut.ToArray(), normalOut.ToArray(), indiceOut.ToArray());

			return output;
        }

        private static void ParseVertexData(string[] lines, int count, List<float> output)
        {
			for(int i = 1; i <= count; i++)
			{		
				output.Add(float.Parse(lines[i]));
			}
        }

        private static void ParseFaceData(string[] lines, List<int> output)
        {
			
        }
    }
}
