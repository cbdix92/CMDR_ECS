using System;
using System.Runtime.InteropServices;

namespace CMDR.Native
{
    internal static partial class Win
    {
        const string Kernel32 = "kernel32.dll";


		/// <summary>
		/// Frees the loaded dynamic-link library (DLL) module and, if necessary, decrements its reference count. 
		/// When the reference count reaches zero, the module is unloaded from the address space of the calling process and the handle is no longer valid.
		/// </summary>
		/// <param name="hModule"> A handle to the loaded library module. <see cref="LoadLibrary(string)"/> </param>
		/// <returns>If the function succeeds, the return value is nonzero. If the function fails, the return value is zero. </returns>
		[DllImport(Kernel32, SetLastError = true)]
		internal static extern bool FreeLibrary(IntPtr hModule);

		/// <summary>
		/// Retrieves the thread identifier of the calling thread.
		/// </summary>
		/// <returns> The return value is the thread identifier of the calling thread. </returns>
		[DllImport(Kernel32, SetLastError = true)]
		internal static extern uint GetCurrentThreadID();

		/// <summary>
		/// Retrieves a module handle for the specified module. The module must have been loaded by the calling process.
		/// </summary>
		/// <param name="lpModuleName"> The name of the loaded module (either a .dll or .exe file). 
		/// If the file name extension is omitted, the default library extension .dll is appended. 
		/// If this parameter is NULL, GetModuleHandle returns a handle to the file used to create the calling process (.exe file). </param>
		/// <returns> If the function succeeds, the return value is a handle to the specified module.
		/// If the function fails, the return value is NULL. </returns>
		[DllImport(Kernel32, SetLastError = true)]
		internal static extern IntPtr GetModuleHandleW([MarshalAs(UnmanagedType.LPWStr)] [In] string lpModuleName);

		/// <summary>
		/// Retrieves the address of an exported function (also known as a procedure) or variable from the specified dynamic-link library (DLL).
		/// </summary>
		/// <param name="hModule"> A handle to the DLL module that contains the function or variable. <see cref="LoadLibrary(string)"/> or <seealso cref="GetModuleHandleW(string)"/> </param>
		/// <param name="procName"> The function or variable name, or the function's ordinal value. </param>
		/// <returns> If the function succeeds, the return value is the address of the exported function or variable. If the function fails, the return value is NULL. </returns>
		[DllImport(Kernel32, CharSet = CharSet.Ansi, ExactSpelling = true, SetLastError = true)]
		internal static extern IntPtr GetProcAddress(IntPtr hModule, string procName);

		[DllImport(Kernel32, SetLastError = true)]
		internal static extern IntPtr LoadLibrary([MarshalAs(UnmanagedType.LPStr)] string lpFileName);

		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern void SetLastError(uint dwErrorCode);
	}
}
