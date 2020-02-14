using System;

namespace CMDR.Components
{
    public interface IProfiler
    {
        // Base class used to set initial state of Components
        void Profile(ref IComponent component);
    }
    public abstract class TransformProfiler : IProfiler
    {
        public float X;
        public float Y;
        public int Z;

        public float Xvel;
        public float Yvel;


        public virtual void Profile(ref IComponent componet)
        {
            Transform target = (Transform)componet;
            
            target.X = X;
            target.Y = Y;
            target.Z = Z;

            target.Xvel = Xvel;
            target.Yvel = Yvel;
        }
    }
}
