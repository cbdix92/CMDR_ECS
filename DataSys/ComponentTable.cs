using System;
using System.Collections.Generic;
using System.Collections;
using CMDR.Components;

namespace CMDR.DataSys
{
    internal class ComponentTable
    {
        private Hashtable _data;

        public ComponentTable()
        {
            Data.GenerateComponents(out _data);
        }

        public List<T> Get<T>()
        {
            dynamic TComponents = _data[typeof(T)];
            return TComponents.Get();
        }
        public Component Generate<T>()
            where T : struct, IComponent<T>
        {
            dynamic TComponents = _data[typeof(T)];

            TComponents.Add(new T { Handle = new Component(TComponents.Count, typeof(T)) });

            return TComponents[TComponents.Count - 1].Handle;
        }
        public void FinalDestroy(Component component)
        {
            dynamic TComponets = _data[component.Type];
            TComponets.Remove(component.ID);
        }


    }
}
