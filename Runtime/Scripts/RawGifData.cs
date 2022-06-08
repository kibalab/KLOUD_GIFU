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

            get => Data.rawData;
        }

        public RawGifData(byte[] data)
        {
            RawData = data;
            Load(data);
        }

        public void Load(byte[] data)
        {
            Data.rawData = data;
            
            ReadHeader();
            ReadCanvasDescriptor();
            
        }

        public void ReadHeader() // 0~5 bytes
        {
            #region Check Sighnature
            
            var Sighnature = true;
            for(int i = 0; i< 3; i++) Sighnature &= Data.rawData[i] == DataStructs.GIF_Header.Sighnature[i];

            if (!Sighnature) Debug.LogError("[GIFU] Is not GIF format");

            #endregion

            Data.Header.Version = (GIFVersion) Data.rawData[4]; // Read GIF Version
        }

        public void ReadCanvasDescriptor() // 6~12 bytes
        {
            #region Read Canvas Scale
            Data.GlobalScreenDestriptor.CanvasWidth = BitConverter.ToUInt16(new byte[2] {DataStructs.rawData[6], DataStructs.rawData[7]});
            Data.GlobalScreenDestriptor.CanvasHeight = BitConverter.ToUInt16(new byte[2] {DataStructs.rawData[8], DataStructs.rawData[9]});
            #endregion

            #region Read BitsField  
            var bits = new BitArray(DataStructs.rawData[10]);
            Data.GlobalScreenDestriptor.BitFields.GlobalColorTableFlag = bits[0];
            Data.GlobalScreenDestriptor.BitFields.ColorResolution =
                ReadUtil.ConvertBitsToByte(ReadUtil.ConvertBoolsToBits(new bool[] {bits[0], bits[0], bits[0]}));
            Data.GlobalScreenDestriptor.BitFields.SortFlag = bits[4];
            Data.GlobalScreenDestriptor.BitFields.Size = ReadUtil.ConvertBitsToByte(ReadUtil.ConvertBoolsToBits(new bool[]{ bits[5], bits[6], bits[7] }));  
            #endregion
            
            Data.GlobalScreenDestriptor.BackgroundColor = DataStructs.rawData[11];
            Data.GlobalScreenDestriptor.PixelAspectRatio = DataStructs.rawData[12];
        }
    }
}