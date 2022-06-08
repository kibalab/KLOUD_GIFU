using System;
using System.Collections;

namespace KLOUD.GIFU
{
    public class ReadUtil
    {
        public static byte ConvertBitsToByte(BitArray bits)
        {
            if (bits.Count != 8)
            {
                throw new ArgumentException("bits");
            }
            byte[] bytes = new byte[1];
            bits.CopyTo(bytes, 0);
            return bytes[0];
        }
        public static BitArray ConvertBoolsToBits(bool[] bits)
        {
            var bitArray = new BitArray(3);
            var i = 0;
            foreach (var bit in bits)
            {
                bitArray.Set(i++, bit);
            }

            return bitArray;
        }
    }
}