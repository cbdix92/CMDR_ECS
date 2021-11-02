using System;

namespace OpenGL
{
    public class BuildInfo : Attribute
    {
        public string Lib;
        public string Name;
        public BuildInfo(string lib, string name)
        {
            (Lib, Name) = (lib, name);
        }
    }
}
