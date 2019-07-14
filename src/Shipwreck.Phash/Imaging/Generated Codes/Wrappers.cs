using System.Runtime;

namespace Shipwreck.Phash.Imaging
{
    internal struct ByteImageWrapper : IByteImageWrapper
    {
        private static readonly IByteImageOperations _Operations = new ByteImageOperations<ByteImageWrapper>();

        private readonly ByteImage _Image;

        public ByteImageWrapper(ByteImage image)
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
            get => ((IByteImage)_Image)[x, y];
        }

        public IByteImageOperations GetOperations()
            => _Operations;
    }

    internal struct Gray8ImageWrapper : IByteImageWrapper
    {
        private static readonly IByteImageOperations _Operations = new ByteImageOperations<Gray8ImageWrapper>();

        private readonly Gray8Image _Image;

        public Gray8ImageWrapper(Gray8Image image)
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
            get => ((IByteImage)_Image)[x, y];
        }

        public IByteImageOperations GetOperations()
            => _Operations;
    }

    internal struct Bgr24LuminanceImageWrapper : IByteImageWrapper
    {
        private static readonly IByteImageOperations _Operations = new ByteImageOperations<Bgr24LuminanceImageWrapper>();

        private readonly Bgr24LuminanceImage _Image;

        public Bgr24LuminanceImageWrapper(Bgr24LuminanceImage image)
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
            get => ((IByteImage)_Image)[x, y];
        }

        public IByteImageOperations GetOperations()
            => _Operations;
    }

}