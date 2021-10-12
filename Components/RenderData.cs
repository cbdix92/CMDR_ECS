using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using ImageDecoder;

namespace CMDR.Components
{
    public delegate long FrameTimer(int _nextFrame);
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

        internal Animation AnimationData;

        public string currentState;

        public void FromFile(string src)
        {
            Receive();
            ImgData = Load.LoadFromFile(src);
            Send();
        }

        public void CreateAnimation(string name, string[] paths, FrameTimer frameTimer)
        {
            Receive();
            if (AnimationData == null)
                AnimationData = new Animation();
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

            AnimationData.InsertFrames(name, _, frameTimer);

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

    internal class Animation
    {
        internal long lastFrame;
        internal long stepSize;
        internal int nextFrame;

        internal FrameTimer frameTimer;

        internal Dictionary<string, List<Texture>> data;

        internal Texture Get(long ticks, string name)
        {
            if (data == null)
                throw new NullReferenceException("Animation frames never loaded!");

            stepSize = frameTimer(nextFrame) * (Stopwatch.Frequency / 1000);

            if (nextFrame == data[name].Count)
            {
                nextFrame = 0;
            }
            if (ticks >= lastFrame + stepSize)
            {
                lastFrame = ticks;
                return data[name][nextFrame++];
            }
            else
            {
                return data[name][nextFrame];
            }

        }

        internal void InsertFrames(string name, Texture[] image, FrameTimer frameTimer)
        {
            this.frameTimer = frameTimer;
            stepSize = this.frameTimer(nextFrame) * (Stopwatch.Frequency / 1000);

            if (data == null)
                data = new Dictionary<string, List<Texture>>();

            if (!data.ContainsKey(name))
                data.Add(name, new List<Texture>(image));


        }
    }
}