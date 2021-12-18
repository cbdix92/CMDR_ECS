using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace CMDR
{
    public static class MeshManager
    {
        public static Mesh Load(string path)
        {
            string[] data = File.ReadAllLines(path);

            List<float> vertOut = new List<float>();
            List<float> normalOut = new List<float>();
            List<int> indiceOut = new List<int>();

            uint VAO = 0;
            uint VBO = 0;
            uint EBO = 0;
            
            foreach(string line in data)
            {
                if (string.Concat(line[0], line[1]) == "vn")
                    ParseFloat(line, normalOut);

                else if (line[0] == 'v')
                    ParseFloat(line, vertOut);

                else if (line[0] == 'f')
                    ParseInt(line, indiceOut);
            }




            return new Mesh(VAO, VBO, EBO, vertOut.ToArray(), normalOut.ToArray(), indiceOut.ToArray());
        }

        private static void ParseFloat(string line, List<float> output)
        {

        }

        private static void ParseInt(string line, List<int> output)
        {

        }
    }
}
