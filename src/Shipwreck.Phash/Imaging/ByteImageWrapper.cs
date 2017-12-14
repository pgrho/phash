using System.Runtime;

namespace Shipwreck.Phash.Imaging
{
    internal struct ByteImageWrapper : IByteImageWrapper
    {
        private readonly ByteImage _Image;

        public ByteImageWrapper(ByteImage image)
        {
            _Image = image;
        }

        public int Width
        {
            [TargetedPatchingOptOut("")]
            get => _Image.Width;
        }

        public int Height
        {
            [TargetedPatchingOptOut("")]
            get => _Image.Height;
        }

        public byte this[int x, int y]
        {
            [TargetedPatchingOptOut("")]
            get => _Image[x, y];
        }
    }
}