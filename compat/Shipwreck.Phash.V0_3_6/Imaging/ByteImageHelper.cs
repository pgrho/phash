using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Shipwreck.Phash.V0_3_6.Imaging
{
    public static class ByteImageHelper
    {
        internal static IByteImageWrapper Wrap(this IByteImage image)
            => image as IByteImageWrapper
                ?? (image as IByteImageWrapperProvider)?.GetWrapper()
                ?? new GenericByteImageWrapper(image);
        
        public static FloatImage Convolve(this IByteImage image, FloatImage kernel)
            => image.Wrap().Convolve(kernel);

        internal static FloatImage Convolve(this IByteImageWrapper image, FloatImage kernel)
            => image.GetOperations().Convolve(image, kernel);

        public static FloatImage Blur(this IByteImage image, double sigma)
            => image.Convolve(FloatImage.CreateGaussian(3, sigma));
    }
}