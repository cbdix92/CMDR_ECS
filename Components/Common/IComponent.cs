using System;

namespace CMDR.Components
{
    public interface IComponent
    {
        Component Handle { get; set; }
    }
    public interface IComponent<T> : IComponent { }



    // Could be used to contain the components data handles at the top level instead of returning an int
    // Returned by the Scene when creating a new component.
    // Passed as an argument at the toplevel to manage components.
    // Work in progress ...
    public sealed class Component
    {
        public int ID;
        public Type Type;
        public GameObject Parent;
        public Scene Scene;
        internal Component(int id, Type type)
        {
            (ID, Type) = (id, type);
        }
    }
}
