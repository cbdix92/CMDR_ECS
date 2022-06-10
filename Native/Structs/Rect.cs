using System;
using System.Runtime.InteropServices;

namespace CMDR.Native
{
	[StructLayout(LayoutKind.Sequential)]
	internal struct Rect
	{
		/// <summary>
        /// Specifies the x-coordinate of the upper-left corner of the rectangle.
        /// </summary>
		public long Left;
		/// <summary>
        /// Specifies the y-coordinate of the upper-left corner of the rectangle.
        /// </summary>
		public long Top;
		/// <summary>
        /// Specifies the x-coordinate of the lower-right corner of the rectangle.
        /// </summary>
		public long Right;
		/// <summary>
        /// Specifies the y-coordinate of the lower-right corner of the rectangle.
        /// </summary>
		public long Bottom;
		
		internal Rect(long left, long top, long right, long bottom)
		{
			(Left, Top, Right, Bottom) = (left, top, right, bottom);
		}
	}
	
}