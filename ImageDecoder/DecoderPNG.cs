using System;
using System.IO;
using System.Collections.Generic;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;

namespace CMDR
{
    internal static class DecodePNG
    {
        const int PLTE = 0x504c5445;
        const int IDAT = 0x49444154;
        const int IEND = 0x49454e44;
		
		private readonly static Dictionary<int, string> _ancillaryChunks = new Dictionary<int, string>()
		{
			{ 0x624b4744, "bKGD" },
			{ 0x6348524d, "cHRM" },
			{ 0x64534947, "dSIG" },
			{ 0x65584966, "eXIf" },
			{ 0x67414d41, "gAMA" },
			{ 0x68495354, "hIST" },
			{ 0x69434350, "iCCP" },
			{ 0x69545874, "iTXt" },
			{ 0x70485973, "pHYs" },
			{ 0x73424954, "sBIT" },
			{ 0x73504c54, "sPLT" },
			{ 0x73524742, "sRGB" },
			{ 0x73544552, "sTER" },
			{ 0x74455874, "tEXt" },
			{ 0x74494d45, "tIME" },
			{ 0x74524e53, "tRNS" },
			{ 0x7a545874, "zTXt" }
		};
		
        internal static Texture Decode(BinaryReader BR)
        {


            int width = (int)BR.ReadBytes(4).ConvertInt(BitCount.Bit32);
            int height = (int)BR.ReadBytes(4).ConvertInt(BitCount.Bit32);
            byte bitDepth = BR.ReadByte();
            byte colorType = BR.ReadByte();
            byte compressionMethod = BR.ReadByte();
            byte filterMethod = BR.ReadByte();
            byte interlaceMethod = BR.ReadByte();
			
			// Storage for ancillary chunk data
			Dictionary<string, byte[]> chunkData = new Dictionary<string, byte[]>();
            
            // Skip IHDR CRC
            BR.BaseStream.Position += 4;

            byte[] DecodedColorData = new byte[width * height * 4];

            List<byte> zlib = new List<byte>();

            int dataStride = 4;

            bool decoding = true;

            while (decoding)
            {
                
                int length = (int)BR.ReadBytes(4).ConvertInt(BitCount.Bit32);
                int chunk = (int)BR.ReadBytes(4).ConvertInt(BitCount.Bit32);
                
				if(_ancillaryChunks.ContainsKey(chunk))
				{
					chunkData.Add(_ancillaryChunks[chunk], BR.ReadBytes(length));
				}
				
                switch(chunk)
                {
                    case PLTE:
						if(length%3 != 0)
							throw new Exception("PNG PLTE CHUNK LENGTH NOT DIVISIBLE BY 3");
						if (colorType == 3)
						{
							throw new NotImplementedException("PLTE");
							break;
						}
						else if (colorType == 2 || colorType == 6)
						{
							throw new NotImplementedException("PLTE");
							break;
						}
							throw new Exception("colorType not set!");

						case IDAT:
							// Combine all IDAT chunks to form the ZLIB
							zlib.AddRange(BR.ReadBytes(length));
							break;

                    case IEND:

                        // Inflate ZLIB stream
                        using (MemoryStream data = new MemoryStream(zlib.ToArray()))
                        {
                            using (InflaterInputStream inflate = new InflaterInputStream(data))
                            {
                                //inflate.CopyTo(data);
                                inflate.Read(DecodedColorData, 0, sizeof(byte)*width*height*4);
                            }
                        }

                        float[] imgDataf = Array.ConvertAll<byte, float>(DecodedColorData, new Converter<byte, float>(ByteToF));

                        //string debug = String.Join(",", _.Select(p => p.ToString()).ToArray());
                        //File.WriteAllText("Decoded.txt", debug);

                        return new Texture(imgDataf, width, height, dataStride);

                }
                // Skip CRC for now ...
                BR.ReadBytes(4);

            }

            throw new InvalidDataException("IEND chunk not found!");
        }

        internal static float ByteToF(byte b)
        {
			// Return a normalized float
            return (float)b/255f;
        }
    }
}
