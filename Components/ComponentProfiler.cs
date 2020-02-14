using System;

namespace CMDR.Components
{
    public abstract class ComponentProfiler
    {
        // Use this base class to store constructor args for components
        public int Handle;

        public virtual void Profile(IComponent component) { }
    }
}
