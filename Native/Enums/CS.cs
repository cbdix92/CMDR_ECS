using System;

namespace CMDR.Native
{
	[Flags]
	internal enum CS : uint
	{
		/// <summary>
        /// Aligns the window's client area on a byte boundary (in the x direction). This style affects the width of the window and its horizontal placement on the display.
        /// </summary>
		BYTEALIGNCLIENT = 0x1000,
		/// <summary>
        /// Aligns the window on a byte boundary (in the x direction). This style affects the width of the window and its horizontal placement on the display.
        /// </summary>
		BYTEALIGNWINDOW = 0x2000,
		/// <summary>
        /// Allocates one device context to be shared by all windows in the class.
        /// </summary>
		CLASSDC = 0x0040,
		/// <summary>
        /// Sends a double-click message to the window procedure when the user double-clicks the mouse while the cursor is within a window belonging to the class.
        /// </summary>
		DBLCLKS = 0x0008,
		/// <summary>
        /// Enables the drop shadow effect on a window.
        /// </summary>
		DROPSHADOW = 0x00020000,
		/// <summary>
        /// Indicates that the window class is an application global class.
        /// </summary>
		GLOBALCLASS = 0x4000,
		/// <summary>
        /// Redraws the entire window if a movement or size adjustment changes the width of the client area.
        /// </summary>
		HREDRAW = 0x0002,
		/// <summary>
        /// Disables Close on the window menu.
        /// </summary>
		NOCLOSE = 0x0200,
		/// <summary>
        /// Allocates a unique device context for each window in the class.
        /// </summary>
		OWNDC = 0x0020,
		/// <summary>
        /// Sets the clipping rectangle of the child window to that of the parent window so that the child can draw on the parent. 
        /// </summary>
		PARENTDC = 0x0080,
		/// <summary>
        /// Saves, as a bitmap, the portion of the screen image obscured by a window of this class. 
        /// </summary>
		SAVEBITS = 0x0800,
		/// <summary>
        /// Redraws the entire window if a movement or size adjustment changes the height of the client area.
        /// </summary>
		VREDRAW = 0x0001
	}
}