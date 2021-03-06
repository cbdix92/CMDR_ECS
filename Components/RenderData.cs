﻿using System;
using System.Drawing;
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

        public Image ImgData { get; internal set; }

        internal Animation AnimationData;

        public string currentState;

        public void FromFile(string src)
        {
            Receive();
            try
            {
                ImgData = Image.FromFile(src);
            }
            catch (FileNotFoundException)
            {
                throw new FileNotFoundException($"'{src}', RenderData");
            }
            Send();
        }

        public void CreateAnimation(string name, string[] paths, int stepSize)
        {
            Receive();
            if (AnimationData == null)
                AnimationData = new Animation();
            // temp debug
            currentState = name;

            Image[] _ = new Image[paths.Length];

            for (int i = 0; i < paths.Length; i++)
            {
                try
                {
                    _[i] = Image.FromFile(paths[i]);
                }
                catch (FileNotFoundException)
                {
                    throw new FileNotFoundException($"'{paths[i]}', Animation");
                }
            }
            AnimationData.InsertFrames(name, _, stepSize);

            Send();
        }

        public Image GetRender(long ticks)
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
        internal long _lastFrame;
        internal long _stepSize;
        internal int _nextFrame;

        internal Dictionary<string, List<Image>> _data;

        internal Image Get(long ticks, string name)
        {
            if (_data == null)
                throw new NullReferenceException("Animation frames never loaded!");

            if (_nextFrame == _data[name].Count)
            {
                _nextFrame = 0;
            }
            if (ticks >= _lastFrame+_stepSize)
            {
                _lastFrame = ticks;
                return _data[name][_nextFrame++];
            }
            else
            {
                return _data[name][_nextFrame];
            }

        }

        internal void InsertFrames(string name, Image[] image, long stepSize)
        {
            _stepSize = stepSize*(Stopwatch.Frequency/1000);

            if (_data == null)
                _data = new Dictionary<string, List<Image>>();

            if (!_data.ContainsKey(name))
                _data.Add(name, new List<Image>(image));


        }
    }
}
