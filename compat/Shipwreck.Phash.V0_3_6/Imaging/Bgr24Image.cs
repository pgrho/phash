using System.Numerics;

namespace Shipwreck.Phash.V0_3_6.Imaging
{
    public class Bgr24Image : IVector3Image
    {
        // TODO: use Span<byte> if the System.Memory.dll released
        internal readonly byte[] _Data;

        internal readonly int _Offset;
        internal readonly int _Stride;
        internal readonly int _PixelSize;

        public Bgr24Image(int width, int height, byte[] data, int offset, int stride, int pixelSize)
        {
            Width = width;
            Height = height;
            _Data = data;
            _Offset = offset;
            _Stride = stride;
            _PixelSize = pixelSize;
        }

        public int Width { get; }

        public int Height { get; }

        public Vector3 this[int x, int y]
        {
            get
            {
                var i = _Offset + y * _Stride + x * _PixelSize;
                return new Vector3(_Data[i + 2], _Data[i + 1], _Data[i]);
            }
        }
    }
}