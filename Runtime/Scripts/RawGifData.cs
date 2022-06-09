using System;
using System.Collections;
using Unity.Plastic.Newtonsoft.Json.Converters;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

namespace KLOUD.GIFU
{

    public class RawGifData
    {
        private DataStructs Data = new DataStructs();

        public byte[] RawData
        {
            set
            {
                Load(value);
            }

            get => Data.RawData;
        }

        public RawGifData(byte[] data)
        {
            RawData = data;
            Load(data);
        }

        public void Load(byte[] data)
        {
            Data.RawData = data;
            
            ReadHeader();
            ReadCanvasDescriptor();
            
        }

        public ulong lastReadIndex = 0;

        public void ReadHeader() // 0~5 bytes
        {
            #region Check Sighnature
            
            var Sighnature = true;
            for(int i = 0; i< 3; i++) Sighnature &= Data.RawData[i] == DataStructs.GIF_Header.Sighnature[i];

            if (!Sighnature) Debug.LogError("[GIFU] Is not GIF format");

            #endregion

            Data.Header.Version = (GIFVersion) Data.RawData[4]; // Read GIF Version

            lastReadIndex = 5;
        }

        public void ReadCanvasDescriptor() // 6~12 bytes
        {
            #region Read Canvas Scale
            Data.GlobalScreenDestriptor.CanvasWidth = BitConverter.ToUInt16(new byte[2] {Data.RawData[6], Data.RawData[7]});
            Data.GlobalScreenDestriptor.CanvasHeight = BitConverter.ToUInt16(new byte[2] {Data.RawData[8], Data.RawData[9]});
            #endregion

            #region Read BitsField  
            var bits = new BitArray(Data.RawData[10]);
            Data.GlobalScreenDestriptor.BitFields.GlobalColorTableFlag = bits[0];
            Data.GlobalScreenDestriptor.BitFields.ColorResolution =
                ReadUtil.ConvertBitsToByte(ReadUtil.ConvertBoolsToBits(new bool[] {bits[0], bits[0], bits[0]}));
            Data.GlobalScreenDestriptor.BitFields.SortFlag = bits[4];
            Data.GlobalScreenDestriptor.BitFields.Size = ReadUtil.ConvertBitsToByte(ReadUtil.ConvertBoolsToBits(new bool[]{ bits[5], bits[6], bits[7] }));  
            #endregion
            
            Data.GlobalScreenDestriptor.BackgroundColor = Data.RawData[11];
            Data.GlobalScreenDestriptor.PixelAspectRatio = Data.RawData[12];

            lastReadIndex = 12;
        }

        public void ReadGlobalColorTable()
        {
            if(!Data.GlobalScreenDestriptor.BitFields.GlobalColorTableFlag) return;
            
            var tableSize = Math.Pow(2, Data.GlobalScreenDestriptor.BitFields.Size + 1);
            var tableByteSize = tableSize * 3;
            for (var i = lastReadIndex + 1; i < tableByteSize; )
            {
                Data.GlobalColorTable.Add(new Color(Data.RawData[i], Data.RawData[i+1], Data.RawData[i+2]));
                i += 3;
            }
        }
        
        
    }
}