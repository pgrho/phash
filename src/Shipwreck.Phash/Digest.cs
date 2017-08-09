using System;
using System.Text;

namespace Shipwreck.Phash
{
    /// <summary>
    /// Digest info
    /// </summary>
    public class Digest
    {
        public Digest(int size)
        {
            Coefficents = new byte[size];
        }

        /// <summary>
        /// hash id
        /// </summary>
        public char[] id;

        [Obsolete]
        public byte[] coeffs => Coefficents;

        /// <summary>
        /// the digest integer coefficient array
        /// </summary>
        public byte[] Coefficents { get; }

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