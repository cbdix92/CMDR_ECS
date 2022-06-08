using System.Runtime.InteropServices;


namespace CMDR.Native
{
	[StructLayout(LayoutKind.Sequential)]
	internal struct Point
	{
		public int X;
		public int Y;
	}
}
