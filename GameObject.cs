using System;
using CMDR.Components;
using System.Collections.Generic;

namespace CMDR
{
    public class GameObject
    {
        public int Handle;
        public Dictionary<Type, int> Components;

        public GameObject(int handle)
        {
            Handle = handle;
            Components = new Dictionary<Type, int>();
        }

        public void Use(IComponent component)
        {

            if (Components.ContainsKey(component.ID))
            {
                Data.Destroy(component.ID, Components[component.ID]);
            }
            Components.Add(component.ID, component.Handle);
        }
        public void Use(IComponent[] components)
        {
            foreach (IComponent component in components)
                Use(component);
        }
    }
}
