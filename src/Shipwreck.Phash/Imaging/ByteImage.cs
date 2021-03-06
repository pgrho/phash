﻿using System.Runtime;

namespace Shipwreck.Phash.Imaging
{
    public sealed partial class ByteImage : IArrayImage<byte>, IByteImageWrapperProvider
    {
        public ByteImage(int width, int height)
        {
            Width = width;
            Height = height;
            Array = new byte[width * height];
        }

        public ByteImage(int width, int height, byte value)
        {
            Width = width;
            Height = height;
            Array = new byte[width * height];
            for (var i = 0; i < Array.Length; i++)
            {
                Array[i] = value;
            }
        }

        public ByteImage(int width, int height, byte[] data)
        {
            Width = width;
            Height = height;
            Array = data;
        }

        public int Width
        {
#if !NO_SERIALIZABLE
            [TargetedPatchingOptOut("")]
#endif
            get;
        }

        public int Height
        {
#if !NO_SERIALIZABLE
            [TargetedPatchingOptOut("")]
#endif
            get;
        }

        public byte[] Array
        {
#if !NO_SERIALIZABLE
            [TargetedPatchingOptOut("")]
#endif
            get;
        }

        public byte this[int x, int y]
        {
#if !NO_SERIALIZABLE
            [TargetedPatchingOptOut("")]
#endif
            get => Array[x + y * Width];
#if !NO_SERIALIZABLE
            [TargetedPatchingOptOut("")]
#endif
            set => Array[x + y * Width] = value;
        }

        public Gray8Image Crop(int x, int y, int width, int height)
            => new Gray8Image(width, height, Array, y * Width + x, Width);

        IByteImageWrapper IByteImageWrapperProvider.GetWrapper()
            => new ByteImageWrapper(this);
    }
}
