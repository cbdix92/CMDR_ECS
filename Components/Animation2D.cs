using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace CMDR.Components
{
    internal struct Animation2D
    {
        internal long lastFrame;
        internal int nextFrame;
		internal Dictionary<string, List<Texture>> data;

        // Backing field is set to the number of ticks to wait between frames. Not Milliseconds!
        private float _stepSize;

        /// <summary>
        /// Get and set the number of milliseconds to wait between each frame.
        /// </summary>
        public float StepSize
        {
            get => (Stopwatch.Frequency / 1000) / _stepSize;
            set
            {
                _stepSize = value * (Stopwatch.Frequency / 1000);
            }
        }

        internal Texture Get(long ticks, string name)
        {
            if (data == null)
                throw new NullReferenceException("Animation frames never loaded!");

            if (nextFrame == data[name].Count)
                nextFrame = 0;

            if (ticks >= lastFrame + StepSize)
            {
                lastFrame = ticks;
                return data[name][nextFrame++];
            }
            else
            {
                return data[name][nextFrame];
            }

        }

        internal void InsertAnimation(string name, Texture[] images, float stepSize)
        {
            StepSize = stepSize;

            if (data == null)
                data = new Dictionary<string, List<Texture>>();

            if (!data.ContainsKey(name))
                data.Add(name, new List<Texture>(images));
        }

    }
}
