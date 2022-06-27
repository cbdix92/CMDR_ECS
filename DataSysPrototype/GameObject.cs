using System;
using CMDR.Components;
using System.Collections.Generic;

namespace CMDR
{
    public struct GameObject
    {
        #region PUBLIC_MEMBERS

        public readonly Scene Scene;

        public uint ID { get; internal set; }

        public ulong Signature { get; internal set; }

        public int ComponentCount { get; internal set; }

        public Type[] Components { get; internal set; }

        #endregion

        #region PRIVATE_MEMBERS

        #endregion

        #region INTERNAL_MEMBERS

        internal static GameObject Default = new GameObject(0, null);

        #endregion

        #region CONTRUCTOR

        public GameObject(uint id, Scene scene)
        {
            (ID, Scene) = (id, scene);

            Components = new int[Data.ComponentTotal];
        }

        #endregion
        #region PUBLIC_METHODS

        public bool Contains<T>()
        {

        }

        public T Get<T>() where T : struct, IComponent<T>
        {

        }

        public int GetComponentID(Type type)
        {

        }

        public void RemoveComponent(Type type)
        {

        }

        public void Use<T>(T component)
        {

        }

        #endregion

        #region  PRIVATE_METHODS

        #endregion
    }
}