using System.Diagnostics;
using System.Collections.Generic;
using GLFW;

namespace CMDR.Systems
{

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

            CreateUpdater(1000, Render.Update);
            CreateUpdater(100, Physics.Update);
            CreateUpdater(100, Input.Update);

            while (!Glfw.WindowShouldClose(Display.Window))
            {
                foreach (Updater updater in Updaters)
                    updater.Update(GameTime);

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
