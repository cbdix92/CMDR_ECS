using System.Diagnostics;

namespace CMDR.Systems
{
    internal class Updater
    {
        private long _lastUpdate;
        private long _target;
        private int _perSecond;
        internal int PerSecond
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
}
