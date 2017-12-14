namespace Shipwreck.Phash.Imaging
{
    internal interface IByteImageOperations
    {
        FloatImage Convolve(IByteImageWrapper image, FloatImage kernel);
    }
}