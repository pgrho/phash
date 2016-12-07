using System;
namespace Shipwreck.Phash.Imaging
{
	partial class FloatImage : IArrayImage<System.Single>
	{
        private readonly int _Width;
        private readonly int _Height;
        private readonly System.Single[] _Data;
		
        public FloatImage(int width, int height)
        {
            _Width = width;
            _Height = height;
            _Data = new System.Single[width * height];
        }

        public FloatImage(int width, int height, System.Single value)
        {
            _Width = width;
            _Height = height;
            _Data = new System.Single[width * height];
            for (var i = 0; i < _Data.Length; i++)
            {
                _Data[i] = value;
            }
        }

        public FloatImage(int width, int height, System.Single[] data)
        {
            _Width = width;
            _Height = height;
            _Data = data;
        }
		
        public int Width => _Width;
        public int Height => _Height;
		public System.Single[] Array => _Data;

        public System.Single this[int x, int y]
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
		
		public System.Single Max()
		{
			System.Single r = 0;
			for (var i = 0; i < _Data.Length; i++)
			{
				r = Math.Max(_Data[i], r);
			}
			return r;
		}
		public System.Single Min()
		{
			System.Single r = 0;
			for (var i = 0; i < _Data.Length; i++)
			{
				r = Math.Min(_Data[i], r);
			}
			return r;
		}
		public System.Single Sum()
		{
			System.Single r = 0;
			for (var i = 0; i < _Data.Length; i++)
			{
				r += _Data[i];
			}
			return r;
		}

        public FloatImage Transpose()
        {
            var r = new FloatImage(_Height, _Width);
            this.TransposeTo(r);
            return r;
        }
	}
	partial class ByteImage : IArrayImage<System.Byte>
	{
        private readonly int _Width;
        private readonly int _Height;
        private readonly System.Byte[] _Data;
		
        public ByteImage(int width, int height)
        {
            _Width = width;
            _Height = height;
            _Data = new System.Byte[width * height];
        }

        public ByteImage(int width, int height, System.Byte value)
        {
            _Width = width;
            _Height = height;
            _Data = new System.Byte[width * height];
            for (var i = 0; i < _Data.Length; i++)
            {
                _Data[i] = value;
            }
        }

        public ByteImage(int width, int height, System.Byte[] data)
        {
            _Width = width;
            _Height = height;
            _Data = data;
        }
		
        public int Width => _Width;
        public int Height => _Height;
		public System.Byte[] Array => _Data;

        public System.Byte this[int x, int y]
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
		
		public System.Int32 Max()
		{
			System.Int32 r = 0;
			for (var i = 0; i < _Data.Length; i++)
			{
				r = Math.Max(_Data[i], r);
			}
			return r;
		}
		public System.Int32 Min()
		{
			System.Int32 r = 0;
			for (var i = 0; i < _Data.Length; i++)
			{
				r = Math.Min(_Data[i], r);
			}
			return r;
		}
		public System.Int32 Sum()
		{
			System.Int32 r = 0;
			for (var i = 0; i < _Data.Length; i++)
			{
				r += _Data[i];
			}
			return r;
		}

        public ByteImage Transpose()
        {
            var r = new ByteImage(_Height, _Width);
            this.TransposeTo(r);
            return r;
        }
	}
}
