namespace Shipwreck.Phash.V0_3_6.Imaging
{
    public class Gray8Image : IByteImageWrapperProvider
    {
        // TODO: use Span<byte> if the System.Memory.dll released
        private readonly byte[] _Data;

        private readonly int _Offset;
        private readonly int _Stride;

        public Gray8Image(int width, int height, byte[] data, int offset, int stride)
        {
            Width = width;
            Height = height;
            _Data = data;
            _Offset = offset;
            _Stride = stride;
        }

        public int Width { get; }

        public int Height { get; }

        public byte this[int x, int y]
        {
            get => _Data[_Offset + y * _Stride + x];
        }

        public Gray8Image Crop(int x, int y, int width, int height)
            => new Gray8Image(width, height, _Data, _Offset + y * _Stride + x, _Stride);

        IByteImageWrapper IByteImageWrapperProvider.GetWrapper()
            => new Gray8ImageWrapper(this);
    }
}