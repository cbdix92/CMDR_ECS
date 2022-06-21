using System.Diagnostics;
using System.Collections.Generic;
using CMDR.Native;

namespace CMDR.Systems
{

    public static class GameLoop
    {

        internal static Stopwatch Time = new Stopwatch();

        internal static List<Updater> Updaters = new List<Updater>();
        public static long GameTime => Time.ElapsedTicks;

        public static void Start()
        {

            Time.Start();

            CreateUpdater(1000, Render.Update);
            CreateUpdater(100, Physics.Update);
            CreateUpdater(100, Input.Update);

            while(Win.HandleMessages())
            {
                foreach (Updater updater in Updaters)
                    updater.Update(GameTime);
            }

            Win.DestroyWindow(Win.CurrentWindow);

        }
        public static void CreateUpdater(int persecond, UpdateHandler update)
        {
            Updater updater = new Updater();
            updater.PerSecond = persecond;
            updater.Handler += update;
            Updaters.Add(updater);
        }
    }
}
