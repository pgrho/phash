using System.Runtime;

namespace Shipwreck.Phash.V0_3_6.Imaging
{
    internal struct GenericByteImageWrapper : IByteImageWrapper
    {
        private readonly IByteImage _Image;

        static IByteImageOperations Wrapper = new ByteImageOperations<GenericByteImageWrapper>();

        public GenericByteImageWrapper(IByteImage image)
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