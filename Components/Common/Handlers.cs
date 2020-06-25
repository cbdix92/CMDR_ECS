using System;

namespace CMDR.Components
{
    public class Component<T>
        where T: IComponent<T>
    {
        internal readonly Scene Scene;
        internal int ID;
        public T this
        {
            get
            {
                // There has to be a better way to do this without needing to retrieve the entire collection. Something to look into later.
                return Scene.Components.Get<T>()[ID];
            }
        }
    }

    public class GameObject
    {
        internal readonly Scene Scene;
        internal int ID;
        public SGameObject this
        {
            get
            {
                return Scene.GameObjects[ID];
            }
        }
    }
}
