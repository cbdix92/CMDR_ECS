using System;

namespace CMDR.Components
{
    public interface IComponent
    {
        Component Handle { get; set; }
        int ID { get; set; }
        int Parent { get; set; } 
        Type Type { get; set; }
        Scene Scene { get; set; }
    }
    public interface IComponent<T> : IComponent { }



    public sealed class Component
    {
        public int ID;
        public int Parent;
        public Type Type;
        public Scene Scene;
        internal Component(int id, int parent, Type type, Scene scene)
        {
            (ID, Parent, Type, Scene) = (id, parent, type, scene);
        }
        public T Get<T>()
        {
            return Scene.Components.Get<T>()[ID];
        }
        public void Update<T>(T component)
        {
            Scene.Components.Get<T>()[ID] = component;
        }
    }
}
