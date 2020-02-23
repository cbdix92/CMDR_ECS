using System;
using CMDR.Components;
using System.Collections.Generic;

namespace CMDR
{
    public class GameObject : IDisposable
    {
        public int ID;
        public Scene Scene;
        public Dictionary<Type, Component> Components { get; private set; }
		
        public GameObject(int id, Scene scene)
        {
            ID = id;
            Scene = scene;
            Components = new Dictionary<Type, Component>();
        }
        public int Get<T>()
        {
            return Components[typeof(T)].ID;
        }

        public void Use(Component component)
        {
            if (Components.ContainsKey(component.Type))
            {
                RemoveComponent(component.Type);
            }
            
            Components.Add(component.Type, component);
        }
        public void Use(Component[] components)
        {
            foreach (Component component in components)
                Use(component);
        }
        public void RemoveComponent(Type type)
        {
            if (!Components.ContainsKey(type))
                throw new ArgumentException($"Component {type} doesn't exist for {this.ToString()}.");

            Components.Remove(type);
        }
        public void Dispose()
        {
            Scene.Destroy(this);
        }
    }
}
