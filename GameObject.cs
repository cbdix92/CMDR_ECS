using System;
using CMDR.Components;
using System.Collections.Generic;

namespace CMDR
{
    public class GameObject
    {
        public int ID;
        public Scene Scene;
    }
    public struct SGameObject
    {
        public GameObject Handle;
        public int ID;
        public Scene Scene;
        public KeyValuePair<Type, int>[] Components;
		
        public int Get<T>()
        {
            foreach (KeyValuePair<Type, int> components in Components)
            {
                if(components.Key == typeof(T))
                {
                    return components.Value;
                }
            }
            return -1;
        }

        public bool Contains(Type type)
        {
            for (int i = 0; i < Components.Length; i++)
            {
                if (Components[i].Key == type && Components[i].Value != -1)
                    return true;
            }
            return false;
        }

        public void Use(Component component)
        {
            for (int i = 0; i < Components.Length; i++)
            {
                if (Components[i].Key == component.Type)
                {
                    Scene.Destroy(Components[i]);
                    Components[i] = new KeyValuePair<Type, int>(component.Type, component.ID);
                    return;
                }
            }
        }
        public void Use(Component[] components)
        {
            foreach (Component component in components)
                Use(component);
        }
        public void RemoveComponent(Type type)
        {
            for (int i = 0; i < Components.Length; i++)
            {
                if (Components[i].Key == type)
                {
                    Scene.Destroy(Components[i]);
                    Components[i] = new KeyValuePair<Type, int>(type, -1);
                }
            }
        }
        internal void ComponentMoved(Type type, int newPosition)
        {
            for (int i = 0; i < Components.Length; i++)
            {
                if (Components[i].Key == type)
                    Components[i] = new KeyValuePair<Type, int>(type, newPosition);
            }
        }
    }
}
