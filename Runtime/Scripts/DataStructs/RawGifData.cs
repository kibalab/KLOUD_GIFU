namespace KLOUD.GIFU
{

    public class RawGifData
    {
        private DataStructs DataStructs = new DataStructs();

        public byte[] RawData
        {
            set
            {
                Load(value);
            }

            get => DataStructs.rawData;
        }

        public RawGifData(byte[] data)
        {
            RawData = data;
            Load(data);
        }

        public void Load(byte[] data)
        {
            DataStructs.rawData = data;
            
            
        }
    }
}