namespace Shipwreck.Phash.Imaging
{
    internal interface IByteImageWrapper : IByteImage
    {
        IByteImageOperations GetOperations();
    }
}