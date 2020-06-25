using System;

namespace CMDR.Components
{
    public class Component<T>
        where T: IComponent<T>
    {
        public readonly Type Type;
        public readonly Scene Scene;
        internal T Data;
        public T Get
        {
            get
            {
                // There has to be a better way to do this without needing to retrieve the entire collection. Something to look into later.
                return Scene.Components.Get<T>()[Data.ID];
            }
        }
    }

    public class GameObject
    {
        public readonly Scene Scene;
        internal SGameObject Data;
        public SGameObject Get
        {
            get
            {
                return Scene.GameObjects[Data.ID];
            }
        }
    }
}
