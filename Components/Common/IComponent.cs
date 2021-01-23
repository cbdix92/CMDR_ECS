using System;

namespace CMDR.Components
{
    public interface IComponent
    {
        int ID { get; set; }
        int Parent { get; set; } 
        Type Type { get; set; }
        Scene Scene { get; set; }

        void Receive();
        void Send();
    }
    public interface IComponent<T> : IComponent { }
}
