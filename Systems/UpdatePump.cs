using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using GLFW;

namespace CMDR.Systems
{


    public delegate void UpdateHandler(long ticks);

    internal class Updater
    {
        private long _lastUpdate;
        private long _target;
        private long _perSecond;
        internal long PerSecond
        {
            get { return _perSecond; }
            set
            {
                _perSecond = value;
                _target = Stopwatch.Frequency / value;
            }
        }

        internal event UpdateHandler Handler;

        internal void Update(long ticks)
        {
            if (_lastUpdate + _target <= ticks && Handler != null)
            {
                Handler(ticks);
                _lastUpdate = ticks;
            }
        }
    }
    public static class GameLoop
    {
        internal static bool Running = true;
        public static Stopwatch Time = new Stopwatch();

        internal static List<Updater> Updaters = new List<Updater>();

        //internal static Thread thread;

        public static long GameTime => Time.ElapsedTicks;

        internal static void Start()
        {
            /*
            thread = new Thread(() =>
                {
                    Time.Start();

                    CreateUpdater(100L, Render.Update);
                    CreateUpdater(100L, Physics.Update);
                    CreateUpdater(100L, Input.Update);

                    while (!Glfw.WindowShouldClose(Display.Window))
                    {
                        foreach (Updater updater in Updaters)
                            updater.Update(GameTime);

                        Glfw.PollEvents();
                    }
                    Glfw.Terminate();
                });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            */

            Time.Start();

            CreateUpdater(1000L, Render.Update);
            CreateUpdater(100L, Physics.Update);
            // Input requires STA apartment state. This seems to break OpenGL for whatever reason.
            //CreateUpdater(100L, Input.Update);

            while (!Glfw.WindowShouldClose(Display.Window))
            {
                foreach (Updater updater in Updaters)
                    updater.Update(GameTime);

                Glfw.PollEvents();
            }
            Glfw.Terminate();

        }
        public static void CreateUpdater(long persecond, UpdateHandler update)
        {
            Updater updater = new Updater();
            updater.PerSecond = persecond;
            updater.Handler += update;
            Updaters.Add(updater);
        }
    }
}
