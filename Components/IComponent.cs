using System;

namespace CMDR.Components
{
    public interface IComponent
    {
        int Parent { get; set; }
        int Handle { get; set; }
        Type ID { get; set; }
    }
}
