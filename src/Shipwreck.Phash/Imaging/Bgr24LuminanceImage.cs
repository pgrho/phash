namespace Shipwreck.Phash.Imaging
{
    public sealed class Bgr24LuminanceImage : Bgr24Image, IByteImageWrapperProvider
    {
        public Bgr24LuminanceImage(int width, int height, byte[] data, int offset, int stride, int pixelSize)
            : base(width, height, data, offset, stride, pixelSize)
        {
        }

        byte IByteImage.this[int x, int y]
            => this[x, y].GetLuminance();

        IByteImageWrapper IByteImageWrapperProvider.GetWrapper()
            => new Bgr24LuminanceImageWrapper(this);
    }
}