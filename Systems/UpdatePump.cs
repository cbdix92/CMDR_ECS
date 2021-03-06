﻿using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace CMDR.Systems
{
    /*
    public class Update
    {
        public event EventHandler<EventArgs> Handler;
        private Timer _timer;
        public int Interval { get => _timer.Interval; set => _timer.Interval = value; }
        public Update(int interval)
        {
            _timer = new Timer();
            Interval = interval;
            _timer.Tick += OnTimeUp;
            _timer.Start();
        }
        private void OnTimeUp(object caller, EventArgs e)
        {
            if (Handler != null)
            {
                Handler(this, e);
            }
        }
        public void Dispose()
        {
            _timer.Dispose();
        }
    }
    internal static class UpdatePump
    {
        public static Update RenderUpdates;
        public static Update PhysicsUpdates;

        public static void Init()
        {
            RenderUpdates = new Update(10);
            PhysicsUpdates = new Update(10);

            //RenderUpdates.Handler += Render.Update;
            //PhysicsUpdates.Handler += Physics.Update;
            //PhysicsUpdates.Handler += Input.DetectKeys;

        }
    }
    */


    internal delegate void UpdateHandler(long ticks);

    internal struct Updater
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
        internal static Stopwatch Time;

        internal static List<Updater> Updaters = new List<Updater>();

        internal static Thread thread;

        internal static void Start()
        {

            thread = new Thread(() =>
                {
                    Time = new Stopwatch();
                    Time.Start();

                    CreateUpdater(100L, Render.Update);
                    CreateUpdater(100L, Physics.Update);
                    CreateUpdater(100L, Input.Update);

                    while (Running)
                    {
                        foreach (Updater updater in Updaters)
                            updater.Update(Time.ElapsedTicks);
                    }
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }
        internal static void CreateUpdater(long persecond, UpdateHandler update)
        {
            Updater foo = new Updater();
            foo.PerSecond = persecond;
            foo.Handler += update;
            Updaters.Add(foo);
        }
    }
}
