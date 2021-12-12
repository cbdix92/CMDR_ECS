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

        const int bKGD = 0x624b4744;
        const int cHRM = 0x6348524d;
        const int dSIG = 0x64534947;
        const int eXIf = 0x65584966;
        const int gAMA = 0x67414d41;
        const int hIST = 0x68495354;
        const int iCCP = 0x69434350;
        const int iTXt = 0x69545874;
        const int pHYs = 0x70485973;
        const int sBIT = 0x73424954;
        const int sPLT = 0x73504c54;
        const int sRGB = 0x73524742;
        const int sTER = 0x73544552;
        const int tEXt = 0x74455874;
        const int tIME = 0x74494d45;
        const int tRNS = 0x74524e53;
        const int zTXt = 0x7a545874;

        static byte[] bitShift;
        internal static Texture Decode(BinaryReader BR)
        {


            int width = (int)BR.ReadBytes(4).ConvertInt(BitCount.Bit32);
            int height = (int)BR.ReadBytes(4).ConvertInt(BitCount.Bit32);
            byte bitDepth = BR.ReadByte();
            byte colorType = BR.ReadByte();
            byte CompressionMethod = BR.ReadByte();
            byte filterMethod = BR.ReadByte();
            byte interlaceMethod = BR.ReadByte();
			
			// Storage for ancillary chunk data
			Dictionary<string, byte[]> chunkData = new Dictionary<string, byte[]>();
            
            // Skip IHDR CRC
            BR.BaseStream.Position += 4;

            byte[] DecodedColorData = new byte[width * height * 4];

            List<byte> zlib = new List<byte>();

            int dataStride = 1;

            bool decoding = true;

            while (decoding)
            {
                
                int length = (int)BR.ReadBytes(4).ConvertInt(BitCount.Bit32);
                int chunk = (int)BR.ReadBytes(4).ConvertInt(BitCount.Bit32);
                
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

                    case bKGD:
						chunkData.Add("bKGD", BR.ReadBytes(length));
						break;

                    case cHRM:
                        chunkData.Add("cHRM", BR.ReadBytes(length));
						break;

                    case dSIG:
                        chunkData.Add("dSIG", BR.ReadBytes(length));
						break;
						
                    case eXIf:
                        chunkData.Add("eXIf", BR.ReadBytes(length));
						break;

                    case gAMA:
                        chunkData.Add("gAMA", BR.ReadBytes(length));
						break;

                    case hIST:
                        chunkData.Add("hIST", BR.ReadBytes(length));
						break;

                    case iCCP:
                        chunkData.Add("iCCP", BR.ReadBytes(length));
						break;

                    case iTXt:
                        chunkData.Add("iTXt", BR.ReadBytes(length));
						break;

                    case pHYs:
                        chunkData.Add("pHYS", BR.ReadBytes(length));
                        break;

                    case sBIT:
                        chunkData.Add("sBIT", BR.ReadBytes(length));
                        break;

                    case sPLT:
                        chunkData.Add("sPLT", BR.ReadBytes(length));
						break;

                    case sRGB:
                        chunkData.Add("sRGB", BR.ReadBytes(length));
                        break;

                    case sTER:
                        chunkData.Add("sTER", BR.ReadBytes(length));
						break;

                    case tEXt:
                        chunkData.Add("tEXt", BR.ReadBytes(length));
						break;

                    case tIME:
                        chunkData.Add("tIME", BR.ReadBytes(length));
						break;

                    case tRNS:
                        chunkData.Add("tRNS", BR.ReadBytes(length));
						break;
                    
                    case zTXt:
                        chunkData.Add("zTXt", BR.ReadBytes(length));
						break;

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
