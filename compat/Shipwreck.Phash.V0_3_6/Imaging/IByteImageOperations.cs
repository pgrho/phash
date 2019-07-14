namespace Shipwreck.Phash.V0_3_6.Imaging
{
    internal interface IByteImageOperations
    {
        FloatImage Convolve(IByteImageWrapper image, FloatImage kernel);
    }
}