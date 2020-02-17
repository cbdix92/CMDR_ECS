using System;
using CMDR.Components;

namespace CMDR.Systems
{
    public static class Physics
    {
        readonly static Type _t = typeof(Transform);
        readonly static Type _c = typeof(Collider);
        readonly static Type _s = typeof(Static);

        public static void Update()
        {
            GameObject[] gameObjects = Data.GameObjects.Get();
            Transform[] transforms = (Transform[])Data.Components[_t].Get();
            Collider[] colliders = (Collider[])Data.Components[_c].Get();

            // Update all transforms
            foreach (GameObject gameObject in gameObjects)
            {
                if (gameObject.Components.ContainsKey(_s))
                    continue;
                if (!Move(ref transforms[gameObject.Components[_t]]))
                    continue;
				SpatialIndexer.CalcGridPos(gameObject);
                if (!gameObject.Components.ContainsKey(_c))
                    continue;

                // Enter Broad Phase Collsion Check

                // Query spatial indexer
                int[] gameObjectColliders = SpatialIndexer.GetNearbyColliders((Collider)colliders[gameObject[_c]]);
                foreach (int i in gameObjectColliders)
                {
                    // Perform Rect Check
                    bool b1 = transforms[gameObject[_t]].X - transforms[gameObjects[i][_t]].X <= colliders[gameObjects[i][_c]].Width;
                    bool b2 = transforms[gameObject[_t]].Y - transforms[gameObjects[i][_t]].Y <= colliders[gameObjects[i][_c]].Height;
                    if (!b1 && b2)
                        continue;

                    // Enter Narrow Phase Collision Check

                    // BitCollider Check
                    if (!BitCollider.BitColliderCheck(transforms[gameObject[_t]], transforms[gameObjects[i][_t]],
                                                      colliders[gameObject[_c]], colliders[gameObjects[i][_c]]))
                        continue;

                    // Resolve Collision
                    // ...

                }

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
        public static void RectCheck()
        {

        }
    }
}
