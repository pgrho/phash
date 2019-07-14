using System.Runtime;

namespace Shipwreck.Phash.Imaging
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
#if !NO_SERIALIZABLE
            [TargetedPatchingOptOut("")]
#endif
            get => _Image.Width;
        }

        public int Height
        {
#if !NO_SERIALIZABLE
            [TargetedPatchingOptOut("")]
#endif
            get => _Image.Height;
        }

        public byte this[int x, int y]
        {
#if !NO_SERIALIZABLE
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
