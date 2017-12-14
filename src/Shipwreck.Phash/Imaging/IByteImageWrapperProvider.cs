namespace Shipwreck.Phash.Imaging
{
    internal interface IByteImageWrapperProvider : IByteImage
    {
        IByteImageWrapper GetWrapper();
    }
}