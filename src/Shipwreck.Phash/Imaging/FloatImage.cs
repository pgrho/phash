using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Shipwreck.Phash.Imaging
{
    public sealed class FloatImage
    {
        private readonly int _Width;
        private readonly int _Height;
        private readonly float[] _Data;

        public FloatImage(int width, int height)
        {
            _Width = width;
            _Height = height;
            _Data = new float[width * height];
        }
        public FloatImage(int width, int height, float value)
        {
            _Width = width;
            _Height = height;
            _Data = new float[width * height];
            for (var i = 0; i < _Data.Length; i++)
            {
                _Data[i] = value;
            }
        }
        public FloatImage(int width, int height, float[] data)
        {
            _Width = width;
            _Height = height;
            _Data = data;
        }
        public int Width => _Width;
        public int Height => _Height;
        public float this[int x, int y]
        {
            get
            {
                var i = x + y * _Width;
                return _Data[i];
            }
            set
            {
                var i = x + y * _Width;
                _Data[i] = value;
            }
        }

        public FloatImage Resize(int w, int h)
        {
            // TODO:bilinearにする

            var r = new FloatImage(w, h);
            var xr = w / (float)_Width;
            var yr = h / (float)_Height;
            for (var sy = 0; sy < _Height; sy++)
            {
                var dy = (int)Math.Max(0, Math.Min(sy * yr, h - 1));
                for (var sx = 0; sx < _Width; sx++)
                {
                    var dx = (int)Math.Max(0, Math.Min(sx * xr, w - 1));

                    r[dx, dy] += this[sx, sy];
                }
            }

            return r;
        }

        public FloatImage Transpose()
        {
            var r = new FloatImage(_Height, _Width);
            for (var sy = 0; sy < _Height; sy++)
            {
                for (var sx = 0; sx < _Width; sx++)
                {
                    r[sy, sx] = this[sx, sy];
                }
            }
            return r;
        }
        public FloatImage Multiply(FloatImage other)
        {
            var r = new FloatImage(_Width, _Height);
            for (var sy = 0; sy < _Height; sy++)
            {
                for (var sx = 0; sx < _Width; sx++)
                {
                    r[sx, sy] = this[sx, sy] * other[sx, sy];
                }
            }
            return r;
        }
    }
}
