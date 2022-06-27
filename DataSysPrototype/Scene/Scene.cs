using System;
using System.Collections.Generic;
using System.Collections;
using CMDR.Components;


namespace CMDR
{
    [Serializable]
    public sealed class Scene
    {
        #region PUBLIC_MEMBERS

        public int ID { get; internal set; }
        
        #endregion

        #region PRIVATE_MEMBERS

        #endregion

        #region INTERNAL_MEMBERS

        internal ComponentTable Components;

        internal GameObjectCollection GameObjects;

        #endregion

        #region CONSTRUCTORS

        public Scene()
        {

            Components = new ComponentTable();

            GameObjects = new GameObjectCollection();

            SceneManager.NewScene(this);

        }

        #endregion

        #region PUBLIC_METHODS

        public void DestroyComponent<T>(int id) where T : struct, IComponent<T>
        {

        }

        public void DestroyGameObject(int id)
        {
            GameObjects.Remove(id);
        }

        public int Add(GameObject gameObject)
        {
            return GameObjects.Add(gameObject);
        }

        public T Get<T>(int id) where T : struct, IComponent<T>
        {
            // Return Component<T> of id
        }

        public GameObject Get(int id)
        {
            return GameObjects[id];
        }

        public void Pair<T>(T component, ref GameObject gameObject) where T : struct, IComponent<T>
        {
            // Pair Component<T> with GameObject
            // Store Component id and type in GameObject as well as store Component in ComponentTable 
        }

        #endregion

        #region PRIVATE_METHODS

        #endregion

        #region INTERNAL_METHODS

        #endregion

    }
}