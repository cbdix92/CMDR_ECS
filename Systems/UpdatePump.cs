using System;
using System.Windows.Forms;

namespace CMDR.Systems
{
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

            RenderUpdates.Handler += Render.Update;
            PhysicsUpdates.Handler += Physics.Update;
            PhysicsUpdates.Handler += Input.DetectKeys;

        }
    }
}
