namespace Shipwreck.Phash.Imaging
{
    public sealed partial class ByteImage : IArrayImage<byte>, IByteImageWrapperProvider
    {
        public ByteImage(int width, int height)
        {
            Width = width;
            Height = height;
            Array = new byte[width * height];
        }

        public ByteImage(int width, int height, byte value)
        {
            Width = width;
            Height = height;
            Array = new byte[width * height];
            for (var i = 0; i < Array.Length; i++)
            {
                Array[i] = value;
            }
        }

        public ByteImage(int width, int height, byte[] data)
        {
            Width = width;
            Height = height;
            Array = data;
        }

        public int Width { get; }
        public int Height { get; }
        public byte[] Array { get; }

        public byte this[int x, int y]
        {
            get => Array[x + y * Width];
            set => Array[x + y * Width] = value;
        }

        IByteImageWrapper IByteImageWrapperProvider.GetWrapper()
            => new ByteImageWrapper(this);
    }
}