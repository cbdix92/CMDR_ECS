using System;
using CMDR.Components;
using System.Collections.Generic;

namespace CMDR.Systems
{
    public static class Physics
    {

        public static void Update(long ticks)
        {
            bool cameraMoved = MoveCamera(ticks);

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

                int result = Convert.ToByte(transformID != -1) | (Convert.ToByte(colliderID != -1) << 1) | (Convert.ToByte(Move(transforms[transformID])) << 2);

                switch (result)
                {
                    case 7:

                        Transform transform = transforms[gameObject.Get<Transform>()];
                        Collider collider = colliders[gameObject.Get<Collider>()];

                        SpatialIndexer.CalcGridPos(ref collider, transform);
                        int[] gameObjectColliders = SpatialIndexer.GetNearbyColliders(collider);

                        foreach (int i in gameObjectColliders)
                        {

                            Transform transform2 = transforms[gameObjects[i].Get<Transform>()];
                            Collider collider2 = colliders[gameObjects[i].Get<Collider>()];
                            // Bounding box checks
                            bool rectCol = 
                                   transform.X <= transform2.X + collider2.ScaleX
                                && transform.X + collider.ScaleX >= transform2.X
                                && transform.Y <= transform2.Y + collider2.ScaleY
                                && transform.Y + collider.ScaleY >= transform2.Y
                                && transform.Z <= transform2.Z + collider2.ScaleZ
                                && transform.Z + collider.ScaleZ >= transform2.Z;


                            // Compare bounding box checks and then bit collider check
                            if (rectCol && BitCollider.BitColliderCheck(transform, transform2, collider, collider2))
                            {
                                //Debug(ticks);
                                continue; // Resolve collision here ...
                            }
                        }

                        continue;

                    case 3:
                        // Nothing needs to be done. Restart the loop
                        if (colliders[colliderID].GridKeys == null )
                            SpatialIndexer.CalcGridPos(ref colliders[colliderID], transforms[transformID]);
                        continue;
                    default:
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
        public static void Debug(long ticks)
        {
            //Console.Clear();
            Console.WriteLine("Collision Deteted: " + ticks.ToString());
        }
        public static bool Move(Transform transform)
        {
            transform.X += transform.Xvel;
            transform.Y += transform.Yvel;
            return transform.Xvel != 0 || transform.Yvel != 0;
        }
        public static bool MoveCamera(long ticks)
        {
            Camera.MoveCamera(Camera.Xvel);
            Camera.StrafeCamera(Camera.Zvel);
            Camera.Y += Camera.Yvel;
            return Camera.Xvel + Camera.Yvel + Camera.Zvel != 0;
        }
        public static bool CanMove(Transform transform)
        {
            return transform.Xvel != 0 || transform.Yvel != 0;
        }
    }
}
