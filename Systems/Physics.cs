﻿using System;
using CMDR.Components;
using System.Collections.Generic;

namespace CMDR.Systems
{
    public static class Physics
    {
        readonly static Type _transformT = typeof(Transform);
        readonly static Type _colliderT = typeof(Collider);

        public static void Update(long ticks)
        {
            Scene scene = SceneManager.ActiveScene;

            SGameObject[] gameObjects = scene.GameObjects.Get();

            Transform[] transforms = scene.Components.Get<Transform>();
            Collider[] colliders = scene.Components.Get<Collider>();

            // Update all transforms
            foreach (SGameObject gameObject in gameObjects)
            {

                int transformID = gameObject.Get<Transform>();
                int colliderID = gameObject.Get<Collider>();

                #region COLLISION_LOGIC

                bool result = transformID != -1 && colliderID != -1 && Move(transforms[transformID]);

                switch (result)
                {
                    case true:

                        Move(transforms[transformID]);
                        
                        SpatialIndexer.CalcGridPos(ref colliders[colliderID], transforms[transformID]);
                        int[] gameObjectColliders = SpatialIndexer.GetNearbyColliders(colliders[colliderID]);

                        foreach (int i in gameObjectColliders)
                        {
                            int transform2 = gameObjects[i].Get<Transform>();
                            int collider2 = gameObjects[i].Get<Collider>();

                            // Bounding box checks
                            bool b1 = transforms[transformID].X - transforms[transform2].X <= colliders[collider2].Width;
                            bool b2 = transforms[transformID].Y - transforms[transform2].Y <= colliders[collider2].Height;

                            // Compare bounding box checks and then bit collider check
                            if (!b1 && b2 && !BitCollider.BitColliderCheck(transforms[transformID], transforms[transform2], colliders[colliderID], colliders[collider2]))
                                continue; // Resolve collision here ...
                        }

                        continue;

                    case false:
                        // Nothing needs to be done. Restart the loop
                        continue;
                }
                #endregion



                /*
                if (transformID == -1)
                    continue;
                if (colliderID == -1)
                {
                    Move(transforms[transformID]);
                    continue;
                }
                if (colliders[colliderID].Static)
                    continue;
                if (!Move(transforms[transformID]))
                    continue;
                #endregion

                #region COLLISION_CHECK

                #region BROAD_PHASE
                // Query spatial indexer for nearby gameobjects
                SpatialIndexer.CalcGridPos(ref colliders[colliderID], transforms[transformID]);
                int[] gameObjectColliders = SpatialIndexer.GetNearbyColliders(colliders[colliderID]);
                
                // Stop the object from trying to collide with itself
                if (gameObjectColliders.Length == 0)
                    continue;

                foreach (int i in gameObjectColliders)
                {
                    int transform2 = gameObjects[i].Get<Transform>();
                    int collider2 = gameObjects[i].Get<Collider>();
                    // Bounding box check
                    bool b1 = transforms[transformID].X - transforms[transform2].X <= colliders[collider2].Width;
                    bool b2 = transforms[transformID].Y - transforms[transform2].Y <= colliders[collider2].Height;
                    if (!b1 && b2)
                        continue;
                        
                #endregion
                

                #region NARROW_PAHSE

                    // Bit collider Check
                    if (!BitCollider.BitColliderCheck(transforms[transformID], transforms[transform2], 
                                                      colliders[colliderID], colliders[collider2]))
                        continue;

                #endregion
                */

            }
        }

        public static bool Move(Transform transform)
        {
            transform.X += transform.Xvel;
            transform.Y += transform.Yvel;
            return transform.Xvel != 0 || transform.Yvel != 0;
        }
        public static bool CanMove(Transform transform)
        {
            return transform.Xvel != 0 || transform.Yvel != 0;
        }
    }
}
