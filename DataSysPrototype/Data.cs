using System;
using System.Linq;
using CMDR.Components;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

namespace CMDR
{
    public static class Data
    {
        #region  PUBLIC_MEMBERS
        
        public static readonly byte StorageScale = Byte.MaxValue;

        public static int ComponentTotal { get; private set; }

        public static Dictionary<Type, ulong> Signatures { get; private set; }

        #endregion

        #region  INTERNAL_MEMBERS

        /// <summary>
        /// Generated at runtime when the Data System creates data storage and the Componet types are looked up using reflection.
        /// ULONG is a BitMask for the position of the components signature bit.
        /// </summary>
        internal static Dictionary<Type, ulong> ComponentBitPositions;

        internal static Dictionary<ulong, GameObjectCollection> SortedGameObjects { get; private set; }

        #endregion

        #region PRIVATE_MEMBERS

        private readonly static IEnumerable _types = Assembly.GetExecutingAssembly().GetTypes().Where(T => T.GetInterfaces().Contains(typeof(IComponent)));

        #endregion

        #region PUBLIC_METHODS

        #endregion

        #region INTERNAL_METHODS

        internal static void GenerateComponents(out Hashtable output)
        {
            output = new Hashtable();

            ulong bitMask = 1;

            Type TComponentCollection = typeof(ComponentCollection<>);

            foreach(Type TComponent in _types)
            {
                if(TComponent.Name == typeof(IComponent<>).name)
                    continue;

                var TNew = TComponentCollection.MakeGenericType(TComponent);

                output.Add(TComponent, Activator.CreateInstance(TNew));

                if(bitMask == 0x8000000000000000)
                    throw new Exception("Component total exceeds max of 64 Components");
                
                ComponentBitPositions.Add(TComponent, bitMask);
                
                // Move 
                bitMask = bitMask << 1;

                ComponentTotal++;
            }
        }

        internal static ulong GetSignature(Type[] types)
        {
            ulong result;

            foreach(Type type in types)
            {
                result |= ComponentBitPositions[type];
            }

            return result;
        }

        internal static ulong RegisterSignature(Type[] types)
        {
            ulong signature = GetSignature(types);

            if(SortedGameObjects.ContainsKey(signature) == false)
            {
                SortedGameObjects.Add(signature, new GameObjectCollection());
            }

            return signature;
        }

        internal static void SortGameObject(ref GameObject gameObject)
        {
            gameObject.Signature = Data.GetSignature(gameObject.ComponentTypes);

            foreach(ulong signatureKey in SortedGameObjects.Keys)
            {
                if(signatureKey & ~(gameObject.Signature) == 0)
                {
                    SortedGameObjects[signatureKey].Add(gameObject);
                }
            }

        }

        #endregion

        #region PRIVATE_METHODS

        #endregion
    }
}