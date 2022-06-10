using System;

namespace CMDR.Native
{
    internal delegate IntPtr HookProc(int code, IntPtr wParam, IntPtr lParam);
	internal delegate IntPtr WNDPROC(IntPtr hWnd, uint uMsg, IntPtr wParam, IntPtr lParam);
}
