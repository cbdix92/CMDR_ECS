using System;

namespace CMDR.Native
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
