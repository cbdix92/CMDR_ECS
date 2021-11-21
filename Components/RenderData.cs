using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;

namespace CMDR.Components
{
    public struct RenderData : IComponent<RenderData>
    {
        #region IComponent
        public int ID { get; set; }
        public int Parent { get; set; }
        public Type Type { get; set; }
        public Scene Scene { get; set; }
        #endregion

        public bool Static { get; set; }

        public Texture ImgData { get; internal set; }

        internal Animator2D AnimationData;

        public string currentState;

        public void FromFile(string src)
        {
            Receive();
            ImgData = Load.LoadFromFile(src);
            Send();
        }

        public unsafe void CreateAnimation2D(string name, string[] paths, float stepSize)
        {
            Receive();
			
			// Is this needed with a struct?
            if (AnimationData.Equals(default(Animator2D)))
                AnimationData = new Animator2D();
            // temp debug
            currentState = name;

            Texture[] _ = new Texture[paths.Length];

            for (int i = 0; i < paths.Length; i++)
            {
                try
                {
                    _[i] = Load.LoadFromFile(paths[i]);
                }
                catch (FileNotFoundException)
                {
                    throw new FileNotFoundException($"'{paths[i]}', Animation");
                }
            }

            AnimationData.InsertFrames(name, _, stepSize);

            Send();
        }

        public Texture GetRender(long ticks)
        {
            //return AnimationData.Get(ticks, currentState);
            return Static ? ImgData : AnimationData.Get(ticks, currentState);
        }

        public void Receive()
        {
            this = Scene.Get<RenderData>(ID);
        }
        public void Send()
        {
            Scene.Update<RenderData>(this);
        }
    }
}