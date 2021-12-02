﻿using System;
using System.IO;

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


        // Specifies if the this RenderData is to return a static image or animation
        public bool Static { get; set; }

        public Shader Shader;

        public Color Color;

        public Texture ImgData { get; internal set; }

        internal Animator2D AnimationData;

        public string currentAnimation;

        public void FromFile(string src)
        {
            Receive();
            ImgData = Load.LoadFromFile(src);
            Send();
        }

        public unsafe void CreateAnimation(string name, string[] paths, float stepSize)
        {
            Receive();
			
			// Is this needed with a struct?
            if (AnimationData.Equals(default(Animator2D)))
                AnimationData = new Animator2D();
            
            currentAnimation = name;

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

            AnimationData.InsertAnimation(name, _, stepSize);

            Send();
        }


        public Texture GetRender(long ticks)
        {
            return Static ? ImgData : AnimationData.Get(ticks, currentAnimation);
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