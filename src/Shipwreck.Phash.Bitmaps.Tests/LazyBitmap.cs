using System;
using System.Drawing;
using System.IO;
using System.Windows.Media.Imaging;

namespace Shipwreck.Phash.Bitmaps.Tests
{
    public class LazyBitmap : IDisposable
    {
        Lazy<Bitmap> bitmap;
        Lazy<BitmapSource> bitmapSource;
        Lazy<FileStream> sourceFile;

        public LazyBitmap(Uri filePath)
        {
            sourceFile = new Lazy<FileStream>(() => File.OpenRead(filePath.AbsolutePath));

            bitmap = new Lazy<Bitmap>(() => BitmapFromStream(sourceFile.Value));
            bitmapSource = new Lazy<BitmapSource>(() => BitmapSourceFromStream(sourceFile.Value));
        }

        public Bitmap Bitmap => bitmap.Value;
        public BitmapSource BitmapSource => bitmapSource.Value;

        static Bitmap BitmapFromStream(Stream stream)
        {
            if (!stream.CanSeek)
                throw new ArgumentException("Stream must be seekable to use as a LazyBitmap");
            stream.Position = 0;
            using (var image = Image.FromStream(stream, true))
            {
                return image.ToBitmap();
            }
        }

        static BitmapSource BitmapSourceFromStream(Stream stream)
        {
            if (!stream.CanSeek)
                throw new ArgumentException("Stream must be seekable to use as a LazyBitmap");
            stream.Position = 0;
            return BitmapFrame.Create(stream);
        }

        public void Dispose()
        {
            DisposeLazy(bitmap);
            DisposeLazy(sourceFile);
        }



        void DisposeLazy<T>(Lazy<T> lazy) where T : IDisposable
        {
            if (lazy != null && lazy.IsValueCreated)
                lazy.Value?.Dispose();
        }
    }
}
