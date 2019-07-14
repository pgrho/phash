namespace Shipwreck.Phash.V0_3_6.Imaging
{
    internal interface IByteImageWrapper : IByteImage
    {
        IByteImageOperations GetOperations();
    }
}