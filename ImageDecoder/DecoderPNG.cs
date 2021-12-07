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
                        throw new NotImplementedException("PLTE");

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

                        float[] _ = Array.ConvertAll<byte, float>(DecodedColorData, new Converter<byte, float>(ByteToF));

                        //string debug = String.Join(",", _.Select(p => p.ToString()).ToArray());
                        //File.WriteAllText("Decoded.txt", debug);

                        return new Texture(_, width, height, dataStride);

                    case bKGD:
                        throw new NotImplementedException("bKGD");

                    case cHRM:
                        throw new NotImplementedException("cHRM");

                    case dSIG:
                        throw new NotImplementedException("dSIG");

                    case eXIf:
                        throw new NotImplementedException("eXIf");

                    case gAMA:
                        UInt32 Gamma = (UInt32)BR.ReadBytes(4).ConvertInt(BitCount.Bit32);
                        break;

                    case hIST:
                        throw new NotImplementedException("hIST");

                    case iCCP:
                        throw new NotImplementedException("iCCP");

                    case iTXt:
                        throw new NotImplementedException("iTXt");

                    case pHYs:
                        UInt32 pixelsPerUnitX = (UInt32)BR.ReadBytes(4).ConvertInt(BitCount.Bit32);
                        UInt32 pixelsPerUnitY = (UInt32)BR.ReadBytes(4).ConvertInt(BitCount.Bit32);
                        bool unitSpecifier = BR.ReadByte().ToBool();
                        break;

                    case sBIT:
                        switch(colorType)
                        {
                            case 0:
                                bitShift = new byte[1];
                                bitShift[0] = (byte)(bitDepth - BR.ReadByte());
                                break;
                            case 2:
                                bitShift = new byte[3];
                                bitShift[0] = (byte)(bitDepth - BR.ReadByte());
                                bitShift[1] = (byte)(bitDepth - BR.ReadByte());
                                bitShift[2] = (byte)(bitDepth - BR.ReadByte());
                                dataStride = 3;
                                break;
                            case 3:
                                bitShift = new byte[3];
                                bitShift[0] = (byte)(bitDepth - BR.ReadByte());
                                bitShift[1] = (byte)(bitDepth - BR.ReadByte());
                                bitShift[2] = (byte)(bitDepth - BR.ReadByte());
                                dataStride = 3;
                                break;
                            case 4:
                                bitShift = new byte[2];
                                bitShift[0] = (byte)(bitDepth - BR.ReadByte());
                                bitShift[1] = (byte)(bitDepth - BR.ReadByte());
                                dataStride = 2;
                                break;
                            case 6:
                                bitShift = new byte[4];
                                bitShift[0] = (byte)(bitDepth - BR.ReadByte());
                                bitShift[1] = (byte)(bitDepth - BR.ReadByte());
                                bitShift[2] = (byte)(bitDepth - BR.ReadByte());
                                bitShift[3] = (byte)(bitDepth - BR.ReadByte());
                                dataStride = 4;
                                break;
                        }
                        break;

                    case sPLT:
                        throw new NotImplementedException("sPLT");

                    case sRGB:
                        int RenderingIntent = BR.ReadByte();
                        break;
                        //throw new NotImplementedException("sRGB");

                    case sTER:
                        throw new NotImplementedException("sTER");

                    case tEXt:
                        throw new NotImplementedException("tEXt");

                    case tIME:
                        throw new NotImplementedException("tIME");

                    case tRNS:
                        switch(colorType)
                        {
                            case 0:
                                break;
                            case 2:
                                break;
                            case 3:
                                break;
                        }
                        throw new InvalidDataException($"tRNS chunk found for color type {colorType}: " + (colorType == 4 ? "Greyscale with alpha" : "Truecolour with alpha"));
                    
                    case zTXt:
                        throw new NotImplementedException("zTXt");

                }
                // Skip CRC for now ...
                BR.ReadBytes(4);

            }

            throw new InvalidDataException("IEND chunk not found!");
        }

        internal static float ByteToF(byte b)
        {
            return (float)b;
        }
    }
}
