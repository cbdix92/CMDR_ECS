using System;
using CMDR.Components;

namespace CMDR
{
    public static class Physics
    {
        public static void Update()
        {
            GameObject[] gameObjects = Data.GameObjects.Get();
            Transform[] transforms = (Transform[])Data.Components[typeof(Transform)].Get();
            var colliders = Data.Components[typeof(Collider)].Get();

            // Update all transforms
            foreach (GameObject gameObject in gameObjects)
            {
                if (gameObject.Components.ContainsKey(typeof(Static)))
                    continue;
                if (!Move(ref transforms[gameObject.Components[typeof(Transform)]]))
                    continue;
                if (!gameObject.Components.ContainsKey(typeof(Collider)))
                    continue;

                // Enter Broad Phase Collsion Check

                // Query spatial indexer
                // ...

                    // Perform Rect Check
                    // ...

                    // Enter Narrow Phase Collision Check

                    // BitCollider Check
                    // ...

                    // Resolve Collision
                    // ...

            }
        }
        public static bool Move(ref Transform transform)
        {
            if (transform.Xvel == 0 && transform.Yvel == 0)
                return false;
            transform.X += transform.Xvel;
            transform.Y += transform.Yvel;
            return true;
        }
    }
}
