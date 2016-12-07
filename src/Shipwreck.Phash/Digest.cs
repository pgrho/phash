using Shipwreck.Phash.Imaging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Shipwreck.Phash
{
    /// <summary>
    /// Digest info
    /// </summary>
    public class Digest
    {
        /// <summary>
        /// hash id
        /// </summary>
        public char[] id;

        /// <summary>
        /// the digest integer coefficient array
        /// </summary>
        public byte[] coeffs;
    }
}