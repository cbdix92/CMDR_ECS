using System;
using System.Linq;
using System.Text;
using System.IO;


namespace CMDR
{
    public static class Decoder
    {

        internal static readonly UInt64 PNG_SIGNATURE = 0x89504E470D0A1A0A;
        internal static readonly byte[] JPEG_SIGNATURE = new byte[] { 255, 216, 255 };

        /// <summary>
        /// Load a file from a specified path and returns a Texture struct.
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static Texture LoadFromFile(string path)
        {
            using (BinaryReader BR = new BinaryReader(File.OpenRead(path), Encoding.BigEndianUnicode))
            {
                //Test PNG
                ulong n = (ulong)BR.ReadBytes(8).ConvertInt(BitCount.Bit64, true);
                if (n == PNG_SIGNATURE)
                {

                    // Check for IHDR header(4 bytes)
                    BR.BaseStream.Position += 4; // Skip IHDR header length since this is always the same.
                    UInt32 IHDR = (UInt32)BR.ReadBytes(4).ConvertInt(BitCount.Bit32);
                    if (IHDR != 0x49484452)
                        throw new FileLoadException("IHDR");

                    // Store Width(4 bytes), Heigth(4 bytes)
                    //texture.Width = (int)BR.ReadBytes(4).ConvertInt(BitCount.Bit32);
                    //texture.Height = (int)BR.ReadBytes(4).ConvertInt(BitCount.Bit32);

                    //BR.BaseStream.Position -= 8;

                    return DecodePNG.Decode(BR);
                }

                //Test JPEG
                BR.BaseStream.Position = 0;
                if (BR.ReadBytes(3).SequenceEqual(JPEG_SIGNATURE))
                {
                    throw new NotImplementedException("JPEG support is not yet implemented");
                }
                throw new FileNotSupported(path + " is not a supported File");
            }
        }
    }
}
