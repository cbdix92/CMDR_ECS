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

        public T[] Get<T>()
        {
            dynamic TComponents = _data[typeof(T)];
            return TComponents.Get();
        }
        public Component Generate<T>(Scene scene)
            where T : struct, IComponent<T>
        {
            dynamic TComponents = _data[typeof(T)];

            TComponents.Add(new T
            {
                Handle = new Component(TComponents.Count, -1, typeof(T), scene),
                ID = TComponents.Count,
                Parent = -1,
                Type = typeof(T),
                Scene = scene
            });

            return TComponents[TComponents.Count - 1].Handle;
        }
        public void FinalDestroy(KeyValuePair<Type, int> component)
        {
            dynamic TComponets = _data[component.Key];
            TComponets.Remove(component.Value);
        }


    }
}
