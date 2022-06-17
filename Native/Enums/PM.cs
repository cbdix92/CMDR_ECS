using System;

namespace CMDR.Native
{
    [Flags]
    internal enum PM : uint
    {
        /// <summary>
        /// Messages are not removed from the queue after processing by PeekMessage.
        /// </summary>
        NOREMOVE = 0x0000,

        /// <summary>
        /// Messages are removed from the queue after processing by PeekMessage.
        /// </summary>
        REMOVE = 0x0001,

        /// <summary>
        /// Prevents the system from releasing any thread that is waiting for the caller to go idle (see WaitForInputIdle).
        /// Combine this value with either PM_NOREMOVE or PM_REMOVE.
        /// </summary>
        NOYIELD = 0x0002
    }
}
