namespace KLOUD.GIFU
{
    public class DataStructs
    {
        public class Header
        {
            public string Sighnature = "GIF"; // Always (47="G", 49="I", 46="F")
            public string Version = ""; // Always (38="8", 39="9",61="a") or (38="8", 37="7",61="a")
        }

        public class GlobalScreenDestriptor
        {
            public byte[] LogicalScreenWidth; // 2Byte
            public byte[] LogicalScreenHeight; // 2Byte

            public class BitField
            {
                public bool[] GlobalColorTableSize; // 3Byte
                public bool ColorTableSortFlag;
                public bool[] BitsPerPixel; // 3Byte
                public bool GlobalColorTableFlag;
                public byte BackgroundColor;
                public byte PixelAspectRatio; // PixelAspectRatio == 0 ? 1:1 : 1:(PixelAspectRatio + 15)/64

            }
        }

        public class GlobalColorTable
        {
            public byte Red;
            public byte Green;
            public byte Blue;
        }

        public class ImageBlock
        {
            public byte ExtentionIntroducer; // Always (21)
            public byte GraphicControlLabel; // Always (F9)
            public byte ByteSize;

            public class BitField
            {
                public bool[] ReservedForFutureUse; // 3Byte
                public bool[] DisposalMethod; // 3Byte
                public bool UserInputFlag;
                public bool TransparentColorFlag;
            }

            public byte[] DelayTime; // 2Byte
            public byte TransparentColorIndex;
            public byte BlockTerminator; // Always (00)
        }

        public class ImageDescriptor
        {
            public byte ImageSeperator; // Always (2C)
            public byte[] ImageLeft; // 2Byte
            public byte[] ImageTop; // 2Byte
            public byte[] ImageWidth; // 2Byte
            public byte[] ImageHeight; // 2Byte

            public class BitField
            {
                public bool LocalColorTableFlag;
                public bool InterlaceFlag;
                public bool SortFlag;
                public bool[] ReservedForFutureUse; // 2Byte
                public bool[] SizeOfLocalColorTable; // 3Byte
            }
        }

        public class LocalColorTable // Always Same GlobalColorTable
        {
            public byte Red;
            public byte Green;
            public byte Blue;
        }

        public class ImageData
        {
            public byte LZWMinimumCodeSize;

            public class Data
            {
                public byte NumberOfBytesOfData;
                public byte[] Data;
            } public Data[] Datas;

            public byte BlockTerminator; // Always (00)
        }

        public byte[] PlainTextExtension;
        public byte[] ApplicationExtension;
        public string CommentExtension;

        public byte Trailer; // Always (3B)
    }
}