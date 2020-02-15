using System;

namespace CMDR.Components
{
    public interface IComponent
    {
        int Handle { get; set; }
        Type ID { get; set; }
        ParentList Parents { get; set; }
    }
}
