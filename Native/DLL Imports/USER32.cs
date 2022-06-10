using System;
using System.Runtime.InteropServices;
using ATOM = System.UInt16;
using LRESULT = System.IntPtr;
using HHOOK = System.IntPtr;
using HWND = System.IntPtr;

namespace CMDR.Native
{
    internal static partial class Win
    {
		const string User32 = "user32.dll";

		/// <summary>
		/// Passes the hook information to the next hook procedure in the current hook chain. A hook procedure can call this function either before or after processing the hook information.
		/// </summary>
		/// <param name="hhk"> This parameter is ignored. </param>
		/// 
		/// <param name="nCode"> The hook code passed to the current hook procedure. 
		/// The next hook procedure uses this code to determine how to process the hook information. </param>
		/// 
		/// <param name="wParam"> The wParam value passed to the current hook procedure. 
		/// The meaning of this parameter depends on the type of hook associated with the current hook chain. </param>
		/// 
		/// <param name="lParam"> The lParam value passed to the current hook procedure. 
		/// The meaning of this parameter depends on the type of hook associated with the current hook chain. </param>
		/// 
		/// <returns> This value is returned by the next hook procedure in the chain. The current hook procedure must also return this value. 
		/// The meaning of the return value depends on the hook type. 
		/// For more information, see the descriptions of the individual hook procedures. </returns>
		/// <see href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-callnexthookex"/> Microsoft Docs </see>
		[DllImport(User32, SetLastError = true)]
		internal static extern LRESULT CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

		/// <summary>
		/// Creates an overlapped, pop-up, or child window with an extended window style.
		/// </summary>
		/// <returns> If the function succeeds, the return value is a handle to the new window. If the function fails, the return value is NULL. </returns>
		/// <see href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-createwindowexw"/> Microsoft Docs </see>
		[DllImport(User32, SetLastError = true)]
		internal static extern HWND CreateWindowExW(
			WS dwExStyle,
			string lpClassName,
			string lpWindowName,
			WS dwStyle,
			int x,
			int y,
			int nWidth,
			int nHeight,
			HWND hWndParent,
			IntPtr hMenu,
			IntPtr hInstance,
			IntPtr lpParam
			);
		
		/// <summary>
		/// The GetMonitorInfo function retrieves information about a display monitor.
		/// </summary>
		/// <param name="hMonitor"> A handle to the display monitor of interest. </param>
		/// <param name="lpmi"> A pointer to a MONITORINFOEX structure that receives information about the specified display monitor. </param>
		/// <returns> If the function succeeds, the return value is nonzero. If the function fails, the return value is zero. </returns>
		/// <see href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getmonitorinfow"/> Microsoft Docs </see>
		[DllImport(User32, SetLastError = true)]
		internal static extern bool GetMonitorInfoW(IntPtr hMonitor, [MarshalAs(UnmanagedType.LPStruct)] ref MONITORINFOEXW lpmi);

		/// <summary>
		/// Registers a window class for subsequent use in calls to the CreateWindow or CreateWindowEx function.
		/// </summary>
		/// <param name="wnd"> A pointer to a WNDCLASSEX structure. 
		/// You must fill the structure with the appropriate class attributes before passing it to the function. </param>
		/// <returns> If the function succeeds, the return value is a class atom that uniquely identifies the class being registered.
		/// If the function fails, the return value is zero. </returns>
		/// <see href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-registerclassexw"/> Microsoft Docs </see>
		[DllImport(User32, SetLastError = true)]
		internal static extern ATOM RegisterClassExW([MarshalAs(UnmanagedType.LPStruct)][In] ref WNDCLASSEXW wnd);

		/// <summary>
		/// Installs an application-defined hook procedure into a hook chain. You would install a hook procedure to monitor the system for certain types of events.
		/// These events are associated either with a specific thread or with all threads in the same desktop as the calling thread.
		/// </summary>
		/// <param name="idHook"> The type of hook procedure to be installed. <see cref="WH"/> </param>
		/// 
		/// <param name="lpfn"> A pointer to the hook procedure. If the dwThreadId parameter is zero or specifies the identifier of a thread created by a different
		/// process, the lpfn parameter must point to a hook procedure in a DLL. Otherwise, lpfn can point to a hook procedure in the code associated with the current process. </param>
		/// 
		/// <param name="hmod"> A handle to the DLL containing the hook procedure pointed to by the lpfn parameter. 
		/// The hMod parameter must be set to NULL if the dwThreadId parameter specifies a thread created by the current process 
		/// and if the hook procedure is within the code associated with the current process. </param>
		/// 
		/// <param name="dwThreadID"> The identifier of the thread with which the hook procedure is to be associated. 
		/// For desktop apps, if this parameter is zero, the hook procedure is associated with all existing threads running in 
		/// the same desktop as the calling thread. For Windows Store apps, see the Remarks section. </param>
		/// 
		/// <returns> If the function succeeds, the return value is the handle to the hook procedure. If the function fails, the return value is NULL. </returns>
		/// <see href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-setwindowshookexw"/> Microsoft Docs </see>
		[DllImport(User32, SetLastError = true)]
		internal static extern HHOOK SetWindowsHookExW(WH idHook, HookProc lpfn, IntPtr hmod, uint dwThreadID);

		/// <summary>
		/// Unregisters a window class, freeing the memory required for the class.
		/// </summary>
		/// <param name="lpClassName"> A null-terminated string or a class atom. If lpClassName is a string, it specifies the window class name. </param>
		/// <param name="hInstance"> A handle to the instance of the module that created the class. </param>
		/// <returns> If the function succeeds, the return value is nonzero. 
		/// If the class could not be found or if a window still exists that was created with the class, the return value is zero. </returns>
		/// <see href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-unregisterclassw"/> Microsoft Docs </see>
		[DllImport(User32, SetLastError = true)]
		internal static extern bool UnregisterClassW(string lpClassName, IntPtr hInstance);
	}
}
