using System;
using System.Text;

namespace Shipwreck.Phash
{
    /// <summary>
    /// Digest info
    /// </summary>
#if NET452
    [Serializable]
#endif
    public class Digest
    {
        internal const int LENGTH = 40;

        public Digest()
        {
            _Coefficents = new byte[LENGTH];
        }

        private readonly byte[] _Coefficents;

        /// <summary>
        /// the digest integer coefficient array
        /// </summary>
        public byte[] Coefficents
        {
            get => _Coefficents;
            set
            {
                if (value == null)
                {
                    Array.Clear(_Coefficents, 0, _Coefficents.Length);
                }
                else if (value.Length == _Coefficents.Length)
                {
                    Array.Copy(value, _Coefficents, value.Length);
                }
                else
                {
                    throw new ArgumentException();
                }
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder(Coefficents.Length * 2 + 2);
            sb.Append("0x");
            foreach (var b in Coefficents)
            {
                sb.Append(b.ToString("X2"));
            }
            return sb.ToString();
        }
    }
}