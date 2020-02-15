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
            Collider[] colliders = (Collider[])Data.Components[typeof(Collider)].Get();
        }
    }
}
