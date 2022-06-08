using System;
using System.Collections.Generic;

namespace KLOUD.GIFU
{
    public enum GIFVersion
    {
        a39 = 0x39,
        a37 = 0x37
    }

    public class DataStructs
    {
        public byte[] rawData;
        
        public class GIF_Header
        {
            
            public static string Sighnature = "GIF"; // Always (47="G", 49="I", 46="F")
            public GIFVersion Version; // Always (38="8", 39="9",61="a") or (38="8", 37="7",61="a")
        } public GIF_Header Header;

        public class GIF_GlobalScreenDestriptor
        {
            public ushort CanvasWidth; // 2Byte
            public ushort CanvasHeight; // 2Byte

            public class BitField
            {
                public bool GlobalColorTableFlag;
                public byte ColorResolution; // 3Bits
                public bool SortFlag;
                public byte Size; // 3Bits
            } public BitField BitFields = new  BitField();
                
            public byte BackgroundColor;
            public byte PixelAspectRatio; // PixelAspectRatio == 0 ? 1:1 : 1:(PixelAspectRatio + 15)/64
        } public GIF_GlobalScreenDestriptor GlobalScreenDestriptor = new GIF_GlobalScreenDestriptor();

        public class GIF_GlobalColorTable
        {
            public byte Red;
            public byte Green;
            public byte Blue;
        } public GIF_GlobalColorTable GlobalColorTable = new GIF_GlobalColorTable();

        public class GIF_ImageBlock
        {
            public static byte ExtentionIntroducer = 0x21; // Always (21)
            public static byte GraphicControlLabel = 0xF9; // Always (F9)
            public byte ByteSize;

            public class BitField
            {
                public bool[] ReservedForFutureUse; // 3Byte
                public bool[] DisposalMethod; // 3Byte
                public bool UserInputFlag;
                public bool TransparentColorFlag;
            } public BitField BitFields = new  BitField();

            public byte[] DelayTime = new byte[2]; // 2Byte
            public byte TransparentColorIndex;
            public static byte BlockTerminator = 0x00; // Always (00)
        } public GIF_ImageBlock ImageBlock = new GIF_ImageBlock();

        public class GIF_ImageDescriptor
        {
            public static byte ImageSeperator = 0x2C; // Always (2C)
            public byte[] ImageLeft = new byte[2]; // 2Byte
            public byte[] ImageTop = new byte[2]; // 2Byte
            public byte[] ImageWidth = new byte[2]; // 2Byte
            public byte[] ImageHeight = new byte[2]; // 2Byte

            public class BitField
            {
                public bool LocalColorTableFlag;
                public bool InterlaceFlag;
                public bool SortFlag;
                public bool[] ReservedForFutureUse = new bool[2]; // 2Bits
                public bool[] SizeOfLocalColorTable = new bool[3]; // 3Bits
            } public BitField BitFields = new  BitField();
        } public GIF_ImageDescriptor ImageDescriptor = new GIF_ImageDescriptor();

        public class GIF_LocalColorTable // Always Same GlobalColorTable
        {
            public byte Red;
            public byte Green;
            public byte Blue;
        } public GIF_LocalColorTable GifLocalColorTable = new GIF_LocalColorTable();

        public class GIF_ImageData
        {
            public byte LZWMinimumCodeSize;

            public class Data
            {
                public byte NumberOfBytesOfData;
                public byte[] data;
            } public List<Data> Datas = new  List<Data>();

            public static byte BlockTerminator = 0x00; // Always (00)
        } public GIF_ImageData GifImageData = new GIF_ImageData();

        public byte[] PlainTextExtension;
        public byte[] ApplicationExtension;
        public string CommentExtension;

        public static byte Trailer = 0x3B; // Always (3B)
    }
}