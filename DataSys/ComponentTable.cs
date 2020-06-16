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
        public T Generate<T>(Scene scene)
            where T : struct, IComponent<T>
        {
            dynamic TComponents = _data[typeof(T)];

            return TComponents.Add(new T
            {
                ID = TComponents.Count,
                Parent = -1,
                Type = typeof(T),
                Scene = scene
            });
        }
        public void FinalDestroy(KeyValuePair<Type, int> component)
        {
            dynamic TComponets = _data[component.Key];
            TComponets.Remove(component.Value);
        }
        public void Update<T>(T component)
        {
            dynamic TComponents = _data[typeof(T)];
            TComponents.Update(component);
        }


    }
}
