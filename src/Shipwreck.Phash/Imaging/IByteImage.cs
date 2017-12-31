namespace Shipwreck.Phash.Imaging
{
    public interface IByteImage
    {
        int Width { get; }
        int Height { get; }
        byte this[int x, int y] { get; }
    }
}