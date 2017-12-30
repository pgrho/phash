using System.Runtime;

namespace Shipwreck.Phash.Imaging
{
    internal struct ByteImageWrapper : IByteImageWrapper
    {
        private readonly ByteImage _Image;

        static IByteImageOperations Wrapper = new ByteImageOperations<ByteImageWrapper>();

        public ByteImageWrapper(ByteImage image)
        {
            _Image = image;
        }

        public int Width
        {
#if NET452
            [TargetedPatchingOptOut("")]
#endif
            get => _Image.Width;
        }

        public int Height
        {
#if NET452
            [TargetedPatchingOptOut("")]
#endif
            get => _Image.Height;
        }

        public byte this[int x, int y]
        {
#if NET452
            [TargetedPatchingOptOut("")]
#endif
            get => _Image[x, y];
        }

        public IByteImageOperations GetOperations()
        {
            return Wrapper;
        }
    }
}