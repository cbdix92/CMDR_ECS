using System;
using CMDR.Components;
using System.Collections.Generic;

namespace CMDR
{
    public struct SGameObject
    {
        public int ID;
        public Scene Scene;
        public KeyValuePair<Type, int>[] Components;
		public int NumberOfComponents { get; private set; }


        // Used for syntactic simplicity
        internal int Get<T>()
        {
            return Get(typeof(T));
        }
        

        // Returns the index of Component "type". Returns -1 if that Component does not exist.
        public int Get(Type type)
        {
            if (!Data.Types.Contains(type))
                throw new ArgumentException($"{type} does not implement the IComponent interface");

            for (int i = 0; i < NumberOfComponents; i++)
            {
                if (Components[i].Key == type)
                    return Components[i].Value;
            }
            return -1;
        }
        public T SGet<T>()
            where T: struct, IComponent<T>
        {
            return Scene.Components.Get<T>()[Get(typeof(T))];
        }
		
        public bool Contains<T>()
        {
            return (Get(typeof(T)) != -1);
        }
		public void Use(IComponent[] components)
        {
            foreach (IComponent component in components)
                Use(component);
        }
        public void Use(IComponent component)
        {
            component.Parent = ID;
            component.Send();
            int i = Get(component.Type);
            // if Components already exist, remove it from memory then overwrite it with the new component
			if (i != -1)
			{
				Scene.Destroy(Components[i]);
				Components[i] = new KeyValuePair<Type, int>(component.Type, component.ID);
                Data.Update(this);
				return;
			}
			Components[NumberOfComponents++] = new KeyValuePair<Type, int>(component.Type, component.ID);
            Data.Update(this);
        }
		
        public void RemoveComponent(Type type)
        {
            int i = Get(type);
            if (i != -1)
            {
                Scene.Destroy(Components[i]);

                // Replace the element at the end of the array with the one to be deleted and then resize the array.
                Components[i] = Components[--NumberOfComponents];
                Array.Resize<KeyValuePair<Type, int>>(ref Components, NumberOfComponents);
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
