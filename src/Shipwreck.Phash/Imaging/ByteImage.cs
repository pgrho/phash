namespace Shipwreck.Phash.Imaging
{
    public sealed partial class ByteImage : IByteImageWrapperProvider
    {
        IByteImageWrapper IByteImageWrapperProvider.GetWrapper()
            => new ByteImageWrapper(this);
    }
}