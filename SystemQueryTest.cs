using System;

namespace CMDR
{

    // CMDR.System example. Generic class shouldn't be needed. Should be able to stick with the Static pattern that we currently use.
    // Otherwise a Generic singleton class will need to be implemented
    internal sealed class Physics<T, C>
        where T : struct, IComponent<T>
        where C : struct, IComponent<C>
    {
        internal ulong Signature = Data.GetSignature(new Type[]{ typeof(Transform), typeof(Collider) });
        
        internal Update(long ticks)
        {
            GameObject[] gameObjects = Scene.QueryGameObject(Signature);
            Transform[] transforms = Scene.GetSortedComponents<Transform>(gameObjects);
            Collider[] colliders = Scene.GetSortedComponents<Collider>(gameObjects);

        }
    }

    internal class Data
    {

        // Generated at runtime when the Data System creates data storage and the Componet types are looked up using reflection.
        // ULONG is a BitMask for the position of the components signature bit.
        internal Dictionary<Type, ulong> ComponentBitPositions;

        // This will be initialized at runtime and maintained as the program runs.
        // ULONG is the signature for connected componenets
        internal Dictionary<ulong, GameObjectCollection> SortedGameObjects { get; private set; }

        internal ulong RegisterSignature(Type[] types)
        {
            ulong signature = GetSignature(types);

            if(SortedGameObjects.ContainsKey(signature) == 0)
            {
                SortedGameObjects.Add(signature, new GameObjectCollection());
            }

            return signature;
        }

        // Return a Signature for the specified types
        internal ulong GetSignature(Type[] types)
        {
            ulong result;

            foreach(Type type in types)
            {
                result |= ComponentBitPositions[type];
            }
            
            return result;
        }
    }

    internal class Scene
    {


        internal T[] GetSortedComponents<T>(ulong signature)
            where T : struct, IComponent<T>
        {
            // More performant version using Span<T>
            // Span<GameObject> gameObjectSpan = new Span<GameObject>(Data.SortedGameObjects[signature]);
            // Span<GameObject> gameObjectSpan = Data.SortedGameObjects[signature].CreateSpan(); // GameObjectCollection.CreateSpan() method.

            GameObjectCollection gameObjects = Data.SortedGameObjects[signature];

            T[] components = Scene.Components.Get<T>();
            
            T[] result = new T[gameObjects.Count];

            for(int i = 0; i < gameObjects.Count; i++)
            {
                result[i] = components[gameObjects[i].Get<T>()];
            }
        }

        // Sort a new GameObject into SortedGameObjects
        internal void SortGameObject(GameObject gameObject)
        {
            gameObject.SortBits = Data.GetSignature(gameObject.ComponentTypes);

            foreach(ulong signatureKey in SortedGameObjects.Keys)
            {
                if(signatureKey & ~(gameObject.SortBits) == 0)
                {
                    SortedGameObjects[signatureKey].AddSorted(gameObject); 
                    // GameObjectCollection.AddSorted(GameObject) will store the GameObjects position inside the sorted array within the GameObject.
                    // This will make removing the GameObject from the sorted collection possible.
                }
            }
        }

    }


}