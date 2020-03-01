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


        public void Use(Component component)
        {
            Scene.GameObjects[ID].Use(component);
        }
    }
    public struct SGameObject
    {
        public GameObject Handle;
        public int ID;
        public Scene Scene;
        public KeyValuePair<Type, int>[] Components;
		public int NumberOfComponents { get; private set; }

        
        // Used for syntactical  simplicity
        internal int Get<T>()
        {
            return Get(typeof(T));
        }
        

        // Returns the index of Component "type". Returns -1 if that Component does not exist.
        public int Get(Type type)
        {
            if (!Data.Types.Contains(type))
                throw new ArgumentException($"{type} is not a Component");

            for (int i = 0; i < NumberOfComponents; i++)
            {
                if (Components[i].Key == type)
                    return Components[i].Value;
            }
            return -1;
        }
		
        public bool Contains<T>()
        {
            return (Get(typeof(T)) != -1);
        }
		
        public void Use(Component component)
        {
            
            int i = Get(component.Type);
            // if Components already exist, remove it from memory then overwrite it with the new component
			if (i != -1)
			{
				Scene.Destroy(Components[i]);
				Components[i] = new KeyValuePair<Type, int>(component.Type, component.ID);
				return;
			}
			Components[NumberOfComponents] = new KeyValuePair<Type, int>(component.Type, component.ID);
			NumberOfComponents++;
        }
		
        public void RemoveComponent(Type type)
        {
            int i = Get(type);
            if (i != -1)
            {
                Scene.Destroy(Components[i]);
                NumberOfComponents--;
                ////////////////////////////////////////////// DOES THIS WORK? 
                //WILL IT PLACE A DEFAULT VALUE STRUCT AT THE END OF THE ARRAY
                Components[i] = new KeyValuePair<Type, int>();
                Array.Sort(Components);
                ///////////////////////////////////////////
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
