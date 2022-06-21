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

        private readonly object _threadLockComponent;

        private readonly object _threadLockGameObject;

        #endregion

        #region INTERNAL_MEMBERS

        internal ComponentTable Components;

        internal GameObjectCollection GameObjects;

        #endregion

        #region CONSTRUCTORS

        public Scene()
        {

            _threadLockComponent = new object();

            _threadLockGameObject = new object();

            Components = new ComponentTable();

            GameObjects = new GameObjectCollection();

            SceneManager.NewScene(this);

        }

        #endregion

        #region PUBLIC_METHODS

        public void DestroyComponent<T>(int id) where T : struct, iComponent<T>
        {

        }

        public void DestroyGameObject(GameObject gameObject)
        {

        }

        public T GenerateComponent<T>() where T : struct, iComponent<T>
        {
            lock(_threadLockComponent)
            {
                
            }
        }

        public GameObject GenerateGameObject()
        {
            lock(_threadLockGameObject)
            {
                return GameObjects.Add(new GameObject(GameObjects.Count, this));
            }

        }

        public T Get<T>(int id) where T : struct, iComponent<T>
        {

        }

        #endregion

        #region PRIVATE_METHODS

        #endregion

        #region INTERNAL_METHODS

        #endregion

    }
}