using System;

namespace CMDR.Native
{
    public enum KF : int
    {
        /// <summary>
        /// Manipulates the extended key flag.
        /// </summary>
        EXTENDED = 0X0100,
        /// <summary>
        /// Manipulates the dialog mode flag, which indicates whether a dialog box is active.
        /// </summary>
        DLGMODE = 0X0800,
        /// <summary>
        /// Manipulates the menu mode flag, which indicates whether a menu is active.
        /// </summary>
        MENUMODE = 0X1000,
        /// <summary>
        /// Manipulates the context code flag.
        /// </summary>
        ALTDOWN = 0X2000,
        /// <summary>
        /// Manipulates the previous key state flag.
        /// </summary>
        REPEAT = 0X4000,
        /// <summary>
        /// Manipulates the transition state flag.
        /// </summary>
        UP = 0X8000,

    }
}
