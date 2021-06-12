using System;
using System.Collections.Generic;
using System.Collections;
using CMDR.Components;
using CMDR.DataSys;

namespace CMDR
{
    public static class SceneManager
    {
        private static Scene _activeScene;
        public static Scene ActiveScene { get => _activeScene; }

        private static List<Scene> _loadedScenes = new List<Scene>();

        internal static void LoadScene(Scene scene)
        {
            if (_activeScene == null)
                _activeScene = scene as Scene;

            _loadedScenes.Add(scene);
            scene.ID = _loadedScenes.Count - 1;
        }
        public static void RemoveScene(int ID)
        {
            _loadedScenes.Remove(_loadedScenes[ID]);
        }
    }

    [Serializable]
    public class Scene
    {
        public int ID { get; set; }

        private object _threadLockGameObject = new object();
        private object _threadLockComponent = new object();

        internal GameObjectCollection GameObjects = new GameObjectCollection();
        internal ComponentTable Components = new ComponentTable();

        public Scene()
        {
            SceneManager.LoadScene(this);
        }
        public void Update<T>(T component)
            where T: IComponent<T>
        {
            Components.Update<T>(component);
        }

        public T Get<T>(int id)
        {
            return Components.Get<T>()[id];
        }

        public SGameObject GenerateGameObject()
        {
            lock (_threadLockGameObject)
            {
                return GameObjects.Generate(this);
            }
        }
        public T Generate<T>()
            where T: struct, IComponent<T>
        {
            lock(_threadLockComponent)
            {
                T component = Components.Generate<T>(this);
                component.Scene = this;
                return component;
            }
        }
        public void Destroy(SGameObject gameObject)
        {

            // Destroy all gameObjects components
            foreach (KeyValuePair<Type, int> component in gameObject.Components)
                Destroy(component);

            GameObjects.FinalDestroy(gameObject.ID);
        }
        internal void Destroy(KeyValuePair<Type,int> component)
        {
            // Called from the GameObject.RemoveComponent
            Components.FinalDestroy(component);
        }

    }
}
