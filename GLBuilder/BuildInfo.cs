using System;

namespace OpenGL
{
    public class BuildInfo : Attribute
    {
        public string Name;
        public BuildInfo(string name)
        {
            Name = name;
        }
    }
}
