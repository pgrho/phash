using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Shipwreck.Phash.Imaging
{
    public static class ByteImageHelper
    {
        internal static IByteImageWrapper Wrap(this IByteImage image)
            => image as IByteImageWrapper
                ?? (image as IByteImageWrapperProvider)?.GetWrapper()
                ?? new GenericByteImageWrapper(image);

        static ConcurrentDictionary<Type, IByteImageOperations> typeOperationsCache = 
            new ConcurrentDictionary<Type, IByteImageOperations>();

        internal static IByteImageOperations GetOperations(IByteImageWrapper image)
            => typeOperationsCache.GetOrAdd(image.GetType(), CreateByteImageOperationsForImageType);

        static IByteImageOperations CreateByteImageOperationsForImageType(Type imageType)
        {
            return (IByteImageOperations)Activator.CreateInstance(typeof(ByteImageOperations<>).MakeGenericType(imageType));
        }

        public static FloatImage Convolve(this IByteImage image, FloatImage kernel)
            => image.Wrap().Convolve(kernel);

        internal static FloatImage Convolve(this IByteImageWrapper image, FloatImage kernel)
            => GetOperations(image).Convolve(image, kernel);

        public static FloatImage Blur(this IByteImage image, double sigma)
            => image.Convolve(FloatImage.CreateGaussian(3, sigma));
    }
}