using System;

namespace CMDR
{
    [Flags]
    public enum WindowStyle : uint
    {
        /// <summary> The window has a thin-line border. </summary>
        BORDER = 0x00800000,

        /// <summary> The window has a title bar (includes the WS_BORDER style). </summary>
        CAPTION = 0x00C00000,
        
        /// <summary> The window is initially disabled. A disabled window cannot receive input from the user.
        /// To change this after a window has been created, use the EnableWindow function. </summary>
        DISABLED = 0x08000000,
        
        /// <summary> The window has a border of a style typically used with dialog boxes. A window with this style
        /// cannot have a title bar. </summary>
        DLGFRAME = 0x00400000,
        
        /// <summary> The window is initially minimized. Same as the WS_MINIMIZE style. </summary>
        ICONIC = 0x20000000,
        
        /// <summary> The window is initially maximized. </summary>
        MAXIMIZE = 0x01000000,
        
        /// <summary> The window has a maximize button. Cannot be combined with the WS_EX_CONTEXTHELP style.
        /// The WS_SYSMENU style must also be specified. </summary>
        MAXIMIZEBOX  = 0x00010000,
        
        /// <summary> he window is initially minimized. Same as the WS_ICONIC style. </summary>
        MINIMIZE = 0x20000000,
        
        /// <summary> The window has a minimize button. Cannot be combined with the WS_EX_CONTEXTHELP style.
        /// The WS_SYSMENU style must also be specified. </summary>
        MINIMIZEBOX = 0x00020000,
        
        /// <summary> The window is an overlapped window. An overlapped window has a title bar and a border.
        /// Same as the WS_TILED style. </summary>
        OVERLAPPED = 0x00000000,
        
        /// <summary> The window is an overlapped window. Same as the WS_TILEDWINDOW style. </summary>
        OVERLAPPEDWINDOW = (OVERLAPPED | CAPTION | SYSMENU | THICKFRAME | MINIMIZEBOX | MAXIMIZEBOX),
        
        /// <summary> The window has a sizing border. Same as the WS_THICKFRAME style. </summary>
        SIZEBOX = 0x00040000,
        
        /// <summary> The window has a window menu on its title bar. The WS_CAPTION style must also be specified. </summary>
        SYSMENU = 0x00080000,
        
        /// <summary> 	The window is a control that can receive the keyboard focus when the user presses the TAB key.
        /// Pressing the TAB key changes the keyboard focus to the next control with the WS_TABSTOP style.
        /// You can turn this style on and off to change dialog box navigation. To change this style after a
        /// window has been created, use the SetWindowLong function. For user-created windows and modeless dialogs
        /// to work with tab stops, alter the message loop to call the IsDialogMessage function. </summary>
        TABSTOP = 0x00010000,
        
        /// <summary> The window has a sizing border. Same as the WS_SIZEBOX style. </summary>
        THICKFRAME = 0x00040000,
        
        /// <summary> The window is an overlapped window. Same as the WS_OVERLAPPEDWINDOW style. </summary>
        TILEDWINDOW = (OVERLAPPED | CAPTION | SYSMENU | THICKFRAME | MINIMIZEBOX | MAXIMIZEBOX),
        
        /// <summary> The window is initially visible.
        /// This style can be turned on and off by using the ShowWindow or SetWindowPos function. </summary>
        VISIBLE = 0x10000000,
        
        /// <summary> The window has a vertical scroll bar. </summary>
        VSCROLL = 0x00200000
    }
}