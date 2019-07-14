namespace Shipwreck.Phash.V0_3_6.Imaging
{
    public interface IByteImage
    {
        int Width { get; }
        int Height { get; }
        byte this[int x, int y] { get; }
    }
}