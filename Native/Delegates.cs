using System;

namespace CMDR.Native
{
    internal delegate IntPtr HookProc(int code, IntPtr wParam, IntPtr lParam);
}
