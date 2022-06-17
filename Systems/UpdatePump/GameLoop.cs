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

        internal static void Start()
        {

            Time.Start();

            CreateUpdater(1000, Render.Update);
            CreateUpdater(100, Physics.Update);
            CreateUpdater(100, Input.Update);

            //while (!Glfw.WindowShouldClose(Display.Window))
            while(Win.CurrentWindow != null)
            {
                foreach (Updater updater in Updaters)
                    updater.Update(GameTime);
                
                Win.HandleMessages();

                Glfw.PollEvents();
            }
            Glfw.Terminate();

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
