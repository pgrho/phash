namespace Shipwreck.Phash.V0_3_6.Imaging
{
    internal interface IByteImageWrapperProvider : IByteImage
    {
        IByteImageWrapper GetWrapper();
    }
}