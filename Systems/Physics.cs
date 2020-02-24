using System;
using CMDR.Components;
using System.Collections.Generic;

namespace CMDR.Systems
{
    public static class Physics
    {
        readonly static Type _transformT = typeof(Transform);
        readonly static Type _colliderT = typeof(Collider);

        public static void Update()
        {
            Scene scene = SceneManager.ActiveScene;

            SGameObject[] gameObjects = scene.GameObjects.Get();

            Transform[] transforms = scene.Components.Get<Transform>();
            Collider[] colliders = scene.Components.Get<Collider>();

            // Update all transforms
            foreach (SGameObject gameObject in gameObjects)
            {

                int transform;
                int collider;

                if (gameObject.Contains<Transform>())
                    transform = gameObject.Get<Transform>();
                else
                    continue;

                // Don't update Static objects
                // if => not static, objects moves, has collider
                if (!colliders[gameObject.Get<Collider>()].Static 
                 && Move(transforms[gameObject.Get<Transform>()]) 
                 && gameObject.Contains<Collider>())
                    collider = gameObject.Get<Collider>();
                else
                    continue;

                SpatialIndexer.CalcGridPos(ref colliders[collider], transforms[transform]);

                // Enter Broad Phase Collsion Check

                // Query spatial indexer
                int[] gameObjectColliders = SpatialIndexer.GetNearbyColliders(colliders[collider]);
                foreach (int i in gameObjectColliders)
                {
                    int transform2 = gameObjects[i].Get<Transform>();
                    int collider2 = gameObjects[i].Get<Collider>();
                    // Perform Rect Check
                    bool b1 = transforms[transform].X - transforms[transform2].X <= colliders[collider2].Width;
                    bool b2 = transforms[transform].Y - transforms[transform2].Y <= colliders[collider2].Height;
                    if (!b1 && b2)
                        continue;

                    // Enter Narrow Phase Collision Check

                    // BitCollider Check
                    if (!BitCollider.BitColliderCheck(transforms[transform], transforms[transform2],
                                                      colliders[collider], colliders[collider2]))
                        continue;

                    // Resolve Collision
                    // ...

                }

            }
        }
        public static bool Move(Transform transform)
        {
            if (transform.Xvel == 0 && transform.Yvel == 0)
                return false;
            transform.X += transform.Xvel;
            transform.Y += transform.Yvel;
            return true;
        }
        public static void RectCheck()
        {

        }
    }
}
