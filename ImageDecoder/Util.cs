using System;
using System.IO;
using System.Linq;
using System.Text;


namespace CMDR
{
    internal static class Util
    {
        internal static readonly byte BoolMask = 0x00000001;

        /// <summary>
        /// Converts an array of bytes into an unsigned integer value.
        /// </summary>
        /// <param name="b">The byte array that will be converted. </param>
        /// <param name="bits">Bit count for the integer that is returned. </param>
        /// <param name="useLittleEndian">Use Little-Endian when converting bytes. Otherwise use Big-Endian. </param>
        /// <returns></returns>
        internal static long ConvertInt(this byte[] b, BitCount bits, bool useLittleEndian=true)
        {
            if(!useLittleEndian)
                Array.Reverse(b);

            long result = 0;

            for (int i = 0; i < (int)bits/8; i++)
            {
                result = result << 8;
                result = result | b[i];
            }
            return result;
        }
        /// <summary>
        /// Checks if the first bit is '1' then returns true. Otherwise return false.
        /// </summary>
        /// <param name="b">Byte that is to be checked.</param>
        /// <returns></returns>
        internal static bool ToBool(this byte b)
        {
            return (b & BoolMask) == 1 ? true : false;
        }


    }
}
