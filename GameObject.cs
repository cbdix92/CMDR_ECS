using System;
using System.Linq;
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
		public int NumberOfComponents { get; private set; }

        public int Get<T>()
        {
			for (int i = 0; i < NumberOfComponents; i++)
			{
				if(Components[i].Key == typeof(T))
					return Components[i].Value;
			}
			return -1;
        }
		
        public bool Contains<T>()
        {
            return (Get<T>() != -1);
        }
		
        public void Add(Component component)
        {
			int i = Get<component.Type>();
			if (i != -1)
			{
				Scene.Destroy(Components[i]);
				Components[i] = new KeyValuePair<Type, int>(component.Type, component.ID);
				return;
			}
			Components[NumberOfComponents] = new KeyValuePair<Type, int>(component.Type, component.ID);
			NumberOfComponents++;
        }
		
        public void RemoveComponent<T>()
        {
            for (int i = 0; i < NumberOfComponents; i++)
            {
                if (Components[i].Key == typeof(T))
                {
                    Scene.Destroy(Components[i]);
                    Components[i] = new KeyValuePair<Type, int>(typeof(T), -1);
                }
            }
        }
		
        internal void ComponentMoved(Type type, int newPosition)
        {
            for (int i = 0; i < NumberOfComponents; i++)
            {
                if (Components[i].Key == type)
                    Components[i] = new KeyValuePair<Type, int>(type, newPosition);
            }
        }
    }
}
