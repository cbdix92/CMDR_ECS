using System;
using System.Runtime.InteropServices;
using ATOM = System.UInt16;
using LRESULT = System.IntPtr;
using HHOOK = System.IntPtr;
using HWND = System.IntPtr;
using HDC = System.IntPtr;

namespace CMDR.Native
{
    internal static partial class Win
    {
		const string User32 = "user32.dll";

		/// <summary>
		/// Passes the hook information to the next hook procedure in the current hook chain. A hook procedure can call this function either before or after processing the hook information.
		/// </summary>
		/// <param name="hhk"> This parameter is ignored. </param>
		/// <param name="nCode"> The hook code passed to the current hook procedure. 
		/// The next hook procedure uses this code to determine how to process the hook information. </param>
		/// <param name="wParam"> The wParam value passed to the current hook procedure. 
		/// The meaning of this parameter depends on the type of hook associated with the current hook chain. </param>
		/// <param name="lParam"> The lParam value passed to the current hook procedure. 
		/// The meaning of this parameter depends on the type of hook associated with the current hook chain. </param>
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
			WS_EX dwExStyle,
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
		/// The <see cref="GetDC"> function retrieves a handle to a device context (DC) for the client area of a specified window or for the entire screen.
		/// </summary>
		/// <param name="hWnd"> A handle to the window whose DC is to be retrieved. If this value is NULL, GetDC retrieves the DC for the entire screen. </param>
		/// <returns> If the function succeeds, the return value is a handle to the DC for the specified window's client area. If the function fails, the return value is NULL. </returns>
		/// <see href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getdc"> MICROSOFT DOCS </see>
		[DllImport(User32, SetLastError = true)]
		internal static extern HDC GetDC(HWND hWnd);
		
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
		/// Retrieves information about the raw input device.
		/// </summary>
		/// <param name="hDevice"> A handle to the raw input device. This comes from the hDevice member of RAWINPUTHEADER or from <see cref="GetRawInputDeviceList">. </param>
		/// <param name="uiCommand"> Specifies what data will be returned in pData. <see cref=""> </param>
		/// <param name="pcbSize"> A pointer to a buffer that contains the information specified by uiCommand. </param>
		/// <returns> If successful, this function returns a non-negative number indicating the number of bytes copied to pData.
		/// If pData is not large enough for the data, the function returns -1.
		/// If pData is NULL, the function returns a value of zero. </returns>
		/// <see href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getrawinputdeviceinfow"/> Microsoft Docs </see>
		[DllImport(User32, SetLastError = true)]	
		internal static extern uint GetRawInputDeviceInfoW(IntPtr hDevice, RIDI uiCommand, IntPtr pData, ref uint pcbSize);

		/// <summary>
		/// Enumerates the raw input devices attached to the system.
		/// </summary>
		/// <param name="pRawInputDeviceList"> An array of RAWINPUTDEVICELIST structures for the devices attached to the system. If NULL, the number of devices are returned in *puiNumDevices. </param>
		/// <param name="puiNumDevices"> If pRawInputDeviceList is NULL, the function populates this variable with the number of devices attached to the system; 
		/// otherwise, this variable specifies the number of RAWINPUTDEVICELIST structures that can be contained in the buffer to which pRawInputDeviceList points </param>
		/// <param name="cbSize"> The size of a RAWINPUTDEVICELIST structure, in bytes. </param>
		/// <returns> If the function is successful, the return value is the number of devices stored in the buffer pointed to by pRawInputDeviceList.
		/// On any other error, the function returns (UINT) -1. </returns>
		/// <see href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getrawinputdevicelist"/> Microsoft Docs </see>
		[DllImport(User32, SetLastError = true)]
		internal static extern uint GetRawInputDeviceList(RAWINPUTDEVICELIST[] pRawInputDeviceList, ref uint puiNumDevices, uint cbSize);


		[DllImport(User32, SetLastError = true)]
		internal static extern IntPtr LoadCursorW(IntPtr hInstance, string lpCursorName);

		[DllImport(User32, SetLastError = true)]
		internal static extern IntPtr LoadImageW(IntPtr hInstance, string name, uint type, int cx, int cy, LR fuLoad);

		/// <summary>
		/// Retrieves the information about the raw input devices for the current application.
		/// </summary>
		/// <param name="pRawInputDeviceList"> An array of <see cref="RAWINPUTDEVICE"> structures for the application. </param>
		/// <param name="puiNUmDevices"> The number of <see cref="RAWINPUTDEVICE"> structures in <see cref="pRawInputDevices"> </param>
		/// <param name="cbSize"> The size, in bytes of a <see cref="RAWINPUTDEVICE"> structure. </param>
		/// <returns> If successful, the function returns a non-negative number that is the number of <see cref="RAWINPUTDEVICE"> structures written to the buffer. Returns -1 if it fails. </returns>
		/// <see href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-getregisteredrawinputdevices"> MICROSOFT DOCS </see>
		[DllImport(User32, SetLastError = true)]
		internal static extern uint GetRegisteredRawInputDevices(RAWINPUTDEVICE[] pRawInputDevices, ref uint puiNumDevices, uint cbSize);

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
		/// Registers the devices that supply the raw input data.
		/// </summary>
		/// <param name="pRawInputDevices"> An array of <see cref="RAWINPUTDEVICE"> structures that represent the devices that supply the raw input. </param>
		/// <param name="uiNumDevices"> The number of <see cref="RAWINPUTDEVICE"> structures pointed to by pRawInputDevices. </param>
		/// <param name="cbSize"> The size, in bytes, of a <see cref="RAWINPUTDEVICE"> structure. </param>
		/// <returns> TRUE if the function succeeds; otherwise, FALSE. If the function fails. </returns>
		/// <see href="https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-registerrawinputdevices"> MICROSOFT DOCS </see>
		[DllImport(User32, SetLastError = true)]
		internal static extern bool RegisterRawInputDevices(RAWINPUTDEVICE[] pRawInputDevices, uint uiNumDevices, uint cbSize);

		/// <summary>
		/// Installs an application-defined hook procedure into a hook chain. You would install a hook procedure to monitor the system for certain types of events.
		/// These events are associated either with a specific thread or with all threads in the same desktop as the calling thread.
		/// </summary>
		/// <param name="idHook"> The type of hook procedure to be installed. <see cref="WH"/> </param>
		/// <param name="lpfn"> A pointer to the hook procedure. If the dwThreadId parameter is zero or specifies the identifier of a thread created by a different
		/// process, the lpfn parameter must point to a hook procedure in a DLL. Otherwise, lpfn can point to a hook procedure in the code associated with the current process. </param>
		/// <param name="hmod"> A handle to the DLL containing the hook procedure pointed to by the lpfn parameter. 
		/// The hMod parameter must be set to NULL if the dwThreadId parameter specifies a thread created by the current process 
		/// and if the hook procedure is within the code associated with the current process. </param>
		/// <param name="dwThreadID"> The identifier of the thread with which the hook procedure is to be associated. 
		/// For desktop apps, if this parameter is zero, the hook procedure is associated with all existing threads running in 
		/// the same desktop as the calling thread. For Windows Store apps, see the Remarks section. </param>
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
